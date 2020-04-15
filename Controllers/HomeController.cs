using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            string Mood = HttpContext.Session.GetString("Mood");
            string Message = HttpContext.Session.GetString("Message");

            if (Happiness == null)
            {
                Fullness = 20;
                Happiness = 20;
                Energy = 50;
                Meals = 3;
                Mood = "/img/neutral.jpeg";
                Message = "Your Bebopdachi is feeling extremely neutral.";
            }

            if (Happiness <= 0 || Fullness <=0)
            {
                Mood = "/img/lose.jpg";
                Message = "Your Bebopdachi has died!";
            }

            if (Happiness >= 100 && Fullness >= 100 && Energy >= 100)
            {
                Mood = "/img/win.jpg";
                Message = "Congratulations! You won!";
            }

            HttpContext.Session.SetInt32("Happiness", (int)Happiness);
            HttpContext.Session.SetInt32("Fullness", (int)Fullness);
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            HttpContext.Session.SetString("Mood", Mood);
            HttpContext.Session.SetString("Message", Message);
            Pet petnow = new Pet((int)Fullness, (int)Happiness, (int)Energy, (int)Meals, Mood, Message);
            return View(petnow);
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            string Message = HttpContext.Session.GetString("Message");
            string Mood = HttpContext.Session.GetString("Mood");
            
            if (Meals > 0)
            {
                Meals--;
                Random d20 = new Random();
                int roll = d20.Next(1, 21);
                if (roll > 5)
                {
                    Random rand = new Random();
                    int gain = rand.Next(5,11);
                    Fullness += gain;
                    Message = $"Yum! Fullness increased by {gain}";
                    int image = rand.Next(4);
                    if (image == 0)
                    {
                        Mood = "/img/eating_success.jpg";
                    }
                    else if (image == 1)
                    {
                        Mood = "/img/eating_success2.jpg";
                    }
                    else if (image == 2)
                    {
                        Mood = "/img/eating_success3.jpg";
                    }
                    else
                    {
                        Mood = "/img/eating_success4.gif";
                    }
                }
                else
                {
                    Message = "Blech!";
                    Mood = "/img/eating_fail.jpg";
                }
            }
            else
            {
                Message = "What she said";
                Mood = "/img/no_meals.jpg";
            }

            HttpContext.Session.SetInt32("Fullness", (int)Fullness);
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            HttpContext.Session.SetString("Message", Message);
            HttpContext.Session.SetString("Mood", Mood);
            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            string Message = HttpContext.Session.GetString("Message");
            string Mood = HttpContext.Session.GetString("Mood");
            
            if (Energy >= 5)
            {
                Energy -= 5;
                Random d20 = new Random();
                int roll = d20.Next(1, 21);
                if (roll > 5)
                {
                    Random rand = new Random();
                    int gain = rand.Next(5,11);
                    Happiness += gain;
                    Message = $"Fun! Happiness increased by {gain}";
                    Mood = "/img/fun.jpg";
                }
                else
                {
                    Message = "No fun!";
                    Mood = "/img/no_fun.jpg";
                }
            }
            else
            {
                Message = "So... sleepy.";
                Mood = "/img/sleepy.gif";
            }

            HttpContext.Session.SetInt32("Energy", (int)Energy);
            HttpContext.Session.SetInt32("Happiness", (int)Happiness);
            HttpContext.Session.SetString("Message", Message);
            HttpContext.Session.SetString("Mood", Mood);
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            int? Energy = HttpContext.Session.GetInt32("Energy");
            int? Meals = HttpContext.Session.GetInt32("Meals");
            string Message = HttpContext.Session.GetString("Message");
            string Mood = HttpContext.Session.GetString("Mood");
            
            if (Energy >= 5)
            {
                Energy -= 5;
                Random rand = new Random();
                int gain = rand.Next(1,4);
                Meals += gain;
                string mealString = (gain == 1) ? "meal" : "meals";
                Message = $"Hi ho, hi ho! Earned {gain} {mealString}!";
                Mood = "/img/working.webp";   
            }
            else
            {
                Message = "So... sleepy.";
                Mood = "/img/sleepy.gif";
            }

            HttpContext.Session.SetInt32("Energy", (int)Energy);
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            HttpContext.Session.SetString("Message", Message);
            HttpContext.Session.SetString("Mood", Mood);
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            int? Energy = HttpContext.Session.GetInt32("Energy");
            string Message = HttpContext.Session.GetString("Message");
            string Mood = HttpContext.Session.GetString("Mood");
            
            Energy += 15;
            Fullness -= 5;
            Happiness -= 5;
            Message = "I dreamt I went to Manderley again.";
            Mood = "/img/asleep.gif";

            HttpContext.Session.SetInt32("Fullness", (int)Fullness);
            HttpContext.Session.SetInt32("Happiness", (int)Happiness);
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            HttpContext.Session.SetString("Message", Message);
            HttpContext.Session.SetString("Mood", Mood);
            return RedirectToAction("Index");
        }

        [HttpGet("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
    }
}
