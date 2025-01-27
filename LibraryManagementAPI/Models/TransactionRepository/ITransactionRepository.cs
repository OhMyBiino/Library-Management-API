using LibraryManagementModels;

namespace LibraryManagementAPI.Models.TransactionRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionModel>> GetTransactionsAsync();
        Task<TransactionModel> GetTransactionByIdAsync(Guid Id);
        Task<IEnumerable<TransactionModel>> SearchAsync(string Query);
        Task<TransactionModel> CreateTransactionAsync(TransactionModel newTransaction);
        Task<TransactionModel> UpdateTransactionAsync(TransactionModel updatedTransaction);
        Task<TransactionModel> DeleteTransactionAsync(Guid Id);
    }
}
