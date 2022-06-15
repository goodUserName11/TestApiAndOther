using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Adapter
{
    public class RankingAdapter : AbstractRankingAdapter
    {
        public override async Task<List<SupplierSearchResultModel>> Ranking(
            List<SupplierFoundModel> supliers, CrititionsModel critiotions, string inn)
        {
            List<SupplierSearchResultModel> supplierResults = new();
            List<CoefficientValue> CoefficientValues = new();

            if (supliers.Count == 0)
                return supplierResults;

            double decreaseCoef = 1 / supliers.Count;
            double decreaseCount = supliers.Count;

            int maxDeliveryTime = supliers.MaxBy(s => s.MinimumDeliveryDays).MinimumDeliveryDays;

            var style = NumberStyles.AllowDecimalPoint;
            var provider = new CultureInfo("en-GB");

            using (SearchAndRangeContext dbContext = new())
            {

                CoefficientValues = dbContext.CoefficientValues
                    .Include(t => t.Coefficient)
                    .ThenInclude(t => t.Type)
                    .Where(t => t.CompanyInn == inn)
                    .ToList();
            }


            supliers = supliers.OrderBy(t => t.Product.Price).ToList();

            foreach (var supplier in supliers)
            {
                double rank = 1;
                double posRank = 0;
                double negRank = 0;
                decimal decimalPrice = 0;

                if (!decimal.TryParse(supplier.Product.Price, style, provider, out decimalPrice))
                    decimalPrice = decimal.Parse(supplier.Product.Price);

                var doublePrice = Convert.ToDouble(decimalPrice);



                if (CoefficientValues.FirstOrDefault(t => t.Coefficient.Name == "Цена").IsActive
                    && (decimalPrice < critiotions.MinPrice || decimalPrice > critiotions.MaxPrice))
                    continue;

                if (CoefficientValues.FirstOrDefault(t => t.Coefficient.Name == "Реестр недобросовестных поставщиков").IsActive
                    && supplier.Dishonesty)
                    continue;

                if (CoefficientValues.FirstOrDefault(t => t.Coefficient.Name == "Банкротство Ликвидация").IsActive
                    && supplier.BankruptcyOrLiquidation)
                    continue;

                if (CoefficientValues.FirstOrDefault(t => t.Coefficient.Name == "Соответствие объему").IsActive
                    && supplier.Product.Count < critiotions.Count
                    && !supplier.WayOfDistribution)
                    continue;

                if (CoefficientValues.FirstOrDefault(t => t.Coefficient.Name == "Конфликт интересов").IsActive
                    && supplier.Conflict == inn)
                    continue;

                foreach (var coefficientValue in CoefficientValues)
                {
                    switch (coefficientValue.Coefficient.Name)
                    {
                        case "Цена":
                            posRank += (doublePrice * coefficientValue.Value * decreaseCoef * decreaseCount);
                            decreaseCount--;

                            break;
                        case "Реестр недобросовестных поставщиков":
                            if (supplier.Dishonesty)
                                negRank += coefficientValue.Value;
                            else posRank += coefficientValue.Value;

                            break;
                        case "Производитель":
                            if (supplier.IsManufacturer)
                                posRank += coefficientValue.Value;
                            else negRank += coefficientValue.Value;

                            break;
                        case "Наличие истории за 3 года":

                            break;
                        case "Субъект малого предпринимательства":
                            if (supplier.SmallBusinessEntity)
                                posRank += coefficientValue.Value;
                            else negRank += coefficientValue.Value;

                            break;
                        case "Соответствие объему":
                            if (supplier.Product.Count >= critiotions.Count)
                                posRank += coefficientValue.Value;
                            else negRank += coefficientValue.Value;

                            break;
                        case "Банкротство Ликвидация":
                            if (supplier.BankruptcyOrLiquidation)
                                negRank += coefficientValue.Value;
                            else posRank += coefficientValue.Value;

                            break;
                        case "Способ поставки":
                            bool wayOfDistr = critiotions.WayOfDestribution.ToLower() == "поэтапный" ? true : false;
                            if (supplier.WayOfDistribution)
                                negRank += coefficientValue.Value;
                            else posRank += coefficientValue.Value;

                            break;
                        case "Сроки поставки":
                            negRank += supplier.MinimumDeliveryDays / maxDeliveryTime * coefficientValue.Value;

                            break;
                        case "Репутация":
                            posRank += supplier.Reputation * coefficientValue.Value;

                            break;
                        case "Конфликт интересов":
                            if (supplier.Conflict == inn)
                                negRank += coefficientValue.Value;
                            else posRank += coefficientValue.Value;

                            break;
                    }
                }

                rank = posRank / negRank;

                var supplRes = new SupplierSearchResultModel(supplier, rank, supplier.Conflict == inn);
                supplierResults.Add(supplRes);

                
            }

            if (supplierResults.Count == 0)
                return supplierResults;

            var maxRank = supplierResults.MaxBy(t => t.Rank).Rank;

            foreach (var supplierResult in supplierResults)
            {
                supplierResult.Rank = supplierResult.Rank / maxRank * 10;

                using (var dbContext = new SearchAndRangeContext())
                {
                    dbContext.SupplierInLists.Add(
                        new SupplierInList()
                        {
                            Id = supplierResult.Id,
                            Conflict = supplierResult.Conflict,
                            Okpd2 = critiotions.Okpd2,
                            Rank = supplierResult.Rank,
                            SupplierId = supplierResult.Inn
                        });

                    await dbContext.SaveChangesAsync();

                    await dbContext.DisposeAsync();
                }
            }

            supplierResults = supplierResults.OrderByDescending(t => t.Rank).ToList();

            return supplierResults;

        }
    }
}
