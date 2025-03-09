using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderAsync(string? FilterOn = null, string? FilterBy = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000);

        Task<Order> CreateOrderAsync(Order order);

        Task<Order?> GetOrderByIdAsync(Guid id);

        Task<Order> UpdateOrder(Guid id, Order order);

        Task<Order> DeleteOrder(Guid id);
    }
}
