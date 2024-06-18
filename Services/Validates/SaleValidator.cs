using TF_PP.DB.Models;
using TF_PP.Services.Exceptions;


namespace TF_PP.Services.Validates
{
    public class SaleValidator
    {
        public static void Validate(TbSale sale)
        {
            if (string.IsNullOrEmpty(sale.Code))
                throw new InvalidEntityException("Código da venda é obrigatório.");

            if (sale.Createat == default)
                throw new InvalidEntityException("Data de criação da venda é obrigatória.");

            if (sale.Price <= 0)
                throw new InvalidEntityException("O preço da venda deve ser maior que zero.");

            if (sale.Qty <= 0)
                throw new InvalidEntityException("A quantidade vendida deve ser maior que zero.");
        }
    }
}