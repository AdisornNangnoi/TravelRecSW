using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;


namespace TravelRecSW
{
    public partial class FrmRegister : Form
    {
        byte[] travellerImage;

        public FrmRegister()
        {
            InitializeComponent();
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
            else if (travellerImage == null)
            {
                shareInfo.showWarningMSG("เลือกรูปด้วย");
            }
            else if (cbConfirm.Checked == false)
            {
                shareInfo.showWarningMSG("ยืนยันการลงทะเบียนด้วย");
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
                string strSql = "INSERT INTO traveller_tb " +
                    "(travellerFullname, travellerEmail, travellerPassword, travellerImage) " +
                    "VALUES " +
                    "(@travellerFullname, @travellerEmail, @travellerPassword, @travellerImage)";
                //สร้าง command
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = strSql;
                sqlCommand.Transaction = sqlTransaction;

                //กำหนดค่า parameter
                sqlCommand.Parameters.AddWithValue("@travellerFullname", tbTravellerFullname.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerEmail", tbTravellerEmail.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerPassword", tbTravellerPassword.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@travellerImage", travellerImage);

                //เริ่มบันทึก

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    conn.Close();
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว","ผลการทำงาน", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    FrmLogin frmLogin = new FrmLogin();
                    frmLogin.Show();
                    this.Hide();
                }
                catch(Exception ex)
                {
                    sqlTransaction.Rollback();
                    conn.Close();
                    shareInfo.showWarningMSG("ไม่สามารถบันทึกข้อมูลได้ ( " + ex.Message + " )" );
                }

            }
        }

        private void tsbtCancel_Click(object sender, EventArgs e)
        {
            pbTravellerImage.Image = Properties.Resources.profile;
            tbTravellerFullname.Clear();
            tbTravellerEmail.Clear();
            tbTravellerPassword.Clear();
            tbTravellerPasswordConfirm.Clear();
            cbConfirm.Checked = false;
        }

        private void tsbtToFrmLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            Hide();
        }
    }
}
