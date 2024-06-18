using TF_PP.DB.Models;
using TF_PP.Services.Exceptions;

namespace TF_PP.Services.Validates
{
    public class StockLogValidator
    {
        public static void Validate(TbStockLog stockLog)
        {
            if (stockLog.Createdat == default)
                throw new InvalidEntityException("Data da movimentação de estoque é obrigatória.");

            if (stockLog.Qty == 0)
                throw new InvalidEntityException("A quantidade movimentada não pode ser zero.");
        }
    }
}