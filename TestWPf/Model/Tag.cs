using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWPF.Model
{
    public partial class Tag
    {
        public Tag()
        {
            IdOrders = new HashSet<Order>();
        }

        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Order> IdOrders { get; set; }
    }
}
