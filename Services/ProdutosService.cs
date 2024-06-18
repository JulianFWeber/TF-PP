using System.Collections.Generic;
using System.Linq;
using TF_PP.DB;
using TF_PP.DB.Models;
using TF_PP.Services.DTOs;
using TF_PP.Services.Parser;
using TF_PP.Services.Validate;
using TF_PP.Services.Exceptions;

namespace TF_PP.Services
{
    public class ProdutosService
    {
        private readonly DBContext _dbContext;

        public ProdutosService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtém um produto pelo ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O produto encontrado ou null se não encontrado.</returns>
        public TbProduct GetById(int id)
        {
            return _dbContext.TbProducts.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Obtém um produto pelo código de barras.
        /// </summary>
        /// <param name="barcode">O código de barras do produto.</param>
        /// <returns>O produto encontrado.</returns>
        /// <exception cref="NotFoundException">Se o produto com o barcode especificado não for encontrado.</exception>
        public TbProduct GetByBarcode(string barcode)
        {
            var existingEntity = _dbContext.TbProducts.FirstOrDefault(c => c.Barcode == barcode);

            if (existingEntity == null)
            {
                throw new NotFoundException("Produto com barcode informado não encontrado");
            }

            return existingEntity;
        }

        /// <summary>
        /// Obtém produtos por parte da descrição.
        /// </summary>
        /// <param name="desc">Parte da descrição a ser buscada.</param>
        /// <returns>Lista de produtos que contêm a descrição especificada.</returns>
        public IEnumerable<TbProduct> GetByDesc(string desc)
        {
            var existingEntities = _dbContext.TbProducts
                                              .Where(c => c.Description.ToUpper().Contains(desc.ToUpper()))
                                              .ToList();
            return existingEntities;
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="dto">Dados do produto a ser criado.</param>
        /// <returns>O produto criado.</returns>
        public TbProduct Post(ProdutoDTO dto)
        {
            if (!ProdutosValidate.Execute(dto))
                return null;

            var product = ProdutoParser.ToEntity(dto);

            _dbContext.Add(product);
            _dbContext.SaveChanges();

            return product;
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="dto">Dados do produto para atualização.</param>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <returns>O produto atualizado.</returns>
        /// <exception cref="NotFoundException">Se o produto com o ID especificado não for encontrado.</exception>
        public TbProduct Put(ProdutoDTO dto, int id)
        {
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Produto com ID informado não encontrado");
            }

            if (!ProdutosValidate.Execute(dto))
                return null;

            var product = ProdutoParser.ToEntity(dto);

            existingEntity.Description = product.Description;
            existingEntity.Barcode = product.Barcode;
            existingEntity.Barcodetype = product.Barcodetype;
            existingEntity.Price = product.Price;
            existingEntity.Costprice = product.Costprice;

            _dbContext.Update(existingEntity);
            _dbContext.SaveChanges();

            return product;
        }

        /// <summary>
        /// Atualiza o estoque de um produto.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="qtd">Nova quantidade em estoque.</param>
        /// <exception cref="NotFoundException">Se o produto com o ID especificado não for encontrado.</exception>
        public void Patch(int id, int qtd)
        {
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Produto com ID informado não encontrado");
            }

            existingEntity.Stock = qtd;

            _dbContext.Update(existingEntity);
            _dbContext.SaveChanges();
        }
    }
}