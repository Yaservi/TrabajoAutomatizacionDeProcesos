namespace ProyectoEscuela.Server.DTOs.Alumno
{
    public sealed record AlumnoUpdateDto(
        string Nombre,
        string Apellido,
        string Direccion,
        DateTime FechaNacimiento,
        string Telefono,
        string Email);
    
}
