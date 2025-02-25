namespace WEB_API_CQRS.src.Infrastructure.Persistence
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductsCollection { get; set; } = string.Empty;
    }
}
