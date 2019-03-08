﻿using System;
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

        public DemoModelService(IDemoDbModelRepository demoDbModelRepository)
        {
            _demoDbModelRepository = demoDbModelRepository;
        }

        public async Task<DemoModel> GetDemoModelByIdAsync(int id)
        {
            if (id == 23) {
                throw new DemoServiceException(DemoServiceException.ErrorType.WrongId);
            }

            var dbModel = await _demoDbModelRepository.GetByIdAsync(id);
            var commonInfo = await _demoDbModelRepository.GetCommonInfoByDemoIdAsync(id);

            var demoModel = new DemoModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Created = dbModel.Created,
                Modified = dbModel.Modified,
                CommonInfo = commonInfo.CommonInfo
            };

            return demoModel;
        }

        public async Task<List<DemoModel>> GetDemoModelsAsync()
        {
            var dbModel = await _demoDbModelRepository.GetAll();
            var commonInfo = await _demoDbModelRepository.GetCommonInfosAsync();

            var demoModel = new List<DemoModel>
            {
                
            };
            return demoModel;
        }
    }
}
