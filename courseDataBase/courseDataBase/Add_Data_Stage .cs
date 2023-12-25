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
    public partial class Add_Data_Stage : Form
    {
        DataBase dataBase = new DataBase();
        public Add_Data_Stage()
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
            var TitleStage = textBox_SALARY.Text;



            // создаём соединение с базой данных
            dataBase.openConnection();

            // создаём запрос на отправку данных в БД
            var querystring = "INSERT INTO stage_construction (StartDate, СompletionDate, TitleStage  ) " +
                              "VALUES (@StartDate, @СompletionDate, @TitleStage  )";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@СompletionDate", СompletionDate);
            command.Parameters.AddWithValue("@TitleStage", TitleStage);



            // выполнение команды на добавление записи в БД
            command.ExecuteNonQuery();

            // закрытие соединения с базой данных
            dataBase.closeConnection();

            MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
