using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using TestApi.Adapter;
using TestApi.Parser;
using TestApi.Model;
using TestApi.Authentication;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SupplierSearchResultModel>>> Get( 
            [FromQuery] CrititionsModel? critiotions = null, [FromQuery] string userId = "")
        {
            List<SupplierSearchResultModel> list = new();

            if (critiotions is null || userId is null)
                return new List<SupplierSearchResultModel>();

            if(critiotions.Count < 0 || critiotions.MinPrice < 0 || critiotions.MaxPrice < critiotions.MinPrice 
                || string.IsNullOrWhiteSpace(critiotions.Okpd2) || string.IsNullOrWhiteSpace(critiotions.Region) 
                || string.IsNullOrWhiteSpace(critiotions.WayOfDestribution))
                return new List<SupplierSearchResultModel>();

            critiotions.Okpd2.Trim();

            string inn = "";

            try
            {
                List<SupplierFoundModel> supList = new List<SupplierFoundModel>();

                supList.AddRange(await AdapterContainer.SupplierSearchAdapters[0].Find(critiotions.Okpd2));
                supList.AddRange(await AdapterContainer.SupplierSearchAdapters[1].Find(critiotions.Okpd2));

                supList.DistinctBy(s => s.Inn);

                using (Data.SearchAndRangeContext dbContext = new Data.SearchAndRangeContext())
                {

                    if (string.IsNullOrEmpty(userId) || userId == "0")
                        inn = dbContext.Users.FirstOrDefault(u => u.Role == UserRoles.Admin.Id).CompanyInn;
                    else
                        inn = (await dbContext.Users.FindAsync(Guid.Parse(userId)))?.CompanyInn;

                }

                list.AddRange(
                    await AdapterContainer.RankingAdapter.Ranking(supList, critiotions, inn));
            }
            catch(HttpRequestException e)
            {
            }

            return list;
        }
    }
}
