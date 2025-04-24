using ProjectApi.Models;
using ProjectApi.Repositories;

namespace Project_Api.Interfaces
{
    public interface TextSession:IGenericRepository<TextSessionDetail>
    {
        //List<TextSessionDetail> GetWithUnreadMessages();
    }
}
