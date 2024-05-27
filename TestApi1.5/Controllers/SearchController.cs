using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using TestApi.Adapter;
using TestApi.Parser;
using TestApi.Model;
using TestApi.Authentication;
using Serilog;
using System.Runtime.InteropServices;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SupplierSearchResultModel>>> GetRangedList( 
            [FromQuery] CrititionsModel? critiotions = null, [FromQuery] string? userId = "")
        {
            Log.Logger.Information($"Ведется поиск и ранжирование поставщиков...");

            List<SupplierSearchResultModel> list = new();

            if (critiotions is null || userId is null)
                return new List<SupplierSearchResultModel>();

            if(critiotions.Count < 0 || critiotions.MinPrice < 0 || critiotions.MaxPrice < critiotions.MinPrice 
                || string.IsNullOrWhiteSpace(critiotions.Okpd2) || string.IsNullOrWhiteSpace(critiotions.Region) 
                || string.IsNullOrWhiteSpace(critiotions.WayOfDestribution))
                return new List<SupplierSearchResultModel>();

            critiotions.Okpd2 = critiotions.Okpd2.Trim();

            string inn = "";

            try
            {
                List<SupplierFoundModel> supList = new List<SupplierFoundModel>();

                Log.Logger.Information($"Начат поиск поставщиков...");

                foreach (var searchAdapterem in AdapterContainer.SupplierSearchAdapters)
                {
                    supList.AddRange(await searchAdapterem.Find(critiotions.Okpd2));
                }
                
                supList.DistinctBy(s => s.Inn);

                Log.Logger.Information($"Поиск поставщиков завершен");

                using (Data.SearchAndRangeContext dbContext = new Data.SearchAndRangeContext())
                {

                    if (string.IsNullOrEmpty(userId) || userId == "0")
                        inn = dbContext.Users.FirstOrDefault(u => u.Role == UserRoles.Admin.Id).CompanyInn;
                    else
                        inn = (await dbContext.Users.FindAsync(Guid.Parse(userId)))?.CompanyInn;

                }

                Log.Logger.Information($"Начато ранжирование поставщиков...");

                list.AddRange(
                    await AdapterContainer.RankingAdapter.Ranking(supList, critiotions, inn));

                Log.Logger.Information($"Ранжирование завершено");
            }
            catch(HttpRequestException e)
            {
                Log.Logger.Error($"Неожиданная ситуация {e.Message}, источник {e.Source}");
            }

            Log.Logger.Information($"Поиск и ранжирование завершены");
            return list;
        }

        /// <summary>
        /// Просто поиск без ранжирования для подсистемы мониторинга
        /// (Доделать на будущее)
        /// </summary>
        /// <param name="okpd2">По какому "продукту" ищем (если пустой, то по всем)</param>
        /// <param name="userId">Возможно пригодится</param>
        /// <param name="count">сколько ищем (чтобы не слишком долгим был поиск)</param>
        /// <returns></returns>
        [HttpGet("OnlySearch")]
        public async Task<ActionResult<List<SupplierSearchResultModel>>> GetSearchedList(
            [FromQuery] string okpd2 = "",
            [FromQuery] string? userId = "",
            [FromQuery] int count = 100)
        {
            Log.Logger.Information($"Ведется поиск поставщиков...");

            List<SupplierSearchResultModel> resList = new();

            okpd2 = okpd2.Trim();

            try
            {
                List<SupplierFoundModel> supList = new List<SupplierFoundModel>();

                Log.Logger.Information($"Начат поиск поставщиков...");

                foreach (var searchAdapterem in AdapterContainer.SupplierSearchAdapters)
                {
                    supList.AddRange(await searchAdapterem.Find(okpd2));
                }

                supList.DistinctBy(s => s.Inn);

                Log.Logger.Information($"Поиск поставщиков завершен");
            }
            catch (HttpRequestException e)
            {
                Log.Logger.Error($"Неожиданная ситуация {e.Message}, источник {e.Source}");
            }

            return Ok(resList);
        }
    }
}
