using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using GongMe.PresentionTier;
using System.Security.Cryptography;

namespace GongMe
{
    public partial class FrmDangNhap : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (textboxPass.Text == "" || textboxUserid.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textboxPass.TextLength < 8)
                {
                    MessageBox.Show("Mật khẩu phải đủ ít nhất 8 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textboxPass.Clear();
                    textboxPass.Focus();
                }
                else
                {


                    String username, password;

                    username = textboxUserid.Text;
                    password = textboxPass.Text;

                    try
                    {
                        String querry = "SELECT *FROM NHANVIEN WHERE SDT = '" + textboxUserid.Text + "' AND MATKHAU = '" + textboxPass.Text + "' OR MAIL = '" + textboxUserid.Text + "' AND MATKHAU = '" + textboxPass.Text + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                        DataTable dtable = new DataTable();
                        sda.Fill(dtable);

                        if (dtable.Rows.Count > 0)
                        {
                            username = textboxUserid.Text;
                            password = textboxPass.Text;

                            //Đăng nhập vào Form chính
                            FrmTraSuaGongMe f = new FrmTraSuaGongMe(dtable.Rows[0][1].ToString().Trim(), dtable.Rows[0][0].ToString().Trim(), textboxUserid.Text, dtable.Rows[0][2].ToString().Trim());
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Sai thông tin đăng nhập!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            textboxUserid.Clear();
                            textboxPass.Clear();
                            textboxUserid.Focus();
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

                    //Lưu đăng nhập
                    if (ckbLuuThongTin.Checked == true)
                    {
                        string userid = textboxUserid.Text;
                        string pass = textboxPass.Text;
                        Properties.Settings.Default.username = userid;
                        Properties.Settings.Default.password = pass;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Reset();
                    }
                }
            }
        }

        private void linkforgotpass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmQuenMatKhau f = new FrmQuenMatKhau();
            f.Show();
            this.Hide();
        }

        private void FrmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void LuuThongTinDangNhap()
        {
            //Lưu đăng nhập
            textboxUserid.Text = Properties.Settings.Default.username;
            textboxPass.Text = Properties.Settings.Default.password;
            if (Properties.Settings.Default.username != "")
            {
                ckbLuuThongTin.Checked = true;
            }
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            LuuThongTinDangNhap();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ẩn hiện mật khẩu
        private void textboxPass_IconLeftClick(object sender, EventArgs e)
        {
            if (textboxPass.UseSystemPasswordChar == true)
            {
                textboxPass.UseSystemPasswordChar = false;
                textboxPass.PasswordChar = '\0';
            }
            else
            {
                textboxPass.UseSystemPasswordChar = true;
                textboxPass.PasswordChar = '●';
            }
        }

        private void textboxUserid_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Đăng nhập bằng Email hoặc Số điện thoại!", textboxUserid);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void textboxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            textboxPass.MaxLength = 24;
        }
    }
}