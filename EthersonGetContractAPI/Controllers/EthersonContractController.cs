using AutoMapper;
using Azure;
using EthersonGetContractAPI.IRepository;
using EthersonGetContractAPI.Model;
using EthersonGetContractAPI.Model.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;



namespace EthersonGetContractAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EthersonContractController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IContractRepository _dbContracts;
        private readonly string ethersonURL = "https://api.etherscan.io/api";


        private static List<ContractsDTO> contractsDTO = new List<ContractsDTO>();
        public EthersonContractController(IConfiguration config, IMapper Mapper, IContractRepository dbContracts)
        {
            _httpClient = new HttpClient();
            _config = config;
            this._response = new APIResponse();
            _mapper = Mapper;
            _dbContracts = dbContracts;
        }


        //Endpoint to get contracts from etherson website and store in database
        [HttpGet("getContractInfoFromEtherson")]

        public async Task<ActionResult<APIResponse>> GetContractInfo(string contractAddress, string ApiKey)
        {
            try
            {


                //APIKey validation
                if (ApiKey == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string>() { "No Apikey" };
                    return BadRequest(_response);
                }

                string apikey = _config["APIKeyStrings:ApiKey"];

                if (apikey != ApiKey)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string>() { "ApiKey is invalid" };
                    return BadRequest(_response);
                }

                //contract Address validation
                if (string.IsNullOrEmpty(contractAddress))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string>() { "No contract address" };
                    return BadRequest(_response);
                }

                string urlWithContract = $"{ethersonURL}?module=account&action=txlist&address={contractAddress}&apikey={ApiKey}";

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                HttpResponseMessage response = await _httpClient.GetAsync(urlWithContract);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    EthersonApiResponse apiResponse = JsonConvert.DeserializeObject<EthersonApiResponse>(data);
                    contractsDTO = apiResponse.Result;
                    List<Contracts> contracts = new List<Contracts>();

                    contracts = _mapper.Map<List<Contracts>>(contractsDTO);

                    //Add to database
                    foreach (var contract in contracts)
                    {
                        await _dbContracts.CreateAsync(contract);
                    }

                    //  contracts = await _dbContracts.GetAllAsync();

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = contractsDTO;


                    return Ok(_response);

                }
                

                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        //Endpoint to get stored contract from DB base on Id
        [HttpGet("getContractInfoFromDB")]
        public async Task<ActionResult<APIResponse>> GetContractInfoFromDB(int id)
        {
            try
            {
                if (id == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string>() { "No Id" };

                    return BadRequest(_response);

                }

                var contractObj = await _dbContracts.GetAsync(u => u.Id == id);

                if (contractObj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string>() { "No value" };
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = contractObj;
                return Ok(_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        //Endpoint to get all the stored contracts from DB 
        [HttpGet("getAllContractInfoFromDB")]

        public async Task<ActionResult<APIResponse>> GetAllContractInfoFromDB()
        {
            try
            {

                IEnumerable<Contracts> contractObj = await _dbContracts.GetAllAsync();

                if (contractObj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string>() { "No value" };
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = contractObj;
                return Ok(_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;

        }

    }
}
