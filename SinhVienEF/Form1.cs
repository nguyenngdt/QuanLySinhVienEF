using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SinhVienEF
{
    public partial class Form1 : Form
    {
        private QuanLySinhVienEntities1 svef = new QuanLySinhVienEntities1();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            
            /*foreach (var user in svef.QuanLyNguoiDungs)
            {
                if (user.TenDangNhap == txtUser.Text && user.MatKhau == txtPass.Text)
                {
                    MessageBox.Show("Login thành công");
                }
                //else MessageBox.Show("Tên đăng nhập hoặc pass không đúng");
            }*/

            //QuanLyNguoiDung kq;
            try
            {
                string pass=getMD5Hash(txtPass.Text);
                QuanLyNguoiDung kq = svef.QuanLyNguoiDungs.FirstOrDefault(u => u.TenDangNhap == txtUser.Text && u.MatKhau ==pass);
                if (kq != null)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    lblError.Text = "";
                }

                else
                {
                    lblError.Text = "Tên đăng nhập hoặc mật khẩu không chính xác (*)";
                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                }
            }                
            catch (EntitySqlException ese)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ese.Message);
            }   

        }

        //Mã hóa pass 
        public static string getMD5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sbuilder = new StringBuilder();

            foreach (byte b in data)
            {
                sbuilder.Append(String.Format("{0:x2}", b));
            }

            return sbuilder.ToString();
        }

        
    }
}
