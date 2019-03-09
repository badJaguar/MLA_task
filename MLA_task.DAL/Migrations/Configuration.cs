using MLA_task.DAL.EF;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MLA_task.DAL.EF.DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DemoContext context)
        {
            context.DemoCommonInfoModels.Add(new DemoCommonInfoDbModel { CommonInfo = "Standard 1" });
            context.DemoCommonInfoModels.Add(new DemoCommonInfoDbModel { CommonInfo = "Standard 2" });
            context.DemoCommonInfoModels.Add(new DemoCommonInfoDbModel { CommonInfo = "Standard 3" });

            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 1", DemoCommonInfoModelId = 1 });
            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 2", DemoCommonInfoModelId = 1 });
            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 3", DemoCommonInfoModelId = 1 });

            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 4", DemoCommonInfoModelId = 2 });
            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 5", DemoCommonInfoModelId = 3 });
            context.DemoDbModels.Add(new DemoDbModel { Name = "Demo 6", DemoCommonInfoModelId = 3 });

            base.Seed(context);
        }
    }
}
