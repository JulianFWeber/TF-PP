using TF_PP.DB.Models;
using TF_PP.Services.DTOs;

namespace TF_PP.Services.Parser
{
    public class StockLogParser
    {
        public static TbStockLog ToEntity(StockLogDTO dto)
        {
            return new TbStockLog
            {
                Productid = dto.Productid,
                Qty = dto.Qty,
                Createdat = dto.Createdat
            };
        }
    }
}