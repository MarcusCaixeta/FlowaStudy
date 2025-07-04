﻿using FlowaStudy.Common.Validation;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlowaStudy.Domain.Common
{
    public class BaseEntity : IComparable<BaseEntity>
    {        
        public Guid Id { get; set; }

        public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
        {
            return Validator.ValidateAsync(this);
        }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
            {
                return 1;
            }

            return other!.Id.CompareTo(Id);
        }
    }
}
