using TestApi.Entity;

namespace TestApi.Adapter
{
    public abstract class AbstractOkpd2Adapter : AbstractAdapter
    {
        public abstract void AddToDb();
        public abstract List<Okpd2> GetAllOkpd2s(int? top = null);
        public abstract List<Okpd2> GetOkpd2sByText(string text);
    }
}
