namespace API.Models.Autentificacion
{
    public class LoginResponse
    {
        public string? NombreOrg { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string Token { get; set; }
    }
}
