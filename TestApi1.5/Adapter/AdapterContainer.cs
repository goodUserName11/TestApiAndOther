namespace TestApi.Adapter
{
    public static class AdapterContainer
    {
        static AdapterContainer()
        {
            Okpd2Adapter = new Okpd2ParserAdapter();
        }

        public static AbstractOkpd2Adapter Okpd2Adapter { get; }
    }
}
