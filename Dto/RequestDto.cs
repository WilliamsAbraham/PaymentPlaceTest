namespace PaymentPlaceTest.Dto
{
    public class RequestDto
    {
        public string amount { get; set; }
        public string reference { get; set; }
        public string email { get; set; }
        public string callback_url { get; set; }
        public bool user_Production_Credentials { get; set; }
    }
}
