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
    public partial class FormPocht : Form
    {
        public FormPocht()
        {
            InitializeComponent();
        }

        private void FormPocht_Load(object sender, EventArgs e)
        {
            textBox1.Text = "PochtTest@mail.ru";
            textBox2.Text = "horaryzero161";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка входа");
            }
            else
            {
                if (checkBox1.Checked == true)
                {

                    Properties.Settings.Default.LOG = textBox1.Text;
                    Properties.Settings.Default.PASS = textBox2.Text;

                }

                Properties.Settings.Default.Name = textBox1.Text;
                Properties.Settings.Default.Pass = textBox2.Text;
                Properties.Settings.Default.Port = Convert.ToInt32(textBox3.Text);
                Properties.Settings.Default.Protokol = comboBox1.Text;
                Properties.Settings.Default.Save();
                FormPochtMain brows = new FormPochtMain();
                brows.ShowDialog();
                Close();
            }
        }
           
    }
}
