
namespace Recipe.Views
{
    partial class RecipeLineAddEditView
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
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.RecipeLineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RecipeLineBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanel.Location = new System.Drawing.Point(1, 1);
            this.LayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.Padding = new System.Windows.Forms.Padding(1);
            this.LayoutPanel.RowCount = 1;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutPanel.Size = new System.Drawing.Size(848, 57);
            this.LayoutPanel.TabIndex = 0;
            // 
            // RecipeLineAddEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RecipeLineAddEditView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(850, 59);
            ((System.ComponentModel.ISupportInitialize)(this.RecipeLineBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ModernTextBox DescTextBox;
        private ModernTextBox MorningTextBox;
        private ModernTextBox NoonTextBox;
        private ModernTextBox EveningTextBox;
        private ModernTextBox NightTextBox;

        private ModernButton AddButton;
        private ModernButton CancelButton;
        private ModernButton SaveButton;

        private ModernComboBox UnitComboBox;

        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        private System.Windows.Forms.BindingSource RecipeLineBindingSource;
    }
}
