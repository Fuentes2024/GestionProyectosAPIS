using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class MiembroEndpointsTests
    {
        private static HttpClient _httpClient;
        private static WebApplicationFactory<Program> _factory;
        private static string _token;

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

        [TestMethod]
        public void AgregarTokenAlaCadena()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        [TestMethod]
        public async Task ObtenerMiembros_ConTokenValido_RetornarListaDeMiembros()
        {
            //Arrange: Pasar authorization a la cabecera
            AgregarTokenAlaCadena();
            //Act: Realizar solicitud para obtener los miembros
            var miembros = await _httpClient.GetFromJsonAsync<List<MiembroEquipoResponse>>("api/miembros");
            //Assert: Verificar que la lista de miembros no sea nula y que tenga elementos
            Assert.IsNotNull(miembros, "la lista de miembros no deberia ser nula.");
            Assert.IsTrue(miembros.Count > 0, "la lista de miembros deberia contener al menos un elemento. ");

        }
        [TestMethod]
        public async Task ObtenerMiembrosPorID_MiembroExistente_RetornaMiembro()
        {
            //Arrange: Pasar authorization a la cabecera y estables ID de miembros existente
            AgregarTokenAlaCadena();
            var miembroId = 1;
            //Act: Realizar solicitud para obtener las miembro por Id
            var miembro = await _httpClient.GetFromJsonAsync<MiembroEquipoResponse>($"api/miembros/{miembroId}");
            //Assert: Verificar que la miembro no sa nula y que tenga el ID correcto 
            Assert.IsNotNull(miembro, "La miembro no tiene que ser nulo");
            Assert.AreEqual(miembroId, miembro.MiembroEquipoId, "El ID del miembro no coincide");
        }
        [TestMethod]
        public async Task GuardarMiembro_ConDatosValidos_RetonarCreated()
        {
            //Arrange: Pasar authorization a la cabecera y prepara la Miembro 
            AgregarTokenAlaCadena();
            var newMiembro = new MiembroEquipoRequest { Cargo = "Usuario", UsuarioId = 1 };
            //Act: Realizar solicitud para aguardar miembro
            var reponse = await _httpClient.PostAsJsonAsync("api/miembros", newMiembro);
            //Assert: Verificar el codigo de estado created
            Assert.AreEqual(HttpStatusCode.Created, reponse.StatusCode, "El miembro no se creo correctamente");
        }
        [TestMethod]
        public async Task ModificarMiemboro_MiembroExistente_RetornarOk()
        {
            //Arrange: Pasar authorization a la cabecera y prepara el miembro 
            AgregarTokenAlaCadena();
            var Existentmiembro = new MiembroEquipoRequest {Cargo = "misioneor", UsuarioId = 1 };
            var miembroId = 6;
            //Act: Realizar solicitud para modificar el miembro existente
            var reponse = await _httpClient.PutAsJsonAsync($"api/miembros/{miembroId}", Existentmiembro);
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.OK, reponse.StatusCode, "el miembro no se modifico");
        }
        [TestMethod]
        public async Task EliminarMiembro_MiembroExistente_RetornaNOContent()
        {
            //Arrange: Pasar authorization a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var miembroId = 6;
            //Act: Realizar solicitud para eliminar el miembro existente
            var reponse = await _httpClient.DeleteAsync($"api/miembros/{miembroId}");
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.NoContent, reponse.StatusCode, "el miembro no se elimino correctamente");
        }

        [TestMethod]
        public async Task EliminarMiembro_MiembroExistente_RetornaNotFond()
        {
            //Arrange: Pasar authorization a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var miembroId = 3;
            //Act: Realizar solicitud para eliminar un miembro existente
            var reponse = await _httpClient.DeleteAsync($"api/miembros/{miembroId}");
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.NotFound, reponse.StatusCode, "Se esperaba un 404 notfound al intentar eliminar a un miembro inexistente ");
        }

    }
}
