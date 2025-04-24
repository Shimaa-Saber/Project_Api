 namespace Project_Api.Repositories.Interfaces
{
    public interface ISpecializationTypeRepository : IRepository<SpecializationType>
    {
        Task<IEnumerable<SpecializationType>> GetByCategoryAsync(string category);
    }
}
