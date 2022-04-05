
namespace Recipe.Views
{
    partial class CustomerFormHistoryPart
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
            this.HistoryLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HistoryCountComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HistoryGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Postcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Town = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LatestRecipeCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PagerLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.HistoryLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // HistoryLayoutPanel
            // 
            this.HistoryLayoutPanel.ColumnCount = 6;
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.HistoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.HistoryLayoutPanel.Controls.Add(this.HistoryCountComboBox, 4, 2);
            this.HistoryLayoutPanel.Controls.Add(this.label1, 3, 2);
            this.HistoryLayoutPanel.Controls.Add(this.HistoryGridView, 1, 0);
            this.HistoryLayoutPanel.Controls.Add(this.PagerLayoutPanel, 1, 2);
            this.HistoryLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.HistoryLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.HistoryLayoutPanel.Name = "HistoryLayoutPanel";
            this.HistoryLayoutPanel.RowCount = 4;
            this.HistoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.HistoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HistoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.HistoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HistoryLayoutPanel.Size = new System.Drawing.Size(1164, 258);
            this.HistoryLayoutPanel.TabIndex = 0;
            // 
            // HistoryCountComboBox
            // 
            this.HistoryCountComboBox.FormattingEnabled = true;
            this.HistoryCountComboBox.Location = new System.Drawing.Point(993, 198);
            this.HistoryCountComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.HistoryCountComboBox.Name = "HistoryCountComboBox";
            this.HistoryCountComboBox.Size = new System.Drawing.Size(139, 29);
            this.HistoryCountComboBox.TabIndex = 0;
            this.HistoryCountComboBox.SelectedValueChanged += new System.EventHandler(this.HistoryCountComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(954, 198);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vis";
            // 
            // HistoryGridView
            // 
            this.HistoryGridView.AllowUserToAddRows = false;
            this.HistoryGridView.AllowUserToDeleteRows = false;
            this.HistoryGridView.AllowUserToResizeRows = false;
            this.HistoryGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.HistoryGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HistoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistoryGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FullName,
            this.Address1,
            this.Postcode,
            this.Town,
            this.Phone,
            this.LatestRecipeCard});
            this.HistoryLayoutPanel.SetColumnSpan(this.HistoryGridView, 4);
            this.HistoryGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryGridView.Location = new System.Drawing.Point(31, 4);
            this.HistoryGridView.Margin = new System.Windows.Forms.Padding(4);
            this.HistoryGridView.MultiSelect = false;
            this.HistoryGridView.Name = "HistoryGridView";
            this.HistoryGridView.ReadOnly = true;
            this.HistoryGridView.RowTemplate.Height = 20;
            this.HistoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HistoryGridView.ShowEditingIcon = false;
            this.HistoryGridView.Size = new System.Drawing.Size(1101, 166);
            this.HistoryGridView.TabIndex = 2;
            this.HistoryGridView.SelectionChanged += new System.EventHandler(this.HistoryGridView_SelectionChanged);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 60;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "Navn";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 235;
            // 
            // Address1
            // 
            this.Address1.HeaderText = "Adresse 1";
            this.Address1.Name = "Address1";
            this.Address1.ReadOnly = true;
            this.Address1.Width = 190;
            // 
            // Postcode
            // 
            this.Postcode.HeaderText = "Post nummer";
            this.Postcode.Name = "Postcode";
            this.Postcode.ReadOnly = true;
            this.Postcode.Width = 130;
            // 
            // Town
            // 
            this.Town.HeaderText = "By";
            this.Town.Name = "Town";
            this.Town.ReadOnly = true;
            this.Town.Width = 150;
            // 
            // Phone
            // 
            this.Phone.HeaderText = "Telefon";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            this.Phone.Width = 145;
            // 
            // LatestRecipeCard
            // 
            this.LatestRecipeCard.HeaderText = "Seneste recept";
            this.LatestRecipeCard.Name = "LatestRecipeCard";
            this.LatestRecipeCard.ReadOnly = true;
            this.LatestRecipeCard.Width = 145;
            // 
            // PagerLayoutPanel
            // 
            this.PagerLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PagerLayoutPanel.ColumnCount = 19;
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.PagerLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.PagerLayoutPanel.Location = new System.Drawing.Point(31, 198);
            this.PagerLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.PagerLayoutPanel.Name = "PagerLayoutPanel";
            this.PagerLayoutPanel.RowCount = 1;
            this.PagerLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PagerLayoutPanel.Size = new System.Drawing.Size(678, 35);
            this.PagerLayoutPanel.TabIndex = 3;
            // 
            // CustomerFormHistoryPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.HistoryLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomerFormHistoryPart";
            this.Size = new System.Drawing.Size(1170, 264);
            this.HistoryLayoutPanel.ResumeLayout(false);
            this.HistoryLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel HistoryLayoutPanel;
        private System.Windows.Forms.ComboBox HistoryCountComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView HistoryGridView;
        private System.Windows.Forms.TableLayoutPanel PagerLayoutPanel;
        private System.Windows.Forms.BindingSource BindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Postcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Town;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn LatestRecipeCard;
    }
}
