namespace Website_ASPNet.SQL.Tables
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime Creation_Date { get; set; }
    }
}
