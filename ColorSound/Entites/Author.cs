using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Entites
{
    public class Author
    {
        public Author()
        {
            Followers = new List<AuthorToAuthor>();
            Following = new List<AuthorToAuthor>();
            Items = new List<Item>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string PortfolioLink { get; set; }
        public string Image { get; set; }
        public string Cover { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public virtual List<AuthorToAuthor> Followers { get; set; }
        public virtual List<AuthorToAuthor> Following { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
