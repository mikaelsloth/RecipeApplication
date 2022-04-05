namespace Recipe.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class RecipeLinesGrid : UserControl
    {
        private ModernLabel? Header;
        private string headerText = "";
        private Font headerFont;

        public RecipeLinesGrid()
        {
            headerFont = Font;
            InitializeComponent();
            AddHeaderText();
        }

        private void AddHeaderText()
        {
            if (!string.IsNullOrWhiteSpace(headerText))
            {
                if (Header is null)
                {
                    if (Panel.Controls.Count > 0) throw new InvalidOperationException("Header must be added before any other elements");
                    Header = new()
                    {
                        Font = headerFont,
                        Dock = DockStyle.Top,
                        ForeColor = ForeColor,
                        Padding = new(20, 0, 0, 0),
                        Size = new(500, 15),
                        TextAlign = ContentAlignment.MiddleLeft,
                        BorderColor = Color.Red,
                        BorderRadius = 4,
                        BorderSize = 1,                        
                    };

                    Panel.Controls.Add(Header);
                }

                Header.Text = headerText;
            }
            else
            {
                Panel.Controls.Remove(Header);
                Header = null;
            }

            Panel.Invalidate();
        }

        public int Add(RecipeLineView recipeLine)
        {
            recipeLine.Size = new(566, 38);
            recipeLine.Dock = DockStyle.Top;
            recipeLine.EditClicked += Line_EditClicked;
            recipeLine.DeleteClicked += Line_DeleteClicked;
            Panel.Controls.Add(recipeLine);
            return Panel.Controls.Count - 1;
        }

        public void Remove(RecipeLineView recipeLine) => Panel.Controls.Remove(recipeLine);

        public RecipeLineView this[int index] => index >= Panel.Controls.Count
                    ? throw new IndexOutOfRangeException("Index is higher than number of RecipeLines")
                    : Panel.Controls.OfType<RecipeLineView>().First(rl => rl.Index == index);

        public void Line_DeleteClicked(object? sender, EventArgs e) => LineDeleteRequested?.Invoke(sender, e);

        public IEnumerable<RecipeLineView> GetLinesWithIndexLargerThan(int index) => Panel.Controls.OfType<RecipeLineView>().Where(rl => rl.Index > index);

        public event EventHandler? LineDeleteRequested;

        public void Line_EditClicked(object? sender, EventArgs e) => LineEditRequested?.Invoke(sender, e);

        public event EventHandler? LineEditRequested;

        [Browsable(false)]
        public bool ScrollActivated { get; set; } = false;

        [DefaultValue("")]
        public string HeaderText 
        {
            get => headerText; 
            set
            {
                headerText = value;
                AddHeaderText();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                if (Header is not null) Header.ForeColor = value;                
            }
        }

        public Font HeaderFont
        {
            get => headerFont;
            set
            {
                headerFont = value;
                if (Header is not null) Header.Font = value;
            }
        }

        private void Panel_SizeChanged(object sender, EventArgs e) => ScrollActivated = Panel.Height > Height;
    }
}
