using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using courseDataBase.Connection;

namespace courseDataBase
{
    public partial class Sign_up : Form
    {
        DataBase dataBase = new DataBase();

        public Sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void log_in_load(object sender, EventArgs e)
        {
            // шифрование пароля и выставление ограничений на кол-во символов по таблице SQL
            textBox_password.PasswordChar = '•';
            pictureBox_HIDEPASSWORD.Visible = false;
            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
        }

        private void BUT_Create_Click(object sender, EventArgs e)
        {
            // регистрация пользователя ClientRadio
            if (ClientRadio.Checked)
            {
                // проверка уже существующих пользовательских аккаунтов в BD
                if (CheckClient())
                {
                    return;
                }

                // переменные для сбора информации с текстовых полей
                var login = textBox_login.Text;
                var password = md5.hashPassword(textBox_password.Text);

                // переменная с запросом для DB
                string querystring = $"insert into register_user(login_user, password_user) values('{login}','{password}')";

                // запрос для DB
                SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

                dataBase.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                    Sign_in frm_sign_in = new Sign_in();
                    this.Hide();
                    frm_sign_in.ShowDialog();
                }
                else
                    MessageBox.Show("Аккаунт не создан!");
                dataBase.closeConnection();
            }
            // регистрация пользователя radioButton1
            if (radioButton1.Checked)
            {
                // проверка уже существующих пользовательских аккаунтов в BD
                if (CheckFormen())
                {
                    return;
                }

                // переменные для сбора информации с текстовых полей
                var login = textBox_login.Text;
                var password = md5.hashPassword(textBox_password.Text);

                // переменная с запросом для DB
                string querystring = $"insert into register_foreman(login_foreman, password_foreman) values('{login}','{password}')";

                // запрос для DB
                SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

                dataBase.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                    Sign_in frm_sign_in = new Sign_in();
                    this.Hide();
                    frm_sign_in.ShowDialog();
                }
                else
                    MessageBox.Show("Аккаунт не создан!");
                dataBase.closeConnection();
            }

            // регистрация админа AdminRadio
            else if (AdminRadio.Checked && md5.hashPassword(textBox_AdminPass.Text) == "21232F297A57A5A743894A0E4A801FC3")
            {
                // проверка уже существующих пользовательских аккаунтов в BD
                if (CheckAdmin())
                {
                    return;
                }

                // переменные для сбора информации с текстовых полей
                var login = textBox_login.Text;
                var password = md5.hashPassword(textBox_password.Text);

                // переменная с запросом для DB
                string querystring = $"insert into register_admin(login_admin, password_admin) values('{login}','{password}')";

                // запрос для DB
                SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

                dataBase.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                    Sign_in frm_sign_in = new Sign_in();
                    this.Hide();
                    frm_sign_in.ShowDialog();
                }
                else
                    MessageBox.Show("Аккаунт не создан!");
                dataBase.closeConnection();
            }

            // не выбран элемент Radio
            else
            {
                MessageBox.Show("Не выбран тип учетной записи!");
                return;
            }
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

        private Boolean CheckClient() // метод проверяющий уже существовующих аккаунтов пользователей
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string quertsting = $"SELECT id_register_user, login_user, password_user FROM register_user WHERE login_user ='{loginUser}' AND password_user = '{passUser}'";

            SqlCommand sqlCommand = new SqlCommand(quertsting, dataBase.getConnection());

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            if(table.Rows.Count > 0 )
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else return false; 
        }
        private Boolean CheckFormen() // метод проверяющий уже существовующих аккаунтов оаба
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string quertsting = $"SELECT id_register_foreman, login_foreman, password_foreman FROM register_foreman WHERE login_foreman ='{loginUser}' AND password_foreman = '{passUser}'";

            SqlCommand sqlCommand = new SqlCommand(quertsting, dataBase.getConnection());

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else return false;
        }

        private Boolean CheckAdmin() // метод проверяющий уже существовующих аккаунтов админов
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string quertsting = $"SELECT id_register_admin, login_admin, password_admin FROM register_admin WHERE login_admin ='{loginUser}' AND password_admin = '{passUser}'";

            SqlCommand sqlCommand = new SqlCommand(quertsting, dataBase.getConnection());

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else return false;
        }
    }
}
