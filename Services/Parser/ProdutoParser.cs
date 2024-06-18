using TF_PP.DB.Models;
using TF_PP.Services.DTOs;


namespace TF_PP.Services.Parser
{
    public class ProdutoParser
    {
        public static TbProduct ToEntity(ProdutoDTO dto)
        {
            return new TbProduct
            {
                Description = dto.Description,
                Barcode = dto.Barcode.ToUpper(),
                Barcodetype = dto.Barcodetype,
                Stock = dto.Stock,
                Price = dto.Price,
                Costprice = dto.Costprice
            };
        }
    }
}