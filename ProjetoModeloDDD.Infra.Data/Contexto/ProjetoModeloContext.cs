using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext() 
            : base("ProjetoModeloDDD")
        {
            FixEfProviderServicesProblem();
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));                       
            
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
        }

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries().Where(w => w.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if(item.State == EntityState.Added)
                {
                    item.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                /*
                 Não modificando a Data de Cadastro quando for um update.
                 */
                if(item.State == EntityState.Modified)
                {
                    item.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}
