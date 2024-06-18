using TF_PP.DB.Models;
using TF_PP.Services.DTOs;

namespace TF_PP.Services.Parser
{
    public class SaleParser
    {
        public static TbSale ToEntity(VendasDTO dto)
        {
            return new TbSale
            {
                Productid = dto.Productid,
                Qty = dto.Qty
            };
        }
    }
}