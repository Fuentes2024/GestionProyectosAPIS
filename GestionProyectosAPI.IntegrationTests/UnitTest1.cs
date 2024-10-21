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
            using var aplication = new WebApplicationFactory<Program>();

            //Crear el cliente Http para enviar solicitudes
            using var _httpClient = aplication.CreateClient();


            var userSession = new UsuarioRequest { Correo = "chepe@gmail.com", Clave = "12345"};
            var response = await _httpClient.PostAsJsonAsync("api/usuarios/Login", userSession);
            if (response.IsSuccessStatusCode)
            {
                // Deserializar la respuesta JSON a un objeto LoginResponse
                var result = await response.Content.ReadFromJsonAsync<string>(); 
            }
        }
    }
}