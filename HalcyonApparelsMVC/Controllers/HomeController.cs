using HalcyonApparelsMVC.DTO;
using HalcyonApparelsMVC.Interfaces;
using HalcyonApparelsMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;

namespace HalcyonApparelsMVC.Controllers
{
    public class HomeController : Controller
    {


        private readonly ISalesforceData _salesforcedata;
        public HomeController(ISalesforceData salesforcedata)
        {
            _salesforcedata = salesforcedata;
        }
        public async Task<IActionResult> AccessoryView()
    {
        HttpClientHandler clienthandler = new HttpClientHandler();
        clienthandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslpolicyerrors) => { return true; };

        HttpClient client = new HttpClient(clienthandler);
        client.BaseAddress = new Uri("https://localhost:7200");
        List<AccessoryDetailsMVC>? acclist = new List<AccessoryDetailsMVC>();

        HttpResponseMessage res = client.GetAsync("api/Accessory/Get").Result;
        if (res.IsSuccessStatusCode)
        {
            var result = res.Content.ReadAsStringAsync().Result;
            acclist = JsonConvert.DeserializeObject<List<AccessoryDetailsMVC>>(result);
        }
        return View(acclist);

    }

    [HttpGet]
    public ActionResult Post()
    { 
        return View();
    }

    //[HttpPost]
    public IActionResult Post(AccessoryDetailsMVC accsrydet)
    {
        accsrydet.ImageUrl = "file";
        HttpClientHandler clienthandler = new HttpClientHandler();
        clienthandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslpolicyerrors) => { return true; };
        string uniqueFileName =ProcessUploadedFile(accsrydet);
        
            AccessoryDTO adto = new AccessoryDTO();
            {
                adto.AccessoryId = accsrydet.AccessoryId;
                adto.AccessoryName = accsrydet.AccessoryName;
                adto.AccessoryType = accsrydet.AccessoryType;
                adto.AccessoryBrand = accsrydet.AccessoryBrand;
                adto.AccessoryPrice = accsrydet.AccessoryPrice;
                adto.AccessoryDiscount = accsrydet.AccessoryDiscount;
                adto.ImageUrl = uniqueFileName;
                

            }

        HttpClient client = new HttpClient(clienthandler);
        client.BaseAddress = new Uri("https://localhost:7200");
        var postTask = client.PostAsJsonAsync<AccessoryDTO>("api/Accessory/Post/", adto);
        postTask.Wait();
        var Result = postTask.Result;
        if (Result.IsSuccessStatusCode)
        {
                TempData["AlertMessage"] = " Accessory Added ";
                return RedirectToAction("AccessoryView");
        }
        return View();
    }



    public async Task<IActionResult> Delete(int id)
    {
            
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7200");
        await client.DeleteAsync($"api/Accessory/Delete/{id}");
        
        TempData["AlertMessage"] = " Accessory Deleted ";
        return RedirectToAction("AccessoryView");

        }

    [HttpPost]
    public async Task<IActionResult> Edit(TempData temp)
    {
        temp.AccessoryId = Convert.ToInt32(TempData["id"]);
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7200");
        var postTask = client.PostAsJsonAsync<TempData>("api/Accessory/Edit", temp);
        postTask.Wait();
        var Result = postTask.Result;
        if (Result.IsSuccessStatusCode)
        {
                TempData["AlertMessage"] = " Accessory Updated";
                return RedirectToAction("AccessoryView");
        }
        return View();

        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7200");

            TempData accssry = new TempData();
            HttpResponseMessage res = await client.GetAsync($"api/Accessory/Get/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                accssry = JsonConvert.DeserializeObject<TempData>(result);
            }


            return View(accssry);
        }

        public async Task<IActionResult> Details(int id)
        {
            
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7200");
                AccessoryDetailsMVC accss = new AccessoryDetailsMVC();

                HttpResponseMessage res = await client.GetAsync($"api/Accessory/Get/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    accss = JsonConvert.DeserializeObject<AccessoryDetailsMVC>(result);
                }
                return View(accss);
          


        }

        private string ProcessUploadedFile(AccessoryDetailsMVC model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


        public List<CustomerDetailsMVC> CustomerAdd()
        {
            var access_token = HttpContext.Session.GetString("Acces_token").ToString();

            var response = _salesforcedata.SalesforceCustomerDetails(access_token);
            var isTrue=_salesforcedata.Post(response);
            return response;
        }


    }


}