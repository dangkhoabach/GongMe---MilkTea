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
using GongMe.DataTier.Models;
using GongMe.PresentionTier;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace GongMe.PresentionTier
{
    public partial class FrmTaiKhoan : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        string CheckMail = Console.ReadLine();

        string user = "";

        public FrmTaiKhoan()
        {
            InitializeComponent();
        }

        public FrmTaiKhoan(string user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void btnThayDoiTT_Click(object sender, EventArgs e)
        {
            //Ẩn hiện các pnl và btn
            pnlXacMinh.Visible = true;
            pnlThongTin.Visible = false;
        }

        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            //Đóng các mục có thể thay đổi thông tin
            txtMail.Enabled = false;
            txtPassword.Enabled = false;
            txtSDT.Enabled = false;

            //Ẩn nút lưu thay đổi
            btnLuuThayDoi.Visible = false;

            //Hiển thị lại nút thay đổi thông tin
            btnThayDoiTT.Visible = true;

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[NHANVIEN] SET [MATKHAU] = '" + txtPassword.Text + "', [MAIL] = '" + txtMail.Text + "', [SDT] = '" + txtSDT.Text + "' WHERE SDT = '" + user + "' OR MAIL = '" + user + "'", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Thay đổi thông tin thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void LoadThongTinTaiKhoan()
        {
            //Lấy thông tin
            String querryThongTin = "SELECT *FROM NHANVIEN WHERE SDT = '" + user + "' OR MAIL = '" + user + "'";
            SqlDataAdapter sdaThongTin = new SqlDataAdapter(querryThongTin, conn);

            DataTable dtableThongTin = new DataTable();
            sdaThongTin.Fill(dtableThongTin);

            //Lấy tên chức vụ
            String querryChucVu = "SELECT TenChucVu FROM ChucVu WHERE RTRIM(MaChucVu) = '" + dtableThongTin.Rows[0][1].ToString().Trim() + "'";
            SqlDataAdapter sdaChucVu = new SqlDataAdapter(querryChucVu, conn);

            DataTable dtableChucVu = new DataTable();
            sdaChucVu.Fill(dtableChucVu);

            //Fill thông tin
            txtHoTen.Text = dtableThongTin.Rows[0][2].ToString().Trim();
            txtSDT.Text = dtableThongTin.Rows[0][3].ToString().Trim();
            txtMail.Text = dtableThongTin.Rows[0][7].ToString().Trim();
            txtPassword.Text = dtableThongTin.Rows[0][6].ToString().Trim();
            txtMaNV.Text = dtableThongTin.Rows[0][0].ToString().Trim();
            txtGioiTinh.Text = dtableThongTin.Rows[0][5].ToString().Trim();
            txtNamSinh.Text = dtableThongTin.Rows[0][4].ToString().Trim();
            txtUserID.Text = dtableThongTin.Rows[0][7].ToString().Trim();
            txtChucVu.Text = dtableChucVu.Rows[0][0].ToString().Trim();
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadThongTinTaiKhoan();

            pnlXacMinh.Visible = false;
            pnlThongTin.Visible = true;
        }

        private void FrmTaiKhoan_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void btnXacMinh_Click(object sender, EventArgs e)
        {
            String username, password;

            username = txtUserID.Text;
            password = txtXacMinhPass.Text;

            try
            {
                String querry = "SELECT *FROM NHANVIEN WHERE MAIL = '" + txtUserID.Text + "' AND MATKHAU = '" + txtXacMinhPass.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = txtUserID.Text;
                    password = txtXacMinhPass.Text;

                    //Ẩn hiện các pnl và btn
                    pnlXacMinh.Visible = false;
                    pnlThongTin.Visible = true;

                    btnLuuThayDoi.Visible = true;
                    btnThayDoiTT.Visible = false;

                    //Mở các mục có thể thay đổi thông tin
                    txtMail.Enabled = true;
                    txtPassword.Enabled = true;
                    txtSDT.Enabled = true;

                    //Xác minh thành công
                    MessageBox.Show("Xác minh thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtXacMinhPass.Clear();
                }
                else
                {
                    MessageBox.Show("Xác minh thất bại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtXacMinhPass.Clear();
                    txtUserID.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSDT.MaxLength = 10;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPassword.MaxLength = 24;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng không để trống mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
            }
            else
            {
                if (txtPassword.TextLength < 8)
                {
                    MessageBox.Show("Mật khẩu phải đủ ít nhất 8 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
        }

        private void txtSDT_Leave(object sender, EventArgs e)
        {
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSDT.Focus();
            }
            else
            {
                if (txtSDT.TextLength < 10)
                {
                    MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSDT.Focus();
                }
            }
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ EMAIL", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMail.Focus();
            }
        }
    }
}