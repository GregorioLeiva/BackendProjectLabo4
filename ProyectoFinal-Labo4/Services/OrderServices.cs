using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Models.Order.Dto;
using ProyectoFinal_Labo4.Models.Orders;
using ProyectoFinal_Labo4.Models.Orders.Dto;
using ProyectoFinal_Labo4.Models.Product.Dto;
using ProyectoFinal_Labo4.Models.Product;
using ProyectoFinal_Labo4.Utils.Exceptions;
using ProyectoFinalLabo4.Repositories;
using System.Net;

namespace ProyectoFinal_Labo4.Services
{
    public class OrderServices
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepo;

        public OrderServices(IMapper mapper, IOrderRepository orderRepo)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
        }

        private async Task<Order> GetOneByIdOrException(int id)
        {
            var order = await _orderRepo.GetOne(o => o.Id == id);
            if (order == null)
            {
                throw new CustomHttpException($"No se encontró la orden con Id = {id}", HttpStatusCode.NotFound);
            }
            return order;
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            var orders = await _orderRepo.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOneById(int id)
        {
            var order = await GetOneByIdOrException(id);
            return _mapper.Map<OrderDTO>(order);
		}

		public async Task<Order> CreateOne(CreateOrderDTO createOrderDto)
		{
			Order order = _mapper.Map<Order>(createOrderDto);

			await _orderRepo.Add(order);
			return order;
		}



		public async Task<Order> UpdateOneById(int id, UpdateOrderDTO updateOrderDto)
        {
            Order order = await GetOneByIdOrException(id);

            var orderMapped = _mapper.Map(updateOrderDto, order);

            await _orderRepo.Update(orderMapped);

            return orderMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var order = await GetOneByIdOrException(id);

            await _orderRepo.Delete(order);
        }
    }
}
