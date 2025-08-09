using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProyectoEscuela.Server.DTOs.Materia;
using ProyectoEscuela.Server.Interfaces.Services;

namespace ProyectoEscuela.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController(
        IMateriaService materiaService,
        IValidator<MateriaInsertDto> createValidator,
        IValidator<MateriaUpdateDto> updateValidator
        ) : ControllerBase
    {
        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> CreateMateria([FromBody] MateriaInsertDto materiaInsertDto, CancellationToken cancellationToken)
        {
            var resultValidation = await createValidator.ValidateAsync(materiaInsertDto, cancellationToken);

            if (!resultValidation.IsValid)
                return BadRequest(resultValidation.Errors);
            try
            {
                var materiaDto = await materiaService.InsertAsync(materiaInsertDto, cancellationToken);
                return Ok(materiaDto);
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
        public async Task<IActionResult> GetByIdMateria([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var materia = await materiaService.GetByIdAsync(id, cancellationToken);
            if (materia == null)
                return NotFound($"No existe materia con {id}");
            return Ok(materia);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> DeleteMateria([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var materia = await materiaService.DeleteAsync(id, cancellationToken);
            if (materia == null)
            {
                return NotFound("Recurso no encontrado");
            }
            return Ok(id);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> UpdateMateria([FromRoute] Guid id, [FromBody] MateriaUpdateDto materiaUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = await updateValidator.ValidateAsync(materiaUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                var materiaDto = await materiaService.UpdateAsync(id, materiaUpdateDto, cancellationToken);
                return Ok(materiaDto);
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
                var pagedResult = await materiaService.GetPageResult(pageNumber, pageSize, cancellationToken);
                return Ok(pagedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
