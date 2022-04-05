namespace Recipe.Printing
{
    using System.Drawing;
    using System.Drawing.Printing;

    internal partial class DocumentRenderer
    {
        internal class LineMetrics
        {
            public LineMetrics(PointF nextBeginPoint, int startIndex, int numberOfCharacters, bool isLineBreak, bool pageBreakBeforeLine, Margins margins)
            {
                IsLineBreak = isLineBreak;
                StartIndex = startIndex;
                NumberOfCharacters = numberOfCharacters;
                PageBreakBeforeLine = pageBreakBeforeLine;
                NextBeginPoint = isLineBreak && pageBreakBeforeLine ? new(margins.Left, margins.Top) : nextBeginPoint;
            }

            internal PointF NextBeginPoint { get; }

            internal int StartIndex { get; }

            internal int NumberOfCharacters { get; }

            internal bool IsLineBreak { get; }

            internal bool PageBreakBeforeLine { get; }
        }
    }
}