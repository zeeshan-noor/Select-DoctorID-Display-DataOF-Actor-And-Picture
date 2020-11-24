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
using System.IO;

namespace WindowsFormsApp2
{
    
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.LABCon);
        SqlCommand cmd;
        SqlDataAdapter da;
        //DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select DOCTORID from DOCTOR order by DOCTORID asc ";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["DOCTORID"].ToString());
            }
            con.Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-M7U802R\SQLEXPRESS;Initial Catalog=COMP_LAB5;Integrated Security=True");
            string sql = "select PICTURE from DOCTOR where DOCTORID='" + comboBox1.Text + "'";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


            sqlcon.Open();
           SqlCommand cmd1 = new SqlCommand(sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["PICTURE"]);
                pictureBox1.Image = new Bitmap(ms);
            }

            SqlCommand cmd2 = sqlcon.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select *from DOCTOR where DOCTORID ='" + comboBox1.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            da1.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                textBox1.Text = dr["ACTOR"].ToString();
        
                textBox3.Text = dr["SERIES"].ToString();
                textBox4.Text = dr["AGE"].ToString();
                listBox2.Items.Add("SERIES" + dr["SERIES"].ToString());

            }


            SqlCommand cmd3 = sqlcon.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "select *from EPISODE where SEASON ='" + comboBox1.Text + "'";
            cmd3.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da11 = new SqlDataAdapter(cmd3);
            da11.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
              
                textBox2.Text = dr["SEASONYEAR"].ToString();
               
                textBox5.Text = dr["TITLE"].ToString();


            }

            SqlCommand cmd4 = sqlcon.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "select ACTOR,NAME from COMPANION where DOCTORID ='" + comboBox1.Text + "'";
            cmd4.ExecuteNonQuery();
            DataTable  dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da1.Fill(dt4);
            foreach (DataRow dr in  dt4.Rows)
            {
               
                listBox2.Items.Add("NAME:"+dr["ACTOR"].ToString());

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
       

    }
}
