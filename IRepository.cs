using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_System_Tut_ecnrypted
{ 
    public interface IRepository
{
    Account GetById(Guid id);
    IList<Account> Table { get; }
    IList<Account> GetMaskedPasswords();
    void AddOrUpdate(Account account);
    void Remove(Guid account);

}
}
