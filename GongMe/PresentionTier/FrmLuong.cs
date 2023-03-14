using GongMe.DataTier.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GongMe.PresentionTier
{
    public partial class FrmLuong : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmLuong()
        {
            InitializeComponent();
        }

        private void FrmLuong_Load(object sender, EventArgs e)
        {
            ChonNhanVien(); //Add data vào Combobox

            KiemTraDatabase(); //Kiểm tra

            LoadlvsLuong(); //Load listview
        }

        void ChonNhanVien()
        {
            String querryNhanVien = "SELECT *FROM NhanVien";
            SqlDataAdapter sdaNhanVien = new SqlDataAdapter(querryNhanVien, conn);

            DataTable dtableNhanVien = new DataTable();
            sdaNhanVien.Fill(dtableNhanVien);

            cbxNhanVien.DataSource = dtableNhanVien;
            cbxNhanVien.ValueMember = "MaNhanVien";
            cbxNhanVien.DisplayMember = "HoTen";
        }

        void ThongTinNhanVien()
        {
            try
            {
                //Lấy mã chức vụ của nhân viên
                String querryMaChucVu = "SELECT MaChucVu FROM NhanVien WHERE MaNhanVien = '" + cbxNhanVien.SelectedValue.ToString().Trim() + "'";
                SqlDataAdapter sdaMaChucVu = new SqlDataAdapter(querryMaChucVu, conn);

                DataTable dtableMaChucVu = new DataTable();
                sdaMaChucVu.Fill(dtableMaChucVu);

                //Lấy tên chức vụ
                String querryChucVu = "SELECT TenChucVu FROM ChucVu WHERE RTRIM(MaChucVu) = '" + dtableMaChucVu.Rows[0][0].ToString().Trim() + "'";
                SqlDataAdapter sdaChucVu = new SqlDataAdapter(querryChucVu, conn);

                DataTable dtableChucVu = new DataTable();
                sdaChucVu.Fill(dtableChucVu);

                //Lấy lương theo giờ của nhân viên
                String querryLuongTheoGio = "SELECT LuongTheoGio FROM ChucVu WHERE RTRIM(MaChucVu) = '" + dtableMaChucVu.Rows[0][0].ToString().Trim() + "'";
                SqlDataAdapter sdaLuongTheoGio = new SqlDataAdapter(querryLuongTheoGio, conn);

                DataTable dtableLuongTheoGio = new DataTable();
                sdaLuongTheoGio.Fill(dtableLuongTheoGio);

                //Fill thông tin nhân viên
                txtChucVu.Text = dtableChucVu.Rows[0][0].ToString().Trim();
                txtLuongTheoGio.Text = dtableLuongTheoGio.Rows[0][0].ToString().Trim();
            }
            catch
            {

            }
        }

        //Kiểm tra lương trong database
        void KiemTraDatabase()
        {
            String querryKiemTra = "SELECT ThoiGian FROM Luong";
            SqlDataAdapter sdaKiemTra = new SqlDataAdapter(querryKiemTra, conn);

            DataTable dtableKiemTra = new DataTable();
            sdaKiemTra.Fill(dtableKiemTra);

            try
            {
                if (dtableKiemTra.Rows[0][0].ToString().Trim() == DateTime.Now.ToString("MM/yyyy"))
                {
                    
                }
            }
            catch
            {
                UpdateLuong();
            }
        }

        void UpdateLuong()
        {
            int LuongCoBan, LuongTheoCV, SoGioLam;

            //Lấy thông tin giờ làm tháng làm
            String querryUpdate = "SELECT MaNhanVien, MONTH(NgayChamCong), SUM(SoGioLam), YEAR(NgayChamCong) FROM ChamCong WHERE SoGioLam IS NOT NULL GROUP BY MaNhanVien, MONTH(NgayChamCong), YEAR(NgayChamCong)";
            SqlDataAdapter sdaUpdate = new SqlDataAdapter(querryUpdate, conn);

            DataTable dtableUpdate = new DataTable();
            sdaUpdate.Fill(dtableUpdate);


            for (int i = 0; i < dtableUpdate.Rows.Count; i++)
            {
                //Lấy mã chức vụ
                String querryMaChucVu = "SELECT MaChucVu FROM NhanVien WHERE MaNhanVien = '"+ dtableUpdate.Rows[i][0].ToString().Trim() + "'";
                SqlDataAdapter sdaMaChucVu = new SqlDataAdapter(querryMaChucVu, conn);

                DataTable dtableMaChucVu = new DataTable();
                sdaMaChucVu.Fill(dtableMaChucVu);

                //Lấy lương giờ theo chức vụ
                String querryLuongTheoCV = "SELECT LuongTheoGio FROM ChucVu WHERE MaChucVu = '" + dtableMaChucVu.Rows[0][0].ToString().Trim() + "'";
                SqlDataAdapter sdaLuongTheoCV = new SqlDataAdapter(querryLuongTheoCV, conn);

                DataTable dtableLuongTheoCV = new DataTable();
                sdaLuongTheoCV.Fill(dtableLuongTheoCV);

                //Tính lương cơ bản
                LuongTheoCV = Convert.ToInt32(dtableLuongTheoCV.Rows[0][0].ToString().Trim());

                if (dtableUpdate.Rows[i][2].ToString().Trim() == null)
                {
                    SoGioLam = 0;
                }
                else
                {
                    SoGioLam = Convert.ToInt32(dtableUpdate.Rows[i][2].ToString().Trim());
                }

                LuongCoBan = SoGioLam * LuongTheoCV;

                //Update lương vào database
                conn.Open();
                SqlCommand cmdcheckin = new SqlCommand("INSERT INTO [dbo].[Luong] ([MaNhanVien], [ThoiGian], [LuongCoBan], [TrangThai], [MaChucVu]) VALUES('" + dtableUpdate.Rows[i][0].ToString().Trim() + "','" + dtableUpdate.Rows[i][1].ToString().Trim() + "/" + dtableUpdate.Rows[i][3].ToString().Trim() + "','" + LuongCoBan + "', N'" + "Chưa kết toán" + "', '" + dtableMaChucVu.Rows[0][0].ToString().Trim() + "')", conn);
                cmdcheckin.ExecuteNonQuery();
                conn.Close();
            }
        }

        //Load bảng lương tất cả nhân viên
        void LoadlvsLuong()
        {
            lvsLuong.Items.Clear();

            //Thông tin lương
            String querryLuong = "SELECT *FROM Luong";
            SqlDataAdapter sdaLuong = new SqlDataAdapter(querryLuong, conn);

            DataTable dtableLuong = new DataTable();
            sdaLuong.Fill(dtableLuong);

            //Lấy thông tin giờ làm
            String querryUpdate = "SELECT SUM(SoGioLam) FROM ChamCong WHERE SoGioLam IS NOT NULL GROUP BY MaNhanVien, MONTH(NgayChamCong), YEAR(NgayChamCong)";
            SqlDataAdapter sdaUpdate = new SqlDataAdapter(querryUpdate, conn);

            DataTable dtableUpdate = new DataTable();
            sdaUpdate.Fill(dtableUpdate);

            for (int i = 0; i < dtableLuong.Rows.Count; i++)
            {
                ListViewItem lvi = lvsLuong.Items.Add(dtableLuong.Rows[i][0].ToString());
                lvi.SubItems.Add(dtableLuong.Rows[i][1].ToString());
                lvi.SubItems.Add(dtableLuong.Rows[i][2].ToString());
                try
                {
                    lvi.SubItems.Add(dtableUpdate.Rows[i][0].ToString().Trim());
                }
                catch
                {
                    lvi.SubItems.Add("0");
                }
                lvi.SubItems.Add(dtableLuong.Rows[i][3].ToString());
                lvi.SubItems.Add(dtableLuong.Rows[i][4].ToString());
            }
        }

        //Load bảng lương theo từng nhân viên chọn bằng cbx
        void LoadlvsLuongTheoNV()
        {
            try
            {
                lvsLuong.Items.Clear();

                //Thông tin lương
                String querryLuongTheoNV = "SELECT *FROM Luong WHERE MaNhanVien = '" + cbxNhanVien.SelectedValue.ToString().Trim() + "'";
                SqlDataAdapter sdaLuongTheoNV = new SqlDataAdapter(querryLuongTheoNV, conn);

                DataTable dtableLuongTheoNV = new DataTable();
                sdaLuongTheoNV.Fill(dtableLuongTheoNV);

                //Lấy thông tin giờ làm
                String querryUpdate = "SELECT SUM(SoGioLam) FROM ChamCong WHERE MaNhanVien = '" + cbxNhanVien.SelectedValue.ToString().Trim() + "' GROUP BY MaNhanVien, MONTH(NgayChamCong), YEAR(NgayChamCong)";
                SqlDataAdapter sdaUpdate = new SqlDataAdapter(querryUpdate, conn);

                DataTable dtableUpdate = new DataTable();
                sdaUpdate.Fill(dtableUpdate);

                for (int i = 0; i < dtableLuongTheoNV.Rows.Count; i++)
                {
                    ListViewItem lvi = lvsLuong.Items.Add(dtableLuongTheoNV.Rows[i][0].ToString());
                    lvi.SubItems.Add(dtableLuongTheoNV.Rows[i][1].ToString());
                    lvi.SubItems.Add(dtableLuongTheoNV.Rows[i][2].ToString());
                    lvi.SubItems.Add(dtableUpdate.Rows[i][0].ToString().Trim());
                    lvi.SubItems.Add(dtableLuongTheoNV.Rows[i][3].ToString());
                    lvi.SubItems.Add(dtableLuongTheoNV.Rows[i][4].ToString());
                }
            }
            catch
            {

            }
        }

        private void cbxNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThongTinNhanVien();
            LoadlvsLuongTheoNV();
        }   

        private void txtThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtThang.MaxLength = 2;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtThang_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtThang.Text, out box_int);
            if (box_int > 12)
            {
                txtThang.Text = "12";
            }
            else
            {
                if (box_int < 1)
                {
                    txtThang.Text = "1";
                }
            }
        }

        private void btnXemTatCa_Click(object sender, EventArgs e)
        {
            LoadlvsLuong();
        }

        private void btnKetToan_Click(object sender, EventArgs e)
        {
            if (lvsLuong.Items.Count <= 0)
                return;
            for (int i = 0; i < lvsLuong.Items.Count; i++)
            {
                if ((txtThang.Text + "/" + txtNam.Text) == lvsLuong.Items[i].SubItems[2].Text && cbxNhanVien.SelectedValue.ToString().Trim() == lvsLuong.Items[i].SubItems[0].Text)
                {
                    if (txtThang.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập thời gian cần kết toán", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn có chắc chắn muốn kết toán lương cho nhân viên ngày không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Luong] SET [TrangThai] = N'" + "Đã kết toán" + "' WHERE (RTRIM([MaNhanVien]) = '" + cbxNhanVien.SelectedValue.ToString().Trim() + "' AND RTRIM([ThoiGian]) = '" + txtThang.Text + "/" + txtNam.Text + "')", conn);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Kết toán thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadlvsLuongTheoNV();
                        }
                        else
                        {
                            LoadlvsLuongTheoNV();
                        }
                    }
                }
                else
                {

                }
            }
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtThang.MaxLength = 4;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtThang.Text, out box_int);
            if (box_int > 2030)
            {
                txtThang.Text = "2030";
            }
            else
            {
                if (box_int < 2010)
                {
                    txtThang.Text = "2010";
                }
            }
        }
    }
}