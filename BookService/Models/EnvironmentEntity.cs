using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BookService.Models
{
    public abstract class EnvironmentEntity
    {
        [IgnoreDataMember]
        public Guid EnvironmentId { get; set; }

        [IgnoreDataMember]
        public Environment Environment { get; set; }
    }
}