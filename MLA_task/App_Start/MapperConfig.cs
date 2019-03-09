using AutoMapper;
using System;
using System.Linq;

namespace MLA_task.App_Start
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith("MLA_Task")).ToArray();
                cfg.AddProfiles(assemblies);

            });
        }
    }
}