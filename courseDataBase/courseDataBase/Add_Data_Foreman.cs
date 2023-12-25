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
using System.Xml.Linq;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace courseDataBase
{
    public partial class Add_Data_Foreman : Form
    {
        DataBase dataBase = new DataBase();
        OpenFileDialog openFileDialog = new OpenFileDialog();

        public Add_Data_Foreman()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void BUT_Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.openConnection();


            var Nname = textBox_FIO.Text;
            var LastName = textBox_PHONE.Text;
            var Qualification = textBox_SALARY.Text;
            var Specialization = textBox_IDBUS.Text;
            var Skills = textBox1.Text;
            var PhoneNumber = textBox2.Text;



            // создаём соединение с базой данных
            dataBase.openConnection();

            // создаём запрос на отправку данных в БД
            var querystring = "INSERT INTO foreman (Nname, LastName, Qualification, Specialization, Skills, PhoneNumber ) " +
                              "VALUES (@Nname, @LastName,@Qualification,@Specialization,@Skills, @PhoneNumber  )";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@Nname", Nname);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Qualification", Qualification);
            command.Parameters.AddWithValue("@Specialization", Specialization);
            command.Parameters.AddWithValue("@Skills", Skills);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber); 


            // выполнение команды на добавление записи в БД
            command.ExecuteNonQuery();

            // закрытие соединения с базой данных
            dataBase.closeConnection();

            MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //private void InsertImage()
        //{
        //    pb_show.SizeMode = PictureBoxSizeMode.StretchImage;

        //    openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        pb_show.Image = Image.FromFile(openFileDialog.FileName);
        //    }
        //}
    }
}

