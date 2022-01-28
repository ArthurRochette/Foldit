namespace foldit
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.transparentLabel1 = new foldit.TransparentLabel();
            this.transparentLabel2 = new foldit.TransparentLabel();
            this.transparentLabel3 = new foldit.TransparentLabel();
            this.SuspendLayout();
            // 
            // transparentLabel1
            // 
            this.transparentLabel1.Location = new System.Drawing.Point(12, 12);
            this.transparentLabel1.Name = "transparentLabel1";
            this.transparentLabel1.Size = new System.Drawing.Size(284, 22);
            this.transparentLabel1.TabIndex = 0;
            this.transparentLabel1.TabStop = false;
            this.transparentLabel1.Text = "This software has been coded by Rochette Arthur.";
            // 
            // transparentLabel2
            // 
            this.transparentLabel2.Location = new System.Drawing.Point(12, 40);
            this.transparentLabel2.Name = "transparentLabel2";
            this.transparentLabel2.Size = new System.Drawing.Size(398, 22);
            this.transparentLabel2.TabIndex = 1;
            this.transparentLabel2.TabStop = false;
            this.transparentLabel2.Text = "Source files can be found here : https://github.com/ArthurSenpaii/Foldit";
            // 
            // transparentLabel3
            // 
            this.transparentLabel3.Location = new System.Drawing.Point(12, 84);
            this.transparentLabel3.Name = "transparentLabel3";
            this.transparentLabel3.Size = new System.Drawing.Size(577, 234);
            this.transparentLabel3.TabIndex = 2;
            this.transparentLabel3.TabStop = false;
            this.transparentLabel3.Text = resources.GetString("transparentLabel3.Text");
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 306);
            this.Controls.Add(this.transparentLabel3);
            this.Controls.Add(this.transparentLabel2);
            this.Controls.Add(this.transparentLabel1);
            this.Name = "Form3";
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private TransparentLabel transparentLabel1;
        private TransparentLabel transparentLabel2;
        private TransparentLabel transparentLabel3;
    }
}