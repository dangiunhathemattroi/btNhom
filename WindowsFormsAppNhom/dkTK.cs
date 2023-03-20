using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsAppNhom
{
    public partial class dkTK : Form
    {
        public dkTK()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=ADMIN\\QUOCHUY;Initial Catalog=quanlybandtdd; Integrated Security=True";
        SqlConnection connection = null;
        //băm mật khẩu
        public static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }



        private void cbDKHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDKHienMK.Checked)
            {
                txtDKMK.PasswordChar = '\0';
                txtDKXN.PasswordChar = '\0';
            }
            else
            {
                txtDKMK.PasswordChar = '*';
                txtDKXN.PasswordChar = '*';
            }
        }

        private void bntDKDN_Click(object sender, EventArgs e)
        {
            this.Hide();
            dangNhap dangNhap = new dangNhap();
            dangNhap.ShowDialog(this);
            this.Close();
        }

        private void bntDKDK_Click(object sender, EventArgs e)
        {
            if (txtDKHo.Text == "")
            {
                txtDKHo.Focus();
            }
            else if (txtDKTen.Text == "")
            {
                txtDKTen.Focus();
            }
            else if (txtDKSDT.Text == "")
            {
                txtDKSDT.Focus();
            }
            else if (txtDKMK.Text == "")
            {
                txtDKMK.Focus();
            }
            else if (txtDKXN.Text == "")
            {
                txtDKXN.Focus();
            }
            else
            {
                try
                {
                    int sdt = int.Parse(txtDKSDT.Text);
                    if (txtDKMK.Text == txtDKXN.Text)
                    {
                        if (sdt.ToString().Length > 11 || sdt.ToString().Length < 8)
                        {
                            txtDKSDT.Text = "";
                            txtDKMK.Focus();
                        }
                        if (txtDKMK.Text != txtDKXN.Text)
                        {
                            txtDKXN.Text = "";
                            txtDKMK.Text = "";
                            txtDKMK.Focus();
                        }
                        DialogResult result = MessageBox.Show(
                            "Bạn có muốn đăng ký tài khoản không.",
                            "Thông báo",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                            );
                        if (result == DialogResult.Yes)
                        {
                            connection = new SqlConnection(connectionString);
                            connection.Open();
                            string input = txtDKMK.Text;
                            string hash = ComputeMD5Hash(input);
                            string query = "INSERT INTO khachHang (maKH, tenKH, soDienThoai, matKhau) VALUES(" + customerName + ", '" + txtDKHo.Text + "" + txtDKTen.Text + "', '" + txtDKSDT.Text + "', '" + hash + "'); ";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            this.Hide();
                            dangNhap dangNhap = new dangNhap();
                            dangNhap.ShowDialog(this);
                            this.Close();
                        }
                        else
                        {
                            
                            this.Close();
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(
                            "Mật khẩu không khớp.",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        txtDKMK.Text = "";
                        txtDKXN.Text = "";
                        txtDKMK.Focus();
                    }

                }
                catch
                {
                    DialogResult result1 = MessageBox.Show(
                        "Số điện thoại không hợp lệ.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    if (result1 == DialogResult.OK)
                    {
                        txtDKSDT.Text = "";
                        txtDKSDT.Focus();
                    }
                    if (txtDKMK.Text != txtDKXN.Text)
                    {
                        DialogResult result = MessageBox.Show(
                            "Mật khẩu không khớp.",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        if (result == DialogResult.OK)
                        {
                            txtDKMK.Text = "";
                            txtDKXN.Text = "";
                            txtDKMK.Focus();
                        }

                    }
                }
            }
        }

        int customerName = 0;
        private void dkTK_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string mnd = "select count(*) as sl from khachHang";
            SqlCommand command = new SqlCommand(mnd, connection);
            SqlDataReader reader = command.ExecuteReader();

            // Lưu dữ liệu vào biến
            while (reader.Read())
            {
                customerName = (int)reader["sl"];
            }
            Console.WriteLine(customerName);
            customerName++;

        }


    }
}
