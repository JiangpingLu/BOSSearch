using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSMDMProdConsoleApp.Models
{
    public class SendGridMail
    {
        //email from
        private string _emailFrom;

        //email to 
        private List<string> _emailTo;

        //email subject
        private string _emailSubject;

        //email HTML
        private string _emailHTML;

        //email text
        private string _emailText;

        //email footer
        private string _emailEnableFooter;

        public string EmailFrom 
        {
            get { return this._emailFrom; }
            set { this._emailFrom = value; }
        }

        public List<string> EmailTo 
        {
            get { return this._emailTo; }
            set { this._emailTo = value; }
        }

        public string EmailSubject
        {
            get { return this._emailSubject; }
            set { this._emailSubject = value; }
        }

        public string EmailHTML
        {
            get { return this._emailHTML; }
            set { this._emailHTML = value; }
        }

        public string EmailText
        {
            get { return this._emailText; }
            set { this._emailText = value; }
        }

        public string EmailEnableFooter
        {
            get { return this._emailEnableFooter; }
            set { this._emailEnableFooter = value; }
        }
    }
}
