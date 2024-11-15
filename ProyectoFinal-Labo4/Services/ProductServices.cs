using AutoMapper;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.Product.Dto;
using ProyectoFinal_Labo4.Repositories;
using ProyectoFinal_Labo4.Utils.Exceptions;
using System.Net;

namespace ProyectoFinal_Labo4.Services
{
	public class ProductServices
	{
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepo;

		public ProductServices(IMapper mapper, IProductRepository productRepo)
		{
			_mapper = mapper;
			_productRepo = productRepo;
		}

		private async Task<Product> GetOneByIdOrException(int id)
		{
			var product = await _productRepo.GetOne(p => p.Id == id);
			if (product == null)
			{
				throw new CustomHttpException($"No se encontró el producto con Id = {id}", HttpStatusCode.NotFound);
			}
			return product;
		}

		public async Task<List<ProductsDTO>> GetAll()
		{
			var productos = await _productRepo.GetAll();
			return _mapper.Map<List<ProductsDTO>>(productos);
		}

		public async Task<ProductDTO> GetOneById(int id)
		{
			var product = await GetOneByIdOrException(id);
			return _mapper.Map<ProductDTO>(product);
		}

		public async Task<Product> CreateOne(CreateProductDTO createProductDto)
		{
			Product product = _mapper.Map<Product>(createProductDto);

			await _productRepo.Add(product);
			return product;
		}

		public async Task<Product> UpdateOneById(int id, UpdateProductDTO updateProductDto)
		{
			Product product = await GetOneByIdOrException(id);

			var productMapped = _mapper.Map(updateProductDto, product);

			await _productRepo.Update(productMapped);

			return productMapped;
		}

		public async Task DeleteOneById(int id)
		{
			var product = await GetOneByIdOrException(id);

			await _productRepo.Delete(product);
		}
	}
}
