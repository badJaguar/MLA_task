﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.Interface
{
    public interface IDemoDbModelRepository
    {
        Task<DemoDbModel> GetByIdAsync(int id);
        Task<List<DemoDbModel>> GetAll();
        Task<DemoDbModel> AddAsync(DemoDbModel dbModel);

        Task<DemoCommonInfoDbModel> GetCommonInfoByDemoIdAsync(int demoDbModelId);
        //Task<List<DemoCommonInfoDbModel>> GetCommonInfosAsync();
    }
}
