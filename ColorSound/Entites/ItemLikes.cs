using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Entites
{
    public class ItemLikes
    {
        public int Id { get; set; }
        public  int ItemId { get; set; }
        public int AuthorId { get; set; }
        public Item Item { get; set; }
        public Author Author { get; set; }

    }
}
