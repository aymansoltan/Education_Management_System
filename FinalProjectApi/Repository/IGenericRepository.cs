namespace FinalProjectMVC.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
        void Save();
        T? GetById(int id);
        List<T> GetAll();

    }
}
