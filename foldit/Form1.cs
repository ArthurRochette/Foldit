using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace foldit
{
    public partial class Form1 : Form
    {
        protected int nbrIcon;
        protected Dictionary<int, string> pblink;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public int id;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// Default constructor for Group Form
        /// </summary>
        /// <param name="id"></param>

        public Form1(int id)
        {
            InitializeComponent();
            pblink = new Dictionary<int, string>();
            nbrIcon = 0;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Properties.Settings.Default.Location;
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            this.id = id;
            readSave();

        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                addElement(file);
            }
            if (this.transparentLayout1.Controls.Count > 0)
            {
                Bitmap newbtm = this.createIco();
                newbtm.Save("icon.nmp");
                this.applyNewIco(newbtm);
            }
        }

        private void pbDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            Debug.WriteLine(pb.Tag);
            pblink.TryGetValue((int)pb.Tag, out string file);
            if (file == null)
            {
                throw new Exception("icon pb as no linked file in memory . . .");
            }
            FileAttributes attr = File.GetAttributes(file);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, " + file);
                exit();
            }
            else
            {
                try
                {
                    new Process
                    {
                        StartInfo = new ProcessStartInfo(file)
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                    exit();

                }
                catch (Exception error)
                {
                    Debug.WriteLine(error);
                }

            }
        }
        private void pbClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PictureBox pb = sender as PictureBox;
                pb.Dispose();
            }
        }

        private void InitializeComponent()
        {
            this.transparentLayout1 = new foldit.TransparentLayout();
            this.transparentLabel4 = new foldit.TransparentLabel();
            this.SuspendLayout();
            // 
            // transparentLayout1
            // 
            this.transparentLayout1.BackColor = System.Drawing.Color.Transparent;
            this.transparentLayout1.Location = new System.Drawing.Point(1, 20);
            this.transparentLayout1.Name = "transparentLayout1";
            this.transparentLayout1.Size = new System.Drawing.Size(283, 240);
            this.transparentLayout1.TabIndex = 1;
            // 
            // transparentLabel4
            // 
            this.transparentLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transparentLabel4.ForeColor = System.Drawing.Color.White;
            this.transparentLabel4.Location = new System.Drawing.Point(268, -1);
            this.transparentLabel4.Name = "transparentLabel4";
            this.transparentLabel4.Size = new System.Drawing.Size(16, 15);
            this.transparentLabel4.TabIndex = 6;
            this.transparentLabel4.TabStop = false;
            this.transparentLabel4.Text = "x";
            this.transparentLabel4.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.transparentLabel4);
            this.Controls.Add(this.transparentLayout1);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save(id);
        }

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.exit();
        }

        private void exit()
        {
            System.Windows.Forms.Application.Exit();
        }

        void readSave()
        {
            File.Open(String.Format("./Save/{0}.config", this.id), FileMode.OpenOrCreate).Close();

            string config = File.ReadAllText(String.Format("./Save/{0}.config", this.id));
            StringReader reader = new StringReader(config);
            while (config.Length > 0)
            {

                string param = reader.ReadLine();
                Debug.WriteLine(param);
                if (param == null) break;
                if (param.StartsWith("element"))
                {
                    param = param.Substring("element ".Length);

                    addElement(param);
                }
                else if (param.StartsWith("location"))
                {
                    param = param.Substring("location ".Length);
                    Debug.WriteLine(param);
                    int x = int.Parse(param.Substring(0, param.IndexOf("/")));
                    int buff = param.IndexOf("/") + 1;
                    int bubu = param.IndexOf(";");
                    int nanma = param.IndexOf("/") + 1;
                    int buffy = bubu - nanma;
                    int y = int.Parse(param.Substring(buff, buffy));
                    Location = new Point(x, y);
                    Debug.WriteLine("window position :" + Location.ToString());
                }

            }
            Debug.WriteLine("readsave fini");
        }

        void Save(int id)
        {
            Debug.WriteLine("Saving...");
            string content = "";
            Debug.WriteLine(transparentLayout1.Controls.Count);
            foreach (Control ctrl in transparentLayout1.Controls)
            {
                content += "element " + pblink[int.Parse(ctrl.Tag.ToString())] + Environment.NewLine;
            }
            content += string.Format("location {0}/{1}", Location.X, Location.Y) + ";";
            File.WriteAllText(String.Format("./Save/{0}.config", id), content);

        }

        void addElement(string path)
        {
            Debug.WriteLine("Meh :" + path);
            PictureBox pb = new PictureBox();
            pb.Tag = nbrIcon;
            pb.Width = 40;
            pb.Height = 40;
            Icon ico = null;
            IntPtr large = IntPtr.Zero, small = IntPtr.Zero;
            try
            {
                ico = Icon.ExtractAssociatedIcon(path);
            }
            catch (Exception error)//better
            {
                ExtractIconEx(@"%SystemRoot%\system32\shell32.dll", 4, out large, out small, 1);//un appelle a chaque fois est un peu abuse fixme large ne retourn rien
                ico = Icon.FromHandle(large);
            }
            pb.Image = ico.ToBitmap();
            pb.BackColor = Color.Transparent;
            pb.MouseDoubleClick += new MouseEventHandler(pbDoubleClick);
            pb.MouseClick += new MouseEventHandler(pbClick);
            transparentLayout1.Controls.Add(pb);

            pblink.Add(nbrIcon, path);
            nbrIcon++;
        }
        public Bitmap createIco()
        {
            List<Image> list = new List<Image>();
            Bitmap final;
            for (int i = 0; i < (transparentLayout1.Controls.Count >= 4 ? 4 : transparentLayout1.Controls.Count); i++)
            {
                list.Add((transparentLayout1.Controls[i] as PictureBox).Image);
            }
            switch (list.Count)
            {
                case 1:
                    final = new Bitmap(list[0].Width, list[0].Height);
                    using (Graphics g = Graphics.FromImage(final))
                    {
                        g.DrawImage(list[0], 0, 0);
                    }
                    break;
                case 2:
                    final = new Bitmap(list[0].Width + list[1].Width, list[0].Height + list[1].Height);
                    using (Graphics g = Graphics.FromImage(final))
                    {
                        g.DrawImage(list[0], 0, 0);
                        g.DrawImage(list[1], list[0].Width, list[0].Height);
                    }
                    break;
                case 3:
                    final = new Bitmap(list[0].Width + list[1].Width, list[0].Height + list[2].Height);
                    using (Graphics g = Graphics.FromImage(final))
                    {
                        g.DrawImage(list[0], 0, 0);
                        g.DrawImage(list[1], list[0].Width, 0);
                        g.DrawImage(list[2], 0, list[0].Height);
                    }
                    break;
                case 4:
                    final = new Bitmap(list[0].Width + list[1].Width, list[0].Height + list[3].Height);
                    using (Graphics g = Graphics.FromImage(final))
                    {
                        g.DrawImage(list[0], 0, 0);
                        g.DrawImage(list[1], list[0].Width, 0);
                        g.DrawImage(list[2], 0, list[0].Height);
                        g.DrawImage(list[3], list[0].Width, list[0].Height);
                    }
                    break;
                default:
                    final = new Bitmap("./Icons/default.png");
                    break;

            }
            return final;
        }

        public void applyNewIco(Bitmap bm)
        {
            Debug.WriteLine("change ico done");
            Debug.WriteLine(Environment.CurrentDirectory + String.Format("/Icons/{0}.ico", id) + "ici");
            Icon ico = Icon.FromHandle(bm.GetHicon());//quality is awful
            FileStream fs = new FileStream(Environment.CurrentDirectory + String.Format("/Icons/{0}.ico", id), FileMode.Create);
            ico.Save(fs);
            fs.Close();


        }
    }

}
