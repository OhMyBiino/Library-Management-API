using LibraryManagementAPI.Database;
using LibraryManagementModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Models.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LibraryContext _context;

        public TransactionRepository(LibraryContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsAsync()
        {
            var transactions = await _context.Transactions.ToListAsync();

            return transactions;
        }

        public async Task<TransactionModel> GetTransactionByIdAsync(Guid Id)
        {
            var transaction = await _context.Transactions.FindAsync(Id);

            return transaction;
        }
        public async Task<IEnumerable<TransactionModel>> SearchAsync(string Query)
        {

            IQueryable<TransactionModel> query = _context.Transactions;

            if (!String.IsNullOrEmpty(Query))
            {
                query = query.Where(transaction => transaction.UserId.Contains(Query) ||
                transaction.BorrowerName.Contains(Query) || transaction.ISBN.Contains(Query) ||
                transaction.Status.Contains(Query));
            }

            return query.ToList();
        }

        public async Task<TransactionModel> CreateTransactionAsync(TransactionModel newTransaction)
        {
            var addedTransaction = await _context.Transactions.AddAsync(newTransaction);


            await _context.SaveChangesAsync();
            return addedTransaction.Entity;
        }
        public async Task<TransactionModel> UpdateTransactionAsync(TransactionModel updatedTransaction)
        {
            var existingTransction = await _context.Transactions.FindAsync(updatedTransaction.UserId);

            if (existingTransction != null)
            {
                existingTransction.BorrowerName = updatedTransaction.BorrowerName;
                existingTransction.Type = updatedTransaction.Type;
                existingTransction.BorrowDate = updatedTransaction.BorrowDate;
                existingTransction.DueDate = updatedTransaction.DueDate;
                existingTransction.Status = updatedTransaction.Status;

                await _context.SaveChangesAsync();
                return updatedTransaction;
            }

            return null;
        }

        public async Task<TransactionModel> DeleteTransactionAsync(Guid Id)
        {
            var transactionToDelete = await _context.Transactions.FindAsync(Id);

            if (transactionToDelete != null) {
                _context.Transactions.Remove(transactionToDelete);
                await _context.SaveChangesAsync();
                return transactionToDelete;
            }

            return null;
        }
    }
}
