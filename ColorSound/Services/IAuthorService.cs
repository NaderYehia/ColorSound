using Grad_Proj.Entites;

namespace Grad_Proj.Services
{
    public interface IAuthorService
    {
        bool IsValidAuthor(Author user);
        bool RegisterAuthor(Author user);
        bool IsExist(string email);
        Author GetAuthorByEmail(string email);
    }
}
