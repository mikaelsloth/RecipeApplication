
namespace Recipe.Views
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CustomersButton = new System.Windows.Forms.CheckBox();
            this.RecipesButton = new System.Windows.Forms.CheckBox();
            this.MedicinsButton = new System.Windows.Forms.CheckBox();
            this.SettingsButton = new Recipe.Views.SettingsButton();
            this.AboutLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.35746F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.64254F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1440, 920);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.CustomersButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.RecipesButton, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.MedicinsButton, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.SettingsButton, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.AboutLabel, 1, 8);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.80645F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.80645F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.80645F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.58064F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(229, 914);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // CustomersButton
            // 
            this.CustomersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomersButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.CustomersButton.AutoSize = true;
            this.CustomersButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CustomersButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.CustomersButton.FlatAppearance.BorderSize = 3;
            this.CustomersButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.CustomersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CustomersButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CustomersButton.Location = new System.Drawing.Point(50, 50);
            this.CustomersButton.Margin = new System.Windows.Forms.Padding(20);
            this.CustomersButton.Name = "CustomersButton";
            this.CustomersButton.Size = new System.Drawing.Size(129, 152);
            this.CustomersButton.TabIndex = 3;
            this.CustomersButton.Text = "Kunder";
            this.CustomersButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CustomersButton.UseVisualStyleBackColor = false;
            this.CustomersButton.Click += new System.EventHandler(this.CustomersButton_Clicked);
            // 
            // RecipesButton
            // 
            this.RecipesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecipesButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.RecipesButton.AutoSize = true;
            this.RecipesButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RecipesButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.RecipesButton.FlatAppearance.BorderSize = 3;
            this.RecipesButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.RecipesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecipesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RecipesButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RecipesButton.Location = new System.Drawing.Point(50, 262);
            this.RecipesButton.Margin = new System.Windows.Forms.Padding(20);
            this.RecipesButton.Name = "RecipesButton";
            this.RecipesButton.Size = new System.Drawing.Size(129, 152);
            this.RecipesButton.TabIndex = 4;
            this.RecipesButton.Text = "MedicinKort";
            this.RecipesButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RecipesButton.UseVisualStyleBackColor = false;
            this.RecipesButton.Click += new System.EventHandler(this.RecipesButton_Clicked);
            // 
            // MedicinsButton
            // 
            this.MedicinsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MedicinsButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.MedicinsButton.AutoSize = true;
            this.MedicinsButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MedicinsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.MedicinsButton.FlatAppearance.BorderSize = 3;
            this.MedicinsButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.MedicinsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MedicinsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MedicinsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MedicinsButton.Location = new System.Drawing.Point(50, 474);
            this.MedicinsButton.Margin = new System.Windows.Forms.Padding(20);
            this.MedicinsButton.Name = "MedicinsButton";
            this.MedicinsButton.Size = new System.Drawing.Size(129, 152);
            this.MedicinsButton.TabIndex = 5;
            this.MedicinsButton.Text = "Medicin";
            this.MedicinsButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MedicinsButton.UseVisualStyleBackColor = false;
            this.MedicinsButton.Click += new System.EventHandler(this.MedicinsButton_Clicked);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.FlatAppearance.BorderSize = 0;
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SettingsButton.Image = global::Recipe.Properties.Resources.Settings_64x;
            this.SettingsButton.Location = new System.Drawing.Point(55, 731);
            this.SettingsButton.Margin = new System.Windows.Forms.Padding(25);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(119, 117);
            this.SettingsButton.TabIndex = 6;
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // AboutLabel
            // 
            this.AboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutLabel.AutoSize = true;
            this.AboutLabel.Location = new System.Drawing.Point(33, 873);
            this.AboutLabel.Name = "AboutLabel";
            this.AboutLabel.Size = new System.Drawing.Size(163, 31);
            this.AboutLabel.TabIndex = 7;
            this.AboutLabel.Text = "© msloth.dk 2021 \r\nabout the software";
            this.AboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AboutLabel.Click += new System.EventHandler(this.AboutLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 926);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1462, 965);
            this.Name = "MainForm";
            this.Text = "Cecilies Naturklinik";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox CustomersButton;
        private System.Windows.Forms.CheckBox RecipesButton;
        private System.Windows.Forms.CheckBox MedicinsButton;
        private SettingsButton SettingsButton;
        private System.Windows.Forms.Label AboutLabel;
    }
}

