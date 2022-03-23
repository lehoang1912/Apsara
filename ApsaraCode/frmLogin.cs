using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Apsara
{
    public partial class frmLogin : Form
    {
        ApsaraDataContext context = new ApsaraDataContext();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://tranduythanh.com/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel
                ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtEmail.Text;
                string password = txtPassword.Text;
                Account ac = context.Accounts.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                if (ac == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (ac.IsPrevent == 1)
                {
                    MessageBox.Show("Tài khoản này đã cấm sử dụng vì vi phạm chính sách", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime d = (DateTime)context.GetCurrentTimeOnServer().FirstOrDefault().CurrentTime;
                TimeSpan t = (d - ac.DateRegistered.Value);
                double n = t.TotalDays;
                if (n > 3 && ac.Type == 1)
                {
                    MessageBox.Show("Đã quá 3 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 30 && ac.Type == 2)
                {
                    MessageBox.Show("Đã hết 30 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 90 && ac.Type == 3)
                {
                    MessageBox.Show("Đã hết 90 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 180 && ac.Type == 4)
                {
                    MessageBox.Show("Đã hết 180 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 270 && ac.Type == 5)
                {
                    MessageBox.Show("Đã hết 270 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (n > 365 && ac.Type == 6)
                {
                    MessageBox.Show("Đã hết 365 ngày sử dụng, vui lòng nộp phí để tiếp tục", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ActionLog al = new ActionLog();
                al.IdAccount = ac.Id;
                al.DateLogin = d;

                context.ActionLogs.InsertOnSubmit(al);
                context.SubmitChanges();

                frmMain.LoginAccount = ac;
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void linkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bool check = IsValidEmail(txtEmail.Text);
            if(check==false)
            {
                MessageBox.Show("Vui lòng cung cấp Email chính xác", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GuiEmail();
        }
        private void GuiEmail()
        {
            try
            {
                Account ac = context.Accounts.FirstOrDefault(x => x.UserName.ToLower().Trim() == txtEmail.Text.ToLower().Trim());
                if(ac!=null)
                {
                    MessageBox.Show("Vui lòng cung cấp Email khác, email này đã sử dụng", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Account a = new Account();
                a.UserName = txtEmail.Text.Trim().ToLower();
                a.Password = "apsara123";
                a.Type = 1;
                a.DateRegistered = context.GetCurrentTimeOnServer().FirstOrDefault().CurrentTime;
                a.IsDeleted = 0;
                a.IsPrevent = 0;
                context.Accounts.InsertOnSubmit(a);
                context.SubmitChanges();

                MessageBox.Show("Chúc mừng bạn đăng ký thành công.\n-----------------------------\nUser:  "+a.UserName+ "\nMật khẩu:  apsara123\n-----------------------------\nBạn được sử dụng 3 ngày miễn phí không giới hạn số lượng Video", "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
