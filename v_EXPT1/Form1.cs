using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Exp_T1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("This will close down the application and all connections. Confirm?", "Cancel connections prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("The application connections has been closed successfully.", "Application Closed!", MessageBoxButtons.OK);
            }
            else
            {
                e.Cancel = true;
                this.Activate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (File.Exists(Variables.MEMO_PATH))
            //{
            /// Just clear already present message
            //LogMessage(String.Empty);
            saveFileDialog1.Filter = "TEXT files (*.txt)|*.txt";
            saveFileDialog1.InitialDirectory = Application.StartupPath;// + "\\ SavedTest\\";

            DialogResult d = DialogResult.Cancel;
            FileInfo fi;
            if (string.IsNullOrEmpty(Variables.MEMO_PATH.Trim()))
                fi =new FileInfo(Application.StartupPath + "\\SavedTest\\" + "temp");
            else
                fi = new FileInfo(Variables.MEMO_PATH);
            // amend the above line to whatever the file name should be
            if (fi.Exists)
                d = DialogResult.OK;
            else
                d = saveFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                string pathholder = saveFileDialog1.FileName;
                saveFileDialog1.Reset();
                //LogMessage("Saving " + file + "...");
                ExFUNC_C1.Export2File(textBox1.Text, pathholder);
                //LogMessage("Saved Successfully.");
                Variables.IsData_saved = true;
            }
            //}


        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "TEXT files (*.txt)|*.txt";
            saveFileDialog1.InitialDirectory = Application.StartupPath;// + "\\ SavedTest\\";

            DialogResult d = DialogResult.Cancel;

            d = saveFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                string pathholder = saveFileDialog1.FileName;
                saveFileDialog1.Reset();
                //LogMessage("Saving " + file + "...");
                ExFUNC_C1.Export2File(textBox1.Text, pathholder);
                //LogMessage("Saved Successfully.");
                Variables.IsData_saved = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Variables.IsData_saved = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(Variables.IsData_saved))
                if (MessageBox.Show("The current unsaved data and the connection is closing. Confirm?", "Closing prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Variables.MEMO_PATH = "";
                    Variables.IsData_saved = false;
                    Variables.Connection_string = "";
                    Variables.port_num = 0;

                    textBox1.Clear();
                }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeToolStripMenuItem_Click(this, e);
        }

        private void fTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the ftp address:", "FTPSet", "SET", 0, 0);
            toolStripMenuItem6_Click(this, e);
            ExFUNC_C1.Export2File(textBox1.Text,Variables.TempFilePath);
            FTPSet.ftpTransfer(Variables.TempFilePath,Variables.FTPAdd,Variables.FTPUser,Variables.FTPPass);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the FTP address:", "FTPSet", "SET", 0, 0);
            string inp2 = Microsoft.VisualBasic.Interaction.InputBox("Please enter the FTP username:", "FTPSet", "SET", 0, 0);
            string inp1 = Microsoft.VisualBasic.Interaction.InputBox("Please enter the FTP password:", "FTPSet", "SET", 0, 0);
            ///(?)  Encryption or something like that
            Variables.FTPAdd = input;
            Variables.FTPUser = inp2;
            Variables.FTPPass = inp1;
        }

        private void cloudToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog1.InitialDirectory = Application.StartupPath;// + "\\ SavedTest\\";

            DialogResult d = DialogResult.Cancel;
            FileInfo fi = new FileInfo(Application.StartupPath + "\\SavedTest\\" + "temp");
            // amend the above line to whatever the file name should be
            if (fi.Exists)
                d = DialogResult.OK;
            else
                d = saveFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                string pathholder = saveFileDialog1.FileName;
                saveFileDialog1.Reset();
                //LogMessage("Saving " + file + "...");
                ExFUNC_C1.Export2File(textBox1.Text, pathholder);
                //XMLFMS.WriteToXmlFile<TextBox>(pathholder, textBox1, false);
                XMLFMS.WriteToXmlFile(pathholder, textBox1.Text);
                //LogMessage("Saved Successfully.");
                Variables.IsData_saved = true;
            }
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Batch script files (*.bat)|*.bat";
            saveFileDialog1.InitialDirectory = Application.StartupPath;// + "\\ SavedTest\\";

            DialogResult d = DialogResult.Cancel;
            FileInfo fi = new FileInfo(Application.StartupPath + "\\SavedTest\\" + "temp");
            // amend the above line to whatever the file name should be
            if (fi.Exists)
                d = DialogResult.OK;
            else
                d = saveFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                string pathholder = saveFileDialog1.FileName;
                saveFileDialog1.Reset();
                //LogMessage("Saving " + file + "...");
                ExFUNC_C1.Export2File(textBox1.Text, pathholder);

                if (MessageBox.Show("Do you want to execute the windows script command?", "RUN PROMPT", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    ExFUNC_C1.ExecuteCommand(pathholder);

                //LogMessage("Saved Successfully.");
                Variables.IsData_saved = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;// + "\\ SavedTest\\";

                DialogResult d = DialogResult.Cancel;
                d = openFileDialog1.ShowDialog();
                if (d == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = (ExFUNC_C1.InputFromFile(openFileDialog1.FileName));
                    Variables.IsData_saved = false;
                    Variables.MEMO_PATH =openFileDialog1.FileName;
                }               
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void setSQLightDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the SQLite db name:", "SQLite setup", "mySQL", 0, 0);
            SQLite.Connect(input);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This opensource project designed for testing the different output methods for exporting a text file. the source code is available on github(https://github.com/michaelkhany/MemoIF)", "ABOUT THE PROJECT",MessageBoxButtons.OKCancel);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
            listBox1.Items.Add(Clipboard.GetText());
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(listBox1.SelectedItem.ToString());
            }
            catch { System.Media.SystemSounds.Exclamation.Play(); }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
            listBox1.Items.Add(Clipboard.GetText());
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = (clipboardToolStripMenuItem.Checked);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Refresh();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(this, e);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(this, e);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(this, e);
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem_Click(this, e);
        }

        private void freeCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }


    public static class Variables
    {
        public static string TempFilePath =@"D:\\Exp_T1TEMP.txt.tmp";
        public static string MEMO_PATH = "";
        public static bool IsData_saved = true;

        public static bool Is_connected = false;
        public static string Connection_string;
        public static int port_num = 8080;

        public static string FTPAdd; public static string FTPUser; public static string FTPPass;
    }
}
