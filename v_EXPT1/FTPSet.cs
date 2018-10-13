using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exp_T1
{
    class FTPSet
    {
        public static bool ftpTransfer(string filePath, string ftpAdd, string user, string pass)
        {
            try
            {
                if (String.IsNullOrEmpty(ftpAdd.Trim()))
                   ftpAdd ="127.0.0.1"; 

                string ftpAddress = ftpAdd;
                string username = user; //"user";
                string password = pass; //"pass";

                using (StreamReader stream = new StreamReader(/*"C:\\" + */filePath))
                {
                    byte[] buffer = Encoding.Default.GetBytes(stream.ReadToEnd());

                    WebRequest request = WebRequest.Create("ftp://" + ftpAddress + "/" + "myfolder" + "/" + filePath);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(username, password);
                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                System.Media.SystemSounds.Beep.Play();
                System.Media.SystemSounds.Exclamation.Play();
                System.Media.SystemSounds.Hand.Play();
                System.Media.SystemSounds.Question.Play();
                return false;
            }
        }
    }     
}
