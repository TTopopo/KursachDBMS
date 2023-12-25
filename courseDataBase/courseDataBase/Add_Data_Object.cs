using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using courseDataBase.Connection;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data;

namespace courseDataBase
{
    public partial class Add_Data_Object : Form
    {
        DataBase dataBase = new DataBase();
        OpenFileDialog openFileDialog = new OpenFileDialog();

        public Add_Data_Object()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void BUT_Save_Click(object sender, EventArgs e)
        {
            // создаём соединение с db
            dataBase.openConnection();


            var Title = textBox_title.Text;
            var Addresss = textBox_startdate.Text;
            var Typee = textBox_completiondate.Text;
            var Statuss = textBox_titlestage.Text;



            // создаём соединение с базой данных
            dataBase.openConnection();

            // создаём запрос на отправку данных в БД
            var querystring = "INSERT INTO oobject (Title, Addresss, Typee,Statuss, PhotoBefore, PhotoAfter) " +
                              "VALUES (@Title, @Addresss, @Typee, @Statuss, @PhotoBefore, @PhotoAfter)";

            // создание экземпляра команды и передача строки запроса и соединения
            var command = new SqlCommand(querystring, dataBase.getConnection());
            // добавление параметров в командуe());

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Addresss", Addresss);
            command.Parameters.AddWithValue("@Typee", Typee);
            command.Parameters.AddWithValue("@Statuss", Statuss);

            // добавление параметра изображения
            if (pictureBox1_before.Image != null)
            {
                var photo = new Bitmap(pictureBox1_before.Image);
                using (var memoryStream1 = new MemoryStream())
                {
                    photo.Save(memoryStream1, ImageFormat.Jpeg);
                    memoryStream1.Position = 0;
                    command.Parameters.AddWithValue("@PhotoBefore", memoryStream1.ToArray());
                }

            }
            else
            {
                command.Parameters.AddWithValue("@PhotoBefore", SqlDbType.VarBinary).Value = DBNull.Value; 
            }
            if (pictureBox2_after.Image != null)
            {
                var photo2 = new Bitmap(pictureBox2_after.Image);
                using (var memoryStream2 = new MemoryStream())
                {
                    photo2.Save(memoryStream2, ImageFormat.Jpeg);
                    memoryStream2.Position = 0;
                    command.Parameters.AddWithValue("@PhotoAfter", memoryStream2.ToArray());
                }
            } else
            {
                command.Parameters.AddWithValue("@PhotoAfter", SqlDbType.VarBinary).Value = DBNull.Value;
            }



            // выполнение команды на добавление записи в БД
            command.ExecuteNonQuery();

            // закрытие соединения с базой данных
            dataBase.closeConnection();

            MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void InsertImageBefore()
        {
            pictureBox1_before.SizeMode = PictureBoxSizeMode.StretchImage;

            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1_before.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void InsertImageAfter()
        {
            pictureBox2_after.SizeMode = PictureBoxSizeMode.StretchImage;

            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2_after.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InsertImageBefore();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertImageAfter();

        }
    }
}
