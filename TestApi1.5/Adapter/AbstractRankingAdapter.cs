using TestApi.Model;

namespace TestApi.Adapter
{
    public abstract class AbstractRankingAdapter : AbstractAdapter
    {
        public abstract Task<List<SupplierSearchResultModel>> Ranking(List<SupplierFoundModel> suppliers,
            CrititionsModel cretitions, string inn);
    }
}
