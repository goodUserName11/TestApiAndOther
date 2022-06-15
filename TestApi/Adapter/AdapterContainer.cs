namespace TestApi.Adapter
{
    public static class AdapterContainer
    {
        static AdapterContainer()
        {
            Okpd2Adapter = new Okpd2ParserAdapter();

            SupplierSearchAdapters = new List<AbstractSearchAdapter>();
            SupplierSearchAdapters.Add(new SupplierDBSearchAdapter());
            SupplierSearchAdapters.Add(new SupplierApiSearchAdapter());

            RankingAdapter = new RankingAdapter();
        }

        public static AbstractOkpd2Adapter Okpd2Adapter { get; }
        public static List<AbstractSearchAdapter> SupplierSearchAdapters { get; }
        public static AbstractRankingAdapter RankingAdapter { get; }
    }
}
