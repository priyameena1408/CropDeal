using CaseStudy.Dtos.CropDto;
using CaseStudy.Models;
using CaseStudy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Services
{
    public class CropService
    {
        ICropRepository _repo;
        public CropService(ICropRepository repository)
        {
            _repo = repository;
        }

        public async Task<ActionResult<CropDetail>> AddCropAsync(AddCropDto newCrop)
        {
            return await _repo.AddCropAsync(newCrop);
        }

        public async Task<IEnumerable<CropDetail>> GetAllCropAsync()
        {
            return await _repo.GetAllCropAsync();
        }

        public async Task<ActionResult<CropDetail>> GetCropByIdAsync(int id)
        {
            return await _repo.GetCropByIdAsync(id);
        }
        public async Task<ActionResult<CropDetail>> EditCropAsync(int id, UpdateCropDto crop)
        {
            return await _repo.EditCropAsync(id, crop);
        }

        public async Task<ActionResult<ViewCropDto>> ViewCropByIdAsync(int id)
        {
            return await _repo.ViewCropByIdAsync(id);
        }
        public async Task CropImage(string path, int cid)
        {
            await _repo.CropImage(path, cid);
        }
    }
}
