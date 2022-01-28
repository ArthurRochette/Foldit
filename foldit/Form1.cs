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

        enum HChangeNotifyEventID
        {
            /// <summary>
            /// All events have occurred.
            /// </summary>
            SHCNE_ALLEVENTS = 0x7FFFFFFF,

            /// <summary>
            /// A file type association has changed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
            /// must be specified in the <i>uFlags</i> parameter.
            /// <i>dwItem1</i> and <i>dwItem2</i> are not used and must be <see langword="null"/>.
            /// </summary>
            SHCNE_ASSOCCHANGED = 0x08000000,

            /// <summary>
            /// The attributes of an item or folder have changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item or folder that has changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_ATTRIBUTES = 0x00000800,

            /// <summary>
            /// A nonfolder item has been created.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item that was created.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_CREATE = 0x00000002,

            /// <summary>
            /// A nonfolder item has been deleted.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item that was deleted.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DELETE = 0x00000004,

            /// <summary>
            /// A drive has been added.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was added.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEADD = 0x00000100,

            /// <summary>
            /// A drive has been added and the Shell should create a new window for the drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was added.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEADDGUI = 0x00010000,

            /// <summary>
            /// A drive has been removed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEREMOVED = 0x00000080,

            /// <summary>
            /// Not currently used.
            /// </summary>
            SHCNE_EXTENDED_EVENT = 0x04000000,

            /// <summary>
            /// The amount of free space on a drive has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive on which the free space changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_FREESPACE = 0x00040000,

            /// <summary>
            /// Storage media has been inserted into a drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that contains the new media.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MEDIAINSERTED = 0x00000020,

            /// <summary>
            /// Storage media has been removed from a drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive from which the media was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MEDIAREMOVED = 0x00000040,

            /// <summary>
            /// A folder has been created. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
            /// or <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that was created.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MKDIR = 0x00000008,

            /// <summary>
            /// A folder on the local computer is being shared via the network.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that is being shared.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_NETSHARE = 0x00000200,

            /// <summary>
            /// A folder on the local computer is no longer being shared via the network.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that is no longer being shared.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_NETUNSHARE = 0x00000400,

            /// <summary>
            /// The name of a folder has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the previous pointer to an item identifier list (PIDL) or name of the folder.
            /// <i>dwItem2</i> contains the new PIDL or name of the folder.
            /// </summary>
            SHCNE_RENAMEFOLDER = 0x00020000,

            /// <summary>
            /// The name of a nonfolder item has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the previous PIDL or name of the item.
            /// <i>dwItem2</i> contains the new PIDL or name of the item.
            /// </summary>
            SHCNE_RENAMEITEM = 0x00000001,

            /// <summary>
            /// A folder has been removed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_RMDIR = 0x00000010,

            /// <summary>
            /// The computer has disconnected from a server.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the server from which the computer was disconnected.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_SERVERDISCONNECT = 0x00004000,

            /// <summary>
            /// The contents of an existing folder have changed,
            /// but the folder still exists and has not been renamed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that has changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// If a folder has been created, deleted, or renamed, use SHCNE_MKDIR, SHCNE_RMDIR, or
            /// SHCNE_RENAMEFOLDER, respectively, instead.
            /// </summary>
            SHCNE_UPDATEDIR = 0x00001000,

            /// <summary>
            /// An image in the system image list has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_DWORD"/> must be specified in <i>uFlags</i>.
            /// </summary>
            SHCNE_UPDATEIMAGE = 0x00008000,

        }
        public enum HChangeNotifyFlags
        {
            /// <summary>
            /// The <i>dwItem1</i> and <i>dwItem2</i> parameters are DWORD values.
            /// </summary>
            SHCNF_DWORD = 0x0003,
            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of ITEMIDLIST structures that
            /// represent the item(s) affected by the change.
            /// Each ITEMIDLIST must be relative to the desktop folder.
            /// </summary>
            SHCNF_IDLIST = 0x0000,
            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
            /// maximum length MAX_PATH that contain the full path names
            /// of the items affected by the change.
            /// </summary>
            SHCNF_PATHA = 0x0001,
            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
            /// maximum length MAX_PATH that contain the full path names
            /// of the items affected by the change.
            /// </summary>
            SHCNF_PATHW = 0x0005,
            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
            /// represent the friendly names of the printer(s) affected by the change.
            /// </summary>
            SHCNF_PRINTERA = 0x0002,
            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
            /// represent the friendly names of the printer(s) affected by the change.
            /// </summary>
            SHCNF_PRINTERW = 0x0006,
            /// <summary>
            /// The function should not return until the notification
            /// has been delivered to all affected components.
            /// As this flag modifies other data-type flags, it cannot by used by itself.
            /// </summary>
            SHCNF_FLUSH = 0x1000,
            /// <summary>
            /// The function should begin delivering notifications to all affected components
            /// but should return as soon as the notification process has begun.
            /// As this flag modifies other data-type flags, it cannot by used by itself.
            /// </summary>
            SHCNF_FLUSHNOWAIT = 0x2000
        }


        [DllImport("shell32.dll")]
        static extern void SHChangeNotify(HChangeNotifyEventID wEventId, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);

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
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.id = id;
            readSave();

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Contains(".lnk"))
                {
                    MessageBox.Show("Please don't drag in shortcuts. Only file are accepted.");
                    return;
                }
                else
                {
                    addElement(file);
                }
                

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
                applyNewIco(createIco());
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
            this.transparentLayout1.Location = new System.Drawing.Point(12, 20);
            this.transparentLayout1.Name = "transparentLayout1";
            this.transparentLayout1.Size = new System.Drawing.Size(236, 240);
            this.transparentLayout1.TabIndex = 1;
            // 
            // transparentLabel4
            // 
            this.transparentLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transparentLabel4.ForeColor = System.Drawing.Color.White;
            this.transparentLabel4.Location = new System.Drawing.Point(249, -1);
            this.transparentLabel4.Name = "transparentLabel4";
            this.transparentLabel4.Size = new System.Drawing.Size(16, 15);
            this.transparentLabel4.TabIndex = 6;
            this.transparentLabel4.TabStop = false;
            this.transparentLabel4.Text = "x";
            this.transparentLabel4.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 261);
            this.Controls.Add(this.transparentLabel4);
            this.Controls.Add(this.transparentLayout1);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save(id);
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
            if (!File.Exists(String.Format("./Save/{0}.config", this.id)))
            {
                MessageBox.Show("Save file of this foldit has not been found. You may already deleted it or the save file has been corrupted. ", "No save found");
                Environment.Exit(1);
            }
            else
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
            Debug.WriteLine(Environment.CurrentDirectory + String.Format("/Icons/{0}.ico", id) + " here");
            Icon ico = Icon.FromHandle(bm.GetHicon());//quality is awful
            FileStream fs = new FileStream(Environment.CurrentDirectory + String.Format("/Icons/{0}.ico", id), FileMode.Create);
            ico.Save(fs);

            fs.Close();

            SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, (IntPtr)null , (IntPtr)null);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            AppUtils.EnableAcrylic(this, Properties.Settings.Default.Color, (AppUtils.ACCENT)Properties.Settings.Default.Accent);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }

}
