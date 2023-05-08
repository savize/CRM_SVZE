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
    public partial class ProductMng : Form
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

        public ProductMng()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        productBll prod = new productBll();
        MessCustm msg = new MessCustm();
        int id;

        public void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = prod.Read();
            dataGridView1.Columns["Id"].Visible = false;

            totProd.Text = prod.readCount().ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Product p = new Product();

            p.Name = ProdValue.Text;
            p.Price = Convert.ToDouble(PriceValue.Text);
            p.Stock = Convert.ToInt16(AmntValue.Text);

            if (label3.Text == "Submit")
            {
                msg.MessShow("Add Product", prod.Create(p), false);

                ProdValue.Text = "";
                PriceValue.Text = "";
                AmntValue.Text = "";
            }
            else
            {
                msg.MessShow("Edit Product", prod.Update(p, id), false);

                ProdValue.Text = "";
                PriceValue.Text = "";
                AmntValue.Text = "";

                label3.Text = "Submit";
            }
            
            DataFill();
        }

        private void ProductMng_Load(object sender, EventArgs e)
        {
            DataFill();
        }

        private void ProdSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = prod.Read(ProdSearch.Text);
            dataGridView1.Columns["Id"].Visible=false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product p = prod.Read(id);

            ProdValue.Text = p.Name;
            PriceValue.Text = p.Price.ToString();
            AmntValue.Text = p.Stock.ToString();

            label3.Text = "Edit";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to delete the selected item?", true);
            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert",prod.Delete(id),false);
            }
            DataFill();
        }
    }

}
