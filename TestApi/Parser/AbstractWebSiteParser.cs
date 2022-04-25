using TestApi.Entity;

namespace TestApi.Parser
{
    public abstract class AbstractWebSiteParser
    {
        public abstract Task<List<Supplier>> Parse(string product);
    }
}
