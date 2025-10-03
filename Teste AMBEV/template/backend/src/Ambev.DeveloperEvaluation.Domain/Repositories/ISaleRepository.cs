using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repositório responsável por persistir e recuperar agregados Sale.
    /// O ciclo de vida de SaleItem é controlado pelo Sale (Aggregate Root).
    /// </summary>
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
