using BLL;
using BusEntity;
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

namespace MomoCRM
{
    public partial class ReminderF : Form
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

        public ReminderF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
    

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        ReminderBll rembl = new ReminderBll();
        UserBLL ubll = new UserBLL();
        MessCustm msg = new MessCustm();
        int id;

        public void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rembl.Read();
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["user_Id"].Visible = false;
            dataGridView1.Columns["Expr1"].Visible = false;

            label8.Text = rembl.ReadCount().ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Reminder r = new Reminder();
            User u = new User();

            r.Title = RemTitle.Text;
            r.Description = RemDesc.Text;
            r.RemindDate = timePick.Value.Date;
            r.RegDate = DateTime.Now;
            u = ubll.ReadUser(AccSugges.Text);
            r.user = u;

            if (label3.Text == "Submit")
            {
                msg.MessShow("Create Reminder",rembl.Create(r, u),false);

            }
            else
            {
                msg.MessShow("Update Reminder",rembl.Update(r, id),false);
                label3.Text = "Submit";
            }

            DataFill();

            RemTitle.Text = "";
            RemDesc.Text = "";
            timePick.Value = DateTime.Now.Date;
            AccSugges.Text = null;
        }

        private void ReminderF_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUsernames())
            {
                names.Add(item);
            }
            AccSugges.AutoCompleteCustomSource = names;

            DataFill();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reminder r = rembl.Read(id);

            RemTitle.Text = r.Title;
            RemDesc.Text = r.Description;
            timePick.Value = r.RemindDate;

            label3.Text = "Edit";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert","Are you sure you want to delete this reminder?", true);
            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert",rembl.Delete(id),false);
            }
            DataFill();

        }

        private void setDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msg.MessShow("Reminder Status",rembl.OffStat(id),false);
            DataFill();

        }

        private void RemSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rembl.Read(RemSearch.Text);
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["user_Id"].Visible = false;
            dataGridView1.Columns["Expr1"].Visible = false;
        }

        private void RemDesc_MouseDown(object sender, MouseEventArgs e)
        {
            if (RemDesc.Text == "Type your description here")
            {
                RemDesc.Text = "";
            }
        }
    }
}
