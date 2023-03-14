using GongMe.DataTier.Models;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GongMe.PresentionTier
{
    public partial class FrmChamCong : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        int month, year;

        string MaNhanVien = "", HoTen = "", MaChucVu = "";

        public FrmChamCong()
        {
            InitializeComponent();
        }
        public FrmChamCong(string MaNhanVien, string HoTen, string MaChucVu)
        {
            InitializeComponent();
            this.MaNhanVien = MaNhanVien;
            this.HoTen = HoTen;
            this.MaChucVu = MaChucVu;
        }

        private void FrmChamCong_Load(object sender, EventArgs e)
        {
            timer1.Start();

            lbOnlineUser.Text = HoTen; //Đang hoạt động

            displayDays(); //Lịch

            userstatus(); //Trạng thái chấm công

            KiemTraChamCong(); //Kiểm tra chấm công

            PhanQuyenQLChamCong(); //Phân quyền QL

            ChonNhanVienChamCong();
        }

        //Hiển thị trạng thái chấm công
        private void userstatus()
        {
            String querryTrangThai = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' )";
            SqlDataAdapter sdaTrangThai = new SqlDataAdapter(querryTrangThai, conn);

            DataTable dtableTrangThai = new DataTable();
            sdaTrangThai.Fill(dtableTrangThai);

            try
            {

                if (dtableTrangThai.Rows[0][4].ToString().Trim() == null)
                {
                    lbUserStatus.Text = "Chưa chấm công"; //Trạng thái
                }
                else
                {
                    lbUserStatus.Text = dtableTrangThai.Rows[0][4].ToString().Trim(); //Trạng thái
                }
            }
            catch
            {

            }
        }

        private void displayDays()
        {
            DateTime now = DateTime.Now;

            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthname + " " + year;

            //Lấy ngày bắt đầu của tháng
            DateTime batdauthang = new DateTime(year,month,1);

            //Lấy số ngày trong tháng
            int days = DateTime.DaysInMonth(year,month);

            //Convert batdauthang thành int
            int ngaytrongtuan = Convert.ToInt32(batdauthang.DayOfWeek.ToString("d"));

            //user control trống
            for (int i = 1; i < ngaytrongtuan; i++)
            {
                UCBlank ucblank = new UCBlank();
                flpnlCalendar.Controls.Add(ucblank);
            }

            //user control ngày
            for (int i = 1; i <= days; i++)
            {
                UCCalendar uccalendar = new UCCalendar();
                uccalendar.days(i);
                flpnlCalendar.Controls.Add(uccalendar);

                //Hiển thị trạng thái theo ngày
                String querrystatus = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + year + "/" + month + "/" + i + "')";
                SqlDataAdapter sdastatus = new SqlDataAdapter(querrystatus, conn);

                DataTable dtablestatus = new DataTable();
                sdastatus.Fill(dtablestatus);

                try
                {
                    uccalendar.trangthai(dtablestatus.Rows[0][4].ToString().Trim());
                }
                catch
                {
                    uccalendar.trangthai("");
                }
            }
        }

        //Hiển thị ngày giờ hôm nay
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToLongTimeString();
            lbtoday.Text = DateTime.Now.ToShortDateString();
        }

        //Check In
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmdcheckin = new SqlCommand("INSERT INTO [dbo].[ChamCong] ([MaNhanVien], [NgayChamCong], [CheckIn], [TrangThai]) VALUES('" + MaNhanVien + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToLongTimeString() + "', '" + "Đã check in" + "')", conn);
            cmdcheckin.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Check in thành công lúc " + DateTime.Now.ToLongTimeString() + " ngày " + DateTime.Now.ToShortDateString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            userstatus(); //Load lại trạng thái

            //Load lại lịch
            flpnlCalendar.Controls.Clear();
            displayDays();

            btnCheckOut.Enabled = true;
            btnCheckIn.Enabled = false;
        }

        //Check Out
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            SqlCommand cmdcheckout = new SqlCommand("UPDATE [dbo].[ChamCong] SET [CheckOut] = '" + DateTime.Now.ToLongTimeString() + "', [TrangThai] = '" + "Hoàn thành" + "' WHERE ([NgayChamCong] = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' AND [MaNhanVien] = '" + MaNhanVien + "')", conn);
            conn.Open();
            cmdcheckout.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Check out thành công lúc " + DateTime.Now.ToLongTimeString() + " ngày " + DateTime.Now.ToShortDateString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            userstatus(); //Load lại trạng thái

            //Load lại lịch
            flpnlCalendar.Controls.Clear();
            displayDays();

            btnCheckOut.Enabled = false;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //Clear flpnl
            flpnlCalendar.Controls.Clear();

            //Giảm tháng
            month--;

            //Giảm năm
            if (month == 0)
            {
                month = 12;
                year--;
            }

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthname + " " + year;

            //Lấy ngày bắt đầu của tháng
            DateTime batdauthang = new DateTime(year, month, 1);

            //Lấy số ngày trong tháng
            int days = DateTime.DaysInMonth(year, month);

            //Convert batdauthang thành int
            int ngaytrongtuan = Convert.ToInt32(batdauthang.DayOfWeek.ToString("d"));

            //user control trống
            for (int i = 1; i < ngaytrongtuan; i++)
            {
                UCBlank ucblank = new UCBlank();
                flpnlCalendar.Controls.Add(ucblank);
            }

            //user control ngày
            for (int i = 1; i <= days; i++)
            {
                UCCalendar uccalendar = new UCCalendar();
                uccalendar.days(i);
                flpnlCalendar.Controls.Add(uccalendar);

                //Hiển thị trạng thái theo ngày
                String querrystatus = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + year + "/" + month + "/" + i + "')";
                SqlDataAdapter sdastatus = new SqlDataAdapter(querrystatus, conn);

                DataTable dtablestatus = new DataTable();
                sdastatus.Fill(dtablestatus);

                try
                {
                    uccalendar.trangthai(dtablestatus.Rows[0][4].ToString().Trim());
                }
                catch
                {
                    uccalendar.trangthai("");
                }
            }
        }

        private void btnQLChamCong_Click(object sender, EventArgs e)
        {
            pnlQLChamCong.Visible = true;
            pnlChamCong.Visible = false;

            btnQLChamCong.Visible = false;
            btnBackChamCong.Visible = true;

            pnlLine.Size = new Size(3, 660);
            pnlLine.Location = new Point(267, 0);
        }

        private void btnBackChamCong_Click(object sender, EventArgs e)
        {
            pnlQLChamCong.Visible = false;
            pnlChamCong.Visible = true;

            btnQLChamCong.Visible = true;
            btnBackChamCong.Visible = false;

            pnlLine.Size = new Size(3, 110);
            pnlLine.Location = new Point(267, 0);
        }

        void UpdateSoGioLam()
        {
            //Update số giờ làm
            SqlCommand cmdUpdate = new SqlCommand("UPDATE [dbo].[ChamCong] SET [SoGioLam] = DATEDIFF(HOUR, CheckIn, CheckOut)", conn);
            conn.Open();
            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }

        void ChonNhanVienChamCong()
        {
            //Combobox nhân viên chấm công
            String querryNhanVienCC = "SELECT *FROM NhanVien";
            SqlDataAdapter sdaNhanVienCC = new SqlDataAdapter(querryNhanVienCC, conn);

            DataTable dtableNhanVienCC = new DataTable();
            sdaNhanVienCC.Fill(dtableNhanVienCC);

            cbxNhanVienCC.DataSource = dtableNhanVienCC;
            cbxNhanVienCC.ValueMember = "MaNhanVien";
            cbxNhanVienCC.DisplayMember = "HoTen";
        }

        //Hiển thị danh sách chấm công của nhân viên
        void HienThiDanhSachChamCong()
        {
            lvsChamCong.Items.Clear();

            String querryChamCong = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + cbxNhanVienCC.SelectedValue.ToString().Trim() + "' AND MONTH(NgayChamCong) = '" + txtThangCC.Text + "')";
            SqlDataAdapter sdaChamCong = new SqlDataAdapter(querryChamCong, conn);

            DataTable dtableChamCong = new DataTable();
            sdaChamCong.Fill(dtableChamCong);

            for (int i = 0; i < dtableChamCong.Rows.Count; i++)
            {
                ListViewItem lvi = lvsChamCong.Items.Add(dtableChamCong.Rows[i][0].ToString());
                lvi.SubItems.Add(dtableChamCong.Rows[i][1].ToString());
                lvi.SubItems.Add(dtableChamCong.Rows[i][2].ToString());
                lvi.SubItems.Add(dtableChamCong.Rows[i][3].ToString());
                lvi.SubItems.Add(dtableChamCong.Rows[i][4].ToString());
                lvi.SubItems.Add(dtableChamCong.Rows[i][5].ToString());
            }
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            UpdateSoGioLam(); //Cập nhật số giờ làm
            HienThiDanhSachChamCong(); //Hiển thị danh sách chấm công của nhân viên
            TongSoGioLam(); //Tính tổng số giờ làm trong tháng
        }

        private void txtThangCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtThangCC.MaxLength = 2;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Tháng từ 1 - 12 (Không được lớn hoặc nhỏ hơn)
        private void txtThangCC_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtThangCC.Text, out box_int);
            if (box_int > 12)
            {
                txtThangCC.Text = "12";
            }
            else
            {
                if (box_int < 1)
                {
                    txtThangCC.Text = "1";
                }
            }
        }

        void TongSoGioLam()
        {
            //Tổng số giờ làm
            String querrySoGioLam = "SELECT SUM(SoGioLam) FROM ChamCong WHERE (MaNhanVien = '" + cbxNhanVienCC.SelectedValue.ToString().Trim() + "' AND MONTH(NgayChamCong) = '" + txtThangCC.Text + "')";
            SqlDataAdapter sdaSoGioLam = new SqlDataAdapter(querrySoGioLam, conn);

            DataTable dtableSoGioLam = new DataTable();
            sdaSoGioLam.Fill(dtableSoGioLam);
            txtSoGioLam.Text = dtableSoGioLam.Rows[0][0].ToString();
        }

        void PhanQuyenQLChamCong()
        {
            //Phân quyền truy cập
            if (MaChucVu == "QL" || MaChucVu == "KT")
            {
                btnQLChamCong.Visible = true;
            }
            else
            {
                btnQLChamCong.Visible = false;
            }
        }
                

        //Button tháng sau
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Clear flpnl
            flpnlCalendar.Controls.Clear();

            //Tăng tháng
            month++;

            //Tăng năm
            if (month == 13)
            {
                month = 1;
                year++;
            }

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthname + " " + year;

            //Lấy ngày bắt đầu của tháng
            DateTime batdauthang = new DateTime(year, month, 1);

            //Lấy số ngày trong tháng
            int days = DateTime.DaysInMonth(year, month);

            //Convert batdauthang thành int
            int ngaytrongtuan = Convert.ToInt32(batdauthang.DayOfWeek.ToString("d"));

            //user control trống
            for (int i = 1; i < ngaytrongtuan; i++)
            {
                UCBlank ucblank = new UCBlank();
                flpnlCalendar.Controls.Add(ucblank);
            }

            //user control ngày
            for (int i = 1; i <= days; i++)
            {
                UCCalendar uccalendar = new UCCalendar();
                uccalendar.days(i);
                flpnlCalendar.Controls.Add(uccalendar);

                //Hiển thị trạng thái theo ngày
                String querrystatus = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + year + "/" + month + "/" + i + "')";
                SqlDataAdapter sdastatus = new SqlDataAdapter(querrystatus, conn);

                DataTable dtablestatus = new DataTable();
                sdastatus.Fill(dtablestatus);

                try
                {
                    uccalendar.trangthai(dtablestatus.Rows[0][4].ToString().Trim());
                }
                catch
                {
                    uccalendar.trangthai("");
                }
            }
        }

        void KiemTraChamCong()
        {
            //Kiểm tra Check In
            try
            {
                String querryCheckInCC = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
                SqlDataAdapter sdaCheckInCC = new SqlDataAdapter(querryCheckInCC, conn);

                DataTable dtableCheckInCC = new DataTable();
                sdaCheckInCC.Fill(dtableCheckInCC);

                if (dtableCheckInCC.Rows.Count > 0)
                {
                    btnCheckIn.Enabled = false;
                }
                else
                {
                    btnCheckIn.Enabled = true;
                }
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }

            //Kiểm tra Check Out
            try
            {
                String querryCheckOutCC = "SELECT *FROM ChamCong WHERE (MaNhanVien = '" + MaNhanVien + "' AND NgayChamCong = '" + DateTime.Now.ToString("MM/dd/yyyy") + "' AND TrangThai = '" + "Đã check in" + "')";
                SqlDataAdapter sdaCheckOutCC = new SqlDataAdapter(querryCheckOutCC, conn);

                DataTable dtableCheckOutCC = new DataTable();
                sdaCheckOutCC.Fill(dtableCheckOutCC);

                if (dtableCheckOutCC.Rows.Count > 0)
                {
                    btnCheckOut.Enabled = true;
                }
                else
                {
                        btnCheckOut.Enabled = false;
                }
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
        }
    }
}