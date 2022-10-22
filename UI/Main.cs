using Advanced_Combat_Tracker;
using FFXIV_ACT_BASE.Data;
using FFXIV_ACT_BASE.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIV_ACT_BASE.UI
{
    public partial class Main : UserControl, IActPluginV1
    {
        public Main()
        {
            InitializeComponent();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            pluginScreenSpace.Controls.Add(this);
            Dock = DockStyle.Fill;
            lblStatus = pluginStatusText;
            lblStatus.Text = "Plugin Started";

            //urlTextBox.Text = Properties.Settings.Default.lastURL.ToString();

            ActGlobals.oFormActMain.BeforeLogLineRead -= OFormActMain_BeforeLogLineRead;
            ActGlobals.oFormActMain.BeforeLogLineRead += OFormActMain_BeforeLogLineRead;

            //UpdateStatusLabel();
        }

        public void DeInitPlugin()
        {
            lblStatus.Text = "No Status";
            ActGlobals.oFormActMain.BeforeLogLineRead -= OFormActMain_BeforeLogLineRead;
        }

        private void OFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            string[] logLine = logInfo.originalLogLine.Split('|');
            if (!int.TryParse(logLine[0], out int logCode))
            {
                return;
            }

            LogDefine.Type logType = (LogDefine.Type)logCode;
            switch (logType)
            {
                case LogDefine.Type.Chat:
                    
                    this.lblChatLog.Text = string.Join(", ", logLine.Select(x => $"\"{x}\""));

                    if (logLine.Length >= 5)
                    {
                        if (logLine[2].Equals(LogDefine.ECHO_CHAT_CODE))
                        {
                            //Command.Execute(logLine[4].ToLower());
                        }
                    }

                    break;

                case LogDefine.Type.ChangePrimaryPlayer:
                    if (PlayerData.SetPlayer(logLine))
                    {
                        //nameText.Text = PlayerData.Name;
                        //UpdateStatusLabel();
                    }
                    break;

                case LogDefine.Type.AddCombatant:
                    if (PlayerData.SetPet(logLine))
                    {
                        //petText.Text = PlayerData.PetName;
                    }
                    break;

                case LogDefine.Type.RemoveCombatant:
                    if (PlayerData.RemovePet(logLine))
                    {
                        //petText.Text = "Not Found";
                    }
                    break;

                case LogDefine.Type.Ability:
                case LogDefine.Type.AOEAbility:
                    //LogData log = new LogData(logLine);
                    //if (log.IsValid && rotationWindow.Visible)
                    //{
                    //    rotationWindow.OnActionCasted(log);
                    //}
                    break;
            }
        }
    }
}
