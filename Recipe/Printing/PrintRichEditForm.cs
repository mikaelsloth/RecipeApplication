namespace Recipe.Printing
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public partial class PrintRichEditForm : Form, IDisposable
    {
        public PrintRichEditForm()
        {
            InitializeComponent();
        }

        public RichTextBox RTF_Print => richTextBox1;

        public byte[] PersistRTFContent() => Encoding.UTF8.GetBytes(richTextBox1.Rtf);

        public void LoadRTFContent(byte[] RTF)
        {
            MemoryStream stream = new(RTF);
            richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
        }
    }
}
