using ProjectApi.Models;

namespace Project_Api.Repositories.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
    }
}
