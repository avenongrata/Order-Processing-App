using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Yustitskii_Kirill_App
{
    public partial class Form1 : Form
    {
        string ConnStr = @"Data Source=КИРИЛЛ-ПК;Initial Catalog=Флейта;Integrated Security=True";      // connect database

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FillClient();
                FillOrder();
                MessageBox.Show("Connected");
            }
            catch (Exception exep)
            {
                MessageBox.Show("Doesn't connect! " + exep.Message);
            }

        }

        private void FillClient()
        {
            string SqlText = "SELECT * FROM [Клиент]";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Клиент]");
            dataGridView1.DataSource = ds.Tables["[Клиент]"].DefaultView;
        }

        private void FillOrder()
        {
            // сформировать строку SQL-запроса
            string SqlText = "SELECT * FROM [Заказ]";
            int index;
            string order_id;
            index = dataGridView1.CurrentRow.Index;
            order_id = dataGridView1[0, index].Value.ToString();
            SqlText = "SELECT * FROM [Заказ],[Клиент] WHERE (([Заказ].[id_client] = ";
            SqlText = SqlText + order_id + ") AND ([Клиент].[id_client] = " + order_id + "))";
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Заказ]");
            dataGridView2.DataSource = ds.Tables["[Заказ]"].DefaultView;
        }

        // method for changing data in tables
        public void MyExecuteNonQuery(string SqlText)
        {
            SqlConnection cn; 
            SqlCommand cmd;

            cn = new SqlConnection(ConnStr);
            cn.Open(); 
            cmd = cn.CreateCommand(); 
            cmd.CommandText = SqlText; 
            cmd.ExecuteNonQuery(); 
            cn.Close(); 
        }

        // ############################## CLIENT ##############################

        // add client
        private void button1_Click(object sender, EventArgs e)
        {
            string SqlText = "INSERT INTO [Клиент] ([id_client],[ФИО]," +
                "[Реквизиты],[Телефон],[Паспорт],[Адрес_регистрации]," +
                "[Дата_рождения]) " +
                "VALUES (1, 'ФИО-01','Реквизиты-01','Телефон-01','Паспорт-01'," +
                "'Адрес_регистраци-01','Дата_рождения-01'";
            Form2 f = new Form2(); // создать экземпляр окна
            if (f.ShowDialog() == DialogResult.OK)
            {
                // сформировать SQL-строку
                SqlText = "INSERT INTO [Клиент] ([id_client],[ФИО]," +
                "[Реквизиты],[Телефон],[Паспорт],[Адрес_регистрации]," +
                "[Дата_рождения]) VALUES (";
                SqlText = SqlText + "\'" + f.textBox1.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox2.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox3.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox4.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox5.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox6.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox7.Text + "\', ";
                // выполнить SQL-команду
                MyExecuteNonQuery(SqlText);
                FillClient();
            }
        }

        // change client
        private void button3_Click(object sender, EventArgs e)
        {
            int index, n;
            string SqlText = "UPDATE [Клиент] SET ";
            string client_id, client_name, client_requisites, client_phone,
            client_passport, client_adds, client_birthday;
            n = dataGridView1.Rows.Count;
            if (n == 1) return;
            Form3 f = new Form3();
            // заполнить форму данными перед открытием
            index = dataGridView1.CurrentRow.Index;
            client_id = dataGridView1[0, index].Value.ToString();
            client_name = dataGridView1[1, index].Value.ToString();
            client_requisites = dataGridView1[2, index].Value.ToString();
            client_phone = dataGridView1[3, index].Value.ToString();
            client_passport = dataGridView1[4, index].Value.ToString();
            client_adds = dataGridView1[5, index].Value.ToString();
            client_birthday = dataGridView1[6, index].Value.ToString();
            f.textBox1.Text = client_id;
            f.textBox2.Text = client_name;
            f.textBox3.Text = client_requisites;
            f.textBox4.Text = client_phone;
            f.textBox5.Text = client_passport;
            f.textBox6.Text = client_adds;
            f.textBox7.Text = client_birthday;

            if (f.ShowDialog() == DialogResult.OK)
            {
                client_id = f.textBox1.Text;
                client_name = f.textBox2.Text;
                client_requisites = f.textBox3.Text;
                client_phone = f.textBox4.Text;
                client_passport = f.textBox5.Text;
                client_adds = f.textBox6.Text;
                client_birthday = f.textBox7.Text;

                SqlText += "id_client = \'" + client_id +
                "\', ФИО = '" + client_name +
                "\', Реквизиты = '" + client_requisites +
                "\', Телефон = '" + client_phone +
                "\', Паспорт = '" + client_passport +
                "\', Адрес_регистрации = '" + client_adds +
                "\', Дата_рождения = '" + client_birthday + "\'";
                SqlText += "WHERE [Клиент].[id_client] = " + client_id;
                MyExecuteNonQuery(SqlText);
                FillClient();

            }
        }

        // delete client
        private void button2_Click(object sender, EventArgs e)
        {
            int index, n;
            string client_id, client_name, client_birthday;
            string SqlText = "DELETE FROM [Клиент] WHERE [Клиент].[id_client] = ";
            n = dataGridView1.Rows.Count;
            if (n == 1) return;
            Form4 f = new Form4();
            index = dataGridView1.CurrentRow.Index;
            client_id = Convert.ToString(dataGridView1[0, index].Value);
            // сформировать SQL-команду
            SqlText = SqlText + client_id;
            // заполнить информационную справку в окне Form5
            client_name = Convert.ToString(dataGridView1[1, index].Value);
            client_birthday = Convert.ToString(dataGridView1[7, index].Value);
            f.label2.Text = client_id + " - " + client_name + " - " + client_birthday;
            if (f.ShowDialog() == DialogResult.OK) // вывести форму
            {
                // выполнить SQL-команду
                MyExecuteNonQuery(SqlText);
                FillClient();
            }
        }


        // ############################## ORDER ##############################

        // add order 
        private void button4_Click(object sender, EventArgs e)
        {
            string SqlText = "INSERT INTO [Заказ] ([id_zakaza],[id_client]," +
                "[id_tovara],[id_spezialista],[Статус],[Количество_позиций]) " +
                "VALUES (1, '1','1','1','Статус-01'," +
                "'Количество_позиций-01'";
            Form5 f = new Form5(); // создать экземпляр окна
            if (f.ShowDialog() == DialogResult.OK)
            {
                // сформировать SQL-строку
                SqlText = "INSERT INTO [Заказ] ([id_zakaza],[id_client]," +
                "[id_tovara],[id_spezialista],[Статус],[Количество_позиций]) VALUES (";
                SqlText = SqlText + "\'" + f.textBox1.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox2.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox3.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox4.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox5.Text + "\', ";
                SqlText = SqlText + "\'" + f.textBox6.Text + "\', ";
                // выполнить SQL-команду
                MyExecuteNonQuery(SqlText);
                FillOrder();
            }
        }

        // change order
        private void button6_Click(object sender, EventArgs e)
        {
            int index, n;
            string SqlText = "UPDATE [Заказ] SET ";
            string order_id, client_id, product_id, specialist_id,
            status, positions_count;
            n = dataGridView2.Rows.Count;
            if (n == 1) return;
            Form6 f = new Form6();
            // заполнить форму данными перед открытием
            index = dataGridView2.CurrentRow.Index;
            order_id = dataGridView2[0, index].Value.ToString();
            client_id = dataGridView2[1, index].Value.ToString();
            product_id = dataGridView2[2, index].Value.ToString();
            specialist_id = dataGridView2[3, index].Value.ToString();
            status = dataGridView2[4, index].Value.ToString();
            positions_count = dataGridView2[5, index].Value.ToString();
            f.textBox1.Text = order_id;
            f.textBox2.Text = client_id;
            f.textBox3.Text = product_id;
            f.textBox4.Text = specialist_id;
            f.textBox5.Text = status;
            f.textBox6.Text = positions_count;

            if (f.ShowDialog() == DialogResult.OK)
            {
                order_id = f.textBox1.Text;
                client_id = f.textBox2.Text;
                product_id = f.textBox3.Text;
                specialist_id = f.textBox4.Text;
                status = f.textBox5.Text;
                positions_count = f.textBox6.Text;

                SqlText += "id_zakaza = \'" + order_id +
                "\', id_client = '" + client_id +
                "\', id_tovara = '" + product_id +
                "\', id_spezialista = '" + specialist_id +
                "\', Статус = '" + status +
                "\', Количество_позиций = '" + positions_count + "\'";
                SqlText += "WHERE [Заказ].[id_zakaza] = " + order_id;
                MyExecuteNonQuery(SqlText);
                FillOrder();
            }
        }

        // delete order
        private void button5_Click(object sender, EventArgs e)
        {
            int index, n;
            string order_id;
            string positions_count, status;
            string SqlText = "DELETE FROM [Заказ] WHERE [Заказ].[id_zakaza] = ";
            n = dataGridView2.Rows.Count;
            if (n == 1) return;
            Form7 f = new Form7();
            index = dataGridView2.CurrentRow.Index;
            order_id = Convert.ToString(dataGridView2[0, index].Value);
            // сформировать SQL-команду
            SqlText = SqlText + order_id;
            // заполнить информационную справку в окне Form5
            positions_count = Convert.ToString(dataGridView2[5, index].Value);
            status = Convert.ToString(dataGridView2[4, index].Value);
            f.label2.Text = order_id + " - " + positions_count + " - " + status;
            if (f.ShowDialog() == DialogResult.OK) // вывести форму
            {
                // выполнить SQL-команду
                MyExecuteNonQuery(SqlText);
                FillOrder();
            }
        }

        // list of specialist
        private void button7_Click(object sender, EventArgs e)
        {
            string SqlText = "SELECT * FROM [Специалист]";
            Form8 f = new Form8();
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Специалист]");
            f.dataGridView1.DataSource = ds.Tables["[Специалист]"].DefaultView;
            f.ShowDialog();
        }

        // information about order
        private void button8_Click(object sender, EventArgs e)
        {
            string SqlText = "SELECT * FROM [Доставка]";
            Form9 f = new Form9();
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Доставка]");
            f.dataGridView1.DataSource = ds.Tables["[Доставка]"].DefaultView;
            f.ShowDialog();
        }

        // count of positions
        private void button9_Click(object sender, EventArgs e)
        {
            // SQL-запрос - определяет максимальные выбросы для любого источника
            string SqlText = "SELECT [Заказ].[id_client], MAX([Заказ].[Количество_позиций]) AS \'Количество_позиций\'" +
            "FROM [Заказ]" +
            "GROUP BY [Заказ].[id_client]";
            Form10 f = new Form10();
            SqlDataAdapter da = new SqlDataAdapter(SqlText, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Заказ]");
            f.dataGridView1.DataSource = ds.Tables["[Заказ]"].DefaultView;
            f.ShowDialog();
        }

        // click on dataGridView1
        private void DataGridView1_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.RowCount;
            int row = dataGridView1.CurrentRow.Index;
            if (n != (row + 1)) // Проверка, был ли клик на последней строке
                FillOrder();
        }

        // Change the active cell in dataGridView1
        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.RowCount;
            int row = dataGridView1.CurrentRow.Index;
            if (n != (row + 1)) // Проверка, был ли клик на последней строке
                FillOrder();
        }

        // unused
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
