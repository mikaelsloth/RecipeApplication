namespace Recipe
{
    partial class TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.modernDatePicker1 = new Recipe.Views.ModernDatePicker();
            this.modernToggleButton1 = new Recipe.Views.ModernToggleButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.recipeLineView1 = new Recipe.Views.RecipeLineView();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(448, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(448, 81);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // modernDatePicker1
            // 
            this.modernDatePicker1.BackColor = System.Drawing.SystemColors.Highlight;
            this.modernDatePicker1.BorderRadius = 8;
            this.modernDatePicker1.BorderSize = 2;
            this.modernDatePicker1.CalendarForeColor = System.Drawing.SystemColors.Window;
            this.modernDatePicker1.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.modernDatePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.Window;
            this.modernDatePicker1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modernDatePicker1.ForeColor = System.Drawing.SystemColors.Window;
            this.modernDatePicker1.Location = new System.Drawing.Point(154, 99);
            this.modernDatePicker1.MinimumSize = new System.Drawing.Size(4, 35);
            this.modernDatePicker1.Name = "modernDatePicker1";
            this.modernDatePicker1.Size = new System.Drawing.Size(200, 35);
            this.modernDatePicker1.TabIndex = 15;
            // 
            // modernToggleButton1
            // 
            this.modernToggleButton1.AutoSize = true;
            this.modernToggleButton1.BackColorOnPosition = System.Drawing.SystemColors.Highlight;
            this.modernToggleButton1.Checked = true;
            this.modernToggleButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modernToggleButton1.Location = new System.Drawing.Point(76, 110);
            this.modernToggleButton1.MinimumSize = new System.Drawing.Size(45, 22);
            this.modernToggleButton1.Name = "modernToggleButton1";
            this.modernToggleButton1.Size = new System.Drawing.Size(45, 22);
            this.modernToggleButton1.TabIndex = 17;
            this.modernToggleButton1.ToggleColorOffPosition = System.Drawing.SystemColors.Window;
            this.modernToggleButton1.ToggleColorOnPosition = System.Drawing.SystemColors.Window;
            this.modernToggleButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Test",
            "Mikael",
            "Laila"});
            this.comboBox1.Location = new System.Drawing.Point(656, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 18;
            // 
            // recipeLineView1
            // 
            this.recipeLineView1.ButtonBackColor = System.Drawing.SystemColors.Highlight;
            this.recipeLineView1.DataMember = "";
            this.recipeLineView1.DataSource = null;
            this.recipeLineView1.GridColor = System.Drawing.Color.Red;
            this.recipeLineView1.Index = 0;
            this.recipeLineView1.Location = new System.Drawing.Point(37, 182);
            this.recipeLineView1.Margin = new System.Windows.Forms.Padding(0);
            this.recipeLineView1.Name = "recipeLineView1";
            this.recipeLineView1.Padding = new System.Windows.Forms.Padding(1);
            this.recipeLineView1.ShowDeleteButton = true;
            this.recipeLineView1.ShowEditButton = true;
            this.recipeLineView1.Size = new System.Drawing.Size(680, 28);
            this.recipeLineView1.TabIndex = 19;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 572);
            this.Controls.Add(this.recipeLineView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.modernToggleButton1);
            this.Controls.Add(this.modernDatePicker1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private Views.ModernDatePicker modernDatePicker1;
        private Views.ModernToggleButton modernToggleButton1;
        private System.Windows.Forms.ComboBox comboBox1;
        private Views.RecipeLineView recipeLineView1;
    }
}