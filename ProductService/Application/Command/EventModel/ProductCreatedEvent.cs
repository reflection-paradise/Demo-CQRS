using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.EventModel
{
    public class ProductCreatedEvent
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
