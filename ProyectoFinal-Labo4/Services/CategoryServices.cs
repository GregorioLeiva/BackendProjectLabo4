using AutoMapper;
using ProyectoFinal_Labo4.Models.Category.Dto;
using ProyectoFinal_Labo4.Models.Category;
using ProyectoFinal_Labo4.Repositories;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;

namespace ProyectoFinal_Labo4.Services
{
    public class CategoryServices
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;

        public CategoryServices(IMapper mapper, ICategoryRepository categoryRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<Category>> GetAll()
        {
            var category = await _categoryRepo.GetAll();
            return category.ToList();
        }

        public async Task<Category> GetOneById(int id)
        {
            var category = await _categoryRepo.GetOne(c => c.Id == id);
            if (category == null)
            {
                throw new CustomHttpException($"No se encontro la categoria con Id = {id}", HttpStatusCode.NotFound);
            }
            return category;
        }

        public async Task<Category> CreateOne(CreateCategoryDTO createCategoryDto)
        {
            Category category = _mapper.Map<Category>(createCategoryDto);

            await _categoryRepo.Add(category);
            return category;
        }

        public async Task<Category> UpdateOneById(int id, UpdateCategoryDTO updateProductoDto)
        {
            Category category = await GetOneById(id);

            var categoryMapped = _mapper.Map(updateProductoDto, category);

            await _categoryRepo.Update(categoryMapped);

            return categoryMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var category = await GetOneById(id);

            await _categoryRepo.Delete(category);
        }

    }

}