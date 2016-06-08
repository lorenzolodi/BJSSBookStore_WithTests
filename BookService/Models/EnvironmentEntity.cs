using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookService.Models
{
    public abstract class EnvironmentEntity
    {
        public Guid EnvironmentId { get; set; }

        public Environment Environment { get; set; }
    }
}