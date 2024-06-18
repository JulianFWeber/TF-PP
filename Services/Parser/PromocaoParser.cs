using TF_PP.DB.Models;
using TF_PP.Services.DTOs;
using Newtonsoft.Json.Linq;


namespace TF_PP.Services.Parser
{
    public class PromotionParser
    {
        public static TbPromotion ToEntity(PromocaoDTO dto)
        {
            return new TbPromotion
            {
                Startdate = dto.Startdate,
                Enddate = dto.Enddate,
                Promotiontype = dto.Promotiontype,
                Productid = dto.Productid,
                Value = dto.Value
            };
        }
    }
}