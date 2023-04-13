using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetApp_BE.Feature.Pets.Models;
using VetApp_BE.Feature.Pets.Repositories;
using VetApp_BE.Feature.Pets.ViewModels;
using VetApp_BE.Helpers;

namespace VetApp_BE.Feature.Pets
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _petRepository;
        private readonly ILogger<PetsController> _logger;

        public PetsController(IPetRepository petRepository, ILogger<PetsController> logger)
        {
            _petRepository = petRepository;
            _logger = logger;
        }

        // CREATE
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<PetViewModels>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(PetViewModels request)
        {
            var response = new ApiResponse<PetViewModels>();
            try
            {
                var datas = ObjectHelpers.Convert<PetViewModels, PetModels>(request);
                _ = await _petRepository.AddAsync(datas);
                response.data = request;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.ToString()}");
                throw;
            }

        }

        // READ
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<List<PetViewModels>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = new ApiResponse<List<PetViewModels>>();
            try
            {
                var result = await _petRepository.GetAllAsync();
                var datas = result.Select(x => ObjectHelpers.Convert<PetModels, PetViewModels>(x)).ToList();

                response.data = datas;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.ToString()}");                
                throw;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<PetViewModels>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PetModels>> GetById(int id)
        {
            var response = new ApiResponse<PetViewModels>();
            try
            {
                var result = await _petRepository.GetByIdAsync(id);
                response.data = ObjectHelpers.Convert<PetModels, PetViewModels>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.ToString()}"); 
                throw;
            }
        }

    }
}
