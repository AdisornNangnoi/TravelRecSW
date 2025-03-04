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
        byte[] travelImage;
        public FrmTravelEdit(int travelId)
        {
            InitializeComponent();
            this.travelId = travelId;

        }

        private void FrmTravelEdit_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(shareInfo.connStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            string strSql = "SELECT * FROM travel_tb WHERE travelId = @travelId";
            SqlCommand sqlCommand = new SqlCommand(strSql, conn);
            sqlCommand.Parameters.AddWithValue("@travelId", travelId);

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                tbTravelPlace.Text = dt.Rows[0]["travelPlace"].ToString();
                dtpTravelStartDate.Value = Convert.ToDateTime(dt.Rows[0]["travelStartDate"]);
                dtpTravelEndDate.Value = Convert.ToDateTime(dt.Rows[0]["travelEndDate"]);
                tbTravelCostTotal.Text = dt.Rows[0]["travelCostTotal"].ToString();

                // โหลดรูปเดิมเก็บไว้
                if (dt.Rows[0]["travelImage"] != DBNull.Value)
                {
                    travelImage = (byte[])dt.Rows[0]["travelImage"];
                    pbTravelImage.Image = Image.FromStream(new MemoryStream(travelImage));
                }
            }
        }

        private void tsbtSave_Click(object sender, EventArgs e)
        {
            if (tbTravelPlace.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนชื่อสถานที่ด้วย");
                return;
            }
            if (dtpTravelEndDate.Value < dtpTravelStartDate.Value)
            {
                shareInfo.showWarningMSG("วันที่กลับต้องไม่น้อยกว่าหรือวันเดียวกันกับวันที่ไป");
                return;
            }
            if (tbTravelCostTotal.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนค่าใช้จ่ายด้วย");
                return;
            }

            SqlConnection conn = new SqlConnection(shareInfo.connStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            string strSql = "UPDATE travel_tb SET " +
                "travelPlace = @travelPlace, " +
                "travelStartDate = @travelStartDate, " +
                "travelEndDate = @travelEndDate, " +
                "travelCostTotal = @travelCostTotal, " +
                "travelImage = @travelImage " +
                "WHERE travelId = @travelId";

            SqlTransaction sqlTransaction = conn.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand(strSql, conn, sqlTransaction);

            sqlCommand.Parameters.AddWithValue("@travelId", travelId);
            sqlCommand.Parameters.AddWithValue("@travelPlace", tbTravelPlace.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@travelStartDate", dtpTravelStartDate.Value.Date);
            sqlCommand.Parameters.AddWithValue("@travelEndDate", dtpTravelEndDate.Value.Date);
            sqlCommand.Parameters.AddWithValue("@travelCostTotal", float.Parse(tbTravelCostTotal.Text.Trim()));

            // ตรวจสอบว่า travelImage มีค่าไหม ถ้าไม่มีให้ใช้รูปเดิม
            if (pbTravelImage.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pbTravelImage.Image.Save(ms, pbTravelImage.Image.RawFormat);
                    travelImage = ms.ToArray();
                }
            }

            sqlCommand.Parameters.AddWithValue("@travelImage", travelImage ?? (object)DBNull.Value);

            try
            {
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("อัปเดตข้อมูลการเดินทางเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                }
                else
                {
                    shareInfo.showWarningMSG("ไม่พบข้อมูลที่ต้องการอัปเดต");
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                conn.Close();
                shareInfo.showWarningMSG("ไม่สามารถอัปเดตข้อมูลได้ ( " + ex.Message + " )");
            }
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

        private void btSelectTravelImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbTravelImage.Image = Image.FromFile(ofd.FileName);

                string extFile = Path.GetExtension(ofd.FileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    if (extFile == ".jpg" || extFile == ".jpeg")
                    {
                        pbTravelImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        pbTravelImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    travelImage = ms.ToArray();
                }
            }
        }

        private void tsbtToFrmLogin_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
