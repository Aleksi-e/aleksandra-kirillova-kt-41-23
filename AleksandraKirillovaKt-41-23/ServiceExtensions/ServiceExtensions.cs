using AleksandraKirillovaKt_41_23.Interfaces;

namespace AleksandraKirillovaKt_41_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();
            return services;
        }
    }
}
