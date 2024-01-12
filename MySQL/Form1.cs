using System;

using MySql.Data.MySqlClient;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Xml.Linq;

namespace MySQL
{
    public partial class Form1 : Form
    {
        MySqlConnection nick = new MySqlConnection("Server=127.0.0.1;Port=3306;Username=root;Password=;Database=nick;");
        MySqlCommand cmd;
        MySqlDataReader reader;

        
        int ID = 0;

        public Form1()
        {
           
            InitializeComponent();
            DisplayData();
        }

        private void ClearData()
        {
            ID = 0;
        }

        private void DisplayData()
        {

            try
            {
                
                nick.Open();
                cmd = new MySqlCommand("SELECT * FROM system1", nick);
                reader = cmd.ExecuteReader();
                listView1.Items.Clear();

                
                while (reader.Read())
                {
                    
                    ListViewItem item = new ListViewItem(reader["ID"].ToString());
                    item.SubItems.Add(reader["Customer"].ToString());
                    item.SubItems.Add(reader["RoomNumber"].ToString());
                    listView1.Items.Add(item);
                }
                reader.Close();
                nick.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error" + e);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void listView(object sender, MouseEventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                
                ID = int.Parse(selectedItem.SubItems[0].Text);

                
                try
                {
                    nick.Open();
                    cmd = new MySqlCommand("SELECT ID, Customer, RoomNumber FROM system1 WHERE ID = @id", nick);
                    cmd.Parameters.AddWithValue("@id", ID);

                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            txtName.Text = reader["Customer"].ToString();
                            txtRoomNumber.Text = reader["RoomNumber"].ToString();
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex);
                }
                finally
                {
                    nick .Close();
                }
            }
            btnSave.Enabled = false;
            txtName.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            lblUpdate.Visible = true;
            txtNN.Visible = true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtRoomNumber.Text != "")
            {
                try
                {
                    nick.Open();
                    cmd = new MySqlCommand("INSERT INTO system1 (Customer, RoomNumber) VALUES (@customer, @roomNumber);", nick);
                    cmd.Parameters.AddWithValue("@customer", txtName.Text);
                    cmd.Parameters.AddWithValue("@roomNumber", txtRoomNumber.Text);
                    cmd.ExecuteNonQuery();
                    nick.Close();
                    txtName.Text = "";
                    txtRoomNumber.Text = "";
                    DisplayData();
                    ClearData();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Input Required");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (txtNN.Text != "" && txtRoomNumber.Text != "")
                {
                    try
                    {
                        nick.Open();
                        cmd = new MySqlCommand("UPDATE system1 SET Customer=@customer, RoomNumber=@roomNumber WHERE ID=@id", nick);
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.Parameters.AddWithValue("@customer", txtNN.Text);
                        cmd.Parameters.AddWithValue("@roomNumber", txtRoomNumber.Text);
                        cmd.ExecuteNonQuery();

                        nick.Close();
                        MessageBox.Show("Customer Updated Successfully");
                        DisplayData();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Input Required");
                }
            }
            else
            {
                MessageBox.Show("Please select a customer from the list.");
            }

            btnSave.Enabled = true;
            txtNN.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            lblUpdate.Visible = false;
            txtNN.Visible = false;
         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    nick.Open();
                    cmd = new MySqlCommand("DELETE FROM system1 WHERE ID = @id", nick);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Check-Out Successfully!");
                    txtName.Text = "";
                    txtNN.Text = "";
                    txtRoomNumber.Text = "";
                    nick.Close();
                    DisplayData();
                    ClearData();
                }
                catch (MySqlException ea)
                {
                    MessageBox.Show("Error: " + ea);
                }

            }
            btnSave.Enabled = true;
            txtName.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            lblUpdate.Visible = false;
            txtNN.Visible = false;
        }

        private void txtNN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm frm1 = new LoginForm();
            frm1.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblRoomNumber_Click(object sender, EventArgs e)
        {

        }

        private void txtRoomNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
