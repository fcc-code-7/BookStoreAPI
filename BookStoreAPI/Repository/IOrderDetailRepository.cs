using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Repository
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetAllOrderDetailAsync(string? FilterOn = null, string? FilterBy = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000);

        Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);

        Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id);

        Task<OrderDetail> UpdateOrderDetail(Guid id, OrderDetail orderDetail);

        Task<OrderDetail> DeleteOrderDetail(Guid id);
    }
}
