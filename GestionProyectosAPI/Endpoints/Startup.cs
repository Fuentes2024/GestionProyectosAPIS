namespace GestionProyectosAPI.Endpoints
{
    public static class Startup
    {
        public static void UseEndpoints(this WebApplication app)
        {
            TareaEndpoints.Add(app);
        }
    }
}
