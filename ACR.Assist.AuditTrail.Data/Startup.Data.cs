using Acr.Assist.AuditTrail.Core.Domain;
using Acr.Assist.AuditTrail.Core.DTO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Acr.Assist.AuditTrail.Data
{
    public class Startup
    {
        public static void Configure()
        {
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            BsonClassMap.RegisterClassMap<AuditTrails>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(c => c.LogID).SetIdGenerator(CombGuidGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<AuditTrailEntry>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(c => c.LogID).SetIdGenerator(CombGuidGenerator.Instance);
            });
        }
    }
}
