using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Models;
using MLA_task.DAL.Interface;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.BLL
{
    public class DemoModelService : IDemoModelService
    {
        private readonly IDemoDbModelRepository _demoDbModelRepository;
        private IMapper _mapper;

        public DemoModelService(IDemoDbModelRepository demoDbModelRepository, IMapper mapper)
        {
            _demoDbModelRepository = demoDbModelRepository;
            _mapper = mapper;
        }

        public async Task<DemoModel> GetDemoModelByIdAsync(int id)
        {
            if (id == 23) {
                throw new DemoServiceException(DemoServiceException.ErrorType.WrongId);
            }
            return await _demoDbModelRepository.GetByIdAsync(id)
                .ContinueWith(t => _mapper.Map<DemoModel>(t.Result));
            
        }
        
        public async Task<List<DemoModel>> GetDemoModelsAsync()
        {
            return await _demoDbModelRepository.GetAll()
                .ContinueWith(task =>
                    _mapper.Map<List<DemoModel>>(task.Result));
        }

        public async Task<DemoModel> AddDemoModelAsync(DemoModel model)
        {
            var result = _mapper.Map<DemoModel>(model);
            var searchRes = await _demoDbModelRepository.GetByIdAsync(model.Id);
            if (searchRes.Id == model.Id)
            {
                throw new ArgumentException();
            }
            result.Id = model.Id + 1;
            result.CommonInfo = model.CommonInfo;
            result.Created = DateTime.UtcNow;
            result.Modified = DateTime.Now;
            result.Name = null;

            return result;
        }
        //var dbHives = await _context.Hives.Where(h => h.Code == createRequest.Code).ToArrayAsync();
        //    if (dbHives.Length > 0)
        //{
        //    throw new RequestedResourceHasConflictException("code");
        //}

        //var dbHive = Mapper.Map<UpdateHiveRequest, DbHive>(createRequest);
        //dbHive.CreatedBy = _userContext.UserId;
        //dbHive.LastUpdatedBy = _userContext.UserId;
        //_context.Hives.Add(dbHive);

        //await _context.SaveChangesAsync();

        //    return Mapper.Map<Hive>(dbHive);
    }
}
