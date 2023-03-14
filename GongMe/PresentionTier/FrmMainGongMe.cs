using GongMe.BusinessTier;
using GongMe.DataTier.Models;
using GongMe.PresentionTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GongMe
{
    public partial class FrmTraSuaGongMe : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        string MACHUCVU = "", MANHANVIEN = "", user = "", HOTEN = "";

        public FrmTraSuaGongMe()
        {
            InitializeComponent();
        }

        public FrmTraSuaGongMe(string MACHUCVU, string MANHANVIEN, string user, string HOTEN)
        {
            InitializeComponent();
            this.MACHUCVU = MACHUCVU;
            this.MANHANVIEN = MANHANVIEN;
            this.user = user;
            this.HOTEN = HOTEN;
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            //Form con hóa đơn
            OpenChildForm(new FrmHoaDon(MACHUCVU));
            labelTop.Text = btnHoaDon.Text;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            //Form con thống kê
            OpenChildForm(new FrmChonThongKe());
            labelTop.Text = btnThongKe.Text;  
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            //Form con nhân viên
            OpenChildForm(new FrmNhanVien());
            labelTop.Text = "QUẢN LÝ NHÂN VIÊN";
        }

        private void FrmTraSuaGongMe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnQuanLy_Click_1(object sender, EventArgs e)
        {
            //Form con quản lý bán hàng
            OpenChildForm(new FrmQuanLyBan(MANHANVIEN));
            labelTop.Text = "QUẢN LÝ BÁN HÀNG";
        }

        //ChildForm
        private Form currentFromChild;
        private void OpenChildForm(Form childForm)
        {
            if(currentFromChild != null)
            {
                currentFromChild.Close();
            }    
            currentFromChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlChild.Controls.Add(childForm);
            pnlChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void linkTaiKhoan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmTaiKhoan f = new FrmTaiKhoan(user);
            f.ShowDialog();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            if (currentFromChild != null)
            {
                currentFromChild.Close();
            }
            labelTop.Text = "HOME";
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FrmDangNhap f = new FrmDangNhap();
            f.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuanLyMon_Click(object sender, EventArgs e)
        {
            //Form con quản lý món
            OpenChildForm(new FrmQuanLyMon());
            labelTop.Text = btnQuanLyMon.Text;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            //Form con nhập hàng
            OpenChildForm(new FrmNhapHang());
            labelTop.Text = btnNhapHang.Text;
        }

        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            //Form con xuất hàng
            OpenChildForm(new FrmXuatHang());
            labelTop.Text = btnXuatHang.Text;
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            //Form con chấm công
            OpenChildForm(new FrmChamCong(MANHANVIEN,HOTEN,MACHUCVU));
            labelTop.Text = btnChamCong.Text;
        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
            //Form con quản lý lương
            OpenChildForm(new FrmLuong());
            labelTop.Text = "QUẢN LÝ LƯƠNG";
        }

        void OnlineUser()
        {
            //Lấy tên chức vụ
            String querryChucVu = "SELECT TenChucVu FROM ChucVu WHERE RTRIM(MaChucVu) = '" + MACHUCVU + "'";
            SqlDataAdapter sdaChucVu = new SqlDataAdapter(querryChucVu, conn);

            DataTable dtableChucVu = new DataTable();
            sdaChucVu.Fill(dtableChucVu);

            //Hiển thị nhân viên Online
            lbOnlineUser.Text = "[" + dtableChucVu.Rows[0][0].ToString().Trim() + "]  " + HOTEN;
        }

        void PhanQuyen()
        {
            //Phân quyền truy cập
            if (MACHUCVU == "NV")
            {
                btnQuanLy.Visible = true;
                btnHoaDon.Visible = false;
                btnThongKe.Visible = false;
                btnNhanVien.Visible = false;
                btnQuanLyMon.Visible = false;
                btnNhapHang.Visible = false;
                btnXuatHang.Visible = false;
                btnChamCong.Visible = true;
                btnLuong.Visible = false;

                //Vị trí btn
                btnQuanLy.Location = new Point(10, 70);
                btnChamCong.Location = new Point(10, 660);
            }
            else
            {
                if (MACHUCVU == "KT")
                {
                    btnQuanLy.Visible = false;
                    btnHoaDon.Visible = false;
                    btnThongKe.Visible = true;
                    btnNhanVien.Visible = false;
                    btnQuanLyMon.Visible = false;
                    btnNhapHang.Visible = false;
                    btnXuatHang.Visible = false;
                    btnChamCong.Visible = true;
                    btnLuong.Visible = true;

                    //Vị trí btn
                    btnLuong.Location = new Point(10, 70);
                    btnThongKe.Location = new Point(10, 130);
                    btnChamCong.Location = new Point(10, 660);
                }
                else
                {
                    btnQuanLy.Visible = true;
                    btnHoaDon.Visible = true;
                    btnThongKe.Visible = true;
                    btnNhanVien.Visible = true;
                    btnQuanLyMon.Visible = true;
                    btnNhapHang.Visible = true;
                    btnXuatHang.Visible = true;
                    btnChamCong.Visible = true;
                    btnLuong.Visible = true;

                    //Vị trí btn
                    btnQuanLy.Location = new Point(10, 70);
                    btnHoaDon.Location = new Point(10, 130);
                    btnQuanLyMon.Location = new Point(10, 190);
                    btnNhanVien.Location = new Point(10, 250);
                    btnNhapHang.Location = new Point(10, 310);
                    btnXuatHang.Location = new Point(10, 370);
                    btnLuong.Location = new Point(10, 430);
                    btnThongKe.Location = new Point(10, 490);
                    btnChamCong.Location = new Point(10, 660);
                }
            }
        }

        private void FrmTraSuaGongMe_Load(object sender, EventArgs e)
        {
            OnlineUser();
            PhanQuyen();
        }
    }
}
