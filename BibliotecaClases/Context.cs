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
        public DbSet<ProductoPedidoCantidad> CanttidadProductos { get; set; }
        public DbSet<ImagenPedido> ImagenesPedidos { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<PedidoDocumento> PedidoDocumentos { get; set; }
        public DbSet<Audit_Oferta> audit_Ofertas { get; set; }
        public DbSet<Audit_OfertaImegen> audit_OfertaImegens { get; set; }
        public DbSet<Audit_Pedidos> audit_Pedidos { get; set; }
        public DbSet<Audit_Documentos> audit_Documentos { get; set; }
        public DbSet<Audit_Productos> audit_Productos { get; set; }
        public DbSet<Audit_Configuraciones> audit_Configuraciones { get; set; }
        public DbSet<Audit_CategoriaProductos> audit_CategoriaProductos { get; set; }
        public DbSet<Audit_Niveles> audit_Niveles { get; set; }
        public DbSet<Audit_ImagenPedido> audit_ImagenPedidos { get; set; }
        public DbSet<Audit_Usuarios> audit_Usuarios { get; set; }
        public DbSet<Audit_ProdPedCantidad> audit_ProdPedCantidads { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }

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
                String baseConnectionString = @"Data Source=TPZPC116;user id=sa;password=50983019Al;MultipleActiveResultSets=True";

                Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlConnectionFactory(baseConnectionString);
            }
            catch (Exception ex)
            {
            }
        }

        public Context(String baseDatos) : base(baseDatos) { }

        //Mapeo de relacion entre *  a  * ( Producto con Pedido),
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<ProductoPedidoCantidad>().HasKey(c => new { c.IdPedido, c.ProductoId,c.Cantidad});
            modelBuilder.Entity<PedidoDocumento>().HasKey(p => new {p.idDocumento, p.idPedido });

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
