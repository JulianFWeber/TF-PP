using TF_PP.DB.Models;
using TF_PP.Services.Exceptions;

namespace TF_PP.Services.Validates
{
    public class PromotionValidator
    {
        public static void Validate(TbPromotion promotion)
        {
            if (promotion.Startdate >= promotion.Enddate)
                throw new InvalidEntityException("A data de início da promoção deve ser anterior à data de término.");

            if (promotion.Value <= 0)
                throw new InvalidEntityException("O valor da promoção deve ser maior que zero.");
        }
    }
}