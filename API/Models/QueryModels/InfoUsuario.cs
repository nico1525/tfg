using API.Helpers;

namespace API.Models.Query_Models
{
    public class InfoUsuario
    {
        public int Id { get; set; }
        public string NombreApellidos { get; set; }
        public string Email { get; set;}
        public string Contraseña { get; set;}
        public string NombreOrg { get; set; }
        public string Dirección { get; set;}
        public Role Rol { get; set; }

    }
}
