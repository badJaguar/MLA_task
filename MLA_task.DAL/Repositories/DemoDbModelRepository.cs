using System;
using System.Collections.Generic;
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
            var models = await _context.DemoDbModels.Select(model => 
                model).ToListAsync();
            return models;
        }

        public async Task<DemoDbModel> AddAsync(DemoDbModel dbModel)
        {
            
            var model = _context.DemoDbModels.Add(dbModel);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var models = await _context.DemoDbModels.Where(dbModel => dbModel.Id == id).ToListAsync();
            var model = models[0];
            _context.DemoDbModels.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<DemoCommonInfoDbModel> GetCommonInfoByDemoIdAsync(int demoDbModelId)
        {
            var demoModel = 
                await _context.DemoDbModels.SingleAsync(item => item.Id == demoDbModelId);

            var commonInfo = 
                await _context.DemoCommonInfoModels.SingleAsync(item => 
                    item.Id == demoModel.DemoCommonInfoModelId);

            return commonInfo;
        }
    }
}