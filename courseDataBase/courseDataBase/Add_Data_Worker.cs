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
    public partial class Add_Data_Worker : Form
    {
        DataBase dataBase = new DataBase();
        public Add_Data_Worker()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void BUT_Save_Click(object sender, EventArgs e)
        {
            {
                // создаём соединение с db
                dataBase.openConnection();


                var Nname = textBox_FIO.Text;
                var LastName = textBox_PHONE.Text;
                var PhoneNumber = textBox_SALARY.Text;
                var Position = textBox_IDBUS.Text;
                var Experience = textBox1.Text;



                // создаём соединение с базой данных
                dataBase.openConnection();

                // создаём запрос на отправку данных в БД
                var querystring = "INSERT INTO worker (Nname, LastName, PhoneNumber, Position, Experience ) " +
                                  "VALUES (@Nname, @LastName, @PhoneNumber, @Position, @Experience  )";

                // создание экземпляра команды и передача строки запроса и соединения
                var command = new SqlCommand(querystring, dataBase.getConnection());
                // добавление параметров в командуe());

                command.Parameters.AddWithValue("@Nname", Nname);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                command.Parameters.AddWithValue("@Position", Position);
                command.Parameters.AddWithValue("@Experience", Experience);



                // выполнение команды на добавление записи в БД
                command.ExecuteNonQuery();

                // закрытие соединения с базой данных
                dataBase.closeConnection();

                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
