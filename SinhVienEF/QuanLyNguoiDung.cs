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
    public partial class QLNguoiDung : Form
    {
        QuanLySinhVienEntities1 sve = new QuanLySinhVienEntities1();
        public QLNguoiDung()
        {
            InitializeComponent();
        }

        private void QLNguoiDung_Load(object sender, EventArgs e)
        {
            LoadGridView1();
        }

        private void LoadGridView1()
        {
            dataGridView1.DataSource = sve.QuanLyNguoiDungs.Select(tk => new
            {
                tendangnhap = tk.TenDangNhap,
                matkhau = tk.MatKhau,
                hoten = tk.HoTen,
                dienthoai = tk.DienThoai,
                email = tk.Email,
                diachi = tk.DiaChi
            });
            dataGridView1.Columns[0].HeaderText = "Tên đăng nhập";
            dataGridView1.Columns[1].HeaderText = "Mật khẩu";
            dataGridView1.Columns[2].HeaderText = "Họ tên";
            dataGridView1.Columns[3].HeaderText = "Điện thoại";
            dataGridView1.Columns[4].HeaderText = "Email";
            dataGridView1.Columns[5].HeaderText = "Địa chỉ";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string nd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            QuanLyNguoiDung tk = sve.QuanLyNguoiDungs.SingleOrDefault(u => u.TenDangNhap.Equals(nd));
            txtUser.Text = tk.TenDangNhap;
            txtPass.Text = tk.MatKhau;
            txtHoTen.Text = tk.HoTen;
            txtDienThoai.Text = tk.DienThoai;
            txtDiaChi.Text = tk.DiaChi;
            txtEmail.Text = tk.Email;

            TrangThai("click");
        }


        private void Xoa()
        {
            string nd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            DialogResult kq = MessageBox.Show("Bạn thực sự muốn xóa '"+nd+"'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
                if (kq == System.Windows.Forms.DialogResult.Yes)
                {
                       try
                       {
                           QuanLyNguoiDung user = sve.QuanLyNguoiDungs.Single(u => u.TenDangNhap.Equals(nd));
                           sve.QuanLyNguoiDungs.DeleteObject(user);
                           sve.SaveChanges();
                           LoadGridView1();
                           TrangThai("l");
                           MessageBox.Show("Người dùng '"+nd+ "' đã được xóa", "Thông báo", MessageBoxButtons.OK);
                       }
                       catch (EntitySqlException ese)
                       {
                           MessageBox.Show("Xóa người dùng thất bại: " + ese.Message);
                       }                                      
                }     
        
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                try
                {
                    QuanLyNguoiDung tk = new QuanLyNguoiDung();
                    tk.TenDangNhap = txtUser.Text;
                    tk.MatKhau = getMD5Hash(txtPass.Text);
                    tk.HoTen = txtHoTen.Text;
                    tk.DienThoai = txtDienThoai.Text;
                    tk.Email = txtEmail.Text;
                    tk.DiaChi = txtDiaChi.Text;

                    sve.QuanLyNguoiDungs.AddObject(tk);
                    sve.SaveChanges();
                    LoadGridView1();
                    MessageBox.Show("Người dùng "+txtUser.Text+" đã được thêm thành công", "Thông báo", MessageBoxButtons.OK);
                }
                catch (EntitySqlException ese)
                {

                    MessageBox.Show("Thêm người dùng thất bại:" + ese.Message);
                }  
            }
            
            
              

        }

        

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            TrangThai("clear");
        }

        private void TrangThai(string str)
        {
            switch (str)
            {
                case "clear":
                    txtUser.Clear();
                    txtPass.Clear();
                    txtHoTen.Clear();
                    txtDienThoai.Clear();
                    txtEmail.Clear();
                    txtDiaChi.Clear();

                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    txtUser.Enabled = true;
                    txtUser.Focus();
                    break;

                case "click":
                    btnThem.Enabled = false;
                    btnXoa.Enabled = true;
                    btnSua.Enabled = true;
                    txtUser.Enabled = false;
                    txtPass.Focus();
                    txtPass.SelectAll();
                    break;
            }

        }

        private bool Check()
        {
            //bool kq = false;
            if (txtUser.Text == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống","Lỗi",MessageBoxButtons.OK);
                txtUser.Focus();                
                return false;
            }

            if (txtPass.Text == "")
            {
                MessageBox.Show("Mật khẩu không được để trống", "Lỗi", MessageBoxButtons.OK);
                txtPass.Focus();
                return false;
            }

            if (sve.QuanLyNguoiDungs.FirstOrDefault(u => u.TenDangNhap.Equals(txtUser.Text))!=null)
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại, vui lòng chọn tên khác", "Lỗi", MessageBoxButtons.OK);
                txtUser.Focus();
                txtUser.SelectAll();
                return false;
            }

            return true;
                   
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tdn = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            try
            {
                QuanLyNguoiDung nd = sve.QuanLyNguoiDungs.SingleOrDefault(u => u.TenDangNhap.Equals(tdn));
                nd.MatKhau = getMD5Hash(txtPass.Text);
                nd.HoTen = txtHoTen.Text;
                nd.DienThoai = txtDienThoai.Text;
                nd.Email = txtEmail.Text;
                nd.DiaChi = txtDiaChi.Text;
                sve.SaveChanges();
                LoadGridView1();
                MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (EntitySqlException ese)
            {
                MessageBox.Show("Cập nhật tài khoản thất bại:" + ese.Message);
            }
            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGridView1();
        }
      

       
    }
}
