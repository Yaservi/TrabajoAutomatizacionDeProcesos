using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProyectoEscuela.Server.DTOs.Calificacion;
using ProyectoEscuela.Server.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoEscuela.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CalificacionController : ControllerBase
    {
        private readonly ICalificacionService _calificacionService;
        private readonly IValidator<CalificacionInsertDto> _createValidator;
        private readonly IValidator<CalificacionUpdateDto> _updateValidator;

        public CalificacionController(
            ICalificacionService calificacionService,
            IValidator<CalificacionInsertDto> createValidator,
            IValidator<CalificacionUpdateDto> updateValidator)
        {
            _calificacionService = calificacionService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> CreateCalificacion([FromBody] CalificacionInsertDto calificacionInsertDto, CancellationToken cancellationToken)
        {
            var resultValidation = await _createValidator.ValidateAsync(calificacionInsertDto, cancellationToken);

            if (!resultValidation.IsValid)
                return BadRequest(resultValidation.Errors);

            try
            {
                var calificacionDto = await _calificacionService.InsertAsync(calificacionInsertDto, cancellationToken);
                return Ok(calificacionDto);
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
        public async Task<IActionResult> GetByIdCalificacion([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var calificacion = await _calificacionService.GetByIdAsync(id, cancellationToken);
            if (calificacion == null)
                return NotFound($"No existe calificación con id {id}");
            return Ok(calificacion);
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetAllCalificaciones(CancellationToken cancellationToken)
        {
            try
            {
                var calificaciones = await _calificacionService.GetAllAsync(cancellationToken);
                return Ok(calificaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpDelete("{id}")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> DeleteCalificacion([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var calificacion = await _calificacionService.DeleteAsync(id, cancellationToken);
            if (calificacion == Guid.Empty)
                return NotFound("Recurso no encontrado");
            return Ok(id);
        }



        [HttpPut("actualizar")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> ActualizarCalificacion([FromBody] CalificacionUpdateDto calificacionUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = await _updateValidator.ValidateAsync(calificacionUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var calificacionDto = await _calificacionService.UpdateAsync(calificacionUpdateDto.Id, calificacionUpdateDto, cancellationToken);
                return Ok(calificacionDto);
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
                var pagedResult = await _calificacionService.GetPageResult(pageNumber, pageSize, cancellationToken);
                return Ok(pagedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
