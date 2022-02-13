using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Entites
{
    public class Item
    {
        public Item()
        {
            CreatedAt = DateTime.Now;
            ItemImages = new List<ItemImages>();
            ItemLikes = new List<ItemLikes>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        public virtual List<ItemImages> ItemImages { get; set; }
        public virtual List<ItemLikes> ItemLikes { get; set; }

    }
}
