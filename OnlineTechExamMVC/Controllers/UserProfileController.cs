using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineTechExamMVC.Models;
using System.Text;

namespace OnlineTechExamMVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserProfileController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7119/api/crud/")
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("");
            var users = JsonConvert.DeserializeObject<List<UserProfile>>(response);
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserProfile user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("create", content);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetStringAsync($"get/{id}");
            var user = JsonConvert.DeserializeObject<UserProfile>(response);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserProfile user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("update", content);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"delete/{id}");
            return RedirectToAction("Index");
        }
    }
}
