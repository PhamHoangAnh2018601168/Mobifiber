using AutoMapper;
using MobiFiber.Models;
using MobiFiber.PartialViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiFiber.Code
{
    public class MappingEntity: Profile
    {
        public MappingEntity()
        {
            CreateMap<List<DeviceViewModel>,List<MobifiberDevice>>();
            CreateMap<DeviceViewModel, MobifiberDevice>();
            CreateMap<PackageViewModel, MobifiberPackage>();
            CreateMap<CustomerViewModel, MobifiberContract>();
        }
    }
}
