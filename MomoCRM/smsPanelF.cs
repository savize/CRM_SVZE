using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sib_api_v3_sdk.Client;
using System.Net.Http;
using System.Xml.Linq;

namespace MomoCRM
{
    public partial class smsPanelF : Form
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

        public smsPanelF()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        MessCustm msg = new MessCustm();
        void clear()
        {
            emCont.Text = "";
            emTit.Text = "";
            EmList.Items.Clear();
            rcvEmail.Text = "";
            rcvName.Text = "";
            emInput.Text = "";
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Configuration.Default.ApiKey.Clear();
            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "SVZE CRM";
            string SenderEmail = "s.zhddesign@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = rcvEmail.Text;
            string ToName = rcvName.Text;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
  
            string CcName = "Savize";
            string CcEmail = "s.zhddesign@gmail.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);

            string HtmlContent = emCont.Text;
            string TextContent = null;
            string Subject = emTit.Text;
            
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            
         
            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = null;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "New Subject");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);

            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, null, Cc, null, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, Cc, HtmlContent, TextContent, Subject, null, null, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                msg.MessShow("Success","Your email has been sent successfully",false);
            }
            catch (Exception y)
            {
                msg.MessShow("Error",y.Message, false);
            }
            clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Configuration.Default.ApiKey.Clear();
            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "SVZE CRM";
            string SenderEmail = "s.zhddesign@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

            foreach (var item in EmList.Items)
            {
                string ToEmail = item.ToString();

                SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, null);
                List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();

                To.Add(smtpEmailTo);


                string HtmlContent = emCont.Text;
                string TextContent = null;
                string Subject = emTit.Text;

                string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
                byte[] Content = System.Convert.FromBase64String(stringInBase64);


                JObject Headers = new JObject();
                Headers.Add("Some-Custom-Name", "unique-id-1234");
                long? TemplateId = null;
                JObject Params = new JObject();
                Params.Add("parameter", "My param value");
                Params.Add("subject", "New Subject");
                List<string> Tags = new List<string>();
                Tags.Add("mytag");
                SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, null);
                List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
                To1.Add(smtpEmailTo1);
                Dictionary<string, object> _parmas = new Dictionary<string, object>();
                _parmas.Add("params", Params);

                SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, null, null, null, Subject);
                List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
                messageVersiopns.Add(messageVersion);
                try
                {
                    var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, null, null, Headers, TemplateId, Params, messageVersiopns, Tags);
                    CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                }
                catch (Exception y)
                {
                    msg.MessShow("Error", y.Message, false);
                }
            }
           
            msg.MessShow("Success", "Your email has been sent successfully", false);
            clear();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(emInput.Text != "")
            {
                EmList.Items.Add(emInput.Text);
            }
        }

        private void emCont_MouseDown(object sender, MouseEventArgs e)
        {
            emCont.Text = "";
        }
    }
}