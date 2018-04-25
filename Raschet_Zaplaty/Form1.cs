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
using System.Configuration;

namespace Raschet_Zaplaty
{
    public partial class Form1 : Form
    {
        double f_nachisleno, f_premiya, f_natbavka, f_summa, f_pdv, f_kVidache, f_oklad2, f_staj2, f_vseg_d, f_otr_d;
        string oklad2, staj2, vseg_d, otr_d, id_sort2, s_premiya;

        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
    
        // моя функция
        private void View( string tbl, string div_view)
        {
            con.Open();
            String query = "SELECT * FROM " + tbl;
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            if(div_view == "dataGridView1")
            {
                dataGridView1.DataSource = dt;
            } else
            {
                dataGridView2.DataSource = dt;
            }
            con.Close();
            DataGridViewColumn column_id_sotr = dataGridView1.Columns[0];
            column_id_sotr.Width = 75;
            DataGridViewColumn column_name = dataGridView1.Columns[1];
            column_name.Width = 276;
           
        }

        private void ClerFild1()
        {
            textBox1.Text = "";
            name.Text = "";
            post.Text = "";
            salary.Text = "";
            staj.Text = "";
            birthday.Text = "";
            gender.Text = "";
            phone.Text = "";
        }

        private void ClerFild2()
        {
            id_zapisi.Text = "";
            id_sort_tbl2.Text = "";
            dateNarah.Text = "";
            prozent.Text = "";
            vsegoDays.Text = "";
            otrabotanoDay.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            View("tbl_sotrudniki", "dataGridView1");
            View("tbl_vedomostey_zp", "dataGridView2");
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            con.Open();
            String query = "INSERT INTO tbl_sotrudniki (Name,Doljnost,Oklad,Staj,Birthday,Gender,Phone) VALUES('" + name.Text + "','" + post.Text + "','" + salary.Text + "','" + staj.Text + "','" + birthday.Text + "','" + gender.Text + "','" + phone.Text + "')";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            status.Text = "The entry was created!";
            View("tbl_sotrudniki", "dataGridView1");
            ClerFild1();
        }

        private void update_Click_1(object sender, EventArgs e)
        {
            con.Open();
            String query = "UPDATE tbl_sotrudniki SET Name = '" + name.Text + "', Doljnost = '" + post.Text + "', Oklad = '" + salary.Text + "', Staj = '" + staj.Text + "', Birthday = '" + birthday.Text + "', Gender = '" + gender.Text + "', Phone = '" + phone.Text + "' WHERE id_sotrudnika = '" + textBox1.Text + "'";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            status.Text = "The entry was update.";
            View("tbl_sotrudniki", "dataGridView1");
        }

        private void newSotrudnik_Click(object sender, EventArgs e)
        {
            ClerFild1();
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            post.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            salary.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            staj.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            birthday.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            gender.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            phone.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id_zapisi.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            id_sort_tbl2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            dateNarah.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            vsegoDays.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            otrabotanoDay.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
            prozent.Text = "0";
        }

        private void delete_Click_1(object sender, EventArgs e)
        {
            con.Open();
            String query = "DELETE tbl_sotrudniki WHERE id_sotrudnika = '" + textBox1.Text + "'";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            status.Text = "The entry was deleted.";
            View("tbl_sotrudniki", "dataGridView1");
            ClerFild1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            String query = "DELETE tbl_vedomostey_zp WHERE id_rascheta = '" + id_zapisi.Text + "'";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            status_page2.Text = "The entry was deleted.";
            View("tbl_vedomostey_zp", "dataGridView2");
            ClerFild2();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            id_sort_tbl2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            id_sort2 = id_sort_tbl2.Text;
            oklad2 = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            staj2 = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            tabControl1.SelectedIndex = 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClerFild2();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            vseg_d = vsegoDays.Text;
            otr_d = otrabotanoDay.Text;
            s_premiya = prozent.Text;
            f_vseg_d = double.Parse(vseg_d);
            f_otr_d = double.Parse(otr_d);
            f_oklad2 = double.Parse(oklad2); 
            f_premiya = double.Parse(s_premiya);
            f_staj2 = double.Parse(staj2);

            f_nachisleno = f_oklad2 / f_vseg_d * f_otr_d;
            f_premiya = (f_nachisleno / 100) * f_premiya;
            if (f_staj2 <= 10)
                f_natbavka = (f_nachisleno / 100) * 10;
            else
                f_natbavka = (f_nachisleno / 100) * 20;
            f_summa = f_natbavka + f_premiya + f_nachisleno;
            f_pdv = (f_summa / 100) * 18;
            f_kVidache = f_summa - f_pdv;

            f_kVidache = Math.Round(f_kVidache, 2);
            f_pdv = Math.Round(f_pdv, 2);
            f_summa = Math.Round(f_summa, 2);
            f_natbavka = Math.Round(f_natbavka, 2);
            f_premiya = Math.Round(f_premiya, 2);
            f_nachisleno = Math.Round(f_nachisleno, 2);

            con.Open();
            String query = "INSERT INTO tbl_vedomostey_zp (id_sotrudnik,date,VsegoRobDney,OtrabotanoDney,Nachisleno,Premiya,Natbavka,Summa,PDV,SummaKVidache) VALUES('" + id_sort2 + "','" + dateNarah.Text + "','" + vsegoDays.Text + "','" + otrabotanoDay.Text + "','" + f_nachisleno.ToString() + "','" + f_premiya.ToString() + "','" + f_natbavka.ToString() + "','" + f_summa.ToString() + "','" + f_pdv.ToString() + "','" + f_kVidache.ToString() + "')";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            status_page2.Text = "The entry was created!";
            View("tbl_vedomostey_zp", "dataGridView2");
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView2.Size.Width, dataGridView2.Size.Height);
            dataGridView2.DrawToBitmap(bmp, dataGridView2.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
