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
    public partial class ActivityF : Form
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

        public ActivityF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

      
        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        ActivityBll acbll = new ActivityBll();
        UserBLL ubll = new UserBLL();
        CustomerBLL cusbll = new CustomerBLL();
        CategoryBll catbll = new CategoryBll();
        MessCustm msg = new MessCustm();
        int id;

        public void Datafill()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = acbll.read();
            dataGridView1.Columns["Id"].Visible = false;
           
            TotAcs.Text = acbll.ReadCount().ToString();

            AutoCompleteStringCollection Accnames = new AutoCompleteStringCollection();
            AutoCompleteStringCollection Cusnames = new AutoCompleteStringCollection();
            AutoCompleteStringCollection categories = new AutoCompleteStringCollection();


            foreach (var item in ubll.ReadUsernames())
            {
                Accnames.Add(item);
            }
            AccValue.AutoCompleteCustomSource = Accnames;

            foreach (var item in cusbll.ReadCuslist())
            {
                Cusnames.Add(item);
            }
            CusValue.AutoCompleteCustomSource = Cusnames;

            foreach (var item in catbll.ReadListCategories())
            {
                categories.Add(item);
            }
            ActCateg.AutoCompleteCustomSource = categories;
        }

        
        private void ActivityF_Load(object sender, EventArgs e)
        {
            Datafill();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Activity actv = new Activity();
            Customer c = new Customer();
            User u = new User();
            ActivityCategory ac = new ActivityCategory();

            actv.Title = ActTitle.Text;
            actv.Description = ActDesc.Text;
            actv.date = DateTime.Now;

            c = cusbll.ReadCus(CusValue.Text);
            actv.Customer = c;

            u = ubll.ReadUser(AccValue.Text);
            actv.User = u;

            ac = catbll.ReadCategory(ActCateg.Text);
            actv.Category = ac;

            Reminder r = new Reminder();
            ReminderBll rembll = new ReminderBll();


            if (label3.Text == "Submit")
            {
                if (checkBox1.Checked)
                {
                    r.Title = ActTitle.Text;
                    r.Description = ActDesc.Text;
                    r.RegDate = DateTime.Now;
                    r.RemindDate = dateTimePicker1.Value.Date;
                    r.user = u;

                    rembll.Create(r, u);
                }

                msg.MessShow("Create Activity",acbll.Create(actv, u, c, ac),false);
            }
            else if(label3.Text == "Edit")
            {     
                
                msg.MessShow("Update Activity", acbll.Update(actv, id), false);
            }
            
            CusValue.Enabled = true;
            AccValue.Enabled = true;
            label3.Text = "Submit";
            checkBox1.Enabled = true;
            ActCateg.Text = null;
            ActDesc.Text = "";
            ActTitle.Text = "";


            Datafill();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activity a = acbll.Read(id);

            ActTitle.Text = a.Title;
            ActDesc.Text = a.Description;

            string category = a.Category.CatName;
            ActCateg.Text = category;

            CusValue.Enabled = false;
            AccValue.Enabled = false;
            checkBox1.Enabled = false;

            label3.Text = "Edit";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert","Are you sure you want to delete this activity?", true);
            if (dr == DialogResult.Yes)
            {
                msg.MessShow("Alert",acbll.Delete(id),false);
            }
            Datafill();
        }

        private void ProdSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = acbll.Read(ActSearch.Text);
            dataGridView1.Columns["Id"].Visible = false;     
        }

        private void ActDesc_MouseDown(object sender, MouseEventArgs e)
        {
            if (ActDesc.Text == "Type your description here")
            {
                ActDesc.Text = "";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SettingF catset = new SettingF();
            MainWindow mw = new MainWindow();
            mw.OpenForm(catset);
        }
    }
}
