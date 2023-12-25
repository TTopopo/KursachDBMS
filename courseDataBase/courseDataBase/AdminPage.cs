using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using courseDataBase.Connection;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace courseDataBase
{
    enum RowState
    {
        Existed,
        Nes,
        Modfield,
        ModfieldNew,
        Deleted,
        Decoration,
        Confirmed
    }

    public partial class AdminPage : Form
    {
        DataBase database = new DataBase();

        int selectedRow;
        int selectedIdBus;
        int selectedRowDriver;
        int selectedIdImage;
        int selectedColumnValue;
        public AdminPage()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // стартовая позиция окна (центр)
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            CreateColumsOobject();
            RefreshDataGridOobject(dataGridView1_oobject);

            CreateColumsCustomer();
            RefreshDataGridCustomer(dataGridView2_customer);

            CreateColumsForeman();
            RefreshDataGridForeman(dataGridView3_foreman);

            CreateColumsWorker();
            RefreshDataGridWorker(dataGridView4_worker);

            CreateColumsStageConstruction();
            RefreshDataGridStageConstruction(dataGridView5_stage_construction);

            CreateColumsStepConstruction();
            RefreshDataGridStepConstruction(dataGridView6_step_construction);
             
        }
        //===================кнопка обновления=======================//
        private void BUT_Refresh_Click(object sender, EventArgs e) // кнопка обновления информации в dataGridView1(объект)
        {
            RefreshDataGridOobject(dataGridView1_oobject);
            ClearObject(); 
        }
        private void button2_Click(object sender, EventArgs e)// кнопка обновления информации в dataGridView2(Клиент)
        {
            RefreshDataGridCustomer(dataGridView2_customer);
            ClearCustomer();
        }
        private void BUT_bus_Refresh_Click(object sender, EventArgs e) // кнопка обновления информации в dataGridView3(Прораб)
        {
            RefreshDataGridForeman(dataGridView3_foreman);
            ClearForeman();
        }
        private void BUT_driver_Refresh_Click(object sender, EventArgs e)// кнопка обновления информации в dataGridView4(Рабочий)
        {
            RefreshDataGridWorker(dataGridView4_worker);
            ClearWorker();
        } 
        private void button38_Click(object sender, EventArgs e)// кнопка обновления информации в dataGridView5(Этап Строительство)
        {
            RefreshDataGridStageConstruction(dataGridView5_stage_construction); 
            ClearStage();
        }
        private void button12_Click(object sender, EventArgs e)// кнопка обновления информации в dataGridView6(Шаг Строительство)
        {
            RefreshDataGridStepConstruction(dataGridView6_step_construction);
            ClearStep();
        }

        //================== ===============//

        private void BUT_ClearFields_Click(object sender, EventArgs e)//очистка объект
        {
            ClearObject();
        }
        private void BUT_NewPost_Click(object sender, EventArgs e) // кнопка создания новой записи ОБЪЕКТ
        {
            Add_Data_Object add_Data_Object = new Add_Data_Object();
            add_Data_Object.Show();
        }

        private void BUT_Delete_Click(object sender, EventArgs e)// кнопка удаление записи ОБЪЕКТ
        {
            DeleteRow(); 
            ClearObject();
        }

        private void BUT_Save_Click(object sender, EventArgs e)// кнопка сохранить записи ОБЪЕКТ
        {
            Update();
        }

        private void BUT_Change_Click(object sender, EventArgs e)// кнопка изменить записи ОБЪЕКТ
        {
            Change();
            ClearObject();
        }

        //=============================//
        private void BUT_bus_ClearField_Click(object sender, EventArgs e)//очистка прораб
        {
            ClearForeman();
        }
        private void button7_Click(object sender, EventArgs e)// кнопка создания новой записи прораб
        {
            Add_Data_Foreman add_Data_Foreman = new Add_Data_Foreman();
            add_Data_Foreman.Show();
        }
        private void button10_Click(object sender, EventArgs e)// кнопка удаление записи прораб
        {
            DeleteForeman();
            ClearForeman();
        }
        private void button9_Click(object sender, EventArgs e)// кнопка изменить записи прораб
        {
            ChangeForeman();
            ClearForeman();
        }
        private void button8_Click(object sender, EventArgs e)// кнопка сохранить записи прораб
        {
            UpdateForeman();
        }
       

       

        //=============================//
        private void button1_Click(object sender, EventArgs e)//очистка клиент
        {
            ClearCustomer();
        } 
        private void button3_Click(object sender, EventArgs e)// кнопка создания новой записи клиент
        {
            Add_Data_Customer add_Data_Customer = new Add_Data_Customer();
            add_Data_Customer.Show();
        }
        private void button6_Click(object sender, EventArgs e)// кнопка удаление записи клиент
        {
            DeleteСustomer();
            ClearCustomer();
        }
        private void button5_Click(object sender, EventArgs e)// кнопка изменить записи клиент
        {
            ChangeCustomer();
            ClearCustomer();
        } 
        private void button4_Click(object sender, EventArgs e)// кнопка сохранить записи клиент
        {
            UpdateCustomer();
        }

        //============================//
        private void button11_Click(object sender, EventArgs e)//очистка шаг строительства
        {
            ClearStep();
        }
        private void button19_Click(object sender, EventArgs e)// кнопка создания новой записи шаг
        {
            Add_Data_Step add_Data_Step = new Add_Data_Step();
            add_Data_Step.Show();
        }

        private void button22_Click(object sender, EventArgs e)// кнопка удаление записи шаг
        {
            DeleteStep();
            ClearStep();
        }

        private void button21_Click(object sender, EventArgs e)// кнопка изменить записи шаг
        {
            ChangeStep();
            ClearStep();
        }

        private void button20_Click(object sender, EventArgs e)// кнопка сохранить записи шаг
        {
            UpdateStep();
        }

        //============================//
        private void BUT_driver_ClearField_Click(object sender, EventArgs e)//очистка рабочие
        {
            ClearWorker();
        }
        private void button15_Click(object sender, EventArgs e)// кнопка создания новой записи рабочие
        {
            Add_Data_Worker add_Data_Worker = new Add_Data_Worker();
            add_Data_Worker.Show();
        }

        private void button18_Click(object sender, EventArgs e)// кнопка удаление записи рабочие
        {
            DeleteWorker();
            ClearWorker();
        }

        private void button17_Click(object sender, EventArgs e)// кнопка изменить записи рабочие
        {
            ChangeWorker();
            ClearWorker();
        }

        private void button16_Click(object sender, EventArgs e)// кнопка сохранить записи рабочие
        {
            UpdateWorker();
        }

        //============================//
        private void button37_Click(object sender, EventArgs e)//очистка этап строительства
        {
            ClearStage();
        }
        private void button33_Click(object sender, EventArgs e)// кнопка создания новой записи этап
        {
            Add_Data_Stage add_Data_Stage = new Add_Data_Stage();
            add_Data_Stage.Show();
        }

        private void button36_Click(object sender, EventArgs e)// кнопка удаление записи этап
        {
            DeleteStage();
            ClearStage();
        }

        private void button35_Click(object sender, EventArgs e)// кнопка изменить записи этап
        {
            ChangeStage();
            ClearStage();
        }

        private void button34_Click(object sender, EventArgs e)// кнопка сохранить записи этап
        {
            UpdateStage();
        }

        //============================//
        private void textBox_SEARCH_TextChanged(object sender, EventArgs e) // текстовое поле для осуществление поиска по DataGridView1(Объект)
        {
            Search(dataGridView1_oobject);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)// текстовое поле для осуществление поиска по DataGridView2(клиент)
        {
            Search_Customer(dataGridView2_customer);
        }
        private void textBox_bus_SEARCH_TextChanged(object sender, EventArgs e)// текстовое поле для осуществление поиска по DataGridView3(прораб)
        {
            SearchForeman(dataGridView3_foreman);
        }
        private void textBox_driver_SEARCH_TextChanged(object sender, EventArgs e)// текстовое поле для осуществление поиска по DataGridView4(рабочие)
        {
            SearchWorker(dataGridView4_worker);
        }
        private void textBox35_TextChanged(object sender, EventArgs e)// текстовое поле для осуществление поиска по DataGridView5(этап)
        {
            SearchStage(dataGridView5_stage_construction);
        }
        private void textBox11_TextChanged(object sender, EventArgs e)// текстовое поле для осуществление поиска по DataGridView6(шаги)
        {
            SearchStep(dataGridView6_step_construction);
        }
         

        private void BUT_confirm_Click(object sender, EventArgs e)
        {
            //database.openConnection();

            //var status = "Подтверждено";

            //var changeQuery = $"UPDATE transation set confirm = '{status}' where id = '{selectedColumnValue}'";

            //var command = new SqlCommand(changeQuery, database.getConnection());
            //command.ExecuteNonQuery();

            //RefreshDataGridConfirmTransation(dataGridView6_step_construction);

            //database.closeConnection();
        }

        private void BUT_cancel_Click(object sender, EventArgs e)
        {
            //database.openConnection();

            //var status = "Отклонено";

            //var changeQuery = $"UPDATE transation set confirm = '{status}' where id = '{selectedColumnValue}'";

            //var command = new SqlCommand(changeQuery, database.getConnection());
            //command.ExecuteNonQuery();

            //RefreshDataGridConfirmTransation(dataGridView6_step_construction);

            //database.closeConnection();
        }

        private void Change()//ОБЪЕКТ
        {
            var selectedRowIndex = dataGridView1_oobject.CurrentCell.RowIndex;

            var id = textBox_ID.Text;
            var title = textBox_Title.Text;
            var address = textBox_Address.Text;
            var type = textBox_Type.Text;
            var status = textBox_Status.Text;  

            if (dataGridView1_oobject.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1_oobject.Rows[selectedRowIndex].SetValues(id, title, address, type, status);
                dataGridView1_oobject.Rows[selectedRowIndex].Cells[7].Value = RowState.Modfield;
            }
        }

        private void ChangeCustomer()//Клиент
        {
            var selectedRowIndex = dataGridView2_customer.CurrentCell.RowIndex;
            
            var id = textBox2.Text;
            var name = textBox3.Text;
            var lastName = textBox4.Text; 
            var phoneNumber = textBox5.Text; 
            if (dataGridView2_customer.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            { 
                    dataGridView2_customer.Rows[selectedRowIndex].SetValues(id, name, lastName, phoneNumber);
                    dataGridView2_customer.Rows[selectedRowIndex].Cells[4].Value = RowState.Modfield; 
            }
        }

        private void ChangeForeman()//Прораб
        {
            var selectedRowIndex = dataGridView3_foreman.CurrentCell.RowIndex;

            var id = textBox6.Text;
            var Name = textBox_Name.Text;
            var LastName = textBox_LastName.Text;
            var Qualification = textBox_Qualification.Text;
            var Specialization = textBox_Specialization.Text;
            var Skills = textBox_Skills.Text;
            var PhoneNumber = textBox_PhoneNumber.Text;

            if (dataGridView3_foreman.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView3_foreman.Rows[selectedRowIndex].SetValues(id,Name, LastName, Qualification, Specialization, Skills, PhoneNumber);
                dataGridView3_foreman.Rows[selectedRowIndex].Cells[7].Value = RowState.Modfield; 
            }
        }
        private void ChangeWorker()// рабочий
        {
            var selectedRowIndex = dataGridView4_worker.CurrentCell.RowIndex;

            var id = textBox13.Text;
            var Name = textBox_Namee.Text;
            var LastName = textBox_LastNamee.Text;
            var PhoneNumber = textBox_PhoneNumberr.Text;
            var Position = textBox_Position.Text;
            var Experience = textBox_Experience.Text; 

            if (dataGridView4_worker.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView4_worker.Rows[selectedRowIndex].SetValues(id, Name, LastName, PhoneNumber, Position, Experience );
                dataGridView4_worker.Rows[selectedRowIndex].Cells[7].Value = RowState.Modfield;
            }
        }
        private void ChangeStage()//Этап
        {
            var selectedRowIndex = dataGridView5_stage_construction.CurrentCell.RowIndex;

            var id = textBox31.Text;
            var StartDate = textBox32.Text;
            var СompletionDate = textBox33.Text;
            var TitleStage = textBox34.Text; 

            if (dataGridView5_stage_construction.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView5_stage_construction.Rows[selectedRowIndex].SetValues(id, StartDate, СompletionDate, TitleStage );
                dataGridView5_stage_construction.Rows[selectedRowIndex].Cells[4].Value = RowState.Modfield;
            }
        }
        private void ChangeStep()//шаг
        {
            var selectedRowIndex = dataGridView6_step_construction.CurrentCell.RowIndex;

            var id = textBox18.Text;
            var StartDate = textBox19.Text;
            var СompletionDate = textBox20.Text;
            var TitleStep = textBox21.Text; 

            if (dataGridView6_step_construction.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView6_step_construction.Rows[selectedRowIndex].SetValues(id, StartDate, СompletionDate, TitleStep);
                dataGridView6_step_construction.Rows[selectedRowIndex].Cells[4].Value = RowState.Modfield;
            }
        }


        //======================Сохранить=======================//
        private void Update()// Сохранить объект
        {
            database.openConnection();

            for (int index = 0; index < dataGridView1_oobject.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1_oobject.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1_oobject.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM oobject WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView1_oobject.Rows[index].Cells[0].Value.ToString();
                    var title = dataGridView1_oobject.Rows[index].Cells[1].Value.ToString();
                    var adress = dataGridView1_oobject.Rows[index].Cells[2].Value.ToString();
                    var type = dataGridView1_oobject.Rows[index].Cells[3].Value.ToString();
                    var status = dataGridView1_oobject.Rows[index].Cells[4].Value.ToString(); 

                    var changeQuery = $"UPDATE oobject SET Title = '{title}', Addresss = '{adress}', Typee = '{type}', Statuss = '{status}'   WHERE id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }

        private void UpdateCustomer()// Сохранить клиент
        {
            database.openConnection();

            for (int index = 0; index < dataGridView2_customer.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2_customer.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView2_customer.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM customer WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView2_customer.Rows[index].Cells[0].Value.ToString();
                    var Nname = dataGridView2_customer.Rows[index].Cells[1].Value.ToString();
                    var LastName = dataGridView2_customer.Rows[index].Cells[2].Value.ToString();
                    var PhoneNumber = dataGridView2_customer.Rows[index].Cells[3].Value.ToString();

                    var changeQuery = $"UPDATE customer SET   Nname = '{Nname}', LastName = '{LastName}' ,PhoneNumber = '{PhoneNumber}' WHERE  id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }
        private void UpdateForeman()// Сохранить прораб
        {
            database.openConnection();

            for (int index = 0; index < dataGridView3_foreman.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView3_foreman.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView3_foreman.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM foreman WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView3_foreman.Rows[index].Cells[0].Value.ToString();
                    var Name = dataGridView3_foreman.Rows[index].Cells[1].Value.ToString();
                    var LastName = dataGridView3_foreman.Rows[index].Cells[2].Value.ToString();
                    var Qualification = dataGridView3_foreman.Rows[index].Cells[3].Value.ToString();
                    var Specialization = dataGridView3_foreman.Rows[index].Cells[4].Value.ToString();
                    var Skills = dataGridView3_foreman.Rows[index].Cells[5].Value.ToString();
                    var PhoneNumber = dataGridView3_foreman.Rows[index].Cells[6].Value.ToString(); 

                    var changeQuery = $"UPDATE foreman SET Name = '{Name}', LastName = '{LastName}', Qualification = '{Qualification}', Specialization = '{Specialization}', Skills = '{Skills}', PhoneNumber = '{PhoneNumber}' WHERE id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }
        private void UpdateWorker()// Сохранит рабочий
        {
            database.openConnection();

            for (int index = 0; index < dataGridView4_worker.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView4_worker.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView4_worker.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM worker WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView4_worker.Rows[index].Cells[0].Value.ToString();
                    var Nname = dataGridView4_worker.Rows[index].Cells[1].Value.ToString();
                    var LastName = dataGridView4_worker.Rows[index].Cells[2].Value.ToString();
                    var PhoneNumber = dataGridView4_worker.Rows[index].Cells[3].Value.ToString();
                    var Position = dataGridView4_worker.Rows[index].Cells[4].Value.ToString();
                    var Experience = dataGridView4_worker.Rows[index].Cells[5].Value.ToString(); 

                    var changeQuery = $"UPDATE worker SET Nname = '{Nname}', LastName = '{LastName}', PhoneNumber = '{PhoneNumber}', Position = '{Position}', Experience = '{Experience}'  WHERE id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }

        private void UpdateStage()// Сохранит Этап
        {
            database.openConnection();

            for (int index = 0; index < dataGridView5_stage_construction.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView5_stage_construction.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView5_stage_construction.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM stage_construction WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView5_stage_construction.Rows[index].Cells[0].Value.ToString();
                    var StartDate = dataGridView5_stage_construction.Rows[index].Cells[1].Value.ToString();
                    var СompletionDate = dataGridView5_stage_construction.Rows[index].Cells[2].Value.ToString();
                    var TitleStage = dataGridView5_stage_construction.Rows[index].Cells[3].Value.ToString(); 

                    var changeQuery = $"UPDATE stage_construction SET StartDate = '{StartDate}', СompletionDate = '{СompletionDate}', TitleStage = '{TitleStage}'  WHERE id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }
        private void UpdateStep()// Сохранит Шаг
        {
            database.openConnection();

            for (int index = 0; index < dataGridView6_step_construction.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView6_step_construction.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView6_step_construction.Rows[index].Cells[0].Value);
                    var deleteQuerry = $"DELETE FROM step_construction WHERE id = {id}";

                    var command = new SqlCommand(deleteQuerry, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modfield)
                {
                    var id = dataGridView6_step_construction.Rows[index].Cells[0].Value.ToString();  
                    var StartDate = dataGridView6_step_construction.Rows[index].Cells[3].Value.ToString();
                    var СompletionDate = dataGridView6_step_construction.Rows[index].Cells[4].Value.ToString();
                    var TitleStep = dataGridView6_step_construction.Rows[index].Cells[5].Value.ToString();

                    var changeQuery = $"UPDATE step_construction SET StartDate = '{StartDate}', СompletionDate = '{СompletionDate}', TitleStep = '{TitleStep}' WHERE id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
        }


        private void ClearObject()  //очистка объект
        {
            textBox_ID.Text = string.Empty;
            textBox_Title.Text = string.Empty;
            textBox_Address.Text = string.Empty;
            textBox_Type.Text = string.Empty;
            textBox_Status.Text = string.Empty;  
        }
        private void ClearCustomer() //очистка клиент
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }
        private void ClearForeman() //очистка  Прораб
        {
            textBox6.Text = string.Empty;
            textBox_Name.Text = string.Empty;
            textBox_LastName.Text = string.Empty;
            textBox_Qualification.Text = string.Empty;
            textBox_Specialization.Text = string.Empty;
            textBox_Skills.Text = string.Empty;
            textBox_PhoneNumber.Text = string.Empty;
        }
        private void ClearWorker()//очистка рабочий
        {
            textBox13.Text = string.Empty;
            textBox_Namee.Text = string.Empty;
            textBox_LastNamee.Text = string.Empty;
            textBox_PhoneNumberr.Text = string.Empty;
            textBox_Position.Text = string.Empty;
            textBox_Experience.Text = string.Empty; 
        }
        private void ClearStage()//очистка этапа
        {
            textBox31.Text = string.Empty;
            textBox32.Text = string.Empty;
            textBox33.Text = string.Empty;
            textBox34.Text = string.Empty; 
        } 
       

        private void ClearStep() //очистка шага
        {
            textBox18.Text = string.Empty;
            textBox19.Text = string.Empty;
            textBox20.Text = string.Empty;
            textBox21.Text = string.Empty; 
        }

        private void DeleteRow()// ОБЪЕКТ
        {
            int index = dataGridView1_oobject.CurrentCell.RowIndex;

            dataGridView1_oobject.Rows[index].Visible = false;

            if (dataGridView1_oobject.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1_oobject.Rows[index].Cells[7].Value = RowState.Deleted; // изменение состояние записи
            }
        }

        private void DeleteСustomer()//клиент
        {
            int index = dataGridView2_customer.CurrentCell.RowIndex;

            dataGridView2_customer.Rows[index].Visible = false;

            if (dataGridView2_customer.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView2_customer.Rows[index].Cells[4].Value = RowState.Deleted; // изменение состояние записи
            }
        }

        private void DeleteForeman()//Прораб
        {
            int index = dataGridView3_foreman.CurrentCell.RowIndex;

            dataGridView3_foreman.Rows[index].Visible = false;

            if (dataGridView3_foreman.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView3_foreman.Rows[index].Cells[7].Value = RowState.Deleted; // изменение состояние записи
            }
        }
        private void DeleteWorker()// рабочий
        {
            int index = dataGridView4_worker.CurrentCell.RowIndex;

            dataGridView4_worker.Rows[index].Visible = false;

            if (dataGridView4_worker.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView4_worker.Rows[index].Cells[6].Value = RowState.Deleted; // изменение состояние записи
            }
        }
        private void DeleteStage()//этап
        {
            int index = dataGridView5_stage_construction.CurrentCell.RowIndex;

            dataGridView5_stage_construction.Rows[index].Visible = false;

            if (dataGridView5_stage_construction.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView5_stage_construction.Rows[index].Cells[4].Value = RowState.Deleted; // изменение состояние записи
            }
        }
        private void DeleteStep()//Шаг
        {
            int index = dataGridView6_step_construction.CurrentCell.RowIndex;

            dataGridView6_step_construction.Rows[index].Visible = false;

            if (dataGridView6_step_construction.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView6_step_construction.Rows[index].Cells[4].Value = RowState.Deleted; // изменение состояние записи
            }
        }
        //==========================поиск==========================//
        private void Search(DataGridView dgw_oobject) // поиск объект
        {
            dgw_oobject.Rows.Clear();
            string searchString = $"SELECT * FROM oobject WHERE concat (id, Title, Addresss, Typee, Statuss) like '%" + textBox_SEARCH.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowOobject(dgw_oobject, read);
            }

            read.Close();
        }

        private void Search_Customer(DataGridView dgw_customer) // поиск клиент
        {
            dgw_customer.Rows.Clear();
            string searchString = $"SELECT * FROM customer WHERE concat (id, Nname, LastName, PhoneNumber) like '%" + textBox1.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowCustomer(dgw_customer, read);
            }

            read.Close();
        }

        private void SearchForeman(DataGridView dgw_foreman) // поиск прораб
        {
            dgw_foreman.Rows.Clear(); 
            string searchString = $"SELECT * FROM foreman WHERE concat (id, Nname, LastName, Qualification, Specialization, Skills, PhoneNumber) like '%" + textBox_bus_SEARCH.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowForeman(dgw_foreman, read);
            }

            read.Close();
        } 
        private void SearchWorker(DataGridView dgw_worker) // поиск  рабочего
        {
            dgw_worker.Rows.Clear();
            string searchString = $"SELECT * FROM worker WHERE concat (id, Nname, LastName, PhoneNumber, Position, Experience) like '%" + textBox_driver_SEARCH.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowWorker(dgw_worker, read);
            }

            read.Close();
        }
        
        private void SearchStage(DataGridView dgw_stage_construction) // поиск этапа
        {
            dgw_stage_construction.Rows.Clear();
            string searchString = $"SELECT * FROM stage_construction WHERE concat (id, StartDate, СompletionDate, TitleStage) like '%" + textBox35.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowStageConstruction(dgw_stage_construction, read);
            }

            read.Close();
        }
        private void SearchStep(DataGridView dgw_step_construction) // поиск  шага
        {
            dgw_step_construction.Rows.Clear();
            string searchString = $"SELECT * FROM step_construction WHERE concat (id, StartDate, СompletionDate, TitleStep) like '%" + textBox11.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleGridStepConstruction(dgw_step_construction, read);
            }

            read.Close();
        }


        //====================================//
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // вывод информации из dataGridView1_oobject по нажатию в textBox'ы
        {
            pictureBox_before.Image = null;
            pictureBox_after.Image = null;
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1_oobject.Rows[selectedRow];

                textBox_ID.Text = row.Cells[0].Value.ToString();
                textBox_Title.Text = row.Cells[1].Value.ToString();
                textBox_Address.Text = row.Cells[2].Value.ToString();
                textBox_Type.Text = row.Cells[3].Value.ToString();
                textBox_Status.Text = row.Cells[4].Value.ToString();
                selectedIdImage = int.Parse(row.Cells[0].Value.ToString());
            }

            // Combine both queries into one to only have a single roundtrip to the database.
            string searchStringImages = $"SELECT PhotoBefore, PhotoAfter FROM oobject WHERE id = '{selectedIdImage}'";

            try
            {
                database.openConnection();

                using (SqlCommand command = new SqlCommand(searchStringImages, database.getConnection()))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["PhotoBefore"] != DBNull.Value && reader["PhotoBefore"] is byte[])
                        {
                            byte[] byteArray1 = (byte[])reader["PhotoBefore"];
                            using (var ms = new MemoryStream(byteArray1))
                            {
                                Bitmap image1 = new Bitmap(ms);
                                pictureBox_before.SizeMode = PictureBoxSizeMode.StretchImage;
                                pictureBox_before.Image = image1;
                            }
                        }

                        if (reader["PhotoAfter"] != DBNull.Value && reader["PhotoAfter"] is byte[])
                        {
                            byte[] byteArray2 = (byte[])reader["PhotoAfter"];
                            using (var ms = new MemoryStream(byteArray2))
                            {
                                Bitmap image2 = new Bitmap(ms);
                                pictureBox_after.SizeMode = PictureBoxSizeMode.StretchImage;
                                pictureBox_after.Image = image2;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                database.closeConnection();
            }
            
        }
        private void dataGridView2_customer_CellClick(object sender, DataGridViewCellEventArgs e)//клиент
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2_customer.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString(); 
            }
        }
        private void dataGridView3_foreman_CellClick(object sender, DataGridViewCellEventArgs e)//Прораб
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3_foreman.Rows[selectedRow];

                textBox6.Text = row.Cells[0].Value.ToString();
                textBox_Name.Text = row.Cells[1].Value.ToString();
                textBox_LastName.Text = row.Cells[2].Value.ToString(); 
                textBox_Qualification.Text = row.Cells[3].Value.ToString();
                textBox_Specialization.Text = row.Cells[4].Value.ToString();
                textBox_Skills.Text = row.Cells[5].Value.ToString();
                textBox_PhoneNumber.Text = row.Cells[6].Value.ToString(); 
            }
        }
        private void dataGridView5_stage_construction_CellClick(object sender, DataGridViewCellEventArgs e)//этап
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5_stage_construction.Rows[selectedRow];

                textBox31.Text = row.Cells[0].Value.ToString();
                textBox32.Text = row.Cells[1].Value.ToString();
                textBox33.Text = row.Cells[2].Value.ToString();
                textBox34.Text = row.Cells[3].Value.ToString(); 
            } 
        }
        private void dataGridView6_step_construction_CellClick(object sender, DataGridViewCellEventArgs e)//шаг
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView6_step_construction.Rows[selectedRow];

                textBox18.Text = row.Cells[0].Value.ToString();
                textBox19.Text = row.Cells[1].Value.ToString();
                textBox20.Text = row.Cells[2].Value.ToString();
                textBox21.Text = row.Cells[3].Value.ToString(); 
            }
        }
         

        private void dataGridView_driver_CellClick(object sender, DataGridViewCellEventArgs e)//рабочий
        {
            selectedRowDriver = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4_worker.Rows[selectedRowDriver];

                textBox13.Text = row.Cells[0].Value.ToString();
                textBox_Namee.Text = row.Cells[1].Value.ToString();
                textBox_LastNamee.Text = row.Cells[2].Value.ToString();
                textBox_PhoneNumberr.Text = row.Cells[3].Value.ToString();
                textBox_Position.Text = row.Cells[4].Value.ToString();
                textBox_Experience.Text = row.Cells[5].Value.ToString();
                 
                //selectedRowDriver = int.Parse(row.Cells[4].Value.ToString());
                //selectedIdImage = int.Parse(row.Cells[0].Value.ToString());
            }

            //string searchString = $"SELECT * FROM bus WHERE id = '{selectedRowDriver}'";
            //string searchStringImage = $"SELECT photo FROM driver WHERE id = '{selectedIdImage}'";

            //SqlCommand command = new SqlCommand(searchString, database.getConnection());

            //database.openConnection();

            //SqlDataReader read = command.ExecuteReader();

            //dataGridView5_stage_construction.Rows.Clear();

            //while (read.Read())
            //{
            //    ReadSingleRowCustomer(dataGridView5_stage_construction, read);
            //}

            //read.Close();

            //database.closeConnection();

            //// --------------------- выгрузка фото из DB --------------------- //

            //SqlCommand command1 = new SqlCommand(searchStringImage, database.getConnection());

            //database.openConnection();

            //SqlDataReader readImage = command1.ExecuteReader();

            //pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

            //if (readImage.Read())
            //{
            //    byte[] byteArray = (byte[])readImage["photo"];

            //    using (var ms = new MemoryStream(byteArray))
            //    {
            //        Bitmap image = new Bitmap(ms);
            //        pbPhoto.Image = image;
            //    }
            //}
            //readImage.Close();
            //database.closeConnection();


            // ---------------------        конец        --------------------- //

        }

        private void CreateColumsOobject() // инициализация столбцов для dataGridView1_oobject
        {
            dataGridView1_oobject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1_oobject.Columns.Add("id", "id");
            dataGridView1_oobject.Columns.Add("Title", "Название");
            dataGridView1_oobject.Columns.Add("Addresss", "Адрес");
            dataGridView1_oobject.Columns.Add("Typee", "Тип");
            dataGridView1_oobject.Columns.Add("Statuss", "Статус");
            dataGridView1_oobject.Columns.Add("id_Prorab", "id Прораб");
            dataGridView1_oobject.Columns.Add("id_Costomer", "id Рабочий");
            dataGridView1_oobject.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumsForeman() // инициализация столбцов для dataGridView3_foreman
        {
            dataGridView3_foreman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView3_foreman.Columns.Add("id", "id");
            dataGridView3_foreman.Columns.Add("Nname", "Имя");
            dataGridView3_foreman.Columns.Add("LastName", "Фамилия");
            dataGridView3_foreman.Columns.Add("Qualification", "Квалификация");
            dataGridView3_foreman.Columns.Add("Specialization", "Специализация");
            dataGridView3_foreman.Columns.Add("Skills", "Навыки");
            dataGridView3_foreman.Columns.Add("PhoneNumber", "Номер телефона"); 
            dataGridView3_foreman.Columns.Add("IsNew", String.Empty);
        }
        private void CreateColumsCustomer() // инициализация столбцов для dataGridView2_customer
        {
            dataGridView2_customer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2_customer.Columns.Add("id", "id");
            dataGridView2_customer.Columns.Add("Nname", "Имя");
            dataGridView2_customer.Columns.Add("LastName", "Фамилия");
            dataGridView2_customer.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView2_customer.Columns.Add("IsNew", String.Empty);
        }
        private void CreateColumsWorker() // инициализация столбцов для dataGridView4_worker
        {
            dataGridView4_worker.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView4_worker.Columns.Add("id", "id");
            dataGridView4_worker.Columns.Add("Nname", "Имя");
            dataGridView4_worker.Columns.Add("LastName", "Фамилия");
            dataGridView4_worker.Columns.Add("PhoneNumber", "Номер телефона");
            dataGridView4_worker.Columns.Add("Position", "Должность");
            dataGridView4_worker.Columns.Add("Experience", "Опыт");
            dataGridView4_worker.Columns.Add("id_Prorab", "id_Prorab");
            dataGridView4_worker.Columns.Add("IsNew", String.Empty);
        }
         
        private void CreateColumsStageConstruction() // инициализация столбцов для dataGridView5_stage_construction
        {
            dataGridView5_stage_construction.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView5_stage_construction.Columns.Add("id", "id");
            dataGridView5_stage_construction.Columns.Add("StartDate", "Дата начала");
            dataGridView5_stage_construction.Columns.Add("СompletionDate", "Дата завершения");
            dataGridView5_stage_construction.Columns.Add("TitleStage", "Название этапа");
            dataGridView5_stage_construction.Columns.Add("IsNew", String.Empty);
        }

        private void CreateColumsStepConstruction() // инициализация столбцов для dataGridView6_step_construction
        {
            dataGridView6_step_construction.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView6_step_construction.Columns.Add("id", "id");
            dataGridView6_step_construction.Columns.Add("StartDate", "Дата начала");
            dataGridView6_step_construction.Columns.Add("СompletionDate", "Дата завершения");
            dataGridView6_step_construction.Columns.Add("TitleStep", "Название шага");
            dataGridView6_step_construction.Columns.Add("IsNew", String.Empty);
        }

       //========================= =========================//

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
        private void ReadSingleRowCustomer(DataGridView dgw_customer, IDataRecord record)//клиент
        {
            dgw_customer.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModfieldNew);
        }
        private void ReadSingleRowForeman(DataGridView dgw_foreman, IDataRecord record)//Прораб
        {
            dgw_foreman.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),  RowState.ModfieldNew);
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
        private void ReadSingleRowStageConstruction(DataGridView dgw_stage_construction, IDataRecord record)//этап
        {
            dgw_stage_construction.Rows.Add(record.GetInt32(0), record.GetDateTime(1), record.GetDateTime(2), record.GetString(3), RowState.ModfieldNew);
        }
        private void ReadSingleGridStepConstruction(DataGridView dgw_step_construction, IDataRecord record)//шаг
        {
            dgw_step_construction.Rows.Add(record.GetInt32(0), record.GetDateTime(1), record.GetDateTime(2), record.GetString(3), RowState.ModfieldNew);
        } 
        //============================ ============================//
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
        private void RefreshDataGridCustomer(DataGridView dgw_customer)//клиетн
        {
            dgw_customer.Rows.Clear();

            string queryString = $"select * from customer";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowCustomer(dgw_customer, reader);
            }
            reader.Close();
        }
        private void RefreshDataGridForeman(DataGridView dgw_foreman) //Прораб
        {
            dgw_foreman.Rows.Clear();

            string queryString = $"select * from foreman";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

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

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowWorker(dgw_worker, reader);
            }
            reader.Close();
        }

        private void RefreshDataGridStageConstruction(DataGridView dgw_stage_construction)
        {
            dgw_stage_construction.Rows.Clear();

            string queryString = $"select * from stage_construction";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowStageConstruction(dgw_stage_construction, reader);
            }
            reader.Close();
        }
        private void RefreshDataGridStepConstruction(DataGridView dgw_step_construction)
        {
            dgw_step_construction.Rows.Clear();

            string queryString = $"select * from step_construction";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleGridStepConstruction(dgw_step_construction, reader);
            }
            reader.Close();
        }
         
         

        private void dataGridView_Confirm_Transaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedColumnValue = e.RowIndex + 1;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_NUMBERROUTE_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_CITY_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_STOPOVER_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox_TIMETRAVEL_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_after_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_before_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_oobject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox_bus_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_bus_BRAND_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_bus_STATENUMBER_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_bus_SEATS_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_foreman_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_customer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_driver_IDBUS_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox_driver_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_driver_FIO_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_driver_PHONE_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_driver_SALARY_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_stage_construction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_worker_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void textBox_SEARCH_CONFIRM_TRANSACTION_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView8_photo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView7_material_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_step_construction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
