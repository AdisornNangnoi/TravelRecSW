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
    public partial class FrmTravelOpt : Form
    {   
        public FrmTravelOpt()
        {
            InitializeComponent();
        }

        private void getTravelFromDBtoDGV() {
            //ติดต่อ DB
            SqlConnection conn = new SqlConnection(shareInfo.connStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            //คำสั่ง SQL
            string strSql = "SELECT travelPlace, travelCostTotal, travelImage,travelId FROM travel_tb " +
                            "WHERE travellerId = @travellerId";
            //สร้าง command
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = strSql;
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.Parameters.AddWithValue("@travellerId", shareInfo.travellerId);
            //สร้าง DataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            conn.Close();

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dgvTravel.RowTemplate.Height = 50;


                dgvTravel.DataSource = dt;

                dgvTravel.Columns[0].HeaderText = "สถานที่ที่ไป";
                dgvTravel.Columns[1].HeaderText = "ค่าใช้จ่าย";
                dgvTravel.Columns[2].HeaderText = "รูปสถานที่ที่ไป";
                dgvTravel.Columns[3].Visible = false;

                dgvTravel.Columns[0].Width = 150;
                dgvTravel.Columns[1].Width = 120;
                dgvTravel.Columns[2].Width = 200;

                DataGridViewImageColumn dgvImage = new DataGridViewImageColumn();
                dgvImage = (DataGridViewImageColumn)dgvTravel.Columns[2];
                dgvImage.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            else
            {

            }
        }

        private void pbTravellerImage_Click(object sender, EventArgs e)
        {
            FrmProfile frmProfile = new FrmProfile(shareInfo.travellerId);

            // Subscribe Event เพื่ออัปเดตข้อมูลแบบเรียลไทม์
            frmProfile.ProfileUpdated += UpdateProfileInfo;

            frmProfile.ShowDialog(this);
        }

        private void lbTravellerFullname_Click(object sender, EventArgs e)
        {
            FrmProfile frmProfile = new FrmProfile(shareInfo.travellerId);

            // Subscribe Event เพื่ออัปเดตข้อมูลแบบเรียลไทม์
            frmProfile.ProfileUpdated += UpdateProfileInfo;

            frmProfile.ShowDialog(this);
        }

        // ฟังก์ชันสำหรับอัปเดตข้อมูลเมื่อ `ProfileUpdated` ถูกเรียก
        private void UpdateProfileInfo()
        {
            lbTravellerFullname.Text = shareInfo.travellerFullname;

            if (shareInfo.travellerImage != null)
            {
                using (MemoryStream ms = new MemoryStream(shareInfo.travellerImage))
                {
                    pbTravellerImage.Image = Image.FromStream(ms);
                }
            }
        }

        private void FrmTravelOpt_Load(object sender, EventArgs e)
        {
            //โชว์รูป
            using (MemoryStream ms = new MemoryStream(shareInfo.travellerImage))
            {
                pbTravellerImage.Image = Image.FromStream(ms);
            }
            //โชว์ชื่อผู้ใช้
            lbTravellerFullname.Text = shareInfo.travellerFullname;

            //โชว์ข้อมูล
            getTravelFromDBtoDGV();
        }

        private void tsbtToFrmLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            Hide();
        }

        private void tsbtAdd_Click(object sender, EventArgs e)
        {
            FrmTravelAdd frmAdd = new FrmTravelAdd();
            frmAdd.ShowDialog(this);
            getTravelFromDBtoDGV();

        }

        private void tsbtEdit_Click(object sender, EventArgs e)
        {
            if (dgvTravel.SelectedRows.Count <= 0)
            {
                shareInfo.showWarningMSG("เลือกข้อมูลที่ต้องการแก้ไขด้วย");
            }
            else
            {
                int indexRow = dgvTravel.CurrentRow.Index;
                int travelId = int.Parse(dgvTravel.Rows[indexRow].Cells[3].Value.ToString());
                FrmTravelEdit frmTravelEdit = new FrmTravelEdit(travelId);
                frmTravelEdit.ShowDialog(this);
                getTravelFromDBtoDGV();
            }

            
        }

        private void tsbtDelete_Click(object sender, EventArgs e)
        {
            // ยืนยันก่อนลบ
            DialogResult result = MessageBox.Show("คุณต้องการลบข้อมูลนี้ใช่หรือไม่?",
                                                  "ยืนยันการลบ",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int indexRow = dgvTravel.CurrentRow.Index;
                int travelId = int.Parse(dgvTravel.Rows[indexRow].Cells[3].Value.ToString());
                // ติดต่อฐานข้อมูล
                SqlConnection conn = new SqlConnection(shareInfo.connStr);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                // คำสั่ง SQL สำหรับลบข้อมูล
                string strSql = "DELETE FROM travel_tb WHERE travelId = @travelId";

                // ใช้ Transaction ป้องกันปัญหาข้อมูลเสียหาย
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(strSql, conn, sqlTransaction);

                // กำหนดค่า parameter
                sqlCommand.Parameters.AddWithValue("@travelId", travelId);

                try
                {
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmTravelOpt_Load(sender, e);

                    }
                    else
                    {
                        shareInfo.showWarningMSG("ไม่พบข้อมูลที่ต้องการลบ");
                    }
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    conn.Close();
                    shareInfo.showWarningMSG("ไม่สามารถลบข้อมูลได้ ( " + ex.Message + " )");
                }
            }
        }

        
    }
}
