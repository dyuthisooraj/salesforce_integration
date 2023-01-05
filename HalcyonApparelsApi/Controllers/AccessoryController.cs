using HalcyonApparelsApplication.DTO;
using HalcyonApparelsApplication.Interfaces;
using HalcyonApparelsDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HalcyonApparelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IAccessory _accsry;
        public AccessoryController(IAccessory accsry)
        {
            _accsry = accsry;
        }

        [HttpGet("Get")]

        public ActionResult<List<AccessoryDetails>> Get()
        {
            var result = _accsry.Get();
            return Ok(result);
        }

        [HttpPost("Post")]
        //[Route]
        public IActionResult Post(AccessoryDTO accdto)
        {
            AccessoryDetails accsry1 = new AccessoryDetails();
            if (!ModelState.IsValid)
                return BadRequest("Is not valid");

            else
            {
                {

                    accsry1.AccessoryId = accdto.AccessoryId;
                    accsry1.AccessoryName = accdto.AccessoryName;
                    accsry1.AccessoryType = accdto.AccessoryType;
                    accsry1.AccessoryBrand = accdto.AccessoryBrand;
                    accsry1.AccessoryPrice = accdto.AccessoryPrice;
                    accsry1.AccessoryDiscount = accdto.AccessoryDiscount;
                    accsry1.ImageUrl = accdto.ImageUrl;
                    

                }

            }

            _accsry.Post(accsry1);
            return Ok();
        }

        //[HttpPost("Post")]
        ////[Route]
        //public ActionResult Post(AccessoryDetails accdto)
        //{
        //    AccessoryDetails accsry1 = new AccessoryDetails();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Is not valid");
        //    }
        //    else
        //    {
        //        //accdto.AccessoryId = accsry1.AccessoryId;
        //        //accdto.AccessoryName = accsry1.AccessoryName;
        //        //accdto.AccessoryType = accsry1.AccessoryType;
        //        //accdto.AccessoryBrand = accsry1.AccessoryBrand;
        //        //accdto.AccessoryPrice = accsry1.AccessoryPrice;
        //        //accdto.AccessoryDiscount = accsry1.AccessoryDiscount;
        //        //accdto.ImageUrl = accsry1.ImageUrl;
        //        //accdto.ImageFile = accsry1.ImageFile;
        //        //var uniquefile = UploadFile(accdto.ImageFile);

        //        //accsry1.ImageUrl = uniquefile;

        //        _accsry.Post(accsry1);
        //        return Ok();
        //    }
        //}

        // DELETE api/<EmployeeController>/5
        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            _accsry.Delete(id);
            return Ok();

        }

        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult<AccessoryDetails> Get(int id)
        {
            return _accsry.Get(id);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(AccessoryDetails acc4)
        {
            if (!ModelState.IsValid)
                return BadRequest("Is not valid");
            _accsry.Edit(acc4);
            return Ok();
        }

        //public static string UploadFile(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"~/images", special + "-" + file.FileName);
        //    using (FileStream ms = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(ms);
        //    }
        //    var filename = special + "=" + file.FileName;
        //    return filePath;

        //}
    }
}