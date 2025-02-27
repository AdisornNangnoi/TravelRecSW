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

namespace TravelRecSW
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void lbToFrmRegister_Click(object sender, EventArgs e)
        {
            FrmRegister frmRegister = new FrmRegister();
            frmRegister.Show();
            Hide();

        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbTravellerEmail.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนอีเมล์ด้วย");
            }
            else if (tbTravellerPassword.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนรหัสผ่านด้วย");
            }
            else
            {
                //ติดต่อ DB
                SqlConnection conn = new SqlConnection(shareInfo.connStr);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                string strSql = "SELECT * FROM traveller_tb WHERE " +
                                "travellerEmail = @travellerEmail AND " +
                                "travellerPassword = @travellerPassword";

                //สร้าง command
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = strSql;
                sqlCommand.Transaction = sqlTransaction;

                //กำหนดค่า parameter
                sqlCommand.Parameters.AddWithValue("@travellerEmail", tbTravellerEmail.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerPassword", tbTravellerPassword.Text.Trim());

                //สั่งให้ command ทำงาน (Select)
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                conn.Close();

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    shareInfo.travellerId = Convert.ToInt32(dt.Rows[0]["travellerId"]);
                    shareInfo.travellerFullname = dt.Rows[0]["travellerFullname"].ToString();
                    shareInfo.travellerEmail = dt.Rows[0]["travellerEmail"].ToString();
                    shareInfo.travellerPassword = dt.Rows[0]["travellerPassword"].ToString();
                    shareInfo.travellerImage = (byte[])dt.Rows[0]["travellerImage"];

                    FrmTravelOpt frmTravelOpt = new FrmTravelOpt();
                    frmTravelOpt.Show();
                    Hide();
                }
                else
                {
                    shareInfo.showWarningMSG("อีเมล์หรือรหัสผ่านไม่ถูกต้อง");
                }
            }
        }
    }
}
