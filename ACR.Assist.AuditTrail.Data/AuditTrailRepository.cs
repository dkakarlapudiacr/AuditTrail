using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.AuditTrail.Core.DTO;
using Acr.Assist.AuditTrail.Data.MongoContext;
using ACR.Assist.AuditTrail.Core.Data;
using MongoDB.Driver;

namespace Acr.Assist.AuditTrail.Data
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        MongoContext.MongoContext dbContext;

        public AuditTrailRepository(string connectionString, string dataBase)
        {
            dbContext = new MongoContext.MongoContext(connectionString, dataBase);
        }

        public async Task<Guid> AddAuditTrail(AuditTrailEntry audittrailentry)
        {
            IMongoCollection<AuditTrailEntry> logs = dbContext.DataBase.GetCollection<AuditTrailEntry>(Constants.AuditTrailCollection);
            await logs.InsertOneAsync(audittrailentry);
            return audittrailentry.LogID;
        }

      
    }
}
