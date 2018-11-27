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
    public partial class Form1 : Form
    {

        OleDbDataReader dr;
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        void GetInventory()
        {

            da = new OleDbDataAdapter("SELECT * FROM tblLogin", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "tblLogin");
            dataGridView1.DataSource = ds.Tables["tblLogin"];
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=|DataDirectory|Inventory Database.mdb");
            GetInventory();

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string usr = txtUser.Text;
            string psw = txtPass.Text;
            
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tblLogin where Username='" + txtUser.Text + "' AND Password='" + txtPass.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Login successful");
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect");
            }

            con.Close();
        }
    }
}
