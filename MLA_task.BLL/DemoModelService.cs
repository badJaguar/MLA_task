﻿using AutoMapper;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Models;
using MLA_task.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //return await _demoDbModelRepository.GetAll()
            //    .ContinueWith(task =>
            //        _mapper.Map<List<DemoModel>>(task.Result));

                var list = await _demoDbModelRepository.GetAll();
                return list.Select(model => _mapper.Map<DemoModel>(model)).ToList();
        }

        public async Task<DemoModel> AddDemoModelAsync(DemoModel model)
        {
            var result = _mapper.Map<DemoModel>(model);

            result.Id = model.Id + 1;
            result.CommonInfo = model.CommonInfo;
            result.Created = DateTime.UtcNow;
            result.Modified = DateTime.Now;
            result.Name = "'Default name' please change.";

            return result;
        }
    }
}
