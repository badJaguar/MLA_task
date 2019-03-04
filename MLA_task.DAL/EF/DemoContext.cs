using System.Data.Entity;
using MLA_task.DAL.Interface.Entities;
using MLA_task.DAL.Migrations;

namespace MLA_task.DAL.EF
{
    public class DemoContext : DbContext
    {
        public DemoContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DemoContext, Configuration>(true));
        }

        public IDbSet<DemoDbModel> DemoDbModels { get; set; }
        public IDbSet<DemoCommonInfoDbModel> DemoCommonInfoModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DemoDbModelConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}