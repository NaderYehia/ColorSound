using System.Collections.Generic;

namespace Grad_Proj.Entites
{
    public class Category
    {
        public Category()
        {
            Items = new List<Item>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}