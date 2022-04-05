
namespace Recipe.Views
{
    partial class RecipeCardBrowsePartView
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
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ExpandButton = new Recipe.Views.ExpandButton();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.CustomerNameLabel = new Recipe.Views.ModernLabel();
            this.DateModernLabel = new Recipe.Views.ModernLabel();
            this.BrowseBindingNavigator = new Recipe.Views.ModernBindingNavigator(this.components);
            this.BrowseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.Red;
            this.HeaderPanel.Controls.Add(this.ExpandButton);
            this.HeaderPanel.Controls.Add(this.HeaderLabel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Padding = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.HeaderPanel.Size = new System.Drawing.Size(1200, 50);
            this.HeaderPanel.TabIndex = 1;
            // 
            // ExpandButton
            // 
            this.ExpandButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExpandButton.Expanded = false;
            this.ExpandButton.ForeColor = System.Drawing.Color.White;
            this.ExpandButton.IconDrawingWidth = 2;
            this.ExpandButton.Location = new System.Drawing.Point(1136, 4);
            this.ExpandButton.Margin = new System.Windows.Forms.Padding(4, 4, 13, 4);
            this.ExpandButton.Name = "ExpandButton";
            this.ExpandButton.Size = new System.Drawing.Size(39, 42);
            this.ExpandButton.TabIndex = 1;
            this.ExpandButton.Click += new System.EventHandler(this.ExpandButton_Click);
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.HeaderLabel.ForeColor = System.Drawing.Color.White;
            this.HeaderLabel.Location = new System.Drawing.Point(490, 11);
            this.HeaderLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(170, 21);
            this.HeaderLabel.TabIndex = 0;
            this.HeaderLabel.Text = "Seneste medicin kort";
            // 
            // CustomerNameLabel
            // 
            this.CustomerNameLabel.BorderColor = System.Drawing.Color.Red;
            this.CustomerNameLabel.BorderRadius = 8;
            this.CustomerNameLabel.BorderSize = 1;
            this.CustomerNameLabel.Location = new System.Drawing.Point(15, 60);
            this.CustomerNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CustomerNameLabel.Name = "CustomerNameLabel";
            this.CustomerNameLabel.Size = new System.Drawing.Size(250, 30);
            this.CustomerNameLabel.TabIndex = 4;
            this.CustomerNameLabel.Text = "Customer Name";
            this.CustomerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateModernLabel
            // 
            this.DateModernLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateModernLabel.BorderColor = System.Drawing.Color.Red;
            this.DateModernLabel.BorderRadius = 8;
            this.DateModernLabel.BorderSize = 1;
            this.DateModernLabel.Location = new System.Drawing.Point(935, 60);
            this.DateModernLabel.Name = "DateModernLabel";
            this.DateModernLabel.Size = new System.Drawing.Size(250, 30);
            this.DateModernLabel.TabIndex = 5;
            this.DateModernLabel.Text = "Date";
            this.DateModernLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrowseBindingNavigator
            // 
            this.BrowseBindingNavigator.AddNewItem = null;
            this.BrowseBindingNavigator.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BrowseBindingNavigator.BindingSource = null;
            this.BrowseBindingNavigator.BorderColor = System.Drawing.Color.Red;
            this.BrowseBindingNavigator.BorderFocusColor = System.Drawing.SystemColors.ActiveBorder;
            this.BrowseBindingNavigator.BorderRadius = 8;
            this.BrowseBindingNavigator.BorderSize = 1;
            this.BrowseBindingNavigator.ControlSeparatorWidth = 3;
            this.BrowseBindingNavigator.CountItem = null;
            this.BrowseBindingNavigator.CountItemFormat = "";
            this.BrowseBindingNavigator.DeleteItem = null;
            this.BrowseBindingNavigator.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BrowseBindingNavigator.Location = new System.Drawing.Point(300, 100);
            this.BrowseBindingNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.BrowseBindingNavigator.MoveFirstItem = null;
            this.BrowseBindingNavigator.MoveLastItem = null;
            this.BrowseBindingNavigator.MoveNextItem = null;
            this.BrowseBindingNavigator.MovePreviousItem = null;
            this.BrowseBindingNavigator.Name = "BrowseBindingNavigator";
            this.BrowseBindingNavigator.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.BrowseBindingNavigator.PositionItem = null;
            this.BrowseBindingNavigator.PositionTextItem = null;
            this.BrowseBindingNavigator.SectionSeparatorWidth = 12;
            this.BrowseBindingNavigator.ShowTextPanel = false;
            this.BrowseBindingNavigator.Size = new System.Drawing.Size(600, 50);
            this.BrowseBindingNavigator.TabIndex = 6;
            // 
            // BrowseBindingSource
            // 
            this.BrowseBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.BrowseBindingSource_AddingNew);
            this.BrowseBindingSource.PositionChanged += new System.EventHandler(this.BrowseBindingSource_PositionChanged);
            // 
            // RecipeCardBrowsePartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.BrowseBindingNavigator);
            this.Controls.Add(this.DateModernLabel);
            this.Controls.Add(this.CustomerNameLabel);
            this.Controls.Add(this.HeaderPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RecipeCardBrowsePartView";
            this.Size = new System.Drawing.Size(1200, 160);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private ExpandButton ExpandButton;
        private System.Windows.Forms.Label HeaderLabel;
        private ModernLabel CustomerNameLabel;
        private ModernLabel DateModernLabel;
        private ModernBindingNavigator BrowseBindingNavigator;
        private System.Windows.Forms.BindingSource BrowseBindingSource;
        private ModernButton GoToNextButton;
        private ModernButton GotoFirstButton;
        private ModernButton GoToLastButton;
        private ModernButton GoToPreviousButton;
        private ModernButton AddNewButton;
        private ModernButton DeleteButton;
        private ModernTextBox PositionTextBox;
        private ModernLabel CountTextLabel;
        private ModernLabel CountLabel;

    }
}
