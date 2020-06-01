using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Reflection;

namespace Don_3000
{
    public partial class FormMain : Form
    {
        string Wopr, DBcon;
        OleDbDataAdapter dan;
        OleDbConnection connect;
        DataSet ds;
        int id = -1;
        int id_dolgnosti = -1;

        public FormMain()
        {
            InitializeComponent();

            DBcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data.accdb;Persist Security Info=False;"; //подключение к базе данных
            BindSotrudniki();
            BindDolgnosti();
            dtpDatePriem.Value = DateTime.Now;
        }

        private void BindDolgnosti()
        {
            Wopr = "SELECT * FROM Dolgnosti;";  //запрос на отображение всех полей таблицы
            dan = new OleDbDataAdapter(Wopr, DBcon);
            connect = new OleDbConnection(DBcon);
            connect.Open();
            ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Таблица");
            cbDolgnosti.DataSource = ds.Tables["Таблица"].DefaultView;
            cbDolgnosti.DisplayMember = "nazvaniye";
            cbDolgnosti.ValueMember = "id_dolgnocti";
            connect.Close();
            cbDolgnosti.SelectedIndex = -1;
        }

        private void BindSotrudniki()
        {
            Wopr = "SELECT Sotrudniki.id_sotr, Sotrudniki.familiya, Sotrudniki.imya, Sotrudniki.otchestvo, Sotrudniki.otdel, Dolgnosti.nazvaniye, " +
                "Sotrudniki.zarplata, Sotrudniki.adress, Sotrudniki.telefon, Sotrudniki.data_priema " +
                "FROM Dolgnosti INNER JOIN Sotrudniki ON Dolgnosti.id_dolgnocti = Sotrudniki.id_dolgnosti;";  //запрос на отображение всех полей таблицы
            dan = new OleDbDataAdapter(Wopr, DBcon);
            connect = new OleDbConnection(DBcon);
            connect.Open();
            ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Таблица");
            dataGridView1.DataSource = ds.Tables["Таблица"].DefaultView;
            connect.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                tbxSelection.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim() + " " + dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim() + " " + dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim();
            }
            else
            {
                id = -1;
                tbxSelection.Text = string.Empty;
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (id != -1)
            {
                DeleteData(id);
                BindSotrudniki();
            }
            else
                MessageBox.Show("Вы не выбрали запись для удаления из базы!");
        }

        private void DeleteData(int _id)
        {
            Wopr = "Delete * from Sotrudniki where id_sotr = " + _id.ToString() + ";";  
            dan = new OleDbDataAdapter(Wopr, DBcon);
            connect = new OleDbConnection(DBcon);
            connect.Open();
            try
            {
                ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Удаление");
                connect.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления записи! Возможно на эту запись ссылаются другие записи базы данных!");
                connect.Close();
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (tbxFam.Text.Trim() != string.Empty && tbxIm.Text.Trim() != string.Empty && tbxOt.Text.Trim() != string.Empty &&
                tbxAdress.Text.Trim() != string.Empty && tbxTelefon.Text.Trim() != string.Empty && tbxZarplata.Text.Trim() != string.Empty && cbOtdel.Text.Trim() != string.Empty &&
                cbDolgnosti.SelectedIndex != -1)
            {
                InsertData();
                BindSotrudniki();
                tbxFam.Text = string.Empty;
                tbxIm.Text = string.Empty;
                tbxOt.Text = string.Empty;
                tbxAdress.Text = string.Empty;
                tbxTelefon.Text = string.Empty;
                tbxZarplata.Text = string.Empty;
                dtpDatePriem.Value = DateTime.Now;
                cbOtdel.Text = string.Empty;
                cbDolgnosti.SelectedIndex = -1;
            }
            else
                MessageBox.Show("Вы не все поля заполнили!");
        }

        private void InsertData()
        {
            Wopr = "Insert into Sotrudniki(familiya, imya, otchestvo, otdel, id_dolgnosti, zarplata, adress, telefon, data_priema) " +
                "values('" + tbxFam.Text.Trim() + "', '" + tbxIm.Text.Trim() + "', '" + tbxOt.Text.Trim() + "', '" +
                cbOtdel.Text.Trim() + "', " + id_dolgnosti.ToString() + ", '" + tbxZarplata.Text.Trim() + "', '" +
                tbxAdress.Text.Trim() + "', '" + tbxTelefon.Text.Trim() + "', '" + dtpDatePriem.Value.ToString("yyyy-MM-dd") + "');";  //запрос на добавление
            dan = new OleDbDataAdapter(Wopr, DBcon);
            connect = new OleDbConnection(DBcon);
            connect.Open();
            try
            {
                ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Добавление");
                connect.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения данных. Проверьте корректность ввода данных!");
                connect.Close();
            }
        }

        private void CbDolgnosti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDolgnosti.SelectedIndex != -1)
            {
                DataRow selectedDataRow = ((DataRowView)cbDolgnosti.SelectedItem).Row;
                id_dolgnosti = Convert.ToInt32(selectedDataRow["id_dolgnocti"]);
            }
            else
                id_dolgnosti = -1;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (id_dolgnosti != -1)
            {
                Wopr = "SELECT Sotrudniki.id_sotr, Sotrudniki.familiya, Sotrudniki.imya, Sotrudniki.otchestvo, Sotrudniki.otdel, Dolgnosti.nazvaniye, " +
                "Sotrudniki.zarplata, Sotrudniki.adress, Sotrudniki.telefon, Sotrudniki.data_priema " +
                    "FROM Dolgnosti INNER JOIN Sotrudniki ON Dolgnosti.id_dolgnocti = Sotrudniki.id_dolgnosti " +
                    "WHERE Sotrudniki.id_dolgnosti = " + id_dolgnosti.ToString() + ";";  //запрос на отображение всех полей таблицы
                dan = new OleDbDataAdapter(Wopr, DBcon);
                connect = new OleDbConnection(DBcon);
                connect.Open();
                ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Таблица");
                dataGridView1.DataSource = ds.Tables["Таблица"].DefaultView;
                connect.Close();
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            int sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox2.Text = sum.ToString() + " руб/мес";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SaveTable(dataGridView1);
        }

        void SaveTable(DataGridView Save_Students)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Сотрудники предприятия";

            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int i = 1; i < dataGridView1.RowCount + 1; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value;
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        private void ОтделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPocht formPocht = new FormPocht();
            formPocht.Show();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }

        private void ПомощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Help.chm");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tbxFam.Text = null;
            tbxIm.Text = null;
            tbxOt.Text = null;
            tbxTelefon.Text = null;
            tbxZarplata.Text = null;
            tbxAdress.Text = null;
            dtpDatePriem.Text = "";
            cbOtdel.Text = null;
            cbDolgnosti.Text = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
