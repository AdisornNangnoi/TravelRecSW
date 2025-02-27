using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace TravelRecSW
{
    public partial class FrmTravelEdit : Form
    {
        private int travelId;
        public FrmTravelEdit(int travelId)
        {
            InitializeComponent();
            this.travelId = travelId;

        }

        private void FrmTravelEdit_Load(object sender, EventArgs e)
        {
            //ติดต่อ DB
            SqlConnection conn = new SqlConnection(shareInfo.connStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            //คำสั่ง SQL
            string strSql = "SELECT * FROM travel_tb " +
                            "WHERE travelId = @travelId";

            //สร้าง command
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = strSql;
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.Parameters.AddWithValue("@travelId", travelId);

            //สร้าง DataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            conn.Close();

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            tbTravelPlace.Text = dt.Rows[0]["travelPlace"].ToString();
            dtpTravelStartDate.Value = Convert.ToDateTime(dt.Rows[0]["travelStartDate"]);
            dtpTravelEndDate.Value = Convert.ToDateTime(dt.Rows[0]["travelEndDate"]);
            tbTravelCostTotal.Text = dt.Rows[0]["travelCostTotal"].ToString();
            pbTravelImage.Image = Image.FromStream(new MemoryStream((byte[])dt.Rows[0]["travelImage"]));
        }

        private void tsbtSave_Click(object sender, EventArgs e)
        {

        }

        private void tsbtCancel_Click(object sender, EventArgs e)
        {
            FrmTravelEdit_Load(sender, e);
        }

        private void tbTravelCostTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
