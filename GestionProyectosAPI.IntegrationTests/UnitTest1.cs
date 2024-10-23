using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //Crear instancia en la aplicacion en memoria
            using var application = new WebApplicationFactory<Program>();

            //Crear el cliente Http para enviar solicitudes
            using var _httpClient = application.CreateClient();


            var userSession = new UsuarioRequest { Correo = "chepe@gmail.com", Clave = "12345"};
            var Response = await _httpClient.PostAsJsonAsync("api/usuarios/Login", userSession);
            if (Response.IsSuccessStatusCode)
            {
                // Deserializar la respuesta JSON a un objeto LoginResponse
                var result = await Response.Content.ReadFromJsonAsync<string>(); 
            }
        }
    }
}