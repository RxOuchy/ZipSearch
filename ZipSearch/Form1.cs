using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ZipSearch
{
    public partial class Form1 : Form
    {

        List<string> filesFound = new List<string>();

        public Form1()
        {
            InitializeComponent();
            this.Width = 300; //Half Size
            //this.Width = 645; //Full Size
            dtStart.Value = DateTime.Now.AddDays(-30);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool ValidateForm()
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text == "Please select a search location")
            {
                txtSearch.Text = "Please select a search location";
                txtSearch.BackColor = Color.FromArgb(237,88,71);
                errors.Add("Please select a search location");
            }

            if (String.IsNullOrEmpty(txtExtract.Text) || txtExtract.Text == "Please select an extraction location")
            {
                txtExtract.Text = "Please select an extraction location";
                txtExtract.BackColor = Color.FromArgb(237, 88, 71);
                errors.Add("Please select an extraction location");
            }

            if (String.IsNullOrEmpty(txtClient.Text) || txtClient.Text == "Please enter the Client UID")
            {
                txtClient.Text = "Please enter the Client UID";
                txtClient.BackColor = Color.FromArgb(237, 88, 71);
                errors.Add("Please enter the Client UID");
            }

            if (String.IsNullOrEmpty(txtFileName.Text) || txtFileName.Text == "Please enter a file name (Product, customers, orders, etc)")
            {
                txtFileName.Text = "Please enter a file name (Product, customers, orders, etc)";
                txtFileName.BackColor = Color.FromArgb(237, 88, 71);
                errors.Add("Please enter a file name (Product, customers, orders, etc)");
            }

            if (String.IsNullOrEmpty(txtFileExt.Text) || txtFileExt.Text == "Please enter a File Extension (csv, txt, etc)")
            {
                txtFileExt.Text = "Please enter a File Extension (csv, txt, etc)";
                txtFileExt.BackColor = Color.FromArgb(237, 88, 71);
                errors.Add("Please enter a File Extension (csv, txt, etc)");
            }

            return errors.Count == 0;
        }

        private void btnSearchFiles_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {

                string zipPath = txtSearch.Text;
                string searchString = txtClient.Text;
                string fileName = txtFileName.Text + "*." + txtFileExt.Text;

                foreach (var filePath in Directory.EnumerateFiles(zipPath, "*.zip"))
                {
                    using (ZipArchive archive = ZipFile.OpenRead(filePath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.StartsWith(searchString, StringComparison.OrdinalIgnoreCase))
                            {
                                string reg = Regex.Escape(fileName).Replace("\\?", ".").Replace("\\*", ".*") + "$";
                                if (Regex.IsMatch(entry.FullName, reg))
                                {
                                    filesFound.Add(entry.FullName);
                                }
                            }
                        }
                    }
                }

                this.Width = 645;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                txtSearch.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                txtExtract.Text = folderBrowserDialog2.SelectedPath;
        }

    }
}
