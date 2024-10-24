using ContactosApi.Services.contact;
using ContactosApi.Services.Contact;
using ContactosApi.Services.UserService;
using ContactosApi.Services.SeekerService;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ContactosApi.Bcrypt;


namespace ContactosApi.Services
{
    public static  class ServiceRegistration
    {
        public static void AddCustomService(this IServiceCollection services)
        {

            //Contactos
            services.AddScoped<GetContactService>();
            services.AddScoped<CreateContactService>();
            services.AddScoped<GetByIdContactService>();
            services.AddScoped<DeleteContactService>();
            services.AddScoped<UpdateContactService>();

            //Usuarios
            services.AddScoped<CreateUserService>();
            services.AddScoped<DeleteUserService>();
            services.AddScoped<GetUserByIdService>();
            services.AddScoped<UpdateUserService>();

            //Buscador
            services.AddScoped<SeekerServices>();

            //Encriptar la contraseña
            services.AddSingleton<PasswordHasher>();
        }
    }
}
