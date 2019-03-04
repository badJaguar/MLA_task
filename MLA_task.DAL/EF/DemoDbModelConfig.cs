﻿using System.Data.Entity.ModelConfiguration;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.EF
{
    public class DemoDbModelConfig : EntityTypeConfiguration<DemoDbModel>
    {
        public DemoDbModelConfig()
        {
            ToTable("DemoTable");
            HasKey(m => m.Id);
            Property(m => m.Name).IsRequired().IsUnicode().HasMaxLength(35).IsVariableLength();
            Property(m => m.Created).IsRequired();
            Property(m => m.Modified).IsOptional();
            Property(m => m.DemoCommonInfoModelId).IsRequired();
            HasRequired(m => m.DemoCommonInfoModel)
                .WithMany(i => i.DemoModels);
        }
    }
}