namespace Infrastructure
{
    public class MongoOptions
    {
        public const string OptionsName = "MongoOptions";
        public required string Endpoint { get; set; }
        public required string Database { get; set; }
        public required string OrdersCollection { get; set; }
    }
}
