using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ApartmentsBilling.PaymentApiSevices.Entities.Common
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdateAt { get; set; }
        public bool Status { get; set; } = true;
    }
}
