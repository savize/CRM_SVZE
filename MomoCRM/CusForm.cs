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
    public partial class CusForm : Form
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

        public CusForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        CustomerBLL cus = new CustomerBLL();
        MessCustm msg = new MessCustm();
        int id;

        public void DatagridFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cus.Read();
            dataGridView1.Columns["Id"].Visible = false;

            int allCus = cus.ReadCusAll();
            int actCus = cus.ReadCusAct();
            totCus.Text = allCus.ToString();
            ActCus.Text = actCus.ToString();
            NonActCus.Text = (allCus - actCus).ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();

            c.CusName = NameValue.Text;
            c.Phone = phoneValue.Text;
            c.RegDate = DateTime.Now;

            if (label3.Text == "Submit")
            {
                msg.MessShow("Customer Submission", cus.Create(c),false);

                NameValue.Text = "";
                phoneValue.Text = "";
            }
            else
            {
                msg.MessShow("Edit Customer", cus.Update(id, c), false);

                NameValue.Text = "";
                phoneValue.Text = "";
                label3.Text = "Submit";

            }

            DatagridFill();
        }

        private void CusSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cus.Read(CusSearch.Text);
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void CusForm_Load(object sender, EventArgs e)
        {
            DatagridFill();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt16(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer c = cus.Read(id);
            
            NameValue.Text = c.CusName;
            phoneValue.Text = c.Phone;

            label3.Text = "Edit";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to delete this contact?", true);
            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert", cus.Delete(id), false);
            }
            DatagridFill();
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = cus.ReadAct();
                dataGridView1.Columns["Id"].Visible = false;
            }
            else
            {
                DatagridFill();
            }
        }
    }
}
