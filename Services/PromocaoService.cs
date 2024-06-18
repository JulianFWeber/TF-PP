using System.Linq;
using TF_PP.Services.DTOs;
using TF_PP.DB.Models;
using TF_PP.DB;
using System;
using System.Collections.Generic;
using TF_PP.Services.Parser;

namespace TF_PP.Services
{
    public class PromotionsService
    {
        private readonly DBContext _dbContext;
        public PromotionsService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TbPromotion GetById(int id)
        {
            return _dbContext.TbPromotions.FirstOrDefault(c => c.Id == id);
        }

        public TbPromotion Post(PromocaoDTO dto)
        {
            var promotion = PromotionParser.ToEntity(dto);

            _dbContext.Add(promotion);
            _dbContext.SaveChanges();

            return promotion;
        }

        public TbPromotion Update(int id, PromocaoDTO dto)
        {
            var existingPromotion = _dbContext.TbPromotions.FirstOrDefault(c => c.Id == id);
            if (existingPromotion == null) throw new Exception("Promotion not found");

            existingPromotion.Startdate = dto.Startdate;
            existingPromotion.Enddate = dto.Enddate;
            existingPromotion.Promotiontype = dto.Promotiontype;
            existingPromotion.Productid = dto.Productid;
            existingPromotion.Value = dto.Value;

            _dbContext.SaveChanges();

            return existingPromotion;
        }

        public IEnumerable<TbPromotion> GetPromotionsByProductAndPeriod(int productId, DateTime startDate, DateTime endDate)
        {
            return _dbContext.TbPromotions
                             .Where(p => p.Productid == productId &&
                                         p.Startdate >= startDate &&
                                         p.Enddate <= endDate)
                             .ToList();
        }

        public List<TbPromotion> GetActivePromotions(int productId)
        {
            var currentDate = DateTime.Now;

            return _dbContext.TbPromotions
                .Where(p => p.Productid == productId
                            && p.Startdate <= DateTime.Now
                            && p.Enddate >= DateTime.Now)
                .OrderBy(p => p.Promotiontype)
               .ToList();
        }
    }
}