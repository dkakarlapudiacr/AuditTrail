using MongoDB.Driver;

namespace Acr.Assist.AuditTrail.Data.MongoContext
{
    public class MongoContext
    {
        MongoClient Client;

        private IMongoDatabase mongoDB;
        public MongoContext(string connectionString, string dataBase)
        {

            var mongoUrl = new MongoUrl(connectionString);
            Client = new MongoClient(connectionString);
            mongoDB = Client.GetDatabase(dataBase);

        }

        public IMongoDatabase DataBase { get => mongoDB; set => mongoDB = value; }
    }
}
