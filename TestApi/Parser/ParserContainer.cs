namespace TestApi.Parser
{
    public static class ParserContainer
    {
        static ParserContainer()
        {
            Okpd2Parser = new Okpd2Parser();
            OtcParser = new OtcParser();
        }

        public static Okpd2Parser Okpd2Parser { get; }
        public static OtcParser OtcParser { get; }
    }
}
