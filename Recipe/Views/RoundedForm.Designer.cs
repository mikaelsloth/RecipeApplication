
namespace Recipe.Views
{
    partial class RoundedForm
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
            this.PanelContainer = new System.Windows.Forms.Panel();
            this.TextBox = new ModernTextBox();
            this.PanelTitleBar = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PanelContainer.SuspendLayout();
            this.PanelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelContainer
            // 
            this.PanelContainer.BackColor = System.Drawing.Color.White;
            this.PanelContainer.Controls.Add(this.TextBox);
            this.PanelContainer.Controls.Add(this.PanelTitleBar);
            this.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContainer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PanelContainer.Location = new System.Drawing.Point(0, 0);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Size = new System.Drawing.Size(330, 467);
            this.PanelContainer.TabIndex = 1;
            this.PanelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContainer_Paint);
            // 
            // modernTextBox1
            // 
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox.BorderRadius = 20;
            this.TextBox.BorderSize = 4;
            this.TextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TextBox.Location = new System.Drawing.Point(13, 36);
            this.TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "modernTextBox1";
            this.TextBox.Padding = new System.Windows.Forms.Padding(13, 10, 13, 10);
            this.TextBox.Size = new System.Drawing.Size(304, 418);
            this.TextBox.TabIndex = 2;
            // 
            // PanelTitleBar
            // 
            this.PanelTitleBar.Controls.Add(this.CloseButton);
            this.PanelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PanelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleBar.Name = "PanelTitleBar";
            this.PanelTitleBar.Size = new System.Drawing.Size(330, 29);
            this.PanelTitleBar.TabIndex = 1;
            this.PanelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Window;
            this.CloseButton.Image = global::Recipe.Properties.Resources.CloseSolution_11x;
            this.CloseButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.CloseButton.Location = new System.Drawing.Point(258, 6);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(55, 20);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Luk";
            this.CloseButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // RoundedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 467);
            this.Controls.Add(this.PanelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RoundedForm";
            this.Text = "RoundedForm";
            this.Activated += new System.EventHandler(this.RoundedForm_Activated);
            this.ResizeEnd += new System.EventHandler(this.RoundedForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.RoundedForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundedForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RoundedForm_MouseDown);
            this.PanelContainer.ResumeLayout(false);
            this.PanelTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelContainer;
        private System.Windows.Forms.Panel PanelTitleBar;
        private System.Windows.Forms.Button CloseButton;
        private ModernTextBox TextBox;
    }
}