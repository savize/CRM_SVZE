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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for UCRemind.xaml
    /// </summary>
    public partial class UCRemind : UserControl
    {
        public UCRemind()
        {
            InitializeComponent();
        }

        MainWindow mw = new MainWindow();
        DashboardBll dashbl = new DashboardBll();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Reminder r = dashbl.readRem(TitleValue.Text);
            r.Status = true;
            
            mw.RemindLoad();
        }
    }
}
