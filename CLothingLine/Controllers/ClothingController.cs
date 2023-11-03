using AutoMapper;
using CLothingLine.Data;
using CLothingLine.Dtos;
using CLothingLine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CLothingLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingController : ControllerBase
    {
        private readonly IClothing _clothing;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ClothingController(IClothing clothing, ILogger logger, IMapper mapper)
        { this._clothing = clothing; this._mapper = mapper;  this._logger = logger; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadClothsDto>>> GetClothings()
        {
            try
            {
                var result = await _clothing.GetClothings();

               
                
                return Ok(_mapper.Map<IEnumerable<ReadClothsDto>>(result));


               
            }
            catch (Exception ex) {
                _logger.LogError(ex, "log error: cannot get cloths ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot return cloths"); }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ReadClothsDto>> GetClothById(int id)
        {
            try
            {
                var result = await _clothing.GetClothById(id);
                if (result != null)
                {
                    return _mapper.Map<ReadClothsDto>(result);
                }

                return NotFound($"cloth with id {id} not found");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "log error: cannot get cloths id  ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot return cloths by that Id"); }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReadClothsDto>> CreateCloth(CreateClothDto clothing)
        {
            try
            {
                
                if (clothing==null)
                {
                    return BadRequest("cloth entry must be complete before submitting");
                }


           

                var clothmodel = _mapper.Map<Clothing>(clothing);
               await _clothing.CreateClothes(clothmodel);
                

                var readDto = _mapper.Map<ReadClothsDto>(clothmodel);
                return CreatedAtAction(nameof(GetClothById), new { id = readDto.Id }, readDto);

            }
            catch (Exception ex) {
                _logger.LogError(ex, " log error: cannot create cloths ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot create cloths"); }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateClothes(UpDateClothDto clothing,int id)
        {
            try
            {
               
               
                var result = await _clothing.GetClothById(id);
                if(result  == null)
                {

                    return NotFound();
                }
                _mapper.Map(clothing, result);
               await _clothing.UpDateClothes(result);
               
                return Ok("update success");
                
            }
            catch (Exception ex) {

                _logger.LogError(ex, "log error: cannot update cloths ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot update clothes");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PatchClothes( JsonPatchDocument< UpDateClothDto>  clothing, int id)
        {
            try
            {
                var result = await _clothing.GetClothById(id);
                if(result == null) { return NotFound(); }

                var topatch = _mapper.Map<UpDateClothDto>(result);
                
                clothing.ApplyTo(topatch);
                if (!TryValidateModel(topatch))
                {

                    return ValidationProblem(ModelState);

                }
                _mapper.Map(topatch, result);
                await _clothing.UpDateClothes(result);
                return Ok("patch successful");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "log error: cannot patch clothes ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot patch clothes");
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteClothsById(int id) {
            try
            {
                var result = await _clothing.GetClothById(id);
                if (result != null)
                {
                    await _clothing.DeleteClothes(id);
                    return Ok("Delete successful");

                }
              
                return NotFound("id not found");
            }
            catch (Exception ex) {
                _logger.LogError( ex, "log error: cannot delete cloth ");
                return StatusCode(StatusCodes.Status500InternalServerError, "cannot delete cloth");
            }
        }
    }
}
