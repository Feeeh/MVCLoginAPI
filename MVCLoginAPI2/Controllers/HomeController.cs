using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MVCLoginAPI2.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System;

namespace MVCLoginAPI2.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyOptions _appSettings;
        private readonly HttpClient client = new HttpClient();

        public HomeController(IOptions<MyOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Index()
        {
            List<Usuario> usuarios = new List<Usuario>();

            HttpResponseMessage responseUsuario = await client.GetAsync($"{_appSettings.BDUrl}Api/GetUsuario");

            if (responseUsuario.IsSuccessStatusCode)
            {
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(await responseUsuario.Content.ReadAsStringAsync());
            }

            return View(usuarios);
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> Cadastro()
        {
            Usuario usuario = new Usuario();

            return View(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Cadastro(Usuario usuario)
        {
            usuario.DataDeCriacao = DateTime.Now;

            var usuarioPayload = JsonConvert.SerializeObject(usuario);
            var httpContent = new StringContent(usuarioPayload, Encoding.UTF8, "application/json");

            await client.PostAsync($"{_appSettings.BDUrl}Api/AddUsuario", httpContent);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> Atualizar(int id)
        {
            Usuario usuario = null;

            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = await client.GetAsync($"{_appSettings.BDUrl}Api/GetUsuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                usuario = JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Atualizar(Usuario usuario)
        {
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = await client.GetAsync($"{_appSettings.BDUrl}Api/GetUsuario/{usuario.Id}");

            if (response.IsSuccessStatusCode)
            {
                var usuarioPayload = JsonConvert.SerializeObject(usuario);

                var httpContent = new StringContent(usuarioPayload, Encoding.UTF8, "application/json");

                await client.PutAsync($"{_appSettings.BDUrl}Api/UpdateUsuario", httpContent);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Remover(int id)
        {
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = await client.GetAsync($"{_appSettings.BDUrl}Api/GetUsuario/{id}");

            if (response.IsSuccessStatusCode)
            {
                await client.DeleteAsync($"{_appSettings.BDUrl}Api/DeleteUsuario/" + id);
            }

            return RedirectToAction("Index");
        }
    }
}
