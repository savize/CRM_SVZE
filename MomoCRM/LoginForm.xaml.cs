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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        MessCustm msg = new MessCustm();
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterForm rgf = new RegisterForm();
            this.Hide();
            rgf.Show();
        }

        User u = new User();
        UserBLL usbl = new UserBLL();
        MainWindow mw = new MainWindow();

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            u = usbl.Login(userValue.Text, passVal.Password.ToString());

            if (u != null)
            {
                mw.loggedInUser = u;
                this.Hide();
                mw.uservalue.Text = u.Username;
                mw.Show();
            }
            else
            {
                msg.MessShow("Alert","Input credentials are not correct. Consider register as a new user or contact Admin if registered before",false);
            }
        }
    }
}
