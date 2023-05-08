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
using Stimulsoft.Report;
using System.Windows;
using System.Windows.Interop;

namespace MomoCRM
{

    public partial class InvoiceF : Form
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

        public InvoiceF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        CustomerBLL cusbl = new CustomerBLL();
        UserBLL ubl = new UserBLL();
        productBll prodbll = new productBll();
        InvoiceBll invbll = new InvoiceBll();
        MessCustm msg = new MessCustm();
        List<Product> Products = new List<Product>();
        Customer c = new Customer();
        Product p = new Product();
        User u = new User();
        int id;
        double sum = 0;

        public void DataFill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = invbll.Read();
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].Visible = false;

            TotInv.Text = invbll.ReadCount().ToString();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection Customers = new AutoCompleteStringCollection();
            AutoCompleteStringCollection products = new AutoCompleteStringCollection();
            AutoCompleteStringCollection Users = new AutoCompleteStringCollection();


            foreach (var item in cusbl.ReadCuslist())
            {
                Customers.Add(item);
            }
            
            CusValue.AutoCompleteCustomSource = Customers;

            foreach (var item in prodbll.ReadList()) 
            {
                products.Add(item);
            }

            ProdValue.AutoCompleteCustomSource = products;

            foreach (var item in ubl.ReadUsernames())
            {
                Users.Add(item);
            }

            AccValue.AutoCompleteCustomSource = Users;
            
            DataFill();
        }

        Invoice inv = new Invoice();

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            c = cusbl.ReadCus(CusValue.Text);
            u = ubl.ReadUser(AccValue.Text);
            
            cusNameVal.Text = c.CusName;
            CusPhoneValue.Text = c.Phone;
            dateValue.Text = DateTime.Now.Date.ToString("yy/MM/dd");

            p = prodbll.ReadProd(ProdValue.Text);
            Products.Add(p);


            dataGridView2.DataSource = null;
            dataGridView2.DataSource = Products;
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["Stock"].Visible = false;
            
            string s = p.Name + " ----- RM " + p.Price ;
            listBox1.Items.Add(s);


            foreach(var item in Products)
            {
                sum += item.Price;
            }

            priceTotbef.Text = sum.ToString();
            PriceTotafter.Text = (sum - Convert.ToDouble(discn.Text)).ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //MainWindow mw = (MainWindow)System.Windows.
            //.Current.Windows.OfType<Window>().FirstOrDefault();
            //u = mw.loggedInUser;

            inv.RegDate = DateTime.Now;

            if(Paid.Checked)
            {
                inv.ChckoutDate = DateTime.Now;
                inv.Status = true;
            }
            else
            {
                inv.Status = false;
            }
                     
            inv.TotalPrice = Convert.ToDouble(PriceTotafter.Text);

            msg.MessShow("Invoice Submission", invbll.Create(inv, c, u, Products), false);

            DataFill();
            cusNameVal.Text = "";
            CusPhoneValue.Text = "";
            dateValue.Text = "";
            sum = 0;
            priceTotbef.Text = "0";
            PriceTotafter.Text = "0";
            Products.Clear();
            discn.Text = "0";           
            Paid.Checked = false;
            listBox1.Items.Clear();
            dataGridView2.DataSource = null;
            CusValue.Text = "";
            ProdValue.Text = "";
            AccValue.Text = "";
        }

        private void label17_Click(object sender, EventArgs e)
        {
            StiReport stimsoft = new StiReport();

            stimsoft.Load(@"C:\\Users\\Savize\\source\\repos\\MomoCRM\\Report.mrt");

            stimsoft.Dictionary.Variables["InvNu"].Value = invbll.InvoiceNumber();
            stimsoft.Dictionary.Variables["CusName"].Value = cusNameVal.Text;
            stimsoft.Dictionary.Variables["CusNumb"].Value = CusPhoneValue.Text;
            stimsoft.Dictionary.Variables["Date"].Value = dateValue.Text;

            stimsoft.RegBusinessObject("Product", Products);
            stimsoft.Render();
            stimsoft.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void InvSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = invbll.Read(InvSearch.Text);
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].Visible = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            msg.MessShow("Invoice Status", invbll.SetPaid(id),false);          
            DataFill();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to delete the selected invoice?", true);
            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert", invbll.Delete(id), false);
            }
            
            DataFill();
        }

        private void discn_TextChanged(object sender, EventArgs e)
        {
            if(discn.Text == "")
            {
                discn.Text = "0";
                PriceTotafter.Text = (sum - Convert.ToDouble(discn.Text)).ToString();
            }
            else
            {
                PriceTotafter.Text = (sum - Convert.ToDouble(discn.Text)).ToString();
            }
        }
    }
}
