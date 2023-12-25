using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using courseDataBase.Connection;
using System.Drawing;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;

namespace courseDataBase
{
    public partial class UserPage : Form
    {
        DataBase dataBase = new DataBase();
        Sign_in signIn = new Sign_in();
        int id_Prorab;
        int id_Costomer;
        int selectedRowDatarid1;
        int selectedRowDatarid2;
        int  IdUser;

        public UserPage()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void UserPage_Load(object sender, EventArgs e)
        {
             CreateColumsOobject();
            RefreshDataGridOobject(dataGridView_1);

            CreateColumsForeman();
            RefreshDataGridForeman(dataGridView1);

            CreateColumsWorker();
            RefreshDataGridWorker(dataGridView2);
            CreateColumsWorker1(); 

        }

        private void BUT_BuyTicket_Click(object sender, EventArgs e)
        {   
            id_Prorab = (int)dataGridView1.Rows[selectedRowDatarid1].Cells[0].Value;

            label6.Text = dataGridView1.Rows[selectedRowDatarid1].Cells[1].Value.ToString() + " " + dataGridView1.Rows[selectedRowDatarid1].Cells[2].Value.ToString();

            MessageBox.Show("Прораб добавлен!" );
        }
        private void button2_Click(object sender, EventArgs e)
        {
            id_Costomer = (int)dataGridView2.Rows[selectedRowDatarid2].Cells[0].Value;

            label7.Text = dataGridView2.Rows[selectedRowDatarid2].Cells[1].Value.ToString() + " " + dataGridView2.Rows[selectedRowDatarid2].Cells[2].Value.ToString();
            
            var changeQuery = $"UPDATE worker SET id_Prorab = '{id_Prorab}'    WHERE id = '{id_Costomer}'";

            var command = new SqlCommand(changeQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
            RefreshDataGridWorker(dataGridView2);
            MessageBox.Show("Рабочий добавлен!");

        }
        private void dataGridView_1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             IdUser = e.RowIndex;
            
            string queryStringIdUser = $"SELECT id_Prorab FROM oobject WHERE id_Prorab = '{dataGridView_1.Rows[IdUser].Cells[5].Value.ToString()}'";
            SqlCommand command1 = new SqlCommand(queryStringIdUser, dataBase.getConnection());

            // Выполнение запроса и запись результата в переменную
            id_Prorab = Convert.ToInt32(command1.ExecuteScalar());

            string queryString = $"select Nname, LastName from foreman WHERE id = '{ id_Prorab}'";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            // Создание объекта чтения данных
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Чтение данных из результата запроса
                string name = reader.GetString(0); // Имя
                string lastName = reader.GetString(1); // Фамилия

                // Присвоение данных элементу Label
                label12.Text = name + " " + lastName; 
            }

            // Закрытие объекта чтения данных
            reader.Close();

            dataGridView3.Rows.Clear();

            string sIdUser = $"SELECT * FROM worker WHERE id_Prorab = '{id_Prorab}' ";

            SqlCommand command3 = new SqlCommand(sIdUser, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                ReadSingleRowWorker(dataGridView3, reader3);
            }
            reader3.Close();
        }
        private void dataGridView3_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            var Title = textBox1.Text;
            var Addresss = textBox2.Text;
            var Typee = textBox3.Text;
            var Statuss = "В процессе"; 
            var querystring = "INSERT INTO oobject (Title, Addresss, Typee,Statuss, id_Prorab, id_Costomer,PhotoBefore, PhotoAfter ) " +
                              "VALUES (@Title, @Addresss, @Typee, @Statuss, @id_Prorab, @id_Costomer, @PhotoBefore, @PhotoAfter)";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Addresss", Addresss);
            command.Parameters.AddWithValue("@Typee", Typee);
            command.Parameters.AddWithValue("@Statuss", Statuss);
            command.Parameters.AddWithValue("@id_Prorab", id_Prorab);
            command.Parameters.AddWithValue("@id_Costomer", id_Costomer);
            command.Parameters.Add("@PhotoBefore", SqlDbType.VarBinary).Value = DBNull.Value;
            command.Parameters.Add("@PhotoAfter", SqlDbType.VarBinary).Value = DBNull.Value;
            command.ExecuteNonQuery();
            MessageBox.Show("Объект добавлен!");

            RefreshDataGridOobject(dataGridView_1);
        }
        private void BUT_Refresh_Click(object sender, EventArgs e)// обновление данных
        {
            RefreshDataGridForeman(dataGridView1);
            RefreshDataGridWorker(dataGridView2);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridOobject(dataGridView_1);
        }
        private void CreateColumsOobject() // инициализация столбцов для dataGridView_route
        {
            dataGridView_1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView_1.Columns.Add("id", "id");
            dataGridView_1.Columns.Add("Title", "Название");
            dataGridView_1.Columns.Add("Addresss", "Адрес");
            dataGridView_1.Columns.Add("Typee", "Тип");
            dataGridView_1.Columns.Add("Statuss", "Статус");
            dataGridView_1.Columns.Add("id_Prorab", "id Прораб");
            dataGridView_1.Columns.Add("id_Costomer", "id Заказчик");
            dataGridView_1.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumsForeman() // инициализация столбцов для dataGridView_control
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Nname", "Имя");
            dataGridView1.Columns.Add("LastName", "Фамилия");
            dataGridView1.Columns.Add("Qualification", "Квалификация");
            dataGridView1.Columns.Add("Specialization", "Специализация");
            dataGridView1.Columns.Add("Skills", "Навыки");
            dataGridView1.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumsWorker() // инициализация столбцов для dataGridView_control
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.Columns.Add("id", "id");
            dataGridView2.Columns.Add("Nname", "Имя");
            dataGridView2.Columns.Add("LastName", "Фамилия");
            dataGridView2.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView2.Columns.Add("Position", "Должность");
            dataGridView2.Columns.Add("Experience", "Опыт");
            dataGridView2.Columns.Add("id_Prorab", "id Прораб");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void CreateColumsWorker1() // инициализация столбцов для dataGridView3 
        {
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView3.Columns.Add("id", "id");
            dataGridView3.Columns.Add("Nname", "Имя");
            dataGridView3.Columns.Add("LastName", "Фамилия");
            dataGridView3.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView3.Columns.Add("Position", "Должность");
            dataGridView3.Columns.Add("Experience", "Опыт");
            dataGridView3.Columns.Add("id_Prorab", "id Прораб");
            dataGridView3.Columns.Add("IsNew", String.Empty);
        }

        private void RefreshDataGridOobject(DataGridView dgw_oobject)//объект
        {
            dgw_oobject.Rows.Clear();

            string queryString = $"select * from oobject";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowOobject(dgw_oobject, reader);
            }
            reader.Close();
        }

        private void RefreshDataGridForeman(DataGridView dgw_foreman) //Прораб
        {
            dgw_foreman.Rows.Clear();

            string queryString = $"select * from foreman";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowForeman(dgw_foreman, reader);
            }
            reader.Close();
        }
        private void RefreshDataGridWorker(DataGridView dgw_worker)
        {
            dgw_worker.Rows.Clear();

            string queryString = $"select * from worker";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowWorker(dgw_worker, reader);
            }
            reader.Close();
        }


        private void ReadSingleRowOobject(DataGridView dgw_oobject, IDataRecord record)//объект
        {
            int? value5 = null;
            int? value6 = null;
            if (!record.IsDBNull(5))
            {
                value5 = record.GetInt32(5);
            }
            if (!record.IsDBNull(6))
            {
                value6 = record.GetInt32(6);
            }

            dgw_oobject.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), value5, value6, RowState.ModfieldNew);
        }
        private void ReadSingleRowForeman(DataGridView dgw_foreman, IDataRecord record)//Прораб
        {
            dgw_foreman.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), RowState.ModfieldNew);
        }

        private void ReadSingleRowWorker(DataGridView dgw_worker, IDataRecord record)//рабочий   
        {
            int? value6 = null;
            if (!record.IsDBNull(6))
            {
                value6 = record.GetInt32(6);
            }
            dgw_worker.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), value6, RowState.ModfieldNew);
        }

        private void textBox_SEARCH_FROM_TextChanged(object sender, EventArgs e)
        {
            SearchFrom(dataGridView_1);
        }

        private void textBox_SEARCH_WHERE_TextChanged(object sender, EventArgs e)
        {
            SearchWhere(dataGridView_1);
        }

        private void SearchFrom(DataGridView dgw) // поиск "Откуда"
        {
            //dgw.Rows.Clear();
            //string searchString = $"select * from route where (city_start) like '%" + textBox_SEARCH_WHERE.Text + "%'";

            //SqlCommand command = new SqlCommand(searchString, dataBase.getConnection());

            //dataBase.openConnection();

            //SqlDataReader read = command.ExecuteReader();

            //while (read.Read())
            //{
            //    ReadSingleRow(dgw, read);
            //}

            //read.Close();
        }

        private void SearchWhere(DataGridView dgw) // поиск "Куда"
         {
        //    dgw.Rows.Clear();
        //    string searchString = $"select * from route where (city_finish) like '%" + textBox_SEARCH_WHERE.Text + "%'";

        //    SqlCommand command = new SqlCommand(searchString, dataBase.getConnection());

        //    dataBase.openConnection();

        //    SqlDataReader read = command.ExecuteReader();

        //    while (read.Read())
        //    {
        //        ReadSingleRow(dgw, read);
        //    }

        //    read.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_route_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_control_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowDatarid1 = e.RowIndex;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowDatarid2 = e.RowIndex;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_SEARCH_TextChanged(object sender, EventArgs e)
        {
            Search1(dataGridView_1);
        }
        private void Search1(DataGridView dgw_oobject1) // поиск объектf
        {
            dgw_oobject1.Rows.Clear();
            string searchString = $"SELECT * FROM oobject WHERE concat (id, Title, Addresss, Typee, Statuss) like '%" + textBox_SEARCH.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowOobject(dgw_oobject1, read);
            }

            read.Close();
        }
    }
}
