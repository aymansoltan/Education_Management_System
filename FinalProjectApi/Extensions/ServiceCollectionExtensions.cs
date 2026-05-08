using FinalProjectMVC.Repository;

namespace FinalProjectMVC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

           
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IInstractorRepository, InstractorRepository>();
            services.AddScoped<ITraineeRepository, TraineeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseResultRepository, CourseResultRepository>();

            return services;
        }
    }
}
