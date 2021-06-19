using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Classes
{
    class csencrypt
    {

        public string encrypted;
        public csencrypt(string cha)
        {
            if (cha == "")
            {
                this.encrypted = "";
            }
            else
            {
                AES Aess = new AES();
                string EncryptedString = Aess.EncryptAESToString(cha, "1234567890abcdef");
                // System.Web.HttpUtility.UrlEncode requires reference to System.Web.dll
                this.encrypted = System.Web.HttpUtility.HtmlEncode(EncryptedString);
            }
        }

    }
}
