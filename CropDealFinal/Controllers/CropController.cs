using CaseStudy.Dtos.CropDto;
using CaseStudy.Models;
using CaseStudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropController : ControllerBase
    {
        private readonly CropService _service;
        public CropController(CropService service)
        {
            _service = service;
        }

        [HttpPost("addCrop")]
        [Authorize(Roles = "Farmer")]
        public async Task<ActionResult<CropDetail>> AddNewCrop(AddCropDto crop)
        {

            var res = await _service.AddCropAsync(crop);
            if (res == null)
            {
                return BadRequest("Error while adding crop details");
            }
            return Ok(res);

        }

        
        [HttpGet("getCrops")]
        public async Task<ActionResult<IEnumerable<CropDetail>>> GetAllCrops()
        {
            var res = await _service.GetAllCropAsync();
            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        
        [HttpGet("getCrops/{id}")]
        public async Task<ActionResult<CropDetail>> GetCropById(int id)
        {
            var res = await _service.GetCropByIdAsync(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPut("editCrop/{cid}")]
        public async Task<ActionResult<CropDetail>> UpdateCrop(UpdateCropDto crop, int cid)
        {

            var res = await _service.EditCropAsync(cid, crop);
            if (res == null)
            {
                return BadRequest("Error while updating crop details");
            }
            return Ok(res);
        }

        [HttpGet("viewCrop/{id}")]
        public async Task<ActionResult<CropDetail>> viewCropById(int id)
        {
            var res = await _service.ViewCropByIdAsync(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPut("cropimg/{cid}")]
        public async Task CropImage(int cid)
        {
            var filepath = "F:\\CaseStudyFinal\\CropDealFinal\\wwwroot\\uploads\\crops\\";
            var files = Request.Form.Files;
            foreach (IFormFile source in files)
            {

                string FileName = source.FileName;
                string filePath = filepath +cid.ToString();
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                string imagePath = filePath + "\\image.jpg";
                using(FileStream stream = System.IO.File.Create(imagePath))
                {
                    await source.CopyToAsync(stream);
                }
                await _service.CropImage(@"https://localhost:44346/uploads/crops/"+cid+"/image.jpg", cid);
            }
        }
    }
}
