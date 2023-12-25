using courseDataBase.Connection;
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

namespace courseDataBase
{
    public partial class Add_Data_Step : Form
    {
        DataBase dataBase = new DataBase();
        public Add_Data_Step()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void BUT_Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.openConnection();


            var StartDate = textBox_FIO.Text;
            var СompletionDate = textBox_PHONE.Text;
            var TitleStep = textBox_SALARY.Text;



            // создаём соединение с базой данных
            dataBase.openConnection();

            // создаём запрос на отправку данных в БД
            var querystring = "INSERT INTO step_construction (StartDate, СompletionDate, TitleStep  ) " +
                              "VALUES (@StartDate, @СompletionDate, @TitleStep  )";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@СompletionDate", СompletionDate);
            command.Parameters.AddWithValue("@TitleStep", TitleStep);



            // выполнение команды на добавление записи в БД
            command.ExecuteNonQuery();

            // закрытие соединения с базой данных
            dataBase.closeConnection();

            MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
