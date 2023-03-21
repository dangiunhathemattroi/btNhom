using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsAppNhom
{
    public partial class qlDoiTac : Form
    {
        public qlDoiTac()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=DESKTOP-D1DV4KN\\SQLEXPRESS;" + "Initial Catalog = quanlybandtdd;" + "Integrated Security = True";
        SqlConnection connection = null;

        //Khởi tạo các đối tượng SqlConnection,SqlDataAdapter,DataTable;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sql;
        SqlDataReader dr;
        private void qlDoiTac_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            sql = "select maDT as 'Mã đối tác', tenDT as 'Tên đối tác', maHH as 'Mã hàng hóa', tensp as 'Tên hàng hóa' from doiTac";
            da = new SqlDataAdapter(sql, connection);
            dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;

        }





        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        /*   public bool KTThongTin()
           {
               if (tbmadt.Text == "")
               {
                   MessageBox.Show("Vui lòng nhập mã đối tác ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tbmadt.Focus();
                   return false;
               }
               if (tbtendt.Text == "")
               {
                   MessageBox.Show("Vui lòng nhập Tên đối tác ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tbtendt.Focus();
                   return false;
               }
               if (tbmasp.Text == "")
               {
                   MessageBox.Show("Vui lòng nhập mã sản phẩm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tbmasp.Focus();
                   return false;
               }
               if (tbtensp.Text == "")
               {
                   MessageBox.Show("Vui lòng nhập tên sản phẩm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   tbtensp.Focus();
                   return false;
               }

               return true;
           }*/

        private void btThem_Click(object sender, EventArgs e)
        {



            sql = "insert into doiTac values (@maDT, @tenDT, @maHH, @tensp)";
            if (tbmadt.Text.Length == 0 || tbmasp.Text.Length == 0 || tbtendt.Text.Length == 0 || tbtensp.Text.Length == 0)
                MessageBox.Show("Hãy nhập dữ liệu");
            else
            {
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@maDT", tbmadt.Text);
                cmd.Parameters.AddWithValue("@tenDT", tbtendt.Text);
                cmd.Parameters.AddWithValue("@maHH", tbmasp.Text);
                cmd.Parameters.AddWithValue("@tensp", tbtensp.Text);
                cmd.ExecuteNonQuery();
                dt.Rows.Clear();
                da.Fill(dt);
            }


        }/*
        private void tbmadt_Enter(object sender, EventArgs e)
        {
            if (tbmadt.Text == "Thêm mới không cần nhập")
            {
                tbmadt.Clear();
                tbmadt.ForeColor = SystemColors.Highlight;
            }
        }
        private void tbmadt_Leave(object sender, EventArgs e)
        {
            if (tbmadt.Text == "")
            {
                tbmadt.Text = "Thêm mới không cần nhập";
                tbmadt.ForeColor = SystemColors.InactiveCaption;
            }
        }
    */
        int ddc;
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            ddc = e.RowIndex;
            tbmadt.Text = Convert.ToString(row.Cells["Mã đối tác "].Value);
            tbtendt.Text = Convert.ToString(row.Cells["Tên đối tác "].Value);
            tbmasp.Text = Convert.ToString(row.Cells["Mã sản phẩm "].Value);
            tbtensp.Text = Convert.ToString(row.Cells["Tên sản phẩm "].Value);
        }

        private void btXoa_Click(object sender, EventArgs e) {
            string a = dt.Rows[ddc][0].ToString();
        
            string xoa = "delete from doiTac where maDT = '" + a + "'";
            cmd = new SqlCommand(xoa, connection);
            cmd.ExecuteNonQuery();
            
            dt.Rows.Clear();
            da.Fill(dt);



        }
        private void btSua_Click(object sender, EventArgs e)
        {
            sql = "update doiTac set maDT=@maDT, tenDT=@tenDT, maHH=@maHH,tensp=@tensp where maDT=@maDT";

            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@maDT", tbmadt.Text);
            cmd.Parameters.AddWithValue("@tenDT", tbtendt.Text);
            cmd.Parameters.AddWithValue("@maHH", tbmasp.Text);
            cmd.Parameters.AddWithValue("@tensp", tbtensp.Text);
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            da.Fill(dt);
            dgv.DataSource = dt;
        }

       
    }
}
   
 

