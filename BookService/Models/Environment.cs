using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookService.Models
{
    public class Environment
    {
        public Guid Id { get; set; }
        public bool SqlInjected { get; set; }
        
        public Environment(){
            this.Id = Guid.NewGuid();
        }
        
    }    
}