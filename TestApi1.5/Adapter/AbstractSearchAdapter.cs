using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Adapter
{
    public abstract class AbstractSearchAdapter : AbstractAdapter
    {
        public abstract Task<List<SupplierFoundModel>> Find(string okpd2);
    }
}
