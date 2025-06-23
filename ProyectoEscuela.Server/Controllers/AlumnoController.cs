
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProyectoEscuela.Server.DTOs.Alumno;
using ProyectoEscuela.Server.Interfaces.Services;
using System.Net.WebSockets;

namespace ProyectoEscuela.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController(
        IAlumnoService alumnoService,
        IValidator<AlumnoInsertDto> createValidator,
        IValidator<AlumnoUpdateDto> updateValidator
        ) : ControllerBase
    {
        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> CreateAlumno([FromBody] AlumnoInsertDto alumnoInsertDto, CancellationToken cancellationToken)
        {
            var resultValidation = await createValidator.ValidateAsync(alumnoInsertDto, cancellationToken);

            if (!resultValidation.IsValid)
                return BadRequest(resultValidation.Errors);
            try
            {
                var alumnoDto = await alumnoService.InsertAsync(alumnoInsertDto, cancellationToken);
                return Ok(alumnoDto);
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
        public async Task<IActionResult> GetByIdAlumno([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var alumno = await alumnoService.GetByIdAsync(id, cancellationToken);
            if (alumno == null)
                return NotFound($"No existe alumno con {id}");
            return Ok(alumno);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> DeleteAlumno([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var alumno = await alumnoService.DeleteAsync(id, cancellationToken);
            if (alumno == null)
            {
                return NotFound("Recurso no encontrado");
            }
            return Ok(id);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> UpdateAlumno([FromBody] Guid id, [FromForm] AlumnoUpdateDto alumnoUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = await updateValidator.ValidateAsync(alumnoUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);
            try
            {
                var alumnoDto = await alumnoService.UpdateAsync(id, alumnoUpdateDto, cancellationToken);
                return Ok(alumnoDto);
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
                var pagedResult = await alumnoService.GetPageResult(pageNumber, pageSize, cancellationToken);
                return Ok(pagedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
