using Aplicacion.Interfaces.Repositorios;
using Infraestructura.Persistencia;
using Infraestructura.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            //DbContext
            services.AddDbContext<BdTiendaContext>(options =>
                options.UseSqlServer(connectionString));

            //Registrar Repositorio Genérico
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Registrar otros repositorios
            services.AddScoped(typeof(IMarcaRepository), typeof(MarcaRepository));            
            services.AddScoped(typeof(IProveedorRepository), typeof(ProveedorRepository));
            services.AddScoped(typeof(ITipoPagoRepository),typeof(TipoPagoRepository));
            services.AddScoped(typeof(ICategoriaRepository), typeof(CategoriaRepository));
            services.AddScoped(typeof(IProductoRepository), typeof(ProductoRepository));



            return services;
        }        
    }
}
