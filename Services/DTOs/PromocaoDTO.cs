using System;

namespace TF_PP.Services.DTOs
{
    public class PromocaoDTO
    {
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public int Promotiontype { get; set; }
        public int Productid { get; set; }
        public decimal Value { get; set; }

    }
}