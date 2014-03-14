using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CSScriptNpp
{
    /// <summary>
    /// Of course XML based config is more natural, however ini file reading (just a few values) is faster. 
    /// </summary>
    public class Config : IniFile
    {
        static Config instance = new Config();

        public static Config Instance { get { return instance; } }

        public Config()
        {
            base.file = Path.Combine(Plugin.ConfigDir, "settings.ini");

            CSScriptIntellisense.Config.Instance.SetFileName(base.file);
            CSScriptIntellisense.Config.Instance.Section = "Intellisense";
            
            Open();
        }

        public bool ClasslessScriptByDefault = false;
        public bool DistributeScriptAsScriptByDefault = true;
        public bool InterceptConsole = false;
        public bool NavigateToRawCodeOnDblClickInOutput = false;
        public bool BuildOnF7 = true;
        public bool CheckUpdatesOnStartup = true;
        public bool FloatingPanelsWarningAlreadyPropted = false;
        public string TargetVersion = "v4.0.30319";
        public string ReleaseNotesViewedFor = "";
        public string LastUpdatesCheckDate = DateTime.MinValue.ToString("yyyy-MM-dd");
        public string SciptHistory = "";
        public int SciptHistoryMaxCount = 10;
        public bool ShowProjectPanel = false;
        public bool ShowOutputPanel = false;
        public bool ShowCodeMapPanel = false;
        public bool ShowDebugPanel = false;
        public int OutputPanelCapacity = 10000; //num of characters
        public bool LocalDebug = true;

        public string Section = "Generic";

        public string GetFileName()
        {
            return base.file;
        }

        public void Save()
        {
            File.WriteAllText(this.file, ""); //clear to get rid of all obsolete values

            SetValue(Section, "ShowProjectPanel", ShowProjectPanel);
            SetValue(Section, "ShowOutputPanel", ShowOutputPanel);
            SetValue(Section, "ShowCodeMapPanel", ShowCodeMapPanel);
            SetValue(Section, "ShowDebugPanel", ShowDebugPanel);
            SetValue(Section, "OutputPanelCapacity", OutputPanelCapacity);
            SetValue(Section, "NavigateToRawCodeOnDblClickInOutput", NavigateToRawCodeOnDblClickInOutput);
            SetValue(Section, "InterceptConsole", InterceptConsole);
            SetValue(Section, "ReleaseNotesViewedFor", ReleaseNotesViewedFor);
            SetValue(Section, "SciptHistory", SciptHistory);
            SetValue(Section, "SciptHistoryMaxCount", SciptHistoryMaxCount);
            SetValue(Section, "LocalDebug", LocalDebug);
            SetValue(Section, "BuildOnF7", BuildOnF7);
            SetValue(Section, "LastUpdatesCheckDate", LastUpdatesCheckDate);
            SetValue(Section, "CheckUpdatesOnStartup", CheckUpdatesOnStartup);
            SetValue(Section, "FloatingPanelsWarningAlreadyPropted", FloatingPanelsWarningAlreadyPropted);
            SetValue(Section, "TargetVersion", TargetVersion);
            SetValue(Section, "ClasslessScriptByDefault", ClasslessScriptByDefault);
            SetValue(Section, "DistributeScriptAsScriptByDefault", DistributeScriptAsScriptByDefault);

            CSScriptIntellisense.Config.Instance.Save();
        }

        public void Open()
        {
            ShowProjectPanel = GetValue(Section, "ShowProjectPanel", ShowProjectPanel);
            ShowOutputPanel = GetValue(Section, "ShowOutputPanel", ShowOutputPanel);
            ShowCodeMapPanel = GetValue(Section, "ShowCodeMapPanel", ShowCodeMapPanel);
            ShowDebugPanel = GetValue(Section, "ShowDebugPanel", ShowDebugPanel);
            SciptHistory = GetValue(Section, "SciptHistory", SciptHistory, 1024 * 4);
            SciptHistoryMaxCount = GetValue(Section, "SciptHistoryMaxCount", SciptHistoryMaxCount);
            OutputPanelCapacity = GetValue(Section, "OutputPanelCapacity", OutputPanelCapacity);
            NavigateToRawCodeOnDblClickInOutput = GetValue(Section, "NavigateToRawCodeOnDblClickInOutput", NavigateToRawCodeOnDblClickInOutput);
            InterceptConsole = GetValue(Section, "InterceptConsole", InterceptConsole);
            LocalDebug = GetValue(Section, "LocalDebug", LocalDebug);
            TargetVersion = GetValue(Section, "TargetVersion", TargetVersion);
            ReleaseNotesViewedFor = GetValue(Section, "ReleaseNotesViewedFor", ReleaseNotesViewedFor);
            BuildOnF7 = GetValue(Section, "BuildOnF7", BuildOnF7);
            LastUpdatesCheckDate = GetValue(Section, "LastUpdatesCheckDate", LastUpdatesCheckDate);
            CheckUpdatesOnStartup = GetValue(Section, "CheckUpdatesOnStartup", CheckUpdatesOnStartup);
            FloatingPanelsWarningAlreadyPropted = GetValue(Section, "FloatingPanelsWarningAlreadyPropted", FloatingPanelsWarningAlreadyPropted);
            ClasslessScriptByDefault = GetValue(Section, "ClasslessScriptByDefault", ClasslessScriptByDefault);
            DistributeScriptAsScriptByDefault = GetValue(Section, "DistributeScriptAsScriptByDefault", DistributeScriptAsScriptByDefault);
            CSScriptIntellisense.Config.Instance.Open();
        }
    }
}
