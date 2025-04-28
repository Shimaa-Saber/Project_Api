using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface ITextSession:IGenericRepository<TextSessionDetail>
    {
        //List<TextSessionDetail> GetWithUnreadMessages();
    }
}
