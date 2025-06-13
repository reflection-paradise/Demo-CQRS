﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CreateProductDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

    }
}
