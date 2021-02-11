using SautinSoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OPMS.Controls
{
    public class CustomRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty RichTextProperty = DependencyProperty.Register("RichText", typeof(string), typeof(CustomRichTextBox), new UIPropertyMetadata(new PropertyChangedCallback(RichTextProperty_Changed)));

        private static void RichTextProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as CustomRichTextBox;
            string text = "\t";
            if (e.NewValue != null && !string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                text = e.NewValue as string;
            }

            try
            {
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
                                TextRange tr = new TextRange(view.Document.ContentStart, view.Document.ContentEnd);
                                tr.Load(ms, DataFormats.Rtf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                App.Logger.Error(ex.Message);
            }

            
        }

        public string RichText
        {
            get => (string)GetValue(RichTextProperty);
            set => SetValue(RichTextProperty, value);
        }
    }
}
