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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace courseDataBase
{
    public partial class ForemanPage : Form
    {
        int IdUser;
        int id_Prorab;
        int id_Object;
        int selectedRowIdProrab;
        DataBase database = new DataBase();
        public ForemanPage()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();

        private void ForemanPage_Load(object sender, EventArgs e)
        {
            CreateColumsOobject1();
            RefreshDataGridOobject(dataGridView2);
            CreateColumsWorker1();
        }
        private void InsertImageBefore()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void InsertImageAfter()
        {
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            openFileDialog.Filter = "Image Files(*.JPG,*.JPEG,*.PNG)|*.JPG;*.JPEG;*.PNG"; //формат загружаемого файла


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertImageBefore();

            try
            {
                // Предположим, что у вас есть метод для открытия соединения с базой данных
                database.openConnection();

                // Ваш код операции с базой данных

                int objectId = int.Parse(dataGridView2.Rows[selectedRowIdProrab].Cells[0].Value.ToString());
                var querystring = "UPDATE oobject SET PhotoBefore = @PhotoBefore WHERE id = @id";
                var command = new SqlCommand(querystring, database.getConnection());
                command.Parameters.Add("@PhotoBefore", SqlDbType.VarBinary);
                command.Parameters.Add("@id", SqlDbType.Int).Value = objectId;

                if (pictureBox1.Image != null)
                {
                    var photo = new Bitmap(pictureBox1.Image);
                    using (var memoryStream1 = new MemoryStream())
                    {
                        photo.Save(memoryStream1, ImageFormat.Jpeg);
                        memoryStream1.Position = 0;
                        command.Parameters["@PhotoBefore"].Value = memoryStream1.ToArray();
                    }
                }
                else
                {
                    command.Parameters["@PhotoBefore"].Value = DBNull.Value;
                }

                int rowsAffected = command.ExecuteNonQuery();

                // Закрытие соединения после выполнения операции с базой данных
                database.closeConnection();

                MessageBox.Show("Фотография успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если возникли
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            InsertImageAfter();
            try
            {
                // Предположим, что у вас есть метод для открытия соединения с базой данных
                database.openConnection();

                // Ваш код операции с базой данных

                int objectId = int.Parse(dataGridView2.Rows[selectedRowIdProrab].Cells[0].Value.ToString());
                var querystring = "UPDATE oobject SET PhotoAfter = @PhotoAfter WHERE id = @id";
                var command = new SqlCommand(querystring, database.getConnection());
                command.Parameters.Add("@PhotoAfter", SqlDbType.VarBinary);
                command.Parameters.Add("@id", SqlDbType.Int).Value = objectId;

                if (pictureBox3.Image != null)
                {
                    var photo = new Bitmap(pictureBox3.Image);
                    using (var memoryStream1 = new MemoryStream())
                    {
                        photo.Save(memoryStream1, ImageFormat.Jpeg);
                        memoryStream1.Position = 0;
                        command.Parameters["@PhotoAfter"].Value = memoryStream1.ToArray();
                    }
                }
                else
                {
                    command.Parameters["@PhotoAfter"].Value = DBNull.Value;
                }

                int rowsAffected = command.ExecuteNonQuery();

                // Закрытие соединения после выполнения операции с базой данных
                database.closeConnection();

                MessageBox.Show("Фотография успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если возникли
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }
        private void CreateColumsOobject1() // инициализация столбцов для dataGridView_route
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.Columns.Add("id", "id");
            dataGridView2.Columns.Add("Title", "Название");
            dataGridView2.Columns.Add("Addresss", "Адрес");
            dataGridView2.Columns.Add("Typee", "Тип");
            dataGridView2.Columns.Add("Statuss", "Статус");
            dataGridView2.Columns.Add("id_Prorab", "id Прораб");
            dataGridView2.Columns.Add("id_Costomer", "id Заказчик");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void CreateColumsWorker1() // инициализация столбцов для dataGridView4_worker
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Nname", "Имя");
            dataGridView1.Columns.Add("LastName", "Фамилия");
            dataGridView1.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView1.Columns.Add("Position", "Должность");
            dataGridView1.Columns.Add("Experience", "Опыт");
            dataGridView1.Columns.Add("id_Prorab", "id_Prorab");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        

        private void dataGridView2_Click(object sender, EventArgs e) //объект
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdUser = e.RowIndex;
            selectedRowIdProrab = e.RowIndex;

            string queryStringIdUser = $"SELECT id_Prorab FROM oobject WHERE id_Prorab = '{dataGridView2.Rows[IdUser].Cells[5].Value.ToString()}'";
            SqlCommand command1 = new SqlCommand(queryStringIdUser, database.getConnection());
            database.openConnection();
            // Выполнение запроса и запись результата в переменную
            id_Prorab = Convert.ToInt32(command1.ExecuteScalar());  

            dataGridView1.Rows.Clear();

            string sIdUser = $"SELECT * FROM worker WHERE id_Prorab = '{id_Prorab}' ";

            SqlCommand command3 = new SqlCommand(sIdUser, database.getConnection());

           

            SqlDataReader reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                ReadSingleRowWorker(dataGridView1, reader3);
            }
            reader3.Close();
            string queryStringImages = $"SELECT PhotoBefore, PhotoAfter FROM oobject WHERE id = '{selectedRowIdProrab}'";
            SqlCommand commandImages = new SqlCommand(queryStringImages, database.getConnection());

            try
            {
                SqlDataReader readerImages = commandImages.ExecuteReader();
                if (readerImages.Read())
                {
                    if (readerImages["PhotoBefore"] != DBNull.Value && readerImages["PhotoBefore"] is byte[])
                    {
                        byte[] photoBeforeBytes = (byte[])readerImages["PhotoBefore"];
                        using (MemoryStream ms = new MemoryStream(photoBeforeBytes))
                        {
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }

                    if (readerImages["PhotoAfter"] != DBNull.Value && readerImages["PhotoAfter"] is byte[])
                    {
                        byte[] photoAfterBytes = (byte[])readerImages["PhotoAfter"];
                        using (MemoryStream ms = new MemoryStream(photoAfterBytes))
                        {
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox3.Image = Image.FromStream(ms);
                        }
                    }
                    readerImages.Close();
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine(ex.Message);
            }
            finally
            {
                database.closeConnection();
            }

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
        private void RefreshDataGridOobject(DataGridView dgw_oobject)//объект
        {
            dgw_oobject.Rows.Clear();

            string queryString = $"select * from oobject";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowOobject(dgw_oobject, reader);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView2);
        }
        private void Search(DataGridView dgw_oobject) // поиск объект
        {
            dgw_oobject.Rows.Clear();
            string searchString = $"SELECT * FROM oobject WHERE concat (id, Title, Addresss, Typee, Statuss) like '%" + textBox1.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowOobject(dgw_oobject, read);
            }

            read.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
