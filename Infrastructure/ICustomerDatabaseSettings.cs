namespace MicroCredential.Infrastructure
{
    public interface ICustomerDatabaseSettings
    {
        string CustomerCollectionName { get; set; }
        
        string ConnectionString { get; set; }
        
        string DatabaseName { get; set; }
    }
}
