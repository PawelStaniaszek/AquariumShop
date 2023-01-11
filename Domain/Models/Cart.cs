using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cart : BaseModel
    {
        
        public string UserId { get; set; }

        public ApiUser User { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
