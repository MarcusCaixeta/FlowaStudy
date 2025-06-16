
using FlowaStudy.Domain.Common;

namespace FlowaStudy.Domain.Entities
{
    public class FinancialAsset : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public DateTime AcquisitionDate { get; private set; }

        public FinancialAsset(string name, decimal value, DateTime acquisitionDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (value <= 0)
                throw new ArgumentException("Value must be greater than zero.", nameof(value));
            if (acquisitionDate > DateTime.Now)
                throw new ArgumentException("Acquisition date cannot be in the future.", nameof(acquisitionDate));

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
