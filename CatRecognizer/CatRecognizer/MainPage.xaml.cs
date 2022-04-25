using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CatRecognizer
{

    public partial class MainPage : ContentPage
    {
        Stream stream;
        string path;
        public MainPage()
        {
            InitializeComponent();
            pickPhoto.Clicked += (s, e) => OnPickPhotoButtonClicked(s, e);
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            var button = (Button)sender;
            Dictionary<string, Stream> dic = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            foreach (KeyValuePair<string, Stream> currentImage in dic)
            {
                stream = currentImage.Value;

                path = currentImage.Key;

                if (stream != null)
                {
                    img.Source = ImageSource.FromStream(() => stream);
                }

                var sampleData = new CatsClassification.ModelInput()
                {
                    ImageSource = path
                };

                var result = CatsClassification.Predict(sampleData);
                output.Text = result.Prediction;
            }

            (sender as Button).IsEnabled = true;
        }
    }
}
