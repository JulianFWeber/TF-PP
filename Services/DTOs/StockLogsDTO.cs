using System;

namespace TF_PP.Services.DTOs
{
    public class StockLogDTO
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public int Qty { get; set; }
        public DateTime Createdat { get; set; }
    }
}