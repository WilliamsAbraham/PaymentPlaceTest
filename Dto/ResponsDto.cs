namespace PaymentPlaceTest.Dto
{
    public class ResponsDto
    {
        public bool status { get; set; }
        public string message { get; set; }

        public Data data { get; set; }
    }

    public class Data
    {
        public string authorization_url { get; set; }
        public string access_Code { get; set; }
        public string reference { get; set; }
    }

}
