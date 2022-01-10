using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace foldit
{
    public partial class FolditConfig : Form
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public List<RadioButton> radioButtonSelected = new List<RadioButton>();

        /// <summary>
        ///   Default constructor for Config Form
        /// </summary>
        public FolditConfig()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            transparentLabel1.Text = "Create\r\n a new shortcut in your desktop";
            transparentLabel1.ForeColor = Color.White;
            transparentLabel3.Text = "Switch between differents type of blur";
            transparentLabel3.ForeColor = Color.White;
            transparentLabelSetAccent.ForeColor = Color.White;
            transparentLabelAlpha.ForeColor = Color.White;
            numericUpDownAlpha.Value = Properties.Settings.Default.AlphaValue;


            if (Properties.Settings.Default.Accent == 2)
            {
                numericUpDownAlpha.Visible = true;
                transparentLabelAlpha.Visible = true;
                buttonSetColor.Visible = true;
            }
            else if (Properties.Settings.Default.Accent == 3)
            {
                buttonSetColor.Visible = false;
                transparentLabelSetAccent.Visible = false;
            }
            refresh();

        }


        /// <summary>
        /// Use to move the window by clicking in window's void
        /// </summary>

        private void Form2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void buttonSwitchBlur_Click(object sender, EventArgs e)
        {
            int accent = Properties.Settings.Default.Accent + 2;
            switch (accent)
            {
                case 3:
                    buttonSetColor.Visible = false;
                    transparentLabelAlpha.Visible = false;
                    numericUpDownAlpha.Visible = false;
                    transparentLabelSetAccent.Visible = false;
                    setAllForeColor(Color.White);
                    break;
                /* fixme : when window start with this parameter, this last take a weird color
            case 2:
                numericUpDownAlpha.Visible = true;
                buttonSetColor.Visible = true;
                transparentLabelSetAccent.Visible = true;
                transparentLabelAlpha.Visible = true;
                break;
                */
                default:
                    accent = 1;
                    buttonSetColor.Visible = true;
                    transparentLabelSetAccent.Visible = true;
                    numericUpDownAlpha.Visible = false;
                    transparentLabelAlpha.Visible = false;
                    break;
            }
            Properties.Settings.Default.Accent = accent;
            Properties.Settings.Default.Save();
            Debug.WriteLine("accent set to " + accent);
            refresh();
        }
        private void buttonCreateNewFold_Click(object sender, EventArgs e)
        {
            int id = Properties.Settings.Default.nbrGroup;
            string shortcutLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("Foldit Group {0}.lnk", id));
            string mm = Environment.CurrentDirectory + "\\Icons\\default.ico";
            string dd = Environment.CurrentDirectory + String.Format("\\Icons/{0}.ico", id);
            if (System.IO.File.Exists(dd))
            {
                System.IO.File.Delete(dd);
            }
            System.IO.File.Copy(mm, dd);
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = "Shortcut created by foldit";   // The description of the shortcut
            shortcut.IconLocation = Environment.CurrentDirectory + "\\Icons\\" + String.Format("{0}.ico", id);           // The icon of the shortcut
            shortcut.TargetPath = Environment.CurrentDirectory.ToString() + "\\foldit.exe"; ;                 // The path of the file that will launch when the shortcut is run
            shortcut.Arguments = id.ToString();
            shortcut.WorkingDirectory = Environment.CurrentDirectory;
            shortcut.Save();
            FileStream file = System.IO.File.Create(Environment.CurrentDirectory + String.Format("/Save/{0}.config", id));
            file.Close();
            id++;
            Properties.Settings.Default.nbrGroup = id;
            Properties.Settings.Default.Save();
            refresh();
        }
        private void buttonDelete_click(object sender, EventArgs e)
        {

            foreach (RadioButton rb in radioButtonContainer.Controls)
            {
                if (rb.Checked)
                {
                    radioButtonContainer.Controls.Remove(rb);
                    delete(int.Parse(rb.Tag.ToString()));
                }

            }

        }
        private void buttonSetColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                color = Color.FromArgb((int)Properties.Settings.Default.AlphaValue, color);
                Properties.Settings.Default.Color = color;
                Properties.Settings.Default.Save();

                BrightnessEvaluation();
                refresh();
            }
        }
        private void numericUpDownAlpha_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDownAlpha = (NumericUpDown)sender;

            if (numericUpDownAlpha.Value < 0) numericUpDownAlpha.Value = 0;
            else if (numericUpDownAlpha.Value > 255) numericUpDownAlpha.Value = 255;

            Color color = Properties.Settings.Default.Color;
            Color newColor = Color.FromArgb((int)numericUpDownAlpha.Value, color);
            Properties.Settings.Default.Color = newColor;
            Properties.Settings.Default.AlphaValue = (int)numericUpDownAlpha.Value;
            Properties.Settings.Default.Save();


            Debug.WriteLine("change color alpha done with : color -> " + newColor);
            refresh();
        }
        private void transparentLabelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Function to delete a foldit group (delete sav file, ico, radiobutton in form, and try to delete existing shortcut on desktop)
        /// </summary>
        /// <param name="i">ID of the foldit group</param>
        private void delete(int i)
        {
            System.IO.File.Delete(Environment.CurrentDirectory + String.Format("/Save/{0}.config", i));
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + String.Format("\\Foldit Group {0}.lnk", i)))
            {
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + String.Format("\\Foldit Group {0}.lnk", i));
            }
            Properties.Settings.Default.nbrGroup -= 1;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Set the ForeColor (Font Color) of all the labels of the Form
        /// </summary>
        /// <param name="newColor">color to be applied to Font</param>
        private void setAllForeColor(Color newColor)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() == typeof(TransparentLabel) || ctrl.GetType() == typeof(RadioButton))
                {
                    ctrl.ForeColor = newColor;
                }
            }
            Properties.Settings.Default.ForeColor = newColor;
        }

        /// <summary>
        /// Evaluate brightness of the window backcolor to pick a visible font color 
        /// </summary>
        private void BrightnessEvaluation()
        {
            float brightness = Properties.Settings.Default.Color.GetBrightness();
            if (brightness > 0.70)
            {
                setAllForeColor(SystemColors.ControlText);
            }
            else
            {
                setAllForeColor(Color.White);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


}
