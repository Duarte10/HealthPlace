using System;
using LiteDB;

namespace HealthPlace.Core.Models
{
    public abstract class ModelBase
    {
        [BsonId(true)]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public abstract override string ToString();

    }
}
