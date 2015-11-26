namespace Dominio.Persistencia
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Modelos;

    public class AconmatContext : DbContext
    {
        // Your context has been configured to use a 'AconmatContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Dominio.Persistencia.AconmatContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AconmatContext' 
        // connection string in the application configuration file.
        public AconmatContext()
            : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Aluno> Alunos { get; set; }
    }
}