using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BLL;
using BusEntity;
using Microsoft.Win32;

namespace MomoCRM
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        LoginForm logf = new LoginForm();
        MessCustm msg = new MessCustm();

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserBLL usbll = new UserBLL();
            User u = new User();

            u.Name = NameValue.Text;
            u.Username = UserValue.Text;
            u.Password = PassValue.Text;
            u.RegDate = DateTime.Now;

            if (u.Pic != null)
            {
                u.Pic = SavePic(NameValue.Text);
            }
            else
            {
                u.Pic = null;
            }

            msg.MessShow("User Registration", usbll.Create(u), false);

            this.Hide();
            logf.Show();
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            logf.Show();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        string SavePic(string Username)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\UserPics\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string Picname = Username + ".JPG";
            try
            {
                string picPath = ofd.FileName;
                if (!File.Exists(path + Picname))
                {
                    File.Copy(picPath, path + Picname, true);
                }
            }
            catch (Exception e)
            {

                msg.MessShow("Error", e.Message, false);
            }
            return path + Picname;
        }


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ofd.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            ofd.Title = "Please upload your profile photo";

            if (ofd.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(ofd.FileName));
                img.Width = 80;
            }

        }
    }
}
