using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using GestionProyectosAPI.Models;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class InformeEndpointsTests
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
        [TestMethod]
        public void AgregarTokenAlaCadena()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
        [TestMethod]
        public async Task ObtenerInforme_ConTokenValido_RetornaListaDeInformes()
        {
            ///Arrange: Pasar autirización a la cadena
            AgregarTokenAlaCadena();
            ///Act: Realizar solicitud para obtener los informes
            var informe = await _httpClient.GetFromJsonAsync<List<InformeResponse>>("api/informes");
            //Assert: Verificar que la lista de informe no sea nula y que tenga elementos
            Assert.IsNotNull(informe, "la lista de informe no deberia ser nula.");
            Assert.IsTrue(informe.Count > 0, "la lista de informe deberia contener al menos un elemento. ");
        }
        [TestMethod]
        public async Task ObtenerInformePorId_InformeExistente_RetorneInforme()
        {
            ///Arrange: Pasar autirización a la cadena y establecer ID de informe existente 
            AgregarTokenAlaCadena();
            var informeId = 14;
            //Act: Realizar solicitud para obtener informe por ID
            var informes = await _httpClient.GetFromJsonAsync<InformeResponse>($"api/informes/{informeId}");
            //Assert: Verificar que la lista de informe no sea nula y que tenga Id correcto
            Assert.IsNotNull(informes, "El informe no deberia ser nulo.");
            Assert.AreEqual(informeId,informes.InformeId, "El ID del informe devuelto no coincide. ");
        }
        [TestMethod]
        public async Task GuardarInforme_ConDatosValidos_RestornarCreated()
        {
            ///Arrange: Pasar autirización a la cadena y preparar el nuevo informe 
            AgregarTokenAlaCadena();
            var newInforme = new InformeRequest { Nombre = "interfas", Descripcion = "visualisacion", FechaInicio = new DateOnly(2024, 11, 30), FechaFin = new DateOnly(2024, 12, 30), EstadoId = 1, MiembroEquipoId = 1, UsuarioId = 1, ProyectoId = 7 };
            //Act: Realizar solicitud para guardar el nuevo informe con nombre duplicado
            var reponse = await _httpClient.PostAsJsonAsync("api/informes", newInforme);
            //Assert: vierifica el codigo del estaado ccreated
            Assert.AreEqual(HttpStatusCode.Created,reponse.StatusCode, "El informe no se creo corretamente");
        }

        [TestMethod]
        public async Task ModificarInforme_InformeExistente_RetornarOk()
        {
            ///Arrange: Pasar autirización a la cadena y preparar el informe modificado, pasando Id
            AgregarTokenAlaCadena();
            var InformeExistente = new InformeRequest { Nombre = "iiii", Descripcion = "fff", FechaInicio = new DateOnly(2024, 10, 30), FechaFin = new DateOnly(2024, 12, 30), EstadoId = 1, MiembroEquipoId = 1, UsuarioId = 1, ProyectoId = 7 };
            //Act: Realizar solicitud para modificar informe existente
            var informeId = 16;
            var reponse = await _httpClient.PutAsJsonAsync($"api/informes/{informeId}", InformeExistente);
            //Assert: Verificar que la respuesta sea ok
            Assert.AreEqual(HttpStatusCode.OK, reponse.StatusCode, "El informe no se modifico");
        }
        [TestMethod]
        public async Task EliminarInforme_InformeExistente_RetornaNOContent()
        {
            //Arrange: Pasar authorization a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var informeId = 16;
            //Act: Realizar solicitud para eliminar tarea existente
            var response = await _httpClient.DeleteAsync($"api/informes/{informeId}");
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "El informe no se elimino correctamente");
        }


    }
}