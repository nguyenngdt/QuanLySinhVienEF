using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SinhVienEF
{
    public partial class SinhVienMain : Form
    {
        public SinhVienMain()
        {
            InitializeComponent();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (Form frm in this.MdiChildren)
            {                
                if (frm.Name == "QLNguoiDung")
                {
                    flag = true;
                    frm.Activate();
                    break;
                }
            }

            if (flag == false)
            {
                QLNguoiDung qlnd = new QLNguoiDung();
                qlnd.MdiParent = this;
                qlnd.Show(); 
            }       
                
            
           
        }

        private void QLMHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "KhoaHoc")
                {
                    flag = true;
                    frm.Activate();
                    break;
                }
            }

            if (flag == false)
            {
                KhoaHoc qlkh = new KhoaHoc();
                qlkh.MdiParent = this;
                qlkh.Show();
            }
        }

        private void SinhVienMain_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            bool flag = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "SVQL")
                {
                    flag = true;
                    frm.Activate();
                    break;
                }
            }

            if (flag == false)
            {
                SVQL sv1 = new SVQL();
                sv1.MdiParent = this;
                sv1.Show();
            }
        }

        

        
    }
}
