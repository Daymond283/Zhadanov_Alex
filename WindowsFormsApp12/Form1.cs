using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string con = "server=localhost;port=3306;user=root;database=курсачь(не трогать!!!!!!!);password=1;charset=utf8";
        DataTable dt;
        MySqlDataAdapter ad;
        MySqlConnection conn = new MySqlConnection(con);
        int flag;
        string zapros;
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            flag = 1;
            zapros = "SELECT " +
            "`обращение`.`Дата обращение`, " +
            "`граждане`.`ФИО` AS `Гражданин`, " +
            "`граждане`.`телефон`, "+
            "`граждане`.`адрес`, "+
            "`обращение`.`Номер талона`, "+
            "`персонал`.`ФИО` AS `Сотрудник`, " +
            "`услуги`.`название` AS `Услуга`, " +
            "`документы`.`название` AS `Требуемый документ` " +
            "FROM " +
            "`обращение` " +
            "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
            "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
            "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
            "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id";
            label2.Text = "Поиск по дате";
            ad = new MySqlDataAdapter(zapros, conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Граждане", "Сотрудники", "Дата обращения" });
            comboBox1.SelectedIndex = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            flag = 2;
            ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане`", conn);
            label2.Text = "Поиск по фио";
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "ФИО", "Телефон", "Адрес" });
            comboBox1.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            if (flag == 2 && comboBox1.SelectedIndex == 0)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане` " +
                "WHERE `граждане`.`ФИО` LIKE \"%" + textBox1.Text + "%\" ", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (flag == 3 && comboBox1.SelectedIndex == 0)
            {
                zapros = "SELECT " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`№ окна`, " +
                "`услуги`.`срок` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` " +
                "WHERE `услуги`.`название` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (flag == 4 && comboBox1.SelectedIndex == 0)
            {
                zapros = "SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон`, " +
                "`персонал`.`№ рабочего окна` " +
                "FROM " +
                "`персонал` " +
                "WHERE `персонал`.`ФИО` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 0 && flag == 1)
            {
                zapros = "SELECT " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`документы`.`название` AS `Требуемый документ`, " +
                "`обращение`.`Дата обращение` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id " +
                "WHERE `граждане`.`ФИО` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 1)
            {
                zapros = "SELECT " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`документы`.`название` AS `Требуемый документ`, " +
                "`обращение`.`Дата обращение` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id " +
                "WHERE `персонал`.`ФИО` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 1)
            {
                zapros = "SELECT " +
                "`обращение`.`Дата обращение`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`документы`.`название` AS `Требуемый документ` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id " +
                "WHERE `обращение`.`Дата обращение` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`телефон`, " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане` " +
                "WHERE `граждане`.`телефон` LIKE \"%" + textBox1.Text + "%\" ", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`адрес`, " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон` " +
                "FROM " +
                "`граждане` " +
                "WHERE `граждане`.`адрес` LIKE \"%" + textBox1.Text + "%\" ", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 3)
            {
                zapros = "SELECT " +
                "`услуги`.`№ окна`, " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`срок` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` " +
                "WHERE `услуги`.`№ окна` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 4)
            {
                zapros = "SELECT " +
                "`персонал`.`телефон`, " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`№ рабочего окна` " +
                "FROM " +
                "`персонал` " +
                "WHERE `персонал`.`телефон` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 4)
            {
                zapros = "SELECT " +
                "`персонал`.`№ рабочего окна`, " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон` " +
                "FROM " +
                "`персонал` " +
                "WHERE `персонал`.`№ рабочего окна` LIKE \"%" + textBox1.Text + "%\" ";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            if (comboBox1.SelectedIndex == 0 && flag == 1)
            {
                zapros = "SELECT " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`документы`.`название` AS `Требуемый документ`, " +
                "`обращение`.`Дата обращение` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id";
                label2.Text = "Поиск по гражданам";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 1)
            {
                zapros = "SELECT " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`документы`.`название` AS `Требуемый документ`, " +
                "`обращение`.`Дата обращение` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id";
                label2.Text = "Поиск по сотрудникам";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 1)
            {
                zapros = "SELECT " +
                "`обращение`.`Дата обращение`, " +
                "`услуги`.`название` AS `Услуга`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес`, " +
                "`обращение`.`Номер талона`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`документы`.`название` AS `Требуемый документ` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id";
                label2.Text = "Поиск по дате обращения";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 0 && flag == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане`", conn);
                label2.Text = "Поиск по гражданам";
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`телефон`, " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане`", conn);
                label2.Text = "Поиск по телефону";
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`адрес`, " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон` " +
                "FROM " +
                "`граждане`", conn);
                label2.Text = "Поиск по адресу";
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 0 && flag == 3)
            {
                zapros = "SELECT " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`№ окна`, " +
                "`услуги`.`срок` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` ";
                label2.Text = "Поиск по услуге";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 3)
            {
                zapros = "SELECT " +
                "`услуги`.`№ окна`, " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`срок` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` ";
                label2.Text = "Поиск по № окна";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 0 && flag == 4)
            {
                zapros = "SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон`, " +
                "`персонал`.`№ рабочего окна` " +
                "FROM " +
                "`персонал`";
                label2.Text = "Поиск по фио";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 1 && flag == 4)
            {
                zapros = "SELECT " +
                "`персонал`.`телефон`, " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`№ рабочего окна` " +
                "FROM " +
                "`персонал`";
                label2.Text = "Поиск по телефону";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (comboBox1.SelectedIndex == 2 && flag == 4)
            {
                zapros = "SELECT " +
                "`персонал`.`№ рабочего окна`, " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон` " +
                "FROM " +
                "`персонал`";
                label2.Text = "Поиск по № рабочего окна";
                ad = new MySqlDataAdapter(zapros, conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            flag = 3;
            zapros = "SELECT " +
            "`услуги`.`название` AS `Услуги`, " +
            "`услуги`.`№ окна`, " +
            "`услуги`.`срок` " +
            "FROM " +
            "`услуги` " +
            "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` ";
            label2.Text = "Поиск по услуге";
            ad = new MySqlDataAdapter(zapros, conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Услуги", "№ окна" });
            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = 0;
            flag = 4;
            zapros = "SELECT " +
            "`персонал`.`ФИО`, " +
            "`персонал`.`телефон`, " +
            "`персонал`.`№ рабочего окна` " +
            "FROM " +
            "`персонал`";
            label2.Text = "Поиск по фио";
            ad = new MySqlDataAdapter(zapros, conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "ФИО", "Телефон", "№ рабочего окна" });
            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //DataGridViewCell cel = null;
            //foreach (DataGridViewCell sel in dataGridView1.SelectedCells)
            //{
            //    cel = sel;
            //    break;
            //}

            //if (cel == null && flag==1)
            //{
            //    label3.Visible = true;
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cel = null;
            foreach (DataGridViewCell sel in dataGridView1.SelectedCells)
            {
                cel = sel;
                break;
            }

            if (cel == null && flag == 1)
            {
                label3.Visible = true;
            }
        }
    }
}