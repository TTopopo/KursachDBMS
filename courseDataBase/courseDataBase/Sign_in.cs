using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using courseDataBase.Connection;

namespace courseDataBase
{
    public partial class Sign_in : Form
    {
        DataBase dataBase = new DataBase();

        public int id_user;

        public Sign_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)

            // временное заполнение полей
            textBox_login.Text = "admin";
            textBox_password.Text = "admin";
        }

        private void log_in_load(object sender, EventArgs e)
        {
            // шифрование пароля и выставление ограничений на кол-во символов по таблице SQL
            textBox_password.PasswordChar = '•';
            pictureBox_HIDEPASSWORD.Visible = false;
            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
        }

        private void pictureBox_SHOWPASSWORD_Click(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar = true;
            pictureBox_HIDEPASSWORD.Visible = true;
            pictureBox_SHOWPASSWORD.Visible = false;
        }

        private void pictureBox_HIDEPASSWORD_Click(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar = false;
            pictureBox_HIDEPASSWORD.Visible = false;
            pictureBox_SHOWPASSWORD.Visible = true;
        }

        private void BUT_Enter_Click(object sender, EventArgs e)
        {
            // переменные для сбора информации с текстовых полей
            var loginUser = textBox_login.Text;
            var passUser = md5.hashPassword(textBox_password.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // авторизация клиента ClientRadio
            if (ClientRadio.Checked)
            {
                // переменная с запросом для DB
                string queryString = $"SELECT id_register_user, login_user, password_user FROM register_user WHERE login_user = '{loginUser}' and password_user = '{passUser}'";

                // запрос для DB
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    string queryStringIdUser = $"SELECT id_register_user FROM register_user WHERE login_user = '{loginUser}' and password_user = '{passUser}'";

                    dataBase.openConnection();

                    // Создание команды SQL
                    SqlCommand command1 = new SqlCommand(queryStringIdUser, dataBase.getConnection()); command1.ExecuteNonQuery();

                    dataBase.closeConnection();

                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UserPage userPage = new UserPage();
                    //this.Hide();
                    userPage.ShowDialog();
                }
                else
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            // авторизация прораб radioButton1ь
            else if (radioButton1.Checked)
            { 
                // переменная с запросом для DB
                string queryString = $"SELECT id_register_foreman, login_foreman, password_foreman FROM register_foreman WHERE login_foreman = '{loginUser}' and password_foreman = '{passUser}'";

                // запрос для DB
                SqlCommand command4 = new SqlCommand(queryString, dataBase.getConnection());

                adapter.SelectCommand = command4;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    string queryStringIdProrab = $"SELECT id_register_foreman FROM register_foreman WHERE login_foreman = '{loginUser}' and password_foreman = '{passUser}'";

                    dataBase.openConnection();

                    // Создание команды SQL
                    SqlCommand command2 = new SqlCommand(queryStringIdProrab, dataBase.getConnection()); command2.ExecuteNonQuery();

                    dataBase.closeConnection();

                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ForemanPage foremanPage = new ForemanPage();
                    //this.Hide();
                    foremanPage.ShowDialog();
                }
                else
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // авторизация админа AdminRadio
            else if (AdminRadio.Checked)
            {
                // переменная с запросом для DB
                string queryString = $"SELECT id_register_admin, login_admin, password_admin FROM register_admin WHERE login_admin = '{loginUser}' and password_admin = '{passUser}'";

                // запрос для DB
                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdminPage adminPage = new AdminPage();
                    //this.Hide();
                    adminPage.ShowDialog();
                }
                else
                    MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не выбран тип учетной записи!");
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_up frm_sign_up = new Sign_up();
            frm_sign_up.Show();
            this.Hide();
        }

        private void ClientRadio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
