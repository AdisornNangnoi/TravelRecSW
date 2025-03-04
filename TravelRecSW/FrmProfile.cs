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
    public partial class FrmProfile : Form

    {
        private int travellerId;
        byte[] travellerImage;
        public FrmProfile(int travellerId)
        {
            InitializeComponent();
            this.travellerId = travellerId;
        }


        private void FrmProfile_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(shareInfo.connStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            string strSql = "SELECT * FROM traveller_tb WHERE travellerId = @travellerId";
            SqlCommand sqlCommand = new SqlCommand(strSql, conn);
            sqlCommand.Parameters.AddWithValue("@travellerId", travellerId);

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                tbTravellerEmail.Text = dt.Rows[0]["travellerEmail"].ToString();
                tbTravellerFullname.Text = dt.Rows[0]["travellerFullname"].ToString();
                tbTravellerPassword.Text = dt.Rows[0]["travellerPassword"].ToString();
                tbTravellerPasswordConfirm.Text = dt.Rows[0]["travellerPassword"].ToString();

                if (dt.Rows[0]["travellerImage"] != DBNull.Value)
                {
                    shareInfo.travellerImage = (byte[])dt.Rows[0]["travellerImage"];
                    pbTravellerImage.Image = Image.FromStream(new MemoryStream(shareInfo.travellerImage));
                }
            }
        }

        private void btSelectTravellerImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbTravellerImage.Image = Image.FromFile(ofd.FileName);

                string extFile = Path.GetExtension(ofd.FileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    if (extFile == ".jpg" || extFile == ".jpeg")
                    {
                        pbTravellerImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        pbTravellerImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    travellerImage = ms.ToArray();
                }
            }
        }
        public event Action ProfileUpdated; // Event สำหรับแจ้งเตือนการอัปเดตข้อมูล

        private void tsbtSave_Click(object sender, EventArgs e)
        {
            if (tbTravellerFullname.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนชื่อด้วย");
            }
            else if (tbTravellerEmail.Text.Trim().Length == 0)
            {
                shareInfo.showWarningMSG("ป้อนอีเมล์ด้วย");
            }
            else if (!tbTravellerEmail.Text.Trim().Contains("@"))
            {
                shareInfo.showWarningMSG("ป้อนอีเมล์ให้ถูกต้องด้วย");
            }
            else if (tbTravellerPassword.Text.Trim().Length < 6)
            {
                shareInfo.showWarningMSG("รหัสผ่านต้องมากกว่า 6 ตัวอักษร");
            }
            else if (tbTravellerPasswordConfirm.Text.Trim() != tbTravellerPassword.Text.Trim())
            {
                shareInfo.showWarningMSG("รหัสผ่านต้องตรงกัน");
            }
            else if (cbConfirm.Checked == false)
            {
                shareInfo.showWarningMSG("ยืนยันการแก้ไขข้อมูลด้วย");
            }
            else
            {
                SqlConnection conn = new SqlConnection(shareInfo.connStr);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                byte[] updatedImage;
                if (travellerImage == null)
                {
                    string strSqlImage = "SELECT travellerImage FROM traveller_tb WHERE travellerId = @travellerId";
                    SqlCommand sqlCmdImage = new SqlCommand(strSqlImage, conn);
                    sqlCmdImage.Parameters.AddWithValue("@travellerId", travellerId);

                    object existingImage = sqlCmdImage.ExecuteScalar();
                    updatedImage = existingImage != DBNull.Value ? (byte[])existingImage : null;
                }
                else
                {
                    updatedImage = travellerImage;
                }

                string strSql = "UPDATE traveller_tb SET " +
                                "travellerFullname = @travellerFullname, " +
                                "travellerEmail = @travellerEmail, " +
                                "travellerPassword = @travellerPassword, " +
                                "travellerImage = @travellerImage " +
                                "WHERE travellerId = @travellerId";

                SqlTransaction sqlTransaction = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(strSql, conn, sqlTransaction);

                sqlCommand.Parameters.AddWithValue("@travellerFullname", tbTravellerFullname.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerEmail", tbTravellerEmail.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerPassword", tbTravellerPassword.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerImage", (object)updatedImage ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@travellerId", travellerId);

                try
                {
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        shareInfo.travellerFullname = tbTravellerFullname.Text.Trim();
                        shareInfo.travellerImage = updatedImage;

                        // เรียก Event แจ้งให้ `FrmTravelOpt` ทราบว่ามีการอัปเดต
                        ProfileUpdated?.Invoke();

                        MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
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
        }

        private void tsbtCancel_Click(object sender, EventArgs e)
        {
            FrmProfile_Load(sender, e);
        }

        private void tsbtToFrmLogin_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void tbTravellerPassword_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int showToolTipTime = 3000;

            ToolTip tt = new ToolTip();
            tt.Show("รหัสผ่านต้อง 6 ตัวขึ้นไป", tb, 20, 20, showToolTipTime);
        }

        private void tbTravellerPasswordConfirm_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int showToolTipTime = 3000;

            ToolTip tt = new ToolTip();
            tt.Show("รหัสผ่านต้อง 6 ตัวขึ้นไป", tb, 20, 20, showToolTipTime);
        }
    }
}
