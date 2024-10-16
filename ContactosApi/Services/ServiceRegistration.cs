using ContactosApi.Services.contact;
using ContactosApi.Services.Contact;

namespace ContactosApi.Services
{
    public static  class ServiceRegistration
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<GetContact>();
            services.AddScoped<CreateContact>();
            services.AddScoped<GetByIdContact>();

        }
    }
}
