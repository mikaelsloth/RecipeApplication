namespace Recipe.Printing
{
    using Recipe.Controller;
    using Recipe.Models.Db;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal partial class GdprDocument
    {
        private readonly IServiceProvider serviceProvider;
        private readonly PrintRichEditForm form;
        private readonly RichTextBox richText;

        public GdprDocument(IServiceProvider serviceProvider, PrintRichEditForm form)
        {
            this.serviceProvider = serviceProvider;
            this.form = form;
            richText = form.RTF_Print;
            PrintMode = PrintMode.Print;
        }

        public void Print()
        {        
            if (Renderer == null) throw new InvalidOperationException("Print renderer is not initialized");
            if (PrintObject == null) throw new InvalidOperationException("Print document is not initialized");

            PrintPreviewDialog? preview;
            PrintDialog? printDialog;
            bool print = true;

            switch (PrintMode)
            {
                case PrintMode.Print:
                    break;
                case PrintMode.Preview:
                    print = false;
                    preview = SetupPreview(PrintObject);
                    _ = preview.ShowDialog();
                    break;
                case PrintMode.PrintWithPreview:
                    preview = SetupPreview(PrintObject);
                    if (preview.ShowDialog() != DialogResult.OK) print = false;
                    break;
                case PrintMode.PrinterDialog:
                case PrintMode.PrintWithPrinterDialog:
                    printDialog = SetupPrinterDialog(PrintObject);
                    if (printDialog.ShowDialog() != DialogResult.OK) print = false;
                    break;
                case PrintMode.PrintWithPreviewAndPrinterDialog:
                    preview = SetupPreview(PrintObject);
                    printDialog = SetupPrinterDialog(PrintObject);
                    if (preview.ShowDialog() == DialogResult.OK)
                    {
                        if (printDialog.ShowDialog() != DialogResult.OK) print = false;
                    }
                    else print = false;
                    break;
                default:
                    throw new InvalidOperationException("Printmode is not valid");
            }

            if (print) PrintObject.Print();
            Renderer = null;
        }

        private static PrintDialog SetupPrinterDialog(PrintDocument print) => new() { Document = print };

        private static PrintPreviewDialog SetupPreview(PrintDocument print) => new() { Document = print, UseAntiAlias = true, Height = print.DefaultPageSettings.Bounds.Height, Width = print.DefaultPageSettings.Bounds.Width };

        public PrintMode PrintMode { get; set; }

        private DocumentRenderer? Renderer { get; set; }

        private PrintDocument? PrintObject { get; set; }

        public async Task LoadData()
        {
            object? obj = serviceProvider.GetService(typeof(ICommonEntitiesController));
            if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(ICommonEntitiesController), "Not defined by ServiceProvider - call IT development");
            ICommonEntitiesController instance = (ICommonEntitiesController)obj;

            CommonEntity? com = await instance.Get();

            PrintDocument myprint = new();
            myprint.DocumentName = "GDPR Kunde Eksemplar";
            myprint.PrintPage += Myprint_PrintPage;

            PrinterSettings ps = new();
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First(size => size.Kind == PaperKind.A4);

            myprint.PrinterSettings.PrinterName = ps.PrinterName;
            myprint.DefaultPageSettings.PaperSize = sizeA4;
            myprint.DefaultPageSettings.Landscape = false;
            Rectangle printarea = myprint.DefaultPageSettings.Bounds;
            Margins margins = myprint.DefaultPageSettings.Margins;

            if (com == null)
            {
                richText.Text = "ERROR";
            }
            else
            {
                form.LoadRTFContent(com.RftData);
            }

            PrintObject = myprint;
        }

        public void CreateRenderer()
        {
            if (PrintObject != null)
                Renderer = DocumentRenderer.CreateFromRichEdit(richText, PrintObject.DefaultPageSettings.Bounds, PrintObject.DefaultPageSettings.Margins);
        }

        private void Myprint_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null) throw new ArgumentException("Printing failed due to null argument", nameof(e));
            if (Renderer == null) throw new InvalidOperationException("Print renderer is not initialized");

            bool pagebreak = false;
            while (!pagebreak && !Renderer.IsPrinted)
            {
                pagebreak = !Renderer.DrawNextSection(e.Graphics);
            }

            if (pagebreak) e.HasMorePages = true;
        }
    }
}
