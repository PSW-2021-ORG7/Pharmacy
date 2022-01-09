using AutoMapper;
using backend.DTO.TenderingDTO;
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
            CreateMap<AdDTO, Ad>();
            CreateMap<ShoppingCart, ShoppingCartFrontDTO>()
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

            CreateMap<TenderingOfferItem, TenderingOfferItemDTO>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.MedicineName.ToString()))
                .ForMember(dest => dest.DosageInMilligrams, opt => opt.MapFrom(src => src.MedicineDosage.ToString()))
                .ForMember(dest => dest.AvailableQuantity, opt => opt.MapFrom(src => src.AvailableQuantity.ToString()))
                .ForMember(dest => dest.PriceForSingleEntity, opt => opt.MapFrom(src => src.PriceForSingleEntity.ToString()))
                .ForMember(dest => dest.PriceForAllAvailableEntity, opt => opt.MapFrom(src => src.GetPriceForAllAvailable().ToString()))
                .ForMember(dest => dest.PriceForAllRequiredEntity, opt => opt.MapFrom(src => src.GetPriceForAllRequired().ToString()));

            CreateMap<TenderingOffer, TenderingOfferDTO>()
                .ForMember(dest => dest.tenderingOfferItems, opt => opt.MapFrom(src => src.tenderingOfferItems))
                .ForMember(dest => dest.PriceForAllAvailable, opt => opt.MapFrom(src => src.GetPriceForAllAvailable().ToString()))
                .ForMember(dest => dest.PriceForAllRequired, opt => opt.MapFrom(src => src.GetPriceForAllRequired().ToString()))
                .ForMember(dest => dest.TotalNumberMissingMedicine, opt => opt.MapFrom(src => src.GetTotalMissing().ToString()));
        }
        
    }
}
