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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using BLL;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    /// 
    public partial class Loading : Window, INotifyPropertyChanged
    {
 

        private BackgroundWorker _bgWork = new BackgroundWorker();
        private int _WorkerState;

        public event PropertyChangedEventHandler PropertyChanged;

        public int WorkerState
        {
            get { return _WorkerState; }
            set
            {
                _WorkerState = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
            }
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public Loading()
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();


            DataContext = this;
            _bgWork.DoWork += (s, e) =>
            {
                for (int i = 0; i <= 100; i+=5)
                {
                    System.Threading.Thread.Sleep(100);
                    WorkerState = i;

                    if(i== 100) 
                    {
                        LoginForm log = new LoginForm();
                        this.Hide();
                        log.Show();
                    }
                }       
            };
            _bgWork.RunWorkerAsync();

        }

        UserBLL usbl = new UserBLL();
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();

            usbl.Read();

            LoginForm logf = new LoginForm();
            this.Hide();
            logf.ShowDialog();
        }


    }
}
