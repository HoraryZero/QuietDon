using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Don_3000
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (tbxLogin.Text.Trim() == "")
            {
                MessageBox.Show("Поле 'Имя пользователя' не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbxPassword.Text.Trim() == "")
            {
                MessageBox.Show("Поле 'Пароль' не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbxLogin.Text.Trim() == "Admin" && tbxPassword.Text.Trim() == "12345")
                this.DialogResult = DialogResult.OK;
            else
                MessageBox.Show("Введены не верные данные аутентификации! \nПроверьте логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TbxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                BtnOk_Click(null, null);
        }
    }
}
