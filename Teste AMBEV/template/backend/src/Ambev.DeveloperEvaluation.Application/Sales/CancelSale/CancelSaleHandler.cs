using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _repository;

        public CancelSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.SaleId, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            if (sale.Cancelled)
                throw new InvalidOperationException("This sale has already been cancelled.");

            sale.Cancel();

            await _repository.UpdateAsync(sale, cancellationToken);

            // Futuro: logar evento SaleCancelled
            // _logger.LogInformation("Sale {SaleNumber} was cancelled.", sale.SaleNumber);

            return new CancelSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled
            };
        }
    }
}
