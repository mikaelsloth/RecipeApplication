namespace Recipe.Printing
{
    using System.Drawing;
    using System.Windows.Forms;

    internal partial class DocumentRenderer
    {
        internal class TextAttributes
        {
            private const int headingspacing = 7;

            public TextAttributes(HorizontalAlignment align, Color backcolor, Font font, bool bullet, Color textcolor, int indentsize)
            {
                Alignment = align;
                BackColor = backcolor;
                Font = font;
                Bullet = bullet;
                TextColor = textcolor;
                IndentSize = indentsize;
                if (font.SizeInPoints == 11) LineSpacingBefore = LineSpacingAfter = headingspacing;
            }

            public HorizontalAlignment Alignment { get; }

            public Color BackColor { get; }

            public Font Font { get; }

            public bool Bullet { get; }

            public Color TextColor { get; }

            public int IndentSize { get; }

            public int LineSpacingBefore { get; }

            public int LineSpacingAfter { get; }

            public bool ValueEquals(TextAttributes attributes) => attributes.Alignment == Alignment && attributes.Font.Equals(Font) && attributes.Bullet == Bullet &&
                    attributes.TextColor == TextColor && attributes.BackColor == BackColor;
        }
    }
}
