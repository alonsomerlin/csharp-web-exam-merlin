using csharp_web_exam.Models;
using csharp_web_exam_api.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace csharp_web_exam.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly HttpClient _httpClient;
        //private readonly AppDbContext _context;
        public AlumnoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5004/api");
        }
        

        public async Task<IActionResult> Index()
        {
            var varResponse = await _httpClient.GetAsync("api/Alumno/listaAlumno");
            if (varResponse.IsSuccessStatusCode)
            {
                var varContent = await varResponse.Content.ReadAsStringAsync();
                var varAlumno = JsonConvert.DeserializeObject<IEnumerable<AlumnoViewModel>>(varContent);
                return View("Index", varAlumno);
            }
            else
            {
                return View(new List<AlumnoViewModel>());
            }
        }
        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Alumno/mostrar/{id}");


            if (response.IsSuccessStatusCode)
            {
                var varContent = await response.Content.ReadAsStringAsync();
                
                var varAlumno = JsonConvert.DeserializeObject<AlumnoViewModel>(varContent);

                return View(varAlumno);
            }
            else
            {
                return RedirectToAction("Details"); 
            }
        }

        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync($"/api/Genero/ListaGenero");
            if (response.IsSuccessStatusCode)
            {
                var varContent = await response.Content.ReadAsStringAsync();

                var varGenero = JsonConvert.DeserializeObject<IEnumerable<GeneroViewModel>>(varContent);
                if (varGenero != null)
                {
                    ViewData["GENERO_ID"] = new SelectList(varGenero, "GENERO_ID", "GENERO_DESC");
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(AlumnoCreateDto alumno)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(alumno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"/api/Alumno/inserta", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear al alumno.");
                }
            }
            
            return View(alumno);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Alumno/borrar/{id}");

            if (response.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el alumno.";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Alumno/mostrar/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var alumno = JsonConvert.DeserializeObject<AlumnoViewModel>(content);

                var response2 = await _httpClient.GetAsync($"/api/Genero/ListaGenero");
                if (response2.IsSuccessStatusCode)
                {
                    var varContent = await response2.Content.ReadAsStringAsync();

                    var varGenero = JsonConvert.DeserializeObject<IEnumerable<GeneroViewModel>>(varContent);
                    if (varGenero != null)
                    {
                        ViewData["GENERO_ID"] = new SelectList(varGenero, "GENERO_ID", "GENERO_DESC");
                    }
                }

                return View(alumno);
            }
            else
                return RedirectToAction("Details");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idAlumno, AlumnoUpdateDto alumno)
        {
            if (ModelState.IsValid)
            {
                idAlumno = alumno.ALUMNO_ID;

                var json = JsonConvert.SerializeObject(alumno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Alumno/actualizar/{idAlumno}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { idAlumno });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el alumno.");
                }
            }

            return View(alumno);
        }
    }
}
