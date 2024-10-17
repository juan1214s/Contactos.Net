using ContactosApi.Services.contact;
using ContactosApi.Services.Contact;
using ContactosApi.Services.UserService;

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
            services.AddScoped<CreateUserService>();
        }
    }
}
