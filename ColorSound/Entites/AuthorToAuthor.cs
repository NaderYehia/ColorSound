using System;

namespace Grad_Proj.Entites
{
    public class AuthorToAuthor
    {
        public AuthorToAuthor()
        {
            Since = DateTime.Now;
        }
        public Author Author { get; set; }
        public int? AuthorId { get; set; }
        public Author Follower { get; set; }
        public int? FollowerId { get; set; }
        public DateTime Since { get; set; }
    }
}