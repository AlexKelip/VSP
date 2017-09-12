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
    public partial class MainWindow : Form
    {
        private readonly IRepository _repository;
        public MainWindow()
        {

            FlashDisk.Check();
            _repository = new Repository();
            InitializeComponent();
            MainGrid.DataSource = _repository.GetMaskedPasswords();

        }
        private void AddNewAccount_Click(object sender, EventArgs e)
        {
            var editor = new Editor(_repository, new Account());
            editor.ShowDialog();
            MainGrid.DataSource = _repository.GetMaskedPasswords();

        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void MainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var edited = MainGrid.Rows[e.RowIndex].DataBoundItem as Account;
            _repository.AddOrUpdate(edited);
        }
        private void MainGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var toEdit = MainGrid.Rows[e.RowIndex].DataBoundItem as Account;
            var editor = new Editor(_repository, toEdit);
            editor.ShowDialog();
            MainGrid.DataSource = _repository.GetMaskedPasswords();
        }
        private void MainGrid_CellClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = MainGrid.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = MainGrid.HitTest(e.X, e.Y).ColumnIndex;

                MainGrid.CurrentCell = MainGrid.Rows[currentMouseOverRow].Cells[currentMouseOverColumn];

                var item = (MainGrid.Rows[currentMouseOverRow].DataBoundItem as Account);

                if (currentMouseOverRow >= 0)
                {
                    var m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("Переглянути", (o, args) =>
                    {
                        var editor = new Editor(_repository, item);
                        editor.ShowDialog();
                    }));

                    m.MenuItems.Add(new MenuItem("Редагувати", (o, args) =>
                    {
                        var editor = new Editor(_repository, item, false);
                        editor.ShowDialog();
                        MainGrid.DataSource = _repository.GetMaskedPasswords();
                    }));

                    m.MenuItems.Add(new MenuItem("Видалити", (o, args) =>
                    {
                        if (item != null) _repository.Remove(item.Id);
                        MainGrid.DataSource = _repository.GetMaskedPasswords();
                    }));
                    m.Show(MainGrid, new Point(e.X, e.Y));
                }


            }
        }


        private void AddNewButton_Click(object sender, EventArgs e)
        {
            var editor = new Editor(_repository, new Account(), false);
            editor.ShowDialog();
            MainGrid.DataSource = _repository.GetMaskedPasswords();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {


        }

        private void MainGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
