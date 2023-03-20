using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppNhom
{
    public partial class xemSP : Form
    {
        string connectionString = "Data Source=ADMIN\\QUOCHUY;Initial Catalog=quanlybandtdd; Integrated Security=True";
        SqlConnection connection = null;
        public xemSP()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            dkTK dkTK = new dkTK();
            dkTK.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            dangNhap dangNhap = new dangNhap();
            dangNhap.ShowDialog();
        }

        private void xemSP_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select linkImg from hangHoa";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = null;
            List<object> dataList = new List<object>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Lấy giá trị của cột từ SqlDataReader và chuyển đổi nó thành kiểu dữ liệu đối tượng (object)
                object data = reader.GetValue(0);

                // Thêm giá trị của cột vào danh sách
                dataList.Add(data);
                textBox1.Text = data.ToString();
            }
            reader.Close();
            pictureBox1.ImageLocation = (string)dataList[0];
            pictureBox2.ImageLocation = (string)dataList[1];
            pictureBox3.ImageLocation = (string)dataList[2];
            pictureBox4.ImageLocation = (string)dataList[3];
            pictureBox5.ImageLocation = (string)dataList[4];
            pictureBox6.ImageLocation = (string)dataList[5];
            pictureBox7.ImageLocation = (string)dataList[7];
            pictureBox8.ImageLocation = (string)dataList[7];
            pictureBox9.ImageLocation = (string)dataList[8];
            //textBox1.Text = (string)dataList[6];

        }
    }
}   
