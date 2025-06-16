
using FlowaStudy.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlowaStudy.Domain.Entities
{
    public class FinancialAssetMongo
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get;  set; }
        [BsonElement("Value")]
        public decimal Value { get;  set; }
        [BsonElement("AcquisitionDate")]
        public DateTime AcquisitionDate { get;  set; }

        public FinancialAssetMongo(Guid id, string name, decimal value, DateTime acquisitionDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero.", nameof(value));
            if (acquisitionDate > DateTime.Now)
                throw new ArgumentException("Acquisition date cannot be in the future.", nameof(acquisitionDate));

            Id = id;
            Name = name;
            Value = value;
            AcquisitionDate = acquisitionDate;
        }

        public void UpdateValue(decimal newValue)
        {
            if (newValue <= 0)
                throw new ArgumentException("Value must be greater than zero.", nameof(newValue));
            Value = newValue;
        }
    }
}
