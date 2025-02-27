using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelRecSW
{
    public partial class FrmTravelAdd : Form
    {
        byte[] travelImage;

        public FrmTravelAdd()
        {
            InitializeComponent();
        }

        private void tsbtSave_Click(object sender, EventArgs e)
        {
            if (tbTravelPlace.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนชื่อสถานที่ด้วย");
            }
            else if (dtpTravelEndDate.Value < dtpTravelStartDate.Value)
            {
                shareInfo.showWarningMSG("วันที่กลับต้องไม่น้อยกว่าหรือวันเดียวกันกับวันที่ไป");
            }
            
            else if(tbTravelCostTotal.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนค่าใช้จ่ายด้วย");
            }
            else if (pbTravelImage.Image == null)
            {
                shareInfo.showWarningMSG("เลือกรูปภาพด้วย");
            }
            else
            {
                //ส่งข้อมูลไปบันทึก
                //ติดต่อ DB
                SqlConnection conn = new SqlConnection(shareInfo.connStr);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                //คำสั่ง SQL
                string strSql = "INSERT INTO travel_tb " +
                    "(travelPlace, travelStartDate, travelEndDate, travelCostTotal, travelImage, travellerId) " +
                    "VALUES " +
                    "(@travelPlace, @travelStartDate, @travelEndDate, @travelCostTotal, @travelImage , @travellerId)";

                //สร้าง command
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = strSql;
                sqlCommand.Transaction = sqlTransaction;

                //กำหนดค่า parameter
                sqlCommand.Parameters.AddWithValue("@travelPlace", tbTravelPlace.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travelStartDate", dtpTravelStartDate.Value.Date);
                sqlCommand.Parameters.AddWithValue("@travelEndDate", dtpTravelEndDate.Value.Date);
                sqlCommand.Parameters.AddWithValue("@travelCostTotal", float.Parse( tbTravelCostTotal.Text.Trim()));
                sqlCommand.Parameters.AddWithValue("@travelImage", travelImage);
                sqlCommand.Parameters.AddWithValue("@travellerId", shareInfo.travellerId);

                //เริ่มบันทึก

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    conn.Close();

                    MessageBox.Show("บันทึกข้อมูลการเดินทางเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    //FrmTravelOpt frmTravelOpt = new FrmTravelOpt();
                    //frmTravelOpt.Show();
                    //Hide();

                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    conn.Close();
                    shareInfo.showWarningMSG("ไม่สามารถบันทึกข้อมูลได้ ( " + ex.Message + " )");
                }
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

        private void tbTravelCostTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tsbtCancel_Click(object sender, EventArgs e)
        {
            tbTravelPlace.Clear();
            dtpTravelStartDate.Value = DateTime.Now;
            dtpTravelEndDate.Value = DateTime.Now;
            tbTravelCostTotal.Clear();
            pbTravelImage.Image = Properties.Resources.logo;
            travelImage = null;
        }

        private void tsbtToFrmLogin_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
