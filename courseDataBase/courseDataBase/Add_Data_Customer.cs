using courseDataBase.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace courseDataBase
{
    public partial class Add_Data_Customer : Form
    {
        DataBase dataBase = new DataBase();

        public Add_Data_Customer()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void BUT_Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.openConnection();


            var Nname = textBox_bus_BRAND.Text;
            var LastName = textBox_bus_STATENUMBER.Text;
            var PhoneNumber = textBox_bus_SEATS.Text; 



            // создаём соединение с базой данных
            dataBase.openConnection();

            // создаём запрос на отправку данных в БД
            var querystring = "INSERT INTO customer (Nname, LastName, PhoneNumber ) " +
                              "VALUES (@Nname, @LastName, @PhoneNumber  )";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@Nname", Nname);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber); 

             

            // выполнение команды на добавление записи в БД
            command.ExecuteNonQuery();

            // закрытие соединения с базой данных
            dataBase.closeConnection();

            MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
