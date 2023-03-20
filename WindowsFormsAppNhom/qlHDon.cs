using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppNhom
{
    public partial class qlHDon : Form
    {
        string connectionString = "Data Source=ADMIN\\QUOCHUY;Initial Catalog=quanlybandtdd; Integrated Security=True";
        SqlConnection connection = null;
        DataTable dttb;
        public qlHDon()
        {
            InitializeComponent();
        }

        private void qlHDon_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string qerry = "select maKh,tenKh,soDienThoai from khachHang";
            string querry = "select mahh,giaban from hanghoa";
            SqlDataAdapter dtp = new SqlDataAdapter(qerry, connection);
            dttb = new DataTable();
            dtp.Fill(dttb);
            DtgvDSHD.DataSource = dttb;
        }
    }
}
