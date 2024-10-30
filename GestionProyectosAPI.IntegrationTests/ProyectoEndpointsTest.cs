using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting; // Asegúrate de tener este using para [TestClass]
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class ProyectoEndpointTests
    {
        private static HttpClient _httpClient;
        private static WebApplicationFactory<Program> _factory;
        private static string _token;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            // Crear instancia de la aplicación en memoria
            _factory = new WebApplicationFactory<Program>();
            // Crear el cliente HTTP
            _httpClient = _factory.CreateClient();

            // Arrange: Preparar la carga útil para el inicio de sesión
            var loginRequest = new UsuarioRequest { Correo = "chepe@gmail.com", Clave = "12345" };
            // Act: Enviar la solicitud de inicio de sesión
            var loginResponse = await _httpClient.PostAsJsonAsync("api/usuarios/Login", loginRequest);
            // Assert: Verificar que el inicio de sesión sea exitoso
            loginResponse.EnsureSuccessStatusCode();
            _token = (await loginResponse.Content.ReadAsStringAsync()).Trim('"');
        }

        /// <summary>
        /// Agregar token de autorización a la cadena de cliente HTTP
        /// </summary>
        private void AgregarTokenAlaCadena()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [TestMethod]
        public async Task ObtenerProyecto_ConTokenValido_RetornaListaDeProyectos()
        {
            // Arrange: Pasar autorización a la cadena
            AgregarTokenAlaCadena();
            // Act: Realizar solicitud para obtener los proyectos
            var proyectos = await _httpClient.GetFromJsonAsync<List<ProyectoResponse>>("api/proyectos");
            // Assert: Verificar que la lista de proyectos no sea nula y que tenga elementos
            Assert.IsNotNull(proyectos, "La lista de proyectos no debería ser nula.");
            Assert.IsTrue(proyectos.Count > 0, "La lista de proyectos debería contener al menos un elemento.");
        }

        [TestMethod]
        public async Task ObtenerProyectoPorID_ProyectoExistente_RetornaProyecto() 
        {
            // Arrange: Pasar autorización a la cadena y establece ID de usuario existente
            AgregarTokenAlaCadena();
            var proyectoId = 7;
            // Act: Realizar solicitud para obtener los proyectos por ID
            var proyecto = await _httpClient.GetFromJsonAsync<ProyectoResponse>($"api/proyectos/{proyectoId}");
            // Assert: Verificar que el proyecto no sea nulo y que tenga el ID correcto
            Assert.IsNotNull(proyecto, "El id no debe ser nulo");
            Assert.AreEqual(proyectoId, proyecto.ProyectoId, "El ID del proyecto devuelto no coincide");
        }

        [TestMethod]
        //Guarda un usuario y reporna un create 
        public async Task GuardarProyecto() {
            // Arrange: Pasar autorización a la cadena y prepara el proyecto
            AgregarTokenAlaCadena();
            // Crear fechas utilizando DateOnly
            var newProyecto = new ProyectoRequest
            {
                Nombre = "ESIET2024",
                Descripcion = "Sin descripcion",
                FechaInicio = new DateOnly(2024, 7, 7), 
                FechaFin = new DateOnly(2024, 8, 8),
                Presupuesto = 9,
                UsuarioId = 1
            };
            // Act: Realizar solicitud para guardar el proyecto
            var response = await _httpClient.PostAsJsonAsync("api/proyectos/", newProyecto);
            // Assert: Verificar el código de estado 
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "El proyecto no se creó correctamente");
        }

        [TestMethod] // Este atributo estaba faltando
        public async Task ModificarProyecto()
        {
            // Arrange: Pasar autorización a la cabecera y prepara el proyecto existente
            AgregarTokenAlaCadena();
            var proyectoExistente = new ProyectoRequest { Nombre = "HOLAAAAAAAAA", UsuarioId = 1 };
            var proyectoId = 13;
            // Act: Realizar solicitud para modificar el proyecto existente
            var response = await _httpClient.PutAsJsonAsync($"api/proyectos/{proyectoId}", proyectoExistente);
            // Assert: Verificar que el código sea OK
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "El proyecto no se modificó");
        }

        [TestMethod]
        public async Task EliminarProyecto()
        {
            // Arrange: Pasar autorización a la cabecera, pasando un Id 
            AgregarTokenAlaCadena();
            var proyectoId = 12;

            // Act: Realizar solicitud para eliminar el proyecto existente
            var response = await _httpClient.DeleteAsync($"api/proyectos/{proyectoId}");

            // Assert: Verificar que el código sea NoContent o OK
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK,
                $"El proyecto no se eliminó correctamente. Se esperaba 204 NoContent o 200 OK, pero se recibió {response.StatusCode}");
        }

        [TestMethod]
        // Proyecto inexistente retorna NotFound
        public async Task EliminarProyectoInexistente() 
        {
            // Arrange: Pasar autorización a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var proyectoId = 3;
            // Act: Realizar solicitud para eliminar un proyecto inexistente
            var response = await _httpClient.DeleteAsync($"api/proyectos/{proyectoId}"); // Corregido "miembros" a "proyectos"
            // Assert: Verificar que el código sea NotFound
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Se esperaba un 404 NotFound al intentar eliminar un proyecto inexistente");
        }
    }
}