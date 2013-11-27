﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSScriptNpp
{
    public partial class ConfigForm : Form
    {
        Config data;

        public ConfigForm()
        {
            InitializeComponent();
        }

        public ConfigForm(Config data)
        {
            this.data = data;

            InitializeComponent();

            var panel = new CSScriptIntellisense.ConfigForm(CSScriptIntellisense.Config.Instance).ContentPanel;
            //panel.Parent = this;
            //panel.ParentForm = this;
            //panel.TopLevel = true;
            //this.AddOwnedForm(panel);
            this.Controls.Add(panel);
            
            //System.ArgumentException: Top-level control cannot be added to a control.
            //this.Controls.Add(panel);
        }

        private void ConfigForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string file = Config.Instance.GetFileName();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DateTime timestamp = File.GetLastWriteTimeUtc(file);
                    Process.Start("notepad.exe", file).WaitForExit();
                    if (File.GetLastWriteTimeUtc(file) != timestamp)
                        Config.Instance.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: \n" + ex.ToString(), "Notepad++");
                }
            });

            Close();
        }
    }
}