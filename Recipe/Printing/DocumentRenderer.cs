namespace Recipe.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    internal partial class DocumentRenderer
    {
        private readonly Queue<RenderPrintSectionBase> printSections = new(256);

        private Rectangle PrintArea { get; init; }

        private Margins PrintMargins { get; init; }

        private string DocumentText { get; init; }

        private PointF currentPosition;

        //private int documentIndex;

        private int LastProcessedTextSectionIndex { get; set; }

        public bool IsPrinted { get; private set; } = false;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. Checked in calling methods CreateFromRichEdit.
        private DocumentRenderer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        { }

        public static DocumentRenderer CreateFromRichEdit(RichTextBox richText, Rectangle printarea, Margins margins, KeyValuePair<string, string>[]? replaceStrings)
        {
            if (replaceStrings != null && replaceStrings.Length > 0) ReplaceLiteral(richText, replaceStrings);

            DocumentRenderer dr = new()
            {
                DocumentText = richText.Text,
                PrintArea = printarea,
                PrintMargins = margins
            };
            dr.currentPosition = new(margins.Left, margins.Top);
            CreateFromRichEditImpl(dr, richText);
            return dr;
        }

        public static DocumentRenderer CreateFromRichEdit(RichTextBox richText, Rectangle printarea, Margins margins) => CreateFromRichEdit(richText, printarea, margins, null);

        private static void ReplaceLiteral(RichTextBox richText, KeyValuePair<string, string>[] replaceStrings)
        {
            foreach (var item in replaceStrings)
            {
                string literal = $"[{{{item.Key}}}]";
                int startIndex = 0;
                while (startIndex > -1)
                {
                    int index = richText.Text.IndexOf(literal, startIndex);
                    startIndex = index;
                    if (index > -1)
                    {
                        richText.Select(index, literal.Length);
                        richText.SelectedText = item.Value;
                    }
                }
            }
        }

        private static void CreateFromRichEditImpl(DocumentRenderer dr, RichTextBox richText)
        {
            Panel panel = new() { Size = new(dr.PrintArea.Width, dr.PrintArea.Height) };
            using Graphics graphics = panel.CreateGraphics();

            PointF currentCreatePosition = new(dr.PrintMargins.Left, dr.PrintMargins.Top);

            for (int i = 0; i < dr.DocumentText.Length; i++)
            {
                richText.Select(i, 1);
                TextAttributes attributes = new(richText.SelectionAlignment,
                    richText.SelectionBackColor,
                    richText.SelectionFont,
                    richText.SelectionBullet,
                    richText.SelectionColor,
                    richText.SelectionIndent);
                _ = richText.Text[i] switch
                {
                    '\r' => dr.CreateCarriageReturn(graphics, ref currentCreatePosition, i, attributes),
                    '\n' => dr.CreateLineBreak(graphics, ref currentCreatePosition, i, attributes),
                    _ => dr.CreateTextSection(graphics, ref currentCreatePosition, ref i, richText, attributes)
                };
            }
        }

        private bool CreateTextSection(Graphics graphics, ref PointF currentCreatePosition, ref int textstartposition, RichTextBox richText, TextAttributes attributes)
        {
            RenderPrintTextSectionObject result = new(graphics, currentCreatePosition, textstartposition, attributes, false);
            result.IncrementCount();

            textstartposition++;
            bool movenext = true;

            while (movenext && textstartposition < DocumentText.Length)
            {
                richText.Select(textstartposition, 1);
                TextAttributes nextattributes = new(richText.SelectionAlignment,
                    richText.SelectionBackColor,
                    richText.SelectionFont,
                    richText.SelectionBullet,
                    richText.SelectionColor,
                    richText.SelectionIndent);

                if (!attributes.ValueEquals(nextattributes))
                {
                    movenext = false;
                    //result.DecrementCount();
                    //textstartposition--;
                    continue;
                }

                if (richText.Text[textstartposition] is '\r' or '\n')
                {
                    movenext = false;
                    //result.DecrementCount();
                    //textstartposition--;
                    continue;
                }

                result.IncrementCount();
                textstartposition++;
            }

            result.CalculateLineMetrics(graphics, PrintArea, PrintMargins, DocumentText);
            currentCreatePosition = result.DrawingEndPosition;
            AddSection(result);
            textstartposition--;
            return true;
        }

        private bool CreateLineBreak(Graphics graphics, ref PointF currentCreatePosition, int textstartposition, TextAttributes attributes)
        {
            RenderPrintNewLineObject result = new(graphics, currentCreatePosition, textstartposition, attributes, false);
            result.CalculateLineMetrics(graphics, PrintArea, PrintMargins, DocumentText);
            currentCreatePosition = result.DrawingEndPosition;
            AddSection(result);
            return true;
        }

        private bool CreateCarriageReturn(Graphics graphics, ref PointF currentCreatePosition, int textstartposition, TextAttributes attributes)
        {
            RenderPrintCarriageReturnObject result = new(graphics, currentCreatePosition, textstartposition, attributes, false);
            result.CalculateLineMetrics(graphics, PrintArea, PrintMargins, DocumentText);
            currentCreatePosition = result.DrawingEndPosition;
            AddSection(result);
            return true;
        }

        private void AddSection(RenderPrintSectionBase section) => printSections.Enqueue(section);

        public bool DrawNextSection(Graphics graphics)
        {
            if (IsPrinted) return false;
            var section = printSections.Peek();

            using StringFormat format = new(StringFormatFlags.NoFontFallback | StringFormatFlags.NoWrap);
            format.Alignment = section.Attributes.Alignment switch
            {
                HorizontalAlignment.Left => section.RightToLeft ? StringAlignment.Far : StringAlignment.Near,
                HorizontalAlignment.Center => StringAlignment.Center,
                HorizontalAlignment.Right => section.RightToLeft ? StringAlignment.Near : StringAlignment.Far,
                _ => StringAlignment.Near
            };

            PointF test = section.DrawingEndPosition;

            bool processed = section switch
            {
                RenderPrintNewLineObject nl => ProcessNonTextSection(section, true),
                RenderPrintCarriageReturnObject cr => ProcessNonTextSection(section),
                RenderPrintTextSectionObject tx => ProcessTextSection(graphics, format, section),
                _ => throw new InvalidOperationException("The RenderPrint child type is not mapped to an action")
            };

            if (processed) _ = printSections.Dequeue();
            if (printSections.Count == 0) IsPrinted = true;
            return processed;
        }

        private bool ProcessTextSection(Graphics graphics, StringFormat format, RenderPrintSectionBase section)
        {
            int i = 0;
            bool jump_to_section = LastProcessedTextSectionIndex > 0;

            foreach (var item in section.LinesMetrics)
            {
                while (jump_to_section && LastProcessedTextSectionIndex >= 0)
                {
                    LastProcessedTextSectionIndex--;
                    continue;
                }

                LastProcessedTextSectionIndex = i;

                if (item.IsLineBreak)
                {
                    currentPosition = item.NextBeginPoint;
                    //documentIndex += item.NumberOfCharacters;
                    if (item.PageBreakBeforeLine) return false;
                    continue;
                }

                if (format.Alignment == StringAlignment.Center)
                {
                    currentPosition = new(currentPosition.X + ((PrintArea.Width - PrintMargins.Left - PrintMargins.Right - currentPosition.X) / 2), currentPosition.Y);
                }

                if (section.Attributes.LineSpacingBefore > 0)
                {
                    currentPosition = new(currentPosition.X, currentPosition.Y + section.Attributes.LineSpacingBefore);
                }

                using SolidBrush brush = new(section.Attributes.TextColor);
                graphics.DrawString(DocumentText[item.StartIndex..(item.StartIndex + item.NumberOfCharacters)], section.Attributes.Font, brush, currentPosition.X, currentPosition.Y, format);
                currentPosition = item.NextBeginPoint;
                i++;
            }

            LastProcessedTextSectionIndex = 0;
            return true;
        }

        private bool ProcessNonTextSection(RenderPrintSectionBase section, bool checkPageBreak = false)
        {
            if (LastProcessedTextSectionIndex > 0) return true;
            currentPosition = section.LinesMetrics[0].NextBeginPoint;
            LastProcessedTextSectionIndex++;
            if (checkPageBreak && section.LinesMetrics[0].PageBreakBeforeLine) return false;
            LastProcessedTextSectionIndex = 0;
            return true;
        }

        private string SaveAsJson() => throw new NotImplementedException();
    }
}
