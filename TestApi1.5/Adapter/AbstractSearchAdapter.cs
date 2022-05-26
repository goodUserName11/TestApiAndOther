using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Adapter
{
    public abstract class AbstractSearchAdapter : AbstractAdapter
    {
        public abstract Task<List<Supplier>> Find(string okpd2, List<CretitionModel> cretitions);
    }
}
