using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login_System_Tut_ecnrypted;

namespace Login_System_Tut_ecnrypted
{ 
 public partial class Editor : Form
{
    private readonly IRepository _repository;
    private readonly Account _account;

    public Editor(IRepository repository, Account account, bool isReadonly = true)
    {
        _repository = repository;
        _account = _repository.GetById(account.Id) ?? new Account();


        InitializeComponent();
        Url.Text = _account.Url;
        Login.Text = _account.Login;
        Password.Text = _account.Password;

        Url.ReadOnly = isReadonly;
        Login.ReadOnly = isReadonly;
        Password.ReadOnly = isReadonly;

        SaveButton.Visible = !isReadonly;

        Text = isReadonly
            ? "Перегляд даних"
            : _account.Login == null && _account.Url == null && _account.Password == null
                ? "Додати аккаунт"
                : "Редагування даних";

    }


    private void SaveButton_Click(object sender, EventArgs e)
    {
        _account.Login = Login.Text;
        _account.Password = Password.Text;
        _account.Url = Url.Text;

        _repository.AddOrUpdate(_account);

        Close();
    }

    private void Url_TextChanged(System.Object sender, System.EventArgs e)

    {
    }
}
}