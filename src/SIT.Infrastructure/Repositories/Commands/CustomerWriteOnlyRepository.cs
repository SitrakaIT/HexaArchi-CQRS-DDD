using Microsoft.EntityFrameworkCore;
using SIT.Core.Domain.Entities;
using SIT.Core.Domain.Repositories.Customers;
using SIT.Infrastructure.Contexts;
using SIT.Infrastructure.Repositories.Common;

namespace SIT.Infrastructure.Repositories.Commands;

public class CustomerWriteOnlyRepository(WriteDbContext context) : BaseWriteOnlyRepository<Customer, int>(context), ICustomerWriteOnlyRepository
{
    private static readonly Func<WriteDbContext, string, Task<bool>> ExistsByNameCompiledAsync = 
        EF.CompileAsyncQuery((WriteDbContext wContext, string name) =>
            wContext
                .Customers
                .AsNoTracking()
                .Any(x => x.Name == name));
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await ExistsByNameCompiledAsync(Context, name); 
    }
}