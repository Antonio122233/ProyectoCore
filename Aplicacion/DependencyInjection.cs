using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;


namespace Aplicacion
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Marcas
            services.AddScoped<IMarcaService, MarcaService>();
            return services;
        }
    }

}
