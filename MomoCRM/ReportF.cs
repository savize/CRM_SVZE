using BLL;
using BusEntity;
using Stimulsoft.Report;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace MomoCRM
{
    public partial class ReportF : Form
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

        public ReportF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



        private void ReportF_Load(object sender, EventArgs e)
        {
            chart1.Visible = false;
        }

        private void PrintGen_Click(object sender, EventArgs e)
        {
            StiReport stimsoft = new StiReport();

            if (Sale.Checked)
            {
                stimsoft.Load(@"C:\\Users\\Savize\\source\\repos\\MomoCRM\\InvoiceReport.mrt");
                stimsoft.Render();
                stimsoft.Show();
            }
            else if (RegsCus.Checked)
            {
                stimsoft.Load(@"C:\\Users\\Savize\\source\\repos\\MomoCRM\\CusReport.mrt");
                stimsoft.Render();
                stimsoft.Show();
            }
            else if (UserAct.Checked)
            {
                stimsoft.Load(@"C:\\Users\\Savize\\source\\repos\\MomoCRM\\ActivityReport.mrt");
                stimsoft.Render();
                stimsoft.Show();
            }
        }

        private void PrvGen_Click(object sender, EventArgs e)
        {
            UserBLL usbll = new UserBLL();
            CustomerBLL cusbll = new CustomerBLL();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Visible = true;

            if (RegsCus.Checked)
            {
                chart1.Series["Sales"].Enabled = false;
                chart1.Series["Customers"].Enabled = true;
                chart1.Series[2].Enabled = false;

                for (int i = 1; i <= 12; i++)
                {
                    chart1.Series["Customers"].Points.AddXY("2023/" + i, Convert.ToInt16(cusbll.ReadCusReport(i)));
                }

                if (columnRad.Checked)
                {
                    chart1.Series["Customers"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if (SplineRad.Checked)
                {
                    chart1.Series["Customers"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
            else if (Sale.Checked)
            {
                chart1.Series["Customers"].Enabled = false;
                chart1.Series[2].Enabled = false;
                chart1.Series["Sales"].Enabled = true;

                foreach (var item in usbll.ReadUsers())
                {
                    chart1.Series["Sales"].Points.AddXY(item.Username, item.invoices.Count());
                }

                if (columnRad.Checked)
                {
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if (SplineRad.Checked)
                {
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
            else if (UserAct.Checked)
            {
                chart1.Series[0].Enabled = false;
                chart1.Series[1].Enabled = false;
                chart1.Series[2].Enabled = true;

                foreach (var data in usbll.ReadUsersActiv())
                {
                    chart1.Series[2].Points.AddXY(data.Username, data.Activities.Count());
                }

                if (columnRad.Checked)
                {
                    chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if (SplineRad.Checked)
                {
                    chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
            else
            {
                chart1.Visible = false;
            }

            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = false;
        }

        private void PrvAdv_Click(object sender, EventArgs e)
        {
            UserBLL usbll = new UserBLL();
            CustomerBLL cusbll = new CustomerBLL();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Visible = true;

            if (RegsCusAdv.Checked)
            {
                chart1.Series[0].Enabled = false;
                chart1.Series[1].Enabled = true;
                chart1.Series[2].Enabled = false;

                int j = 0;

                for (int i = dateFrom.Value.Month; i <= dateTo.Value.Month; i++)
                {
                    chart1.Series[1].Points.AddXY(dateFrom.Value.Month + j, cusbll.ReadCusReport(i));
                    j++;
                }

                if (columnRad.Checked)
                {
                    chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if(SplineRad.Checked)
                {
                    chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
            else if (UserAcAdv.Checked)
            {
                chart1.Series[0].Enabled = false;
                chart1.Series[1].Enabled = false;
                chart1.Series[2].Enabled = true;

                foreach (var item in usbll.ReadUsersActiv())
                {
                    int x = 0;

                    foreach (var q in item.Activities)
                    {
                        if (q.date >= dateFrom.Value.Date && q.date <= dateTo.Value.Date)
                        {
                            x++;
                        }
                    }
                    chart1.Series[2].Points.AddXY(item.Username, x);
                }
                if (columnRad.Checked)
                {
                    chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if (SplineRad.Checked)
                {
                    chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
            else if (SaleAdv.Checked)
            {
                chart1.Series[0].Enabled = true;
                chart1.Series[1].Enabled = false;
                chart1.Series[2].Enabled = false;

                foreach (var item in usbll.ReadUsers())
                {
                    int x = 0;
                    foreach (var q in item.invoices)
                    {
                        if (q.RegDate >= dateFrom.Value.Date && q.RegDate <= dateTo.Value.Date)
                        {
                            x++;
                        }
                    }
                    chart1.Series[0].Points.AddXY(item.Username, x);
                }
                if (columnRad.Checked)
                {
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                }
                else if (SplineRad.Checked)
                {
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                }
            }
        }

    }
}
