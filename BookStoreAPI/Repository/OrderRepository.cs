using BookStoreAPI.Data;
using BookStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreDbContext dbContext;

        public OrderRepository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Status = "Pending";
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteOrder(Guid id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrderAsync(string? FilterOn = null, string? FilterBy = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000)
        {
            var Orders = dbContext.Orders.AsQueryable();
            //Filter
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterBy) == false)
            {
                if (FilterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    Orders = Orders.Where(c => c.Status.Contains(FilterBy));
                }
            }

            //Sort
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending)
                    {
                        Orders = Orders.OrderBy(c => c.Status);
                    }
                    else
                    {
                        Orders = Orders.OrderByDescending(c => c.Status);
                    }
                }
            }
            //Pagination
            var OrdersSkip = (PageNo - 1) * PageSize;
            return await Orders.Skip(OrdersSkip).Take(PageSize).Include(c => c.User).Include(c => c.OrderDetails).ToListAsync();
        }

        public Task<Order?> GetOrderByIdAsync(Guid id)
        {
            var order = dbContext.Orders.Include(c => c.User).Include(c => c.OrderDetails).FirstOrDefaultAsync(c => c.OrderId == id);
            return order;
        }

        public async Task<Order> UpdateOrder(Guid id, Order order)
        {
            var fetchOrder = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (fetchOrder == null)
            {
                return null;
            }
            fetchOrder.OrderDate = order.OrderDate;
            fetchOrder.Status = order.Status;
            fetchOrder.UserId = order.UserId;
            dbContext.Orders.Update(fetchOrder);
            await dbContext.SaveChangesAsync();
            return fetchOrder;
        }
    }
}
