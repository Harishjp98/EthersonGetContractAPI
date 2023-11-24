using AutoMapper;
using EthersonGetContractAPI.Model;
using EthersonGetContractAPI.Model.DTOs;

namespace EthersonGetContractAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Contracts,ContractsDTO>().ReverseMap();
        }
    }
}
