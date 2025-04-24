using Project_Api.Interfaces;
using ProjectApi.Models;
using static Project_Api.Reposatories.PaymentRepository;

namespace Project_Api.Reposatories
{
    public class PaymentRepository:Paymentt
    {
       
            private readonly ApplicationDbContext _context;

            public PaymentRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Payment> GetAll() =>
                _context.Payments.ToList();

            public Payment GetById(int id) =>
                _context.Payments.Find(id);

            //public List<Payment> GetByStatus(string status) =>
            //    _context.Payments
            //        .Where(p => p.Status == status)
            //        .ToList();

            public void insert(Payment obj) =>
                _context.Payments.Add(obj);

            public void Update(Payment obj) =>
                _context.Payments.Update(obj);

            public void Delete(Payment obj) =>
                _context.Payments.Remove(obj);

            public void Save() => _context.SaveChanges();
        }
    }

