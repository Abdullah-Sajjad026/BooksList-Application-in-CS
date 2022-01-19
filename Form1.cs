using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD_Application
{
    public partial class MainForm : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        int Id = -1;

        string connectionString = "Data Source=localhost; Initial Catalog= ReadBooksListDB; Integrated Security=true;";


        public MainForm()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            displayData();
            clearForm();
        }

       public void displayData()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new SqlDataAdapter("SELECT * FROM BooksList", conn);
            adapter.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }
        public void clearForm()
        {
            nameBox.Text = "";
            authorBox.Text = "";
            reviewBox.Text = "";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            nameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            authorBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            reviewBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if(nameBox.Text == "" || authorBox.Text == "")
            {
                MessageBox.Show("Please fill required fields.");
            }
            else
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand($"INSERT INTO BooksList (Book_Name, Book_Author, Book_Review) values ('{nameBox.Text}', '{authorBox.Text}', '{reviewBox.Text}')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    clearForm();
                    displayData();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

     

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if(nameBox.Text == "" || authorBox.Text == "" || Id == -1)
            {
                MessageBox.Show("Double-Click a record to update it.");
            }
            else
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand($"UPDATE BooksList SET Book_Name = '{nameBox.Text}', Book_Author = '{authorBox.Text}', Book_Review = '{reviewBox.Text}' WHERE Id = {Id};", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Id = -1;
                    clearForm();
                    displayData();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (nameBox.Text == "" || authorBox.Text == "" || Id == -1)
            {
                MessageBox.Show("Double-Click a record to delete it.");
            }
            else
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand($"DELETE FROM BooksList WHERE Id={Id}", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Id = -1;
                    clearForm();
                    displayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
