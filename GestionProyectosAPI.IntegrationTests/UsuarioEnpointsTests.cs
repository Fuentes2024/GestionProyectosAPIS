using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class UsuarioEnpointsTests
    {

        private static HttpClient _httpClient;
        private static WebApplicationFactory<Program> _factory;
        private static string _token;

        ///Summary
        ///Configuracion del entorno de prueba inializando la api y obteniendo el token jwt
        ///Summary
        ///
        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            //Crear instancia en la aplicacion en memoria
            _factory = new WebApplicationFactory<Program>();
            //Crear el cliente Http
            _httpClient = _factory.CreateClient();

            //Arrange: Prepara la carga util para el inicio de sesion
            var loginRequest = new UsuarioRequest { Correo = "chepe@gmail.com", Clave = "12345" };
            //Act: Enviar la salicitud de inicio de sesion
            var loginResponse = await _httpClient.PostAsJsonAsync("api/usuarios/Login", loginRequest);
            // Assert: Verificar que el inicio de sesion sea exitoso
            loginResponse.EnsureSuccessStatusCode();
            _token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"');
        }
        /// <summary>
        /// Agregar token de autorizacion ala cadena de cliente HTTP
        /// </summary>
        [TestMethod]
        public void AgregarTokenAlaCadena() {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
        [TestMethod]
        public async Task ObtenerUsuario_ConTokenValido_RetornaListaDeUsuarios()
        {
            ///Arrange: Pasar autirización a la cadena
             AgregarTokenAlaCadena();
            ///Act: Realizar solicitud para obtener los usuarios
            var usuarios = await _httpClient.GetFromJsonAsync<List<UsuarioResponse>>("api/usuarios");
            //Assert: Verificar que la lista de usuario no sea nula y que tenga elementos
           Assert.IsNotNull(usuarios,"la lista de usuarios no deberia ser nula.");
            Assert.IsTrue(usuarios.Count > 0, "la lista de usuarios deberia contener al menos un elemento. ");

        }
    }
}
