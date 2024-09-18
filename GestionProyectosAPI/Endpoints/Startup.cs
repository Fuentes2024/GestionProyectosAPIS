namespace GestionProyectosAPI.Endpoints
{
    public static class Startup
    {
        public static void UseEndpoints(this WebApplication app)
        {
            TareaEndpoints.Add(app);
            ProyectoEndpoints.Add(app);
            UsuarioEnpoints.Add(app);
            RolEndpoints.Add(app);
            MiembroEndpoints.Add(app);
        }
    }
}
