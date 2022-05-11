namespace TestApi.Adapter
{
    public static class AdapterContainer
    {
        static AdapterContainer()
        {
            Okpd2Adapter = new Okpd2ParserAdapter();
            OtcAdapter = new OtcParserAdapter();
            //RoseltorgParser = new RoseltorgParser();
        }

        public static AbstractOkpd2Adapter Okpd2Adapter { get; }
        public static AbstractSearchAdapter OtcAdapter { get; }
        //public RoseltorgParserAdapter RoseltorgParserAdapter { get; private init; }
    }
}
