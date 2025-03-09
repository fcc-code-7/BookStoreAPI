using AutoMapper;
using BookStoreAPI.Models.Domain;
using BookStoreAPI.Models.DTO;

namespace BookStoreAPI.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Book, BookStoreDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AddRequestCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateRequestCategoryDTO, Category>().ReverseMap();

        }
    }
}
