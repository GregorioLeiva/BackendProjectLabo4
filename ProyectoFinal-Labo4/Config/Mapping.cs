using AutoMapper;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Models.Product.Dto;
using ProyectoFinal_Labo4.Models.User.Dto;
using ProyectoFinal_Labo4.Models.User;

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
			// Usuarios
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<User, UsersDTO>().ReverseMap();
			CreateMap<User, CreateUserDTO>().ReverseMap();

			// Actualizar y no parsear los valores 'NULL'
			CreateMap<UpdateUserDTO, User>()
				.ForAllMembers(opts =>
				{
					opts.Condition((src, dest, srcMember) => srcMember != null);
				});
		}
	}
}
