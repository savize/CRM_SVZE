using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoCRM
{
    public class MessCustm
    {
        public DialogResult MessShow(string title, string message, bool buttons)
        {
            MessageB msg = new MessageB();

            msg.Title.Text = title;
            msg.Desc.Text = message;

            if (buttons)
            {
                msg.yesBtn.Visible = true;
                msg.NoBtn.Visible = true;
                msg.OkBtn.Visible = false;
            }
            else
            {
                msg.yesBtn.Visible = false;
                msg.NoBtn.Visible = false;
                msg.OkBtn.Visible = true;
                msg.BackColor = Color.FromArgb(61, 90, 128);
            }

            msg.ShowDialog();
            return msg.DialogResult;
        }
    }
}
