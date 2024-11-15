using AutoMapper;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.Product.Dto;
using ProyectoFinal_Labo4.Models.User.Dto;
using ProyectoFinal_Labo4.Models.User;
using ProyectoFinal_Labo4.Models.Order.Dto;
using ProyectoFinal_Labo4.Models.Orders.Dto;
using ProyectoFinal_Labo4.Models.Orders;
using ProyectoFinal_Labo4.Models.Category;
using ProyectoFinal_Labo4.Models.Category.Dto;

namespace ProyectoFinal_Labo4.Config
{
	public class Mapping : Profile
	{
		public Mapping()
		{
			// Para no convertir los atributos 'int?' a 0 en la conversion de los 'null'
			// valor defecto int -> 0
			CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

			// Aqui es necesario hacer esto con bool? ya que tampoco devuelve el tipo 'null'.
			// valor defecto bool -> false
			CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

			//PD: Esta solución hay que aplicarla para todos aquellos tipos que no tengan como valor por defecto 'null'

			CreateMap<Product, ProductDTO>().ReverseMap();
			CreateMap<Product, ProductsDTO>().ReverseMap();
			CreateMap<Product, CreateProductDTO>().ReverseMap();

			// Actualizar y no parsear los valores 'NULL'
			CreateMap<UpdateProductDTO, Product>()
					.ForAllMembers(opts =>
					{
						opts.Condition((src, dest, srcMember) => srcMember != null);
					});
			//Category
			CreateMap<Category, CreateCategoryDTO>().ReverseMap();
			CreateMap<UpdateCategoryDTO, Category>()
					.ForAllMembers(opts =>
					{
						opts.Condition((src, dest, srcMember) => srcMember != null);
					});


			// Usuarios
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<User, UsersDTO>().ReverseMap();
			CreateMap<User, CreateUserDTO>().ReverseMap();
			CreateMap<User, UserLoginResponseDTO>().ForMember(
					dest => dest.Roles,
					opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToList())

				);

			// Actualizar y no parsear los valores 'NULL'
			CreateMap<UpdateUserDTO, User>()
				.ForAllMembers(opts =>
				{
					opts.Condition((src, dest, srcMember) => srcMember != null);
				});
			// Mapeo para Order y sus DTOs
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<CreateOrderDTO, Order>().ReverseMap();
			CreateMap<UpdateOrderDTO, Order>().ReverseMap();
			CreateMap<UpdateOrderDTO, Order>()
				.ForAllMembers(opts =>
				{
					opts.Condition((src, dest, srcMember) => srcMember != null);
				});
		}
	}
}
