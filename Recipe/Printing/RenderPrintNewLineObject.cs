namespace Recipe.Printing
{
    using System.Drawing;
    using System.Drawing.Printing;

    internal partial class DocumentRenderer
    {
        internal class RenderPrintNewLineObject : RenderPrintSectionBase
        {
            public RenderPrintNewLineObject(Graphics graphics, PointF drawingstartposition, int textstartposition, TextAttributes attributes, bool righttoleft = false) : base(graphics, drawingstartposition, textstartposition, attributes, righttoleft)
            {
                TextLenght = 1;
            }

            public override void CalculateLineMetrics(Graphics graphics, Rectangle printarea, Margins margins, string fullText)
            {
                LineMetrics line = CreateNewLine(printarea, margins, DrawingStartPosition, 1);
                lines.Add(line);
                DrawingEndPosition = line.NextBeginPoint;
            }
        }
    }
}
