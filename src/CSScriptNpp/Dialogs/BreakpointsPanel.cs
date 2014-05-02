﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSScriptNpp.Dialogs
{
    public partial class BreakpointsPanel : Form
    {
        public BreakpointsPanel()
        {
            InitializeComponent();
            RefreshItems();
            Debugger.OnBreakpointChanged = RefreshItems;
        }

        public void RefreshItems()
        {
            breakPoints.Items.Clear();

            var g = CreateGraphics();
            int maxWidth = 100;
            int index = 1;
            var items = Debugger.GetActiveBreakpoints()
                                .Select(x => new { Data = x, Fields = x.Split('|') })
                                .OrderByDescending(x => x.Fields[0])
                                .ThenByDescending(x => int.Parse(x.Fields[1]))
                                .Select(x => x.Data)
                                .Reverse();

            foreach (var item in items)
            {
                string[] parts = item.Split('|');

                string file = parts[0];
                string line = parts[1];

                var li = new ListViewItem(index.ToString());
                li.SubItems.Add(Path.GetFileName(file));
                li.SubItems.Add(line);
                li.Tag = item;
                li.ToolTipText = string.Format("{0} ({1})", file, line);

                maxWidth = Math.Max(maxWidth, (int)g.MeasureString(li.SubItems[1].Text, breakPoints.Font).Width);
                this.breakPoints.Items.Add(li);
                index++;
            }

            this.breakPoints.Columns[1].Width = maxWidth + 10;
        }

        ToolTip toolTip = new ToolTip();

        private void stack_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //Breakpoint syntax: <file>|<line>
                string[] parts = (breakPoints.SelectedItems[0].Tag as string).Split('|');
                Npp.NavigateToFileContent(parts[0], int.Parse(parts[1]) - 1, 1);

                //because we only show non-autogenerated files there is no need to translate the location
                //Though if this changes in the future the following 'translation' 
                //routine should be uncommented
                //string breakpointSpec = string.Format("{0}|{1}:1|{1}:1", parts[0], parts[1]);
                //var location = Debugger.FileLocation.Parse(breakpointSpec);
                //Debugger.TranslateCompiledLocation(location);
                //Npp.NavigateToFileContent(location.File, location.Line, 1);
            }
            catch { }
        }

        private void breakpoints_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            //toolTip.InitialDelay = 2000;
            var cursor = this.PointToClient(MousePosition);
            cursor.Offset(15, 15);
            toolTip.Show(e.Item.ToolTipText, this, cursor.X, cursor.Y, 800);
        }

        private void removeAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debugger.RemoveAllBreakpoints();
        }
    }
}
