using AutoMapper;
using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewMedicineDTO, Medicine>();
            CreateMap<Medicine, MedicineForShowingDTO>();

            CreateMap<UserRegistrationDTO, User>();
            CreateMap<User, UserRegistrationDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderWithIdDTO,Order>();
            CreateMap<ShoppingCart, ShoppingCartFrontDTO>()
               // .IncludeMembers(s => s.User, s => s.ShoppingCart_Id, s => s.ShoppingCartItem)
                .ForMember(dest => dest.items, opt => opt.MapFrom(s => s.ShoppingCartItem))
                .ForMember(dest => dest.ShoppingCart_Id, opt => opt.MapFrom(src => src.ShoppingCart_Id.ToString()))
                .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.User.UserId.ToString()))
                .ForMember(dest => dest.finalPrice, opt => opt.MapFrom(src => src.getFinalPrice().ToString()));

            CreateMap<OrderItem, ShoppingCartItemFrontDTO>()
                .ForMember(dest => dest.item_Id, opt => opt.MapFrom(src => src.OrderItemId.ToString()))
                .ForMember(dest => dest.medicineName, opt => opt.MapFrom(src => src.Medicine.Name.ToString()))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.PriceForSingleEntity.ToString()))
                .ForMember(dest => dest.priceAll, opt => opt.MapFrom(src => src.getPriceForAll().ToString()))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Quantity.ToString()));
        }
    }
}
