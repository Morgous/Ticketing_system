using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Repositories.Interfaces
{
    public interface IConcurrencyRepository<T> where T : class
    {
        Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel);
        void SetOriginalVersion(T entity, int originalVersion);
    }
}
