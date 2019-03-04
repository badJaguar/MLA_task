﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MLA_task.DAL.EF;
using MLA_task.DAL.Interface;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.Repositories
{
    public class DemoDbModelRepository : IDemoDbModelRepository
    {
        private readonly DemoContext _context;

        public DemoDbModelRepository(DemoContext context)
        {
            _context = context;
        }

        public async Task<DemoDbModel> GetByIdAsync(int id)
        {
            var model = await _context.DemoDbModels.SingleAsync(item => item.Id == id);

            return model;
        }

        public async Task<List<DemoDbModel>> GetAll()
        {
            var models = await _context.DemoDbModels.ToListAsync();
            return models;
        }

        public async Task<DemoCommonInfoDbModel> GetCommonInfoByDemoIdAsync(int demoDbModelId)
        {
            var demoModel = 
                await _context.DemoDbModels.SingleAsync(item => item.Id == demoDbModelId);

            var commonInfo = 
                await _context.DemoCommonInfoModels.SingleAsync(item => item.Id == demoModel.DemoCommonInfoModelId);

            return commonInfo;
        }

        public async Task<List<DemoCommonInfoDbModel>> GetCommonInfosAsync()
        {
            var demoModel =
                await _context.DemoDbModels.ToListAsync();

            var commonInfo =
                await _context.DemoCommonInfoModels.SingleAsync(item => Equals(item.DemoModels, demoModel));

            return new List<DemoCommonInfoDbModel> {commonInfo};
        }
    }
}