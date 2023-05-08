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
    public partial class SettingF : Form
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

        public SettingF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        CategoryBll catbl = new CategoryBll();
        MessCustm msg = new MessCustm();
        int id;

        void Datafill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = catbl.Read();
            dataGridView1.Columns["Id"].Visible = false;

            TotCategories.Text = catbl.ReadCount().ToString();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            ActivityCategory ac = new ActivityCategory();
            ac.CatName = CatValue.Text;

            if (label3.Text == "Submit")
            {
                msg.MessShow("Category Submission", catbl.Create(ac), false);
            }
            else
            {
                msg.MessShow("Update Category",catbl.Update(id,ac), false);
                label3.Text = "Submit";
            }

            CatValue.Text = "";
            Datafill();
        }

        private void SettingF_Load(object sender, EventArgs e)
        {
            Datafill();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            ActivityF actF = new ActivityF();
            actF.Datafill();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivityCategory ac = catbl.Read(id);
            CatValue.Text = ac.CatName;

            label3.Text = "Edit";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to delete this activity category?", true);

            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert", catbl.Delete(id), false);
            }
        }
    }
}
