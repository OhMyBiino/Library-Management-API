using LibraryManagementAPI.Models.TransactionRepository;
using LibraryManagementAPI.Models.UserRepository;
using LibraryManagementModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransactions()
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsAsync();

                if (transactions == null) 
                {
                    return NotFound("No Transactions.");
                }

                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving transactions from the database.");
            }
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransactionById([FromRoute]Guid Id)
        {
            try
            {
                var transaction = await _transactionRepository.GetTransactionByIdAsync(Id);

                if (transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving transaction with Id:{Id} from the database.");
            }
        }

        [HttpGet("{User}/{Id:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetUserTransactions([FromRoute] Guid UserId)
        {
            try
            {
                var userTransactions = await _transactionRepository.GetUserTransactionsAsync(UserId);

                if (userTransactions.Any())
                {
                    return Ok(userTransactions);
                }

                return NotFound("No Transactions.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error retrieving transaction with ID:{UserId} from the database.");
            }
        }

        [HttpGet("{Borrowed}/{User}/{Id:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetUserBorrowTransactions([FromRoute] Guid Id)
        {
            try
            {
                var borrowedTransactions = await _transactionRepository.GetUserBorrowTransactionsAsync(Id);

                if (borrowedTransactions.Any()) 
                {
                    return Ok(borrowedTransactions);
                }

                return NotFound("No user transactions found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                        "Error retrieving transactions from the database.");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> Search([FromQuery]string? Query)
        {
            try
            {
                if (Query == null) 
                {
                    return NotFound("No results");
                }

                var results = await _transactionRepository.SearchAsync(Query);

                if (results.Any()) 
                {
                    return Ok(results);
                }

                return NotFound("No results found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error searching transactions from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> CreateTransaction([FromBody]TransactionModel newTransaction)
        {
            try
            {
                var existingTransaction = await _transactionRepository.GetTransactionByIdAsync(newTransaction.TransactionId);

                if (existingTransaction != null) 
                {
                    return Conflict("Transaction was already made.");
                }

                var createdTransaction = await _transactionRepository.CreateTransactionAsync(newTransaction);

                return CreatedAtAction(nameof(GetTransactionById),
                        new { Id = createdTransaction.TransactionId }, createdTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> UpdateTransaction([FromRoute]Guid Id, [FromBody]TransactionModel updatedTransaction)
        {
            try
            {
                if (Id != updatedTransaction.TransactionId) 
                {
                    return BadRequest("Id mismatch");
                }

                var existingTransaction = await _transactionRepository.GetTransactionByIdAsync(Id);

                if (existingTransaction == null) 
                {
                    return NotFound();
                }

                var latestTransaction = _transactionRepository.UpdateTransactionAsync(existingTransaction);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating transaction from the database.");
            }
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> DeleteTransaction([FromRoute]Guid Id)
        {
            try
            {
                var transactionToDelete = await _transactionRepository.GetTransactionByIdAsync(Id);

                if (transactionToDelete == null) 
                {
                    return NotFound($"Transaction with ID: {Id} could not be found.");
                }

                var deletedTransaction = await _transactionRepository.DeleteTransactionAsync(Id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting transaction from the database.");
            }
        }
    }
}
