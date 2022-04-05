
namespace Recipe.Views
{
    partial class RecipeCardDetailPartView
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
            this.components = new System.ComponentModel.Container();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.RemarksButton = new Recipe.Views.ModernRemarksButton();
            this.PrintButton = new Recipe.Views.ModernButton();
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AddPillsButton = new Recipe.Views.ModernButton();
            this.AddLiquidButton = new Recipe.Views.ModernButton();
            this.PillsLinesGrid = new Recipe.Views.RecipeLinesGrid();
            this.LiquidLinesGrid = new Recipe.Views.RecipeLinesGrid();
            this.RecipeCardBinding = new System.Windows.Forms.BindingSource(this.components);
            this.ButtonPanel.SuspendLayout();
            this.LayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeCardBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.RemarksButton);
            this.ButtonPanel.Controls.Add(this.PrintButton);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 776);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(1200, 43);
            this.ButtonPanel.TabIndex = 0;
            // 
            // RemarksButton
            // 
            this.RemarksButton.BackColor = System.Drawing.SystemColors.Control;
            this.RemarksButton.BorderColor = System.Drawing.Color.Red;
            this.RemarksButton.BorderRadius = 8;
            this.RemarksButton.BorderSize = 2;
            this.RemarksButton.CallOutPosition = Recipe.Views.CallOutFormPosition.TopRight;
            this.RemarksButton.Expanded = false;
            this.RemarksButton.FlatAppearance.BorderSize = 0;
            this.RemarksButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemarksButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemarksButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RemarksButton.Location = new System.Drawing.Point(203, 2);
            this.RemarksButton.Name = "RemarksButton";
            this.RemarksButton.Remarks = null;
            this.RemarksButton.Size = new System.Drawing.Size(150, 40);
            this.RemarksButton.TabIndex = 1;
            this.RemarksButton.Text = "Bemærkninger";
            this.RemarksButton.UseVisualStyleBackColor = false;
            this.RemarksButton.RemarksChanged += new System.EventHandler(this.RemarksButton_RemarksChanged);
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.SystemColors.Control;
            this.PrintButton.BorderColor = System.Drawing.Color.Red;
            this.PrintButton.BorderRadius = 8;
            this.PrintButton.BorderSize = 2;
            this.PrintButton.FlatAppearance.BorderSize = 0;
            this.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PrintButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PrintButton.Location = new System.Drawing.Point(1034, 2);
            this.PrintButton.Margin = new System.Windows.Forms.Padding(1);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(150, 40);
            this.PrintButton.TabIndex = 0;
            this.PrintButton.Text = "Gem og Print";
            this.PrintButton.UseVisualStyleBackColor = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 4;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LayoutPanel.Controls.Add(this.AddPillsButton, 1, 1);
            this.LayoutPanel.Controls.Add(this.AddLiquidButton, 3, 1);
            this.LayoutPanel.Controls.Add(this.PillsLinesGrid, 0, 1);
            this.LayoutPanel.Controls.Add(this.LiquidLinesGrid, 2, 1);
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 3;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Size = new System.Drawing.Size(1200, 776);
            this.LayoutPanel.TabIndex = 1;
            // 
            // AddPillsButton
            // 
            this.AddPillsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddPillsButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddPillsButton.BorderColor = System.Drawing.Color.Red;
            this.AddPillsButton.BorderRadius = 4;
            this.AddPillsButton.BorderSize = 2;
            this.AddPillsButton.FlatAppearance.BorderSize = 0;
            this.AddPillsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPillsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddPillsButton.Image = global::Recipe.Properties.Resources.Add_thin_16x16_Black;
            this.AddPillsButton.Location = new System.Drawing.Point(575, 75);
            this.AddPillsButton.Margin = new System.Windows.Forms.Padding(0);
            this.AddPillsButton.Name = "AddPillsButton";
            this.AddPillsButton.Size = new System.Drawing.Size(25, 25);
            this.AddPillsButton.TabIndex = 0;
            this.AddPillsButton.Text = "modernButton1";
            this.AddPillsButton.UseVisualStyleBackColor = false;
            this.AddPillsButton.Click += new System.EventHandler(this.AddPillsButton_Click);
            // 
            // AddLiquidButton
            // 
            this.AddLiquidButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddLiquidButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddLiquidButton.BorderColor = System.Drawing.Color.Red;
            this.AddLiquidButton.BorderRadius = 4;
            this.AddLiquidButton.BorderSize = 2;
            this.AddLiquidButton.FlatAppearance.BorderSize = 0;
            this.AddLiquidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddLiquidButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddLiquidButton.Image = global::Recipe.Properties.Resources.Add_thin_16x16_Black;
            this.AddLiquidButton.Location = new System.Drawing.Point(1175, 75);
            this.AddLiquidButton.Margin = new System.Windows.Forms.Padding(0);
            this.AddLiquidButton.Name = "AddLiquidButton";
            this.AddLiquidButton.Size = new System.Drawing.Size(25, 25);
            this.AddLiquidButton.TabIndex = 1;
            this.AddLiquidButton.Text = "modernButton1";
            this.AddLiquidButton.UseVisualStyleBackColor = false;
            this.AddLiquidButton.Click += new System.EventHandler(this.AddLiquidButton_Click);
            // 
            // PillsLinesGrid
            // 
            this.PillsLinesGrid.ForeColor = System.Drawing.Color.Red;
            this.PillsLinesGrid.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.PillsLinesGrid.HeaderText = "Vitaminer && Piller";
            this.PillsLinesGrid.Location = new System.Drawing.Point(3, 3);
            this.PillsLinesGrid.Name = "PillsLinesGrid";
            this.LayoutPanel.SetRowSpan(this.PillsLinesGrid, 2);
            this.PillsLinesGrid.ScrollActivated = false;
            this.PillsLinesGrid.Size = new System.Drawing.Size(569, 770);
            this.PillsLinesGrid.TabIndex = 2;
            this.PillsLinesGrid.Tag = "1";
            this.PillsLinesGrid.LineDeleteRequested += new System.EventHandler(this.PillsLinesGrid_LineDeleteRequested);
            this.PillsLinesGrid.LineEditRequested += new System.EventHandler(this.PillsLinesGrid_LineEditRequested);
            // 
            // LiquidLinesGrid
            // 
            this.LiquidLinesGrid.ForeColor = System.Drawing.Color.Red;
            this.LiquidLinesGrid.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.LiquidLinesGrid.HeaderText = "Dråber && Flasker";
            this.LiquidLinesGrid.Location = new System.Drawing.Point(603, 3);
            this.LiquidLinesGrid.Name = "LiquidLinesGrid";
            this.LayoutPanel.SetRowSpan(this.LiquidLinesGrid, 2);
            this.LiquidLinesGrid.ScrollActivated = false;
            this.LiquidLinesGrid.Size = new System.Drawing.Size(569, 770);
            this.LiquidLinesGrid.TabIndex = 3;
            this.LiquidLinesGrid.Tag = "2";
            this.LiquidLinesGrid.LineDeleteRequested += new System.EventHandler(this.LiquidLinesGrid_LineDeleteRequested);
            this.LiquidLinesGrid.LineEditRequested += new System.EventHandler(this.LiquidLinesGrid_LineEditRequested);
            // 
            // RecipeCardDetailPartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.LayoutPanel);
            this.Controls.Add(this.ButtonPanel);
            this.Name = "RecipeCardDetailPartView";
            this.Size = new System.Drawing.Size(1200, 819);
            this.ButtonPanel.ResumeLayout(false);
            this.LayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecipeCardBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ButtonPanel;
        private ModernButton PrintButton;
        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private ModernRemarksButton RemarksButton;
        private ModernButton AddPillsButton;
        private ModernButton AddLiquidButton;
        private RecipeLinesGrid PillsLinesGrid;
        private RecipeLinesGrid LiquidLinesGrid;
        private System.Windows.Forms.BindingSource RecipeCardBinding;
    }
}
