namespace Recipe.Printing
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    internal partial class DocumentRenderer
    {
        internal class RenderPrintTextSectionObject : RenderPrintSectionBase
        {

            public void IncrementCount() => TextLenght++;

            public void DecrementCount() => TextLenght--;

            public RenderPrintTextSectionObject(Graphics graphics,
                                          PointF drawingstartposition,
                                          int textstartposition,
                                          TextAttributes attributes,
                                          bool righttoleft) : base(graphics, drawingstartposition, textstartposition, attributes, righttoleft)
            {
            }

            public override void CalculateLineMetrics(Graphics graphics, Rectangle printarea, Margins margins, string fullText)
            {
                int textstart = TextStartPosition;
                bool firstline = true;
                PointF startpos = DrawingStartPosition;
                LineMetrics? line = null;

                while (textstart < TextStartPosition + TextLenght)
                {
                    if (textstart > TextStartPosition)
                    {
                        line = CreateNewLine(printarea, margins, startpos, textstart);
                        startpos = line.NextBeginPoint;
                        lines.Add(line);
                    }

                    textstart += CalculateLineMetricsImpl(startpos, graphics, printarea, margins, fullText, textstart, firstline);
                    int latest = lines.Count - 1;
                    line = lines[latest];
                    startpos = line.NextBeginPoint;
                    firstline = false;
                }

                if (line != null) DrawingEndPosition = line.NextBeginPoint;
            }

            private int CalculateLineMetricsImpl(PointF startPosition, Graphics graphics, Rectangle printarea, Margins margins, string fullText, int textStartPosition, bool firstline = false)
            {
                float remainingLineWidth = printarea.Width - margins.Left - margins.Right - DrawingStartPosition.X;
                int remainingLineWidthOffset = firstline && DrawingStartPosition.X == margins.Left ? Attributes.IndentSize : 0;

                using StringFormat format = new(StringFormatFlags.NoFontFallback | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces);
                format.Alignment = Attributes.Alignment switch
                {
                    HorizontalAlignment.Left => RightToLeft ? StringAlignment.Far : StringAlignment.Near,
                    HorizontalAlignment.Center => StringAlignment.Center,
                    HorizontalAlignment.Right => RightToLeft ? StringAlignment.Near : StringAlignment.Far,
                    _ => StringAlignment.Near
                };

                SizeF size = graphics.MeasureString(fullText[textStartPosition..(TextStartPosition + TextLenght)], Attributes.Font, new SizeF(remainingLineWidth + remainingLineWidthOffset, LineHeight), format, out int fitted, out int lines);
                if (fitted == 0)
                {
                    //this is not allowed to happen on second line since it means the font size is excessive
                    //if (!firstline) throw new InvalidOperationException("The font size can not be handled");
                    //this is not allowed to happen on first line if we started from left margin since it means the font size is excessive
                    if (startPosition.X == margins.Left + Attributes.IndentSize) throw new InvalidOperationException("The font size can not be handled");
                    //we just need to go down a line
                    this.lines.Add(CreateNewLine(printarea, margins, startPosition, textStartPosition, 0));
                    return 0;
                }
                else
                {
                    if (textStartPosition + fitted == TextStartPosition + TextLenght)
                    {
                        //all characters fitted and can be printed in one go
                        this.lines.Add(new(new(startPosition.X + size.Width, startPosition.Y + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore), textStartPosition, fitted, false, false, margins));
                        return fitted;
                    }
                    else
                    {
                        //not all characters could be fitted
                        //check if we started from left margin
                        if (startPosition.X == margins.Left + Attributes.IndentSize)
                        {
                            //find out if we can make a decent break by finding a space
                            int calculated_fitted = CreateCorrectLinebreak(fullText, textStartPosition, fitted);
                            //if no decent line break position is found, print all that fits - otherwise print the line until the last space found
                            int final_fitted = calculated_fitted == 0 ? fitted : calculated_fitted;
                            this.lines.Add(new(new(startPosition.X + size.Width, startPosition.Y + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore), textStartPosition, final_fitted, false, false, margins));
                            return final_fitted;
                        }
                        //We did not start from margin of page
                        else
                        {
                            //find out if we can make a decent break by finding a space
                            int calculated_fitted = CreateCorrectLinebreak(fullText, textStartPosition, fitted);
                            //if it does not fit on the part of the line check if it fits by starting on a new line
                            if (calculated_fitted == 0)
                            {
                                SizeF size_test = graphics.MeasureString(fullText[textStartPosition..(TextStartPosition + TextLenght)], Attributes.Font, new SizeF(printarea.Width - margins.Left - margins.Right, LineHeight + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore), format, out int test_fitted, out int test_lines);
                                //does it all fit in now?
                                if (textStartPosition + test_fitted == TextStartPosition + TextLenght)
                                {
                                    this.lines.Add(CreateNewLine(printarea, margins, startPosition, textStartPosition, 0));
                                    return 0;
                                }
                                else
                                { //if not check if no spaces are still not fount, since then we just print the very long word
                                    int test_calculated_fitted = CreateCorrectLinebreak(fullText, textStartPosition, test_fitted);
                                    if (test_calculated_fitted == 0)
                                    {
                                        //no decent breaks found by pushing one line down. Just start print the very long word
                                        this.lines.Add(new(new(startPosition.X + size.Width, startPosition.Y + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore), textStartPosition, fitted, false, false, margins));
                                        return fitted;
                                    }
                                    else
                                    {
                                        //by moving one line down a space could be found. insert a line break
                                        this.lines.Add(CreateNewLine(printarea, margins, startPosition, textStartPosition, 0));
                                        return 0;
                                    }
                                }
                            }
                            else
                            {
                                // a decent line break was found. Let's print what fits.
                                this.lines.Add(new(new(startPosition.X + size.Width, startPosition.Y + Attributes.LineSpacingAfter + Attributes.LineSpacingBefore), textStartPosition, calculated_fitted, false, false, margins));
                                return calculated_fitted;
                            }
                        }
                    }
                }
            }

            private static int CreateCorrectLinebreak(string fullText, int textStartPosition, int fitted)
            {
                bool pass_stopcheck = true;
                int calculated_start = textStartPosition + fitted;
                //
                if (fullText[calculated_start] == ' ' || fullText[calculated_start + 1] == ' ')
                {
                    for (int i = calculated_start + 1; pass_stopcheck && i < fullText.Length; i++)
                    {
                        if (fullText[i] == ' ')
                        {
                            fitted++;
                            continue;
                        }

                        pass_stopcheck = false;
                    }

                    return fitted;
                }
                else
                {
                    int latest_space = fullText.LastIndexOf(' ', calculated_start);
                    return latest_space <= textStartPosition ? 0 : fitted - (calculated_start - latest_space) + 1;
                }
            }
        }
    }
}