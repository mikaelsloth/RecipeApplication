
namespace Recipe.Views
{
    partial class RemarksButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button.Location = new System.Drawing.Point(0, 0);
            this.Button.Name = "button1";
            this.Button.Size = new System.Drawing.Size(81, 29);
            this.Button.TabIndex = 0;
            this.Button.UseVisualStyleBackColor = true;
            this.Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // RemarksButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Button);
            this.Name = "RemarksButton";
            this.Size = new System.Drawing.Size(81, 29);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.Button Button;
    }
}
