using GestionProyectosAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GestionProyectosAPI.IntegrationTests
{
    [TestClass]
    public class TareaEndpointsTests
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
        public async Task ObtenerTareas_ConTokenValido_RetornarListaDeTareas()
        { 
            //Arrange: Pasar authorization a la cabecera
            AgregarTokenAlaCadena();
            ///Act: Realizar solicitud para obtener las tareas
            var tareas = await _httpClient.GetFromJsonAsync<List<TareaResponse>>("api/tareas");
            //Assert: Verificar que la lista de tareas no sea nula y que tenga elementos
            Assert.IsNotNull(tareas, "la lista de tarea no deberia ser nula.");
            Assert.IsTrue(tareas.Count > 0, "la lista de tareas deberia contener al menos un elemento. ");

        }

        [TestMethod]
        public async Task ObtenerTareasPorID_TareaExistente_RetornaTarea()
        {
            //Arrange: Pasar authorization a la cabecera y estables ID de tarea existente
            AgregarTokenAlaCadena();
            var tareaId = 8;
            ///Act: Realizar solicitud para obtener las tareas por Id
            var tareas = await _httpClient.GetFromJsonAsync<TareaResponse>($"api/tareas/{tareaId}");
            //Assert: Verificar que la tarea no sa nula y que tenga el ID correcto 
            Assert.IsNotNull(tareas, "La tarea no tiene que ser nulo");
            Assert.AreEqual(tareaId, tareas.TareaId, "El ID del tarea no coincide");
        }
        [TestMethod]
        public async Task GuardarTareas_ConDatosValidos_RetonarCreated()
        {
            //Arrange: Pasar authorization a la cabecera y prepara la tarea 
            AgregarTokenAlaCadena();
            var newTarea = new TareaRequest {Nombre = "ElIMINAR", Descripcion = "HOLAAS", EstadoTarea = "EN PROCESO", FechaInicio = new DateOnly(2024, 10, 24), FechaFin = new DateOnly(2024, 10, 24), Prioridad = "INTERMEDIA", MiembroEquipoId = 1, ProyectoId = 7,};
            ///Act: Realizar solicitud para aguardar tarea
            var reponse = await _httpClient.PostAsJsonAsync("api/tareas", newTarea);
            //Assert: Verificar el codigo de estado created
            Assert.AreEqual(HttpStatusCode.Created, reponse.StatusCode, "la tarea no se creo correctamente");
        }
       
         [TestMethod]
        public async Task ModificarTarea_TareaExistente_RetornarOk()
        {
            //Arrange: Pasar authorization a la cabecera y prepara la tarea 
            AgregarTokenAlaCadena();
            var ExistentTarea = new TareaRequest { Nombre = "eeeeeeeeeeeeeeeee", Descripcion = "TTT", EstadoTarea = "EN PROCESO", FechaInicio = new DateOnly(2024, 10, 24), FechaFin = new DateOnly(2024, 10, 24), Prioridad = "BAJA", MiembroEquipoId = 1, ProyectoId = 7, };
            var tareaId = 12;
            //Act: Realizar solicitud para modificar tarea existente
            var reponse = await _httpClient.PutAsJsonAsync($"api/tareas/{tareaId}", ExistentTarea);
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.OK, reponse.StatusCode, "la tarea no se modifico");
        }
        [TestMethod]
        public async Task EliminarTarea_TareaExistente_RetornaNOContent()
        {
            //Arrange: Pasar authorization a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var tareaId = 12;
            //Act: Realizar solicitud para eliminar tarea existente
            var reponse = await _httpClient.DeleteAsync($"api/tareas/{tareaId}");
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.NoContent, reponse.StatusCode, "la tarea no se elimino correctamente");
        }

        [TestMethod]
        public async Task EliminarTarea_TareaExistente_RetornaNotFond()
        {
            //Arrange: Pasar authorization a la cabecera, pasando un Id
            AgregarTokenAlaCadena();
            var tareaId = 10;
            //Act: Realizar solicitud para eliminar tarea existente
            var reponse = await _httpClient.DeleteAsync($"api/tareas/{tareaId}");
            //Assert: Verificar el codigo sea ok
            Assert.AreEqual(HttpStatusCode.NotFound, reponse.StatusCode, "Se esperaba un 404 notfound al intentar eliminar a una tarea inexistente ");
        }
    }
}
