using AutoMapper;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Models;
using MLA_task.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                var result =  await _demoDbModelRepository.GetByIdAsync(id);
                return _mapper.Map<DemoModel>(result);
            
        }
        
        public async Task<List<DemoModel>> GetDemoModelsAsync()
        {
            //return await _demoDbModelRepository.GetAll()
            //    .ContinueWith(task =>
            //        _mapper.Map<List<DemoModel>>(task.Result));

            var list = await _demoDbModelRepository.GetAll();
            return list.Select(model => _mapper.Map<DemoModel>(model)).ToList();

            //var task = _demoDbModelRepository.GetAll();
            //var result = task.ContinueWith(task1 =>
            //_mapper.Map<List<DemoModel>>(task1.Result));
            //return await result;
        }

        public async Task<DemoModel> AddDemoModelAsync(DemoModel model)
        {
            var m = _mapper.Map<DemoModel, DemoDbModel>(model);

            var result = await _demoDbModelRepository.AddAsync(m);

            //result.Id = model.Id;
            //result.CommonInfo = model.CommonInfo;
            //result.Name = "'Default name' please change.";

            return _mapper.Map<DemoDbModel, DemoModel>(result);
        }

        public async Task DeleteModelByIdAsync(int id) => 
            await _demoDbModelRepository.DeleteAsync(id);
    }
}
