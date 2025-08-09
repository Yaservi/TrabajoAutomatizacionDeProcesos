using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProyectoEscuela.Server.DTOs.Maestro;
using ProyectoEscuela.Server.Interfaces.Services;

namespace ProyectoEscuela.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestroController(
         IMaestroService maestroService,
         IValidator<MaestroInsertDto> createValidator,
         IValidator<MaestroUpdateDto> updateValidator
        ) : ControllerBase
    {
        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> CreateMaestro([FromBody] MaestroInsertDto maestroInsertDto, CancellationToken cancellationToken)
        {
            var resultValidation = await createValidator.ValidateAsync(maestroInsertDto, cancellationToken);

            if (!resultValidation.IsValid)
                return BadRequest(resultValidation.Errors);
            try
            {
                var maestroDto = await maestroService.InsertAsync(maestroInsertDto, cancellationToken);
                return Ok(maestroDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetByIdMaestro([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var maestro = await maestroService.GetByIdAsync(id, cancellationToken);
            if (maestro == null)
                return NotFound($"No existe maestro con {id}");
            return Ok(maestro);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> DeleteMaestro([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var maestro = await maestroService.DeleteAsync(id, cancellationToken);
            if (maestro == null)
            {
                return NotFound("Recurso no encontrado");
            }
            return Ok(id);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> UpdateMaestro([FromRoute] Guid id, [FromBody] MaestroUpdateDto maestroUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = await updateValidator.ValidateAsync(maestroUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                var maestroDto = await maestroService.UpdateAsync(id, maestroUpdateDto, cancellationToken);
                return Ok(maestroDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("Pagination")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var pagedResult = await maestroService.GetPageResult(pageNumber, pageSize, cancellationToken);
                return Ok(pagedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
