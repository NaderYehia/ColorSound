using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Entites
{
    public class Orders
    {
        public Orders()
        {
            CreatedAt = DateTime.Now;
            TransactionId = Guid.NewGuid();
        }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TransactionId { get; set; }
    }
}
