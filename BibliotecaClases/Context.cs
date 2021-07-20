using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<CategoriaProducto> Categorias { get; set; }



        public Context()
             : base("GestOn")
        {
            ConfigureForSqlServer();
        }
        public static void ConfigureForSqlServer()
        {
            try
            {
                //Local 
                String baseConnectionString = @"Data Source=TPZPC127\SQLEXPRESS;user id=sa;password=Alicia2206**;MultipleActiveResultSets=True";
                Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlConnectionFactory(baseConnectionString);
            }
            catch (Exception ex)
            {
            }
        }

        public Context(String baseDatos) : base(baseDatos) { }


        //Mapeo de relacion entre *  a  * ( Producto con Pedido)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Pedido>().HasMany(p => p.productos).WithMany().Map(mc =>
            {
                mc.ToTable("ProductoPedido");
                mc.MapLeftKey("IdPedido");
                mc.MapRightKey("ProductoId");
            });
        }

        public class Initializer : IDatabaseInitializer<Context>
        {
            public void InitializeDatabase(Context context)
            {
                context.Database.Delete();
                context.Database.Create();
            }
        }


       
        }
    }
