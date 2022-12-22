using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlazingPizza
{
    public class Stupid
    {
        [Key]
        public int OrderId { get; set; }

        public string UserId { get; set; }

    }
}
