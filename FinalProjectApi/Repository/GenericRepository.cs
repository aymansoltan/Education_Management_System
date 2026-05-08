using FinalProjectApi.Models;


namespace FinalProjectMVC.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ITIContext context;

        public GenericRepository(ITIContext context)
        {
            this.context=context;
        }

        public void Add(T obj)
        {
            context.Add(obj);
        }

        public void Update(T obj)
        {
           context.Update(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Delete(int id )
        {
            T entity = GetById(id);

            if (entity!=null)
                context.Remove(entity);

        }

        public List<T> GetAll()
        {
           return context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }



       
    }
}
