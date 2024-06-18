using TF_PP.DB.Models;
using AutoMapper;
using TF_PP.Services.DTOs;

namespace TF_PP.Services.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<TbProduct, ProdutoDTO>();
            CreateMap<TbPromotion, PromocaoDTO>();
            CreateMap<TbSale, VendasDTO>();
            CreateMap<TbStockLog, StockLogDTO>();
        }
    }
}