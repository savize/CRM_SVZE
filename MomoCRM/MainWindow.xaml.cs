using BLL;
using BusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DashboardBll dashbll = new DashboardBll();

        public User loggedInUser = new User();
        public void OpenForm (Form f)
        {
            Window g =  this.FindName("Main") as Window;
            BlurBitmapEffect blur = new BlurBitmapEffect();
            blur.Radius = 15;

            g.BitmapEffect = blur;
            
            f.ShowDialog();
            blur.Radius = 0;
            g.BitmapEffect = blur;
        }


        public void Refresh()
        {
            uservalue.Text = loggedInUser.Username;
            invValue.Text = dashbll.InvCount(loggedInUser);
            CusValue.Text = dashbll.CusCount();
            actCus.Text = dashbll.CusActCount();
            RemValue.Text = dashbll.RemCount(loggedInUser);

            RemindLoad();
        }

        public void RemindLoad()
        {
            RemGrid.Children.Clear();
            int a = 0;

            foreach (var item in dashbll.UserReminders(loggedInUser))
            {
                UCRemind ucr = new UCRemind();

                ucr.TitleValue.Text = item.Title;
                ucr.ContValue.Text = item.Description;

                RemGrid.Children.Remove(ucr);

                Grid.SetRow(ucr, a);

                RemGrid.Children.Add(ucr);
                
                a++;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CusForm cf = new CusForm();
            OpenForm(cf);
            Refresh();

        }

        private void prodmng_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductMng pm = new ProductMng();
            OpenForm(pm);
            Refresh();

        }

        private void invoice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InvoiceF inv = new InvoiceF();
            OpenForm(inv);
            Refresh();
        }

        private void activity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActivityF af = new ActivityF();
            OpenForm(af);
            Refresh();
            RemindLoad();
        }

        private void Reminders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReminderF remf = new ReminderF();
            OpenForm(remf);
            Refresh();
            RemindLoad();
        }

        private void smsPanl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            smsPanelF sms = new smsPanelF();
            OpenForm(sms);
            Refresh();
        }

        private void Reports_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReportF rep = new ReportF();
            OpenForm(rep);
            Refresh();
        }

        private void cusmng_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            CusForm cf = new CusForm();
            OpenForm(cf);
            Refresh();
        }

        MessCustm msg = new MessCustm();
        private void usmng_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserRegister usrg = new UserRegister();
            OpenForm(usrg);
            Refresh();
        }

   
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult dr = msg.MessShow("Shutdown", "Are you sure you want to close the application?", true);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            DialogResult dr = msg.MessShow("Alert", "Are you sure you want to log out of the system?", true);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                loggedInUser = null;
                LoginForm lgf = new LoginForm();

                this.Hide();
                lgf.Show();
            }

        }
    }
}
