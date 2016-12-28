using System;
using System.Text.RegularExpressions;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace UwpUaf.Shared.Converters
{
    public class PngDataUrlToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dataUrl = value as string;

            var match = Regex.Match(dataUrl, @"data:image/(?<type>.+?),(?<data>.+)");
            var base64Data = match.Groups["data"].Value;
            var binData = System.Convert.FromBase64String(base64Data);

            using (var ims = new InMemoryRandomAccessStream())
            {
                using (var dw = new DataWriter(ims))
                {
                    dw.WriteBytes(binData);
                    var i = dw.StoreAsync().AsTask().Result;
                    ims.Seek(0);

                    var img = new BitmapImage();
                    img.SetSource(ims);

                    return img;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return string.Empty;
        }
    }
}
