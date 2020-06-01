using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace Don_3000
{
    public partial class FormPochtMain : Form
    {
        public FormPochtMain()
        {
            InitializeComponent();
        }

        static public int port;
        static public string emailFrom; // адрес
        static public string password; // пароль 
        static public String emailTo;
        static public String subject;
        static public String body;
        static public OpenFileDialog opfd;
        static public int p1;
        static public string[] p = new string[20];
        static public int i1 = 0;
        static public int i = 0;

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormPochtMain_Load(object sender, EventArgs e)
        {
            textBox1.Text = "rnd.programmer.mike@gmail.com";
            label1.Text = Properties.Settings.Default.Name;
            port = Properties.Settings.Default.Port;
            emailFrom = Properties.Settings.Default.Name;
            password = Properties.Settings.Default.Pass;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Name = "";
            Properties.Settings.Default.Save();
            FormPocht brows = new FormPocht();
            brows.ShowDialog();
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            { MessageBox.Show("Остались пустые поля!", "Предупреждение"); }
            else
            {
                opfd = new OpenFileDialog();
                emailTo = textBox1.Text;
                subject = textBox2.Text;
                body = textBox3.Text;

                string smtpAddress = Properties.Settings.Default.Protokol;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true; // false если отправляется только текст

                if (p1 == 1)
                {
                    for (i = 0; i < i1; i++)
                    {
                        mail.Attachments.Add(new Attachment(p[i]));
                    }
                    p1 = 0;
                    i = 0;
                    i1 = 0;

                }

                using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    if (checkBox1.Checked == true)
                    { smtp.EnableSsl = true; }
                    else { smtp.EnableSsl = false; }
                    try
                    {
                        smtp.Send(mail);//отправка
                        MessageBox.Show("Сообщение отправлено!", "Успешно");
                        listBox1.Items.Clear();
                        textBox2.Text = "";
                        textBox3.Text = "";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось отправить", "Ошибка"); // ошибка
                    }



                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            opfd = new OpenFileDialog();
            if (opfd.ShowDialog(this) == DialogResult.OK)
            {
                p[i] = opfd.FileName;
                listBox1.Items.Add(p[i]);
                p1 = 1;
                i++;
                i1++;
            }
        }
    }
}
