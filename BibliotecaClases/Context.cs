using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;


namespace BibliotecaClases
{
    class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        public Context()
             : base("GestOn")
        {
            ConfigureForSqlServer();
        }
        public static void ConfigureForSqlServer()
        {
            try
            {//Local 
                String baseConnectionString = @"Data Source=LAPTOP-9SVDO2G2\SQLEXPRESS;user id=sa;password=123456789;MultipleActiveResultSets=True";


                Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlConnectionFactory(baseConnectionString);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
