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
            //Añadir dependencia  de servicios
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IProvedorService,ProveedorService>();
            services.AddScoped<ITipoPagoService,TipoPagoService>();

            return services;
        }
    }

}
