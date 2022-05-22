using TestApi.Entity;

namespace TestApi.Adapter
{
    public abstract class AbstractSearchAdapter : AbstractAdapter
    {
        public abstract Task<List<Supplier>> Find(string okpd2);
    }
}
