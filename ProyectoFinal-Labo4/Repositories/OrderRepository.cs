using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Labo4.Config;
using ProyectoFinal_Labo4.Models.Orders;
using ProyectoFinal_Labo4.Models.User;
using ProyectoFinal_Labo4.Repositories;
using System.Linq.Expressions;
using System.Linq;

namespace ProyectoFinalLabo4.Repositories
{
    public interface IOrderRepository : IRepository<Order> { }
    public class OrderRespository : Repository<Order>, IOrderRepository
    {
        public OrderRespository(ApplicationDbContext db) : base(db) { }
	}
   
}