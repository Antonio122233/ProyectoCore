using Aplicacion.Interfaces.Servicios;
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
            services.AddScoped<ICategoriaService,CategoriaService>();
            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<ICompraService,CompraService>();

            //me falta agregar compra
            return services;
        }
    }

}
