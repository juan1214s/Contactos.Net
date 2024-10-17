using ContactosApi.Services.contact;
using ContactosApi.Services.Contact;

namespace ContactosApi.Services
{
    public static  class ServiceRegistration
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<GetContactService>();
            services.AddScoped<CreateContactService>();
            services.AddScoped<GetByIdContactService>();
            services.AddScoped<DeleteContactService>();
            services.AddScoped<UpdateContactService>();
        }
    }
}
