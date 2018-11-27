using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        string Id;

        public Form2()
        {
            InitializeComponent();
        }

        void GetInventory()
        {
            
            da = new OleDbDataAdapter("SELECT * FROM tblInventory", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblInventory");
            dataGridView1.DataSource = ds.Tables["tblInventory"];
            con.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
            {
                con = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=|DataDirectory|Inventory Database.mdb");
            GetInventory();
        }

        


         private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
         {
             Id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
             txtCategory.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
             txtCategory.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
             txtDescription.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
             txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
             txtSPrice.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
             txtQuantity.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
         }

         private void btnInsert_Click_1(object sender, EventArgs e)
         {
             con.Open();
             string query = "Insert into tblInventory ([Category], [Name], [Description], [Price], [Selling Price], [Quantity]) values (@Category, @Name, @Description, @Price, @SPrice, @Quantity)";
             cmd = new OleDbCommand(query, con);

             cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
             cmd.Parameters.AddWithValue("@Name", txtName.Text);
             cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
             cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
             cmd.Parameters.AddWithValue("@SPrice", txtSPrice.Text);
             cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);

             cmd.ExecuteNonQuery();
             con.Close();
             MessageBox.Show("Add successfully");
             GetInventory();
             clear();
         }

         private void btnUpdat_Click(object sender, EventArgs e)
         {
             if (dataGridView1.CurrentRow.Selected == false)
             {
                 MessageBox.Show("Select item first!");
             }

             else
             {
             con.Open();
             string query = "Update tblInventory Set [Category]=@Category, [Name]=@Name, [Description]=@Description, [Price]=@Price, [Selling Price]=@SPrice, [Quantity]=@Quantity Where ID=@Id";
             cmd = new OleDbCommand(query, con);
            
             cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
             cmd.Parameters.AddWithValue("@Name", txtName.Text);
             cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
             cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
             cmd.Parameters.AddWithValue("@SPrice", txtSPrice.Text);
             cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
             cmd.Parameters.AddWithValue("@Id", Id);
             cmd.ExecuteNonQuery();
             con.Close();
             MessageBox.Show("update successful");
             GetInventory();
             clear();
             }
         }

         private void btnDelete_Click_1(object sender, EventArgs e)
         {
             con.Open();
             string query = "Delete From tblInventory Where ID=@id";
             cmd = new OleDbCommand(query, con);
             cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);

             cmd.ExecuteNonQuery();
             con.Close();
             MessageBox.Show("Delete successful");
             GetInventory();
             clear();
         }

         public void clear()
    {
      txtCategory.Clear();
             txtDescription.Clear();
             txtName.Clear();
             txtPrice.Clear();
             txtSPrice.Clear();
             txtQuantity.Clear();
             dataGridView1.ClearSelection();
    }

         private void btnCancel_Click(object sender, EventArgs e)
         {
             clear();
         }



         private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             Id = Convert.ToString(dataGridView1.CurrentRow.Cells["Id"].Value);
             txtCategory.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Category"].Value);
             txtName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Name"].Value);
             txtDescription.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Description"].Value);
             txtPrice.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Price"].Value);
             txtSPrice.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Selling Price"].Value);
             txtQuantity.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Quantity"].Value);
         }

         private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
             if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
         }

         private void txtSPrice_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
             if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
         }

         private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
             if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
         }

    }
}
