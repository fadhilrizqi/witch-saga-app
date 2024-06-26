using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WitchSaga.Models;

namespace WitchSaga.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CalculateKills(int ageOfDeathA, int yearOfDeathA, int ageOfDeathB, int yearOfDeathB)
        {
            int birthYearA = yearOfDeathA - ageOfDeathA;
            int birthYearB = yearOfDeathB - ageOfDeathB;

            if (birthYearA < 0 || birthYearB < 0)
                return Json(-1);

            int killsOnBirthYearA = CalculateKillsOnBirthYear(birthYearA);
            int killsOnBirthYearB = CalculateKillsOnBirthYear(birthYearB);

            double averageKills = (killsOnBirthYearA + killsOnBirthYearB) / 2.0;

            return Json(averageKills);
        }

        private int CalculateKillsOnBirthYear(int birthYear)
        {
            int totalKills = 0;
            int prevKill = 1;
            for (int i = 1; i <= birthYear; i++)
            {
                totalKills += prevKill;
                prevKill = i;
            }
            return totalKills;
        }
    }
}
