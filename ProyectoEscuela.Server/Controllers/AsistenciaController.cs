using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProyectoEscuela.Server.DTOs.Asistencia;
using ProyectoEscuela.Server.Interfaces.Services;

namespace ProyectoEscuela.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController(
           IAsistenciaService asistenciaService,
            IValidator<AsistenciaInsertDto> createValidator,
            IValidator<AsistenciaUpdateDto> updateValidator
        ) : ControllerBase
    {

        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> CreateAlumno([FromBody] AsistenciaInsertDto asistenciaInsertDto, CancellationToken cancellationToken)
        {
            var resultValidation = await createValidator.ValidateAsync(asistenciaInsertDto, cancellationToken);

            if (!resultValidation.IsValid)
                return BadRequest(resultValidation.Errors);
            try
            {
                var asistenciaDto = await asistenciaService.InsertAsync(asistenciaInsertDto, cancellationToken);
                return Ok(asistenciaDto);
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
        public async Task<IActionResult> GetByIdAsistencia([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var asistencia = await asistenciaService.GetByIdAsync(id, cancellationToken);
            if (asistencia == null)
                return NotFound($"No existe asistencia con {id}");
            return Ok(asistencia);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> DeleteAsistencia([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var asistencia = await asistenciaService.DeleteAsync(id, cancellationToken);
            if (asistencia == null)
            {
                return NotFound("Recurso no encontrado");
            }
            return Ok(id);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> UpdateAsistencia([FromRoute] Guid id, [FromBody] AsistenciaUpdateDto asistenciaUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = await updateValidator.ValidateAsync(asistenciaUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                var asistenciaDto = await asistenciaService.UpdateAsync(id, asistenciaUpdateDto, cancellationToken);
                return Ok(asistenciaDto);
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
                var pagedResult = await asistenciaService.GetPageResult(pageNumber, pageSize, cancellationToken);
                return Ok(pagedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
