namespace Recipe.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    internal partial class DocumentRenderer
    {
        internal abstract class RenderPrintSectionBase
        {
            private const int linespacingbelow = 0;

            private readonly float lineHeight;

            public bool RightToLeft { get; }

            protected readonly IList<LineMetrics> lines = new List<LineMetrics>();

            public ReadOnlyCollection<LineMetrics> LinesMetrics => ((List<LineMetrics>)lines).AsReadOnly();

            public int TextStartPosition { get; }

            public int TextLenght { get; protected set; }

            public TextAttributes Attributes { get; }

            public PointF DrawingStartPosition { get; }

            public PointF DrawingEndPosition { get; protected set; }

            public RenderPrintSectionBase(Graphics graphics,
                                          PointF drawingstartposition,
                                          int textstartposition,
                                          TextAttributes attributes,
                                          bool righttoleft = false)
            {
                DrawingStartPosition = drawingstartposition;
                DrawingEndPosition = new(0, 0);
                TextStartPosition = textstartposition;
                Attributes = attributes;
                RightToLeft = righttoleft;
                TextLenght = 0;
                lineHeight = TextRenderer.MeasureText(graphics, "My", attributes.Font, new Size(0, 0), TextFormatFlags.NoPadding).Height;
            }

            protected static bool ValidatePageBreak(PointF nextBeginPoint, Rectangle printarea, Margins margins) => nextBeginPoint.Y > printarea.Height - margins.Bottom;

            public abstract void CalculateLineMetrics(Graphics graphics, Rectangle printarea, Margins margins, string fullText);

            protected LineMetrics CreateNewLine(Rectangle printarea, Margins margins, PointF startpos, int startIndex, int textLenght = 1)
            {
                PointF newbegin = new(margins.Left, startpos.Y + LineHeight + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore);
                LineMetrics line = new(newbegin, startIndex, textLenght, true, ValidatePageBreak(newbegin, printarea, margins), margins);
                return line;
            }

            protected static LineMetrics CreateCarriageReturn(Margins margins, PointF startpos, int startIndex, int textLenght = 1)
            {
                PointF newbegin = new(margins.Left, startpos.Y);
                LineMetrics line = new(newbegin, startIndex, textLenght, true, false, margins);
                return line;
            }

            protected float LineHeight => Math.Max(Attributes.Font.Height + linespacingbelow, lineHeight);
        }
    }
}