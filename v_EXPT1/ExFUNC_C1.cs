using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;

namespace Exp_T1
{
    public static class ExFUNC_C1
    {
        public static void Export2File(string TEXT, string path)
        {
            //File.CreateText(path);

            //this code segment write data to file.
            FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(TEXT);
            writer.Close();
        }

        public static string InputFromFile(string path)
        {
            //this code segment read data from the file.
            FileStream fs2 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fs2);
            string Content = reader.ReadToEnd();
            reader.Close();
            return (Content);
        }

        

        /// <summary>
        ///     connectionStrings
        ///         add name="YourDB" connectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True"
        ///         providerName="System.Data.SqlClient" 
        ///     /connectionStrings
        /// </summary>
        /// <param name="INP"></param>
        /// <param name="ConnectionSTR"></param>
        public static void SQLight_INSERT(string INP, string ConnectionSTR)
        {
            // Get Connection String            
            ConnectionStringSettings conn = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionSTR];
            // Create connection object
            SqlConnection connection = new SqlConnection(conn.ToString());
            SqlCommand command = connection.CreateCommand();
            try
            {
                // Open the connection.
                connection.Open();
                // Execute the insert command.
                command.CommandText = ("INSERT INTO Your_Tbl(DATUM) VALUES(\'"
                            + (INP + ("\',\'")));
                command.ExecuteNonQuery();
            }
            finally
            {
                // Close the connection.
                connection.Close();
            }

            // Display success message.
            ///MsgBox("Insertion Successful");
        }


        public static void ExecuteCommand(string filePath)//string command)
        {
            Process proc = null;

            string _batDir = string.Format(@"C:\");
            proc = new Process();
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(filePath);//_batDir;
            proc.StartInfo.FileName = filePath;//"myfile.bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();
            int ExitCode = proc.ExitCode;
            proc.Close();
            MessageBox.Show("Bat file executed succesfully. (ExitCode: "+ExitCode.ToString()+")");

            //var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            //processInfo.CreateNoWindow = true;
            //processInfo.UseShellExecute = false;
            //processInfo.RedirectStandardError = true;
            //processInfo.RedirectStandardOutput = true;

            //var process = Process.Start(processInfo);

            //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("output>>" + e.Data);
            //process.BeginOutputReadLine();

            //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("error>>" + e.Data);
            //process.BeginErrorReadLine();

            //process.WaitForExit();

            //Console.WriteLine("ExitCode: {0}", process.ExitCode);
            //process.Close();

        }
    }
}
