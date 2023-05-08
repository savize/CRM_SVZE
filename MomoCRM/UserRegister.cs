using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BusEntity;
using BLL;

namespace MomoCRM
{
    public partial class UserRegister : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        public UserRegister()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        UserBLL userbl = new UserBLL();
        OpenFileDialog ofd = new OpenFileDialog();
        Image pic;
        int id;
        MessCustm msg = new MessCustm();

        string SavePic(string Username)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPics\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string Picname = Username + ".JPG";
            try
            {
                string picPath = ofd.FileName;
                if (!File.Exists(path + Picname))
                {
                    File.Copy(picPath, path + Picname, true);
                }

            }
            catch (Exception e)
            {
                msg.MessShow("Error", e.Message, false);
            }
            return path + Picname;
        }

        public void DataFill()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = userbl.Read();
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["Pic"].Visible = false;

            int allUs = userbl.ReadCountAll();
            int actUs = userbl.ReadCountAct();
            totUs.Text = allUs.ToString();
            ActUs.Text = actUs.ToString();
            NonActUs.Text = (allUs - actUs).ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "Please choose you profile picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                picProf.Image = pic;
                picProf.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            User u = new User();

            u.Name = FullnameValue.Text;
            u.Username = UsernameValue.Text;
            u.Password = PassConfirmValue.Text;
            u.RegDate = DateTime.Now;

            if ((passValue.Text == "" || PassConfirmValue.Text == "") || picProf.Image == null)
            {
                errorLab.Visible = true;
                errorLab.Text = "Please input all the requested fields";
            }
            else if (passValue.Text != PassConfirmValue.Text)
            {
                errorLab.Visible = true;
                errorLab.Text = "The password and the confirmation password are not the same";
            }
            else
            {
                errorLab.Visible = false;
                if (SubmitBtn.Text == "Submit")
                {
                    u.Pic = SavePic(UsernameValue.Text);
                    msg.MessShow("User Submission", userbl.Create(u), false);                
                }
                else
                {
                    u.Pic = SavePic(UsernameValue.Text);
                    msg.MessShow("Edit User", userbl.Update(u, id), false);

                    SubmitBtn.Text = "Submit";
                }

                DataFill();
                FullnameValue.Text = "";
                UsernameValue.Text = "";
                passValue.Text = "";
                PassConfirmValue.Text = "";
                picProf.Image = Properties.Resources.UpPic;
            }
        }



        private void UserRegister_Load(object sender, EventArgs e)
        {
            DataFill();
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Id"].Value);
        }
    
        private void UsSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = userbl.Read(UsSearch.Text);
            dataGridView2.Columns["Id"].Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            User u = userbl.Read(id);

            FullnameValue.Text = u.Name;
            UsernameValue.Text = u.Username;
            if (u.Pic != null)
            {
                picProf.Image = Image.FromFile(u.Pic);
            }
            else
            {
                picProf.Image = Properties.Resources.UpPic;
            }

            picProf.SizeMode = PictureBoxSizeMode.StretchImage;
            SubmitBtn.Text = "Edit";
        }

        private void setDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msg.MessShow("Change Status", userbl.SetStat(id), false);
            DataFill();
            checkBox1.Checked = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to delete the selected user?", true);

            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert",userbl.Delete(id),false);
            }

            DataFill();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = userbl.ReadActive();
                dataGridView2.Columns["Id"].Visible = false;
                dataGridView2.Columns["Pic"].Visible = false;
            }
            else
            {
                DataFill();
            }
        }
    }
}