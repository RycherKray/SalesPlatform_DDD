using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementação do repositório de vendas usando EF Core.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma venda pelo ID, incluindo os itens relacionados.
        /// </summary>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Obtém todas as vendas, incluindo os itens, sem tracking para melhor performance.
        /// </summary>
        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Adiciona uma nova venda e salva as alterações.
        /// </summary>
        public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Atualiza uma venda existente e salva as alterações.
        /// </summary>
        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Remove uma venda pelo ID, se existir.
        /// </summary>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null) return;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
