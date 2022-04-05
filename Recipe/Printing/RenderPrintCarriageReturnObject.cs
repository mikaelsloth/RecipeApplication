namespace Recipe.Printing
{
    using System.Drawing;
    using System.Drawing.Printing;

    internal partial class DocumentRenderer
    {
        internal class RenderPrintCarriageReturnObject : RenderPrintSectionBase
        {
            public RenderPrintCarriageReturnObject(Graphics graphics, PointF drawingstartposition, int textstartposition, TextAttributes attributes, bool righttoleft = false) : base(graphics, drawingstartposition, textstartposition, attributes, righttoleft)
            {
                TextLenght = 1;
            }

            public override void CalculateLineMetrics(Graphics graphics, Rectangle printarea, Margins margins, string fullText) => lines.Add(CreateCarriageReturn(margins, DrawingStartPosition, 1));
        }
    }
}
