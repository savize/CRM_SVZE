using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for ClockWidget.xaml
    /// </summary>
    public partial class ClockWidget : UserControl
    {
        public ClockWidget()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timeTick);
            timer.Start();
        }

        private void timeTick(object sender, EventArgs e)
        {
            TimeS.Content = DateTime.Now.ToString();
        }



    }
}
