using EthersonGetContractAPI.Model;
using EthersonGetContractAPI.Model.DTOs;

namespace EthersonGetContractAPI
{
    public class EthersonApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ContractsDTO> Result { get; set; }
    }
}
