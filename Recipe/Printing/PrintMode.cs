namespace Recipe.Printing
{
    internal enum PrintMode : int
    {
        /// <summary>
        /// Print the document. If PrinterDialog is not set the default printer will be used
        /// </summary>
        Print = 1,
        /// <summary>
        /// Show Preview Form only
        /// </summary>
        Preview = 2,
        /// <summary>
        /// Show Preview Form before printing the documen to default printer
        /// </summary>
        PrintWithPreview = Print | Preview,
        /// <summary>
        /// Show Printer Dialog form. Implies Print
        /// </summary>
        PrinterDialog = 4,
        /// <summary>
        /// Print the document using the printer selected from the Printer Dialog
        /// </summary>
        PrintWithPrinterDialog = Print | PrinterDialog,
        /// <summary>
        /// Show Preview Form before printing the document using the printer selected from the Printer Dialog
        /// </summary>
        PrintWithPreviewAndPrinterDialog = Print | Preview | PrinterDialog
    }
}
