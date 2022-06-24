using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace foldit
{
    partial class FolditConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
        }

        public void refresh()
        {
            Debug.WriteLine(Properties.Settings.Default.AlphaValue);
            Debug.WriteLine("refreshing: color " + Properties.Settings.Default.Color);
            Debug.WriteLine("refreshing: Accent " + (AppUtils.ACCENT)Properties.Settings.Default.Accent);
            
            AppUtils.EnableAcrylic(this, Properties.Settings.Default.Color, (AppUtils.ACCENT)Properties.Settings.Default.Accent);
            radioButtonContainer.Controls.Clear();
            for (int i = 0; i < Properties.Settings.Default.nbrGroup; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Text = String.Format("Groupe {0}", i);
                rb.Tag = i;
                rb.ForeColor = Color.White;
                radioButtonContainer.Controls.Add(rb);
            }

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCreateNewFold = new System.Windows.Forms.Button();
            this.transparentLabelCreateNew = new foldit.TransparentLabel();
            this.transparentLabelTitle = new foldit.TransparentLabel();
            this.buttonSwitchBlur = new System.Windows.Forms.Button();
            this.transparentLabelSwitch = new foldit.TransparentLabel();
            this.transparentLabelExit = new foldit.TransparentLabel();
            this.radioButtonContainer = new foldit.TransparentLayout();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSetColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.transparentLabelSetAccent = new foldit.TransparentLabel();
            this.numericUpDownAlpha = new System.Windows.Forms.NumericUpDown();
            this.transparentLabelAlpha = new foldit.TransparentLabel();
            this.transparentLabelAbout = new foldit.TransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateNewFold
            // 
            this.buttonCreateNewFold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCreateNewFold.Location = new System.Drawing.Point(34, 66);
            this.buttonCreateNewFold.Name = "buttonCreateNewFold";
            this.buttonCreateNewFold.Size = new System.Drawing.Size(125, 23);
            this.buttonCreateNewFold.TabIndex = 0;
            this.buttonCreateNewFold.Text = "Create new Foldit";
            this.buttonCreateNewFold.UseVisualStyleBackColor = true;
            this.buttonCreateNewFold.Click += new System.EventHandler(this.buttonCreateNewFold_Click);
            // 
            // transparentLabelCreateNew
            // 
            this.transparentLabelCreateNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.transparentLabelCreateNew.Location = new System.Drawing.Point(215, 66);
            this.transparentLabelCreateNew.Name = "transparentLabelCreateNew";
            this.transparentLabelCreateNew.Size = new System.Drawing.Size(214, 51);
            this.transparentLabelCreateNew.TabIndex = 1;
            this.transparentLabelCreateNew.TabStop = false;
            this.transparentLabelCreateNew.Text = "Create a new shortcut on your desktop";
            // 
            // transparentLabelTitle
            // 
            this.transparentLabelTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.transparentLabelTitle.ForeColor = System.Drawing.Color.Black;
            this.transparentLabelTitle.Location = new System.Drawing.Point(240, 12);
            this.transparentLabelTitle.Name = "transparentLabelTitle";
            this.transparentLabelTitle.Size = new System.Drawing.Size(122, 37);
            this.transparentLabelTitle.TabIndex = 2;
            this.transparentLabelTitle.TabStop = false;
            this.transparentLabelTitle.Text = "Foldit Config";
            // 
            // buttonSwitchBlur
            // 
            this.buttonSwitchBlur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSwitchBlur.Location = new System.Drawing.Point(34, 123);
            this.buttonSwitchBlur.Name = "buttonSwitchBlur";
            this.buttonSwitchBlur.Size = new System.Drawing.Size(125, 23);
            this.buttonSwitchBlur.TabIndex = 3;
            this.buttonSwitchBlur.Text = "Switch Blur";
            this.buttonSwitchBlur.UseVisualStyleBackColor = true;
            this.buttonSwitchBlur.Click += new System.EventHandler(this.buttonSwitchBlur_Click);
            // 
            // transparentLabelSwitch
            // 
            this.transparentLabelSwitch.ForeColor = System.Drawing.Color.Black;
            this.transparentLabelSwitch.Location = new System.Drawing.Point(215, 123);
            this.transparentLabelSwitch.Name = "transparentLabelSwitch";
            this.transparentLabelSwitch.Size = new System.Drawing.Size(214, 33);
            this.transparentLabelSwitch.TabIndex = 4;
            this.transparentLabelSwitch.TabStop = false;
            this.transparentLabelSwitch.Text = "Switch between differents types of blur";
            // 
            // transparentLabelExit
            // 
            this.transparentLabelExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transparentLabelExit.ForeColor = System.Drawing.Color.White;
            this.transparentLabelExit.Location = new System.Drawing.Point(577, 0);
            this.transparentLabelExit.Name = "transparentLabelExit";
            this.transparentLabelExit.Size = new System.Drawing.Size(16, 23);
            this.transparentLabelExit.TabIndex = 5;
            this.transparentLabelExit.TabStop = false;
            this.transparentLabelExit.Text = "x";
            this.transparentLabelExit.Click += new System.EventHandler(this.transparentLabelExit_Click);
            // 
            // radioButtonContainer
            // 
            this.radioButtonContainer.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonContainer.Location = new System.Drawing.Point(215, 300);
            this.radioButtonContainer.Name = "radioButtonContainer";
            this.radioButtonContainer.Size = new System.Drawing.Size(183, 145);
            this.radioButtonContainer.TabIndex = 6;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(34, 300);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(125, 23);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_click);
            // 
            // buttonSetColor
            // 
            this.buttonSetColor.Location = new System.Drawing.Point(34, 161);
            this.buttonSetColor.Name = "buttonSetColor";
            this.buttonSetColor.Size = new System.Drawing.Size(125, 23);
            this.buttonSetColor.TabIndex = 8;
            this.buttonSetColor.Text = "Set Color";
            this.buttonSetColor.UseVisualStyleBackColor = true;
            this.buttonSetColor.Click += new System.EventHandler(this.buttonSetColor_Click);
            // 
            // transparentLabelSetAccent
            // 
            this.transparentLabelSetAccent.ForeColor = System.Drawing.Color.Black;
            this.transparentLabelSetAccent.Location = new System.Drawing.Point(215, 162);
            this.transparentLabelSetAccent.Name = "transparentLabelSetAccent";
            this.transparentLabelSetAccent.Size = new System.Drawing.Size(214, 33);
            this.transparentLabelSetAccent.TabIndex = 9;
            this.transparentLabelSetAccent.TabStop = false;
            this.transparentLabelSetAccent.Text = "Set Accentuation Color";
            // 
            // numericUpDownAlpha
            // 
            this.numericUpDownAlpha.Location = new System.Drawing.Point(450, 162);
            this.numericUpDownAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownAlpha.Name = "numericUpDownAlpha";
            this.numericUpDownAlpha.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownAlpha.TabIndex = 10;
            this.numericUpDownAlpha.Visible = false;
            this.numericUpDownAlpha.Value = Properties.Settings.Default.AlphaValue;
            this.numericUpDownAlpha.ValueChanged += new System.EventHandler(this.numericUpDownAlpha_ValueChanged);
            // 
            // transparentLabelAlpha
            // 
            this.transparentLabelAlpha.ForeColor = System.Drawing.Color.Black;
            this.transparentLabelAlpha.Location = new System.Drawing.Point(475, 133);
            this.transparentLabelAlpha.Name = "transparentLabelAlpha";
            this.transparentLabelAlpha.Size = new System.Drawing.Size(75, 23);
            this.transparentLabelAlpha.TabIndex = 11;
            this.transparentLabelAlpha.TabStop = false;
            this.transparentLabelAlpha.Text = "Alpha";
            this.transparentLabelAlpha.Visible = false;
            // 
            // transparentLabelAbout
            // 
            this.transparentLabelAbout.ForeColor = System.Drawing.Color.Black;
            this.transparentLabelAbout.Location = new System.Drawing.Point(533, 444);
            this.transparentLabelAbout.Name = "transparentLabelAbout";
            this.transparentLabelAbout.Size = new System.Drawing.Size(47, 23);
            this.transparentLabelAbout.TabIndex = 12;
            this.transparentLabelAbout.TabStop = false;
            this.transparentLabelAbout.Text = "About";
            this.transparentLabelAbout.Click += new System.EventHandler(this.transparentLabelAbout_Click);
            // 
            // FolditConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 479);
            this.Controls.Add(this.transparentLabelAbout);
            this.Controls.Add(this.transparentLabelAlpha);
            this.Controls.Add(this.numericUpDownAlpha);
            this.Controls.Add(this.transparentLabelSetAccent);
            this.Controls.Add(this.buttonSetColor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.radioButtonContainer);
            this.Controls.Add(this.transparentLabelExit);
            this.Controls.Add(this.transparentLabelSwitch);
            this.Controls.Add(this.buttonSwitchBlur);
            this.Controls.Add(this.transparentLabelTitle);
            this.Controls.Add(this.transparentLabelCreateNew);
            this.Controls.Add(this.buttonCreateNewFold);
            this.Name = "FolditConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FolditConfig";
            this.Shown += new System.EventHandler(this.FolditConfig_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonCreateNewFold;
        private TransparentLabel transparentLabelCreateNew;
        private TransparentLabel transparentLabelTitle;
        private Button buttonSwitchBlur;
        private TransparentLabel transparentLabelSwitch;
        private TransparentLabel transparentLabelExit;
        private TransparentLayout radioButtonContainer;
        private Button buttonDelete;
        private Button buttonSetColor;
        private ColorDialog colorDialog1;
        private TransparentLabel transparentLabelSetAccent;
        private NumericUpDown numericUpDownAlpha;
        private TransparentLabel transparentLabelAlpha;
        private TransparentLabel transparentLabelAbout;
    }


}