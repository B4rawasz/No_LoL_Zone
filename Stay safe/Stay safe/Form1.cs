using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Stay_safe
{
    public partial class Form1 : Form
    {
        Thread app = new Thread(LookFor.lookForApp);

        //NotifyIcon notifyIcon = new NotifyIcon();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            app.Start();
            icon = notifyIcon;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LookFor.run = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(300);
            }
        }

        public static NotifyIcon icon;

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        public static void rescue()
        {
            icon.BalloonTipText = "That was close 😅";
            icon.BalloonTipTitle = "BRO!";
            icon.ShowBalloonTip(300);
            icon.BalloonTipText = "With me, you are safe";
            icon.BalloonTipTitle = "I'm here";
        }
    }

    public class LookFor
    {
        public static bool run = true;
        public static void lookForApp()
        {
            while (run)
            {
                Thread.Sleep(10000);
                Process[] runingProcess = Process.GetProcesses();

                for (int i = 0; i < runingProcess.Length; i++)
                {
                    if (runingProcess[i].ProcessName == "LeagueClient")
                    {
                        runingProcess[i].Kill();
                        Form1.rescue();
                    }
                }
            }
        }
    }
}