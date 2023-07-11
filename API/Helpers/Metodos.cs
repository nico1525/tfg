namespace API.Helpers
{
    public static class Metodos
    {
        public static string ObtenerNombreMes(int numeroMes)
        {
            string[] nombresMeses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            if (numeroMes >= 1 && numeroMes <= 12)
            {
                return nombresMeses[numeroMes - 1];
            }
            else { return "mes inválido"; }
        }
    }
}
