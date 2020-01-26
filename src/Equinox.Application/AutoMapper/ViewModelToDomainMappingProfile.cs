﻿using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));

            CreateMap<BongViewModel, RegisterNewBongCommand>()
                .ConstructUsing(c => new RegisterNewBongCommand(c.Name, c.ReferenceNo, c.ArrivingInStock));
            CreateMap<BongViewModel, UpdateBongCommand>()
                .ConstructUsing(c => new UpdateBongCommand(c.Id, c.Name, c.ReferenceNo, c.ArrivingInStock));
        }
    }
}
