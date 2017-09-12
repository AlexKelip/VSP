using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Login_System_Tut_ecnrypted
{ 
    public class Repository : IRepository
{
    public Repository()
    {
        RefreshData();
    }

    public Account GetById(Guid id)
    {
        return Table.FirstOrDefault(account => account.Id == id);
    }

    public IList<Account> Table { get; set; }

    public IList<Account> GetMaskedPasswords()
    {
        var maskedList = Table.Select(account => new Account
        {
            Id = account.Id,
            Login = account.Login,
            Url = account.Url,
            Password = String.Join("", Enumerable.Repeat('*', account.Password.Length))
        });
        return maskedList.ToList();
    }

    public void AddOrUpdate(Account account)
    {
        var existed = GetById(account.Id);
        if (existed == null)
        {
            Table.Add(account);
        }
        else
        {
            existed.Login = account.Login;
            existed.Password = account.Password;
            existed.Url = account.Url;
        }
        SaveChanges();
    }

    public void Remove(Guid accountId)
    {
        var existed = GetById(accountId);
        if (existed == null)
            return;
        Table.Remove(existed);
        SaveChanges();
    }

    private void SaveChanges()
    {
        FlashDisk.Check();
        var data = JsonConvert.SerializeObject(Table.ToList());
        if (File.Exists("data.ef"))
            File.Delete("data.ef");
        File.WriteAllText("data.ef", data);
        RefreshData();
    }

    private void RefreshData()
    {
        try
        {
            FlashDisk.Check();
            var data = File.ReadAllText("data.ef");
            Table = JsonConvert.DeserializeObject<List<Account>>(data);
        }
        catch
        {
            Table = new List<Account>();
        }
    }
}
}