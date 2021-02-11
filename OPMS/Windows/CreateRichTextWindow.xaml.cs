using OPSMBackend.DataEntities;
using SautinSoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPMS.Windows
{
    public partial class CreateRichTextWindow : WindowBase
    {
        public CreateRichTextWindow()
        {
            InitializeComponent();
        }

        public override void Init(object data)
        {
            if (data != null && data is string)
            {
                var text = data as string;
                if (!string.IsNullOrEmpty(text))
                {
                    var h2r = new HtmlToRtf();

                    using (var stream = new MemoryStream())
                    {
                        h2r.OpenHtml(text);
                        h2r.ToRtf(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(stream))
                        {
                            var txt = reader.ReadToEnd();
                            byte[] byteArray = Encoding.ASCII.GetBytes(txt);

                            using (MemoryStream ms = new MemoryStream(byteArray))
                            {
                                TextRange tr = new TextRange(MainRTB.Document.ContentStart, MainRTB.Document.ContentEnd);
                                tr.Load(ms, DataFormats.Rtf);
                            }
                        }
                    }
                }

            }
        }

        public event EventHandler OnOkClicked;
        public event EventHandler OnCancelClicked;

        public void OnOkClicked_Invoked(string text)
        {
            OnOkClicked?.Invoke(this, new CreateRichTextWindowEventArgs(text));
        }

        public void OnCancelClicked_Invoked()
        {
            OnCancelClicked?.Invoke(this, new EventArgs());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string rtfText; //string to save to db
            TextRange tr = new TextRange(MainRTB.Document.ContentStart, MainRTB.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, DataFormats.Rtf);
                rtfText = Encoding.ASCII.GetString(ms.ToArray());
            }


            var r2h = new RtfToHtml();
            r2h.OutputFormat = RtfToHtml.eOutputFormat.HTML_32;
            r2h.Encoding = RtfToHtml.eEncoding.UTF_8;

            using (MemoryStream ms = new MemoryStream())
            {
                r2h.OpenRtf(rtfText);
                r2h.ToHtml(ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(ms))
                {
                    rtfText = streamReader.ReadToEnd();
                }
            }

            var textToRemove = rtfText.Substring(rtfText.Length - 224, 208); //224 is the start of the watermark div... the length of the watermark div is 208
            rtfText = rtfText.Replace(textToRemove, "");

            OnOkClicked_Invoked(rtfText);
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            OnOkClicked = null;
            OnCancelClicked = null;
            base.OnClosing(e);
        }

    }

    public class CreateRichTextWindowEventArgs : EventArgs
    {
        public string Text { get; set; }
        public CreateRichTextWindowEventArgs(string text)
        {
            Text = text;
        }
    }
}
