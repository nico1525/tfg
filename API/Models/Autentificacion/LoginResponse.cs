namespace API.Models.Autentificacion
{
    public class LoginUserResponse
    {
        public string? NombreApellidos { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }
    }
}
