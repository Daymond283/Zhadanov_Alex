using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace запрос_курсовой
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string con = "server=localhost;port=3306;user=root;database=курсачь(не трогать!!!!!!!);password=1;charset=utf8";
        DataTable dt;
        MySqlDataAdapter ad;
        MySqlConnection conn;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
            "`услуги`.`название` AS `Услуга`, " +
            "`персонал`.`ФИО` AS `Сотрудник`, " +
            "`граждане`.`ФИО` AS `Гражданин`, " +
            "`документы`.`название` AS `Требуемый документ`, " +
            "`обращение`.`Дата обращение` " +
            "FROM " +
            "`обращение` " +
            "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
            "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
            "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
            "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Граждане g = new Граждане();
            Hide();
            g.ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Услуги y = new Услуги();
            Hide();
            y.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Персонал p = new Персонал();
            Hide();
            p.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Документы d = new Документы();
            Hide();
            d.ShowDialog();
            Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Обращение o = new Обращение();
            Hide();
            o.ShowDialog();
            Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            if (tabControl1.SelectedIndex == 0)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`услуги`.`название` AS `Услуга`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`документы`.`название` AS `Требуемый документ` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон`, " +
                "`граждане`.`адрес` " +
                "FROM " +
                "`граждане`", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView2.DataSource = dt.DefaultView;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон`, " +
                "`персонал`.`№ рабочего окна` " +
                "FROM " +
                "`персонал`", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView3.DataSource = dt.DefaultView;
            }
            if (tabControl1.SelectedIndex == 3)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`срок` AS `Срок выполнения` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна`", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView4.DataSource = dt.DefaultView;
            }
            if (tabControl1.SelectedIndex == 4)
            {
                ad = new MySqlDataAdapter("SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`№ рабочего окна`, " +
                "`услуги`.`название` AS `Услуга` " +
                "FROM " +
                "`персонал` " +
                "INNER JOIN `услуги` ON `услуги`.`№ окна` = `персонал`.`№ рабочего окна`", conn);
                dt = new DataTable();
                ad.Fill(dt);
                dataGridView5.DataSource = dt.DefaultView;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
                "`услуги`.`название` AS `Услуга`, " +
                "`персонал`.`ФИО` AS `Сотрудник`, " +
                "`граждане`.`ФИО` AS `Гражданин`, " +
                "`документы`.`название` AS `Требуемый документ` " +
                "FROM " +
                "`обращение` " +
                "INNER JOIN `граждане` ON `обращение`.`id гражданина` = `граждане`.id  " +
                "INNER JOIN `документы` ON `обращение`.`id документа` = `документы`.id " +
                "INNER JOIN `персонал` ON `обращение`.`id сотрудника` = `персонал`.id " +
                "INNER JOIN `услуги` ON `обращение`.`id услуги` = `услуги`.id " +
                "WHERE `услуги`.`название` LIKE \"%" + textBox1.Text + "%\" " +
                "OR `персонал`.`ФИО` LIKE \"%" + textBox1.Text + "%\" " +
                "OR `граждане`.`ФИО` LIKE \"%" + textBox1.Text + "%\" " +
                "OR `документы`.`название` LIKE \"%" + textBox1.Text + "%\" ", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
                "`граждане`.`ФИО`, " +
                "`граждане`.`телефон`, " +
            "`граждане`.`адрес` " +
            "FROM " +
                "`граждане` " +
                "WHERE `граждане`.`телефон` LIKE \"%" + textBox2.Text + "%\" " +
                "OR `граждане`.`адрес` LIKE \"%" + textBox2.Text + "%\" " +
                "OR `граждане`.`ФИО` LIKE \"%" + textBox2.Text + "%\" ", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView2.DataSource = dt.DefaultView;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`телефон`, " +
                "`персонал`.`№ рабочего окна` " +
            "FROM " +
                "`персонал` " +
                "WHERE `персонал`.`ФИО` LIKE \"%" + textBox3.Text + "%\" " +
            "OR `персонал`.`телефон` LIKE \"%" + textBox3.Text + "%\" " +
                "OR `персонал`.`№ рабочего окна` LIKE \"%" + textBox3.Text + "%\" ", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView3.DataSource = dt.DefaultView;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
                "`услуги`.`название` AS `Услуги`, " +
                "`услуги`.`срок` AS `Срок выполнения` " +
                "FROM " +
                "`услуги` " +
                "INNER JOIN `персонал` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` " +
                "WHERE `услуги`.`название` LIKE \"%" + textBox4.Text + "%\" " +
                "OR `услуги`.`срок` LIKE \"%" + textBox4.Text + "%\" ", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView4.DataSource = dt.DefaultView;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(con);
            ad = new MySqlDataAdapter("SELECT " +
                "`персонал`.`ФИО`, " +
                "`персонал`.`№ рабочего окна`, " +
                "`услуги`.`название` AS `Услуга` " +
                "FROM " +
                "`персонал` " +
                "INNER JOIN `услуги` ON `персонал`.`№ рабочего окна` = `услуги`.`№ окна` " +
                "WHERE `персонал`.`ФИО` LIKE \"%" + textBox5.Text + "%\" " +
            "OR `услуги`.`название` LIKE \"%" + textBox5.Text + "%\" " +
                "OR `персонал`.`№ рабочего окна` LIKE \"%" + textBox5.Text + "%\" ", conn);
            dt = new DataTable();
            ad.Fill(dt);
            dataGridView5.DataSource = dt.DefaultView;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
