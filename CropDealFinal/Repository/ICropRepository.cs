using CaseStudy.Dtos.CropDto;
using CaseStudy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Repository
{
    public interface ICropRepository
    {
        Task<ActionResult<CropDetail>> AddCropAsync(AddCropDto crop);
        Task<IEnumerable<CropDetail>> GetAllCropAsync();
        Task<ActionResult<CropDetail>> GetCropByIdAsync(int id);

        Task<ActionResult<CropDetail>> EditCropAsync(int id, UpdateCropDto crop);
        Task<ActionResult<ViewCropDto>> ViewCropByIdAsync(int id);
        Task CropImage(string path, int cid);
    }
}
