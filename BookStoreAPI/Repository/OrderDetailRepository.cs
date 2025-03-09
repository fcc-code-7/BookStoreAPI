using BookStoreAPI.Data;
using BookStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly BookStoreDbContext dbContext;

        public OrderDetailRepository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var book = await dbContext.Books.FindAsync(orderDetail.BookId);
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }

                if (book.StockQuantity < orderDetail.Quantity)
                {
                    throw new Exception("Insufficient stock.");
                }

                book.StockQuantity -= orderDetail.Quantity; // Reduce stock
                dbContext.Books.Update(book);

                orderDetail.Price = book.Price * orderDetail.Quantity; // Calculate price
                await dbContext.OrderDetails.AddAsync(orderDetail);
                var order = await dbContext.Orders.FindAsync(orderDetail.OrderId);
                if (order == null)
                {
                    throw new Exception("Order not found.");
                }
                order.Status = "Completed";
                dbContext.Orders.Update(order);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return orderDetail;
            }
            catch (Exception ex)
            {
                var order = await dbContext.Orders.FindAsync(orderDetail.OrderId);
                if (order == null)
                {
                    throw new Exception("Order not found.");
                }
                order.Status = "Failed";
                dbContext.Orders.Update(order);
                await dbContext.SaveChangesAsync();
                await transaction.RollbackAsync();
                throw new Exception($"Failed to create order detail: {ex.Message}");
            }
        }

        public async Task<OrderDetail> DeleteOrderDetail(Guid id)
        {
            var orderDetail = await dbContext.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return null;
            }
            dbContext.OrderDetails.Remove(orderDetail);
            await dbContext.SaveChangesAsync();
            var book = await dbContext.Books.FindAsync(orderDetail.BookId);
            if (book != null)
            {
                book.StockQuantity += orderDetail.Quantity;
                dbContext.Books.Update(book);
                await dbContext.SaveChangesAsync();
            }
            return orderDetail;
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailAsync(string? FilterOn = null, string? FilterBy = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000)
        {
            var OrderDetails = dbContext.OrderDetails.AsQueryable();
            ////Filter
            //if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterBy) == false)
            //{
            //    if (FilterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
            //    {
            //        OrderDetails = OrderDetails.Where(c => c.Quantity.Contains(FilterBy));
            //    }
            //}

            ////Sort
            //if (string.IsNullOrWhiteSpace(SortBy) == false)
            //{
            //    if (SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
            //    {
            //        if (isAscending)
            //        {
            //            OrderDetails = OrderDetails.OrderDetailBy(c => c.Status);
            //        }
            //        else
            //        {
            //            OrderDetails = OrderDetails.OrderDetailByDescending(c => c.Status);
            //        }
            //    }
            //}
            //Pagination
            var OrderDetailsSkip = (PageNo - 1) * PageSize;
            return await OrderDetails.Skip(OrderDetailsSkip).Take(PageSize).Include(c => c.Book).Include(c => c.Order).ToListAsync();
        }

        public Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id)
        {
            var orderDetail = dbContext.OrderDetails.Include(c => c.Book).Include(c => c.Order).FirstOrDefaultAsync(c => c.OrderDetailId == id);
            return orderDetail;
        }

        public async Task<OrderDetail> UpdateOrderDetail(Guid id, OrderDetail orderDetail)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var existingOrderDetail = await dbContext.OrderDetails.FindAsync(id);
                if (existingOrderDetail == null)
                {
                    throw new Exception("Order detail not found.");
                }

                var book = await dbContext.Books.FindAsync(orderDetail.BookId);
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }

                // Adjust stock quantity
                int stockAdjustment = existingOrderDetail.Quantity - orderDetail.Quantity;
                if (book.StockQuantity + stockAdjustment < 0)
                {
                    throw new Exception("Insufficient stock for update.");
                }

                book.StockQuantity += stockAdjustment; // Update stock
                dbContext.Books.Update(book);

                // Update order detail
                existingOrderDetail.OrderId = orderDetail.OrderId;
                existingOrderDetail.BookId = orderDetail.BookId;
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Price = book.Price * orderDetail.Quantity;

                dbContext.OrderDetails.Update(existingOrderDetail);
                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return existingOrderDetail;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to update order detail: {ex.Message}");
            }
        }
    }
}
