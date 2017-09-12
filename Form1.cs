using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AesEncDec;
using System.IO;

namespace Login_System_Tut_ecnrypted
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegForm rf = new RegForm();
            rf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usrTxt.Text.Length < 3 || passTxt.Text.Length < 5)
            {
                MessageBox.Show("Логін або пароль невірний або занадто короткий!");
            }
            else
            {
                string dir = usrTxt.Text;
                if (!Directory.Exists("data\\" + dir))
                    MessageBox.Show("Користувач {0} не знайдений!", dir);
                else
                {
                    var sr = new StreamReader("data\\" + dir + "\\data.ls");

                    string encusr = sr.ReadLine();
                    string encpass = sr.ReadLine();
                    sr.Close();

                    string decusr = AesCryp.Decrypt(encusr);
                    string decpass = AesCryp.Decrypt(encpass);

                    if (decusr == usrTxt.Text && decpass == passTxt.Text)
                    {

                        this.Hide();

                        MainWindow ss = new MainWindow();
                        ss.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Помилка! логін або пароль невірний!");
                    }

                }
            }
        }

        private void usrTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
