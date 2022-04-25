using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using System.Collections.Generic;

namespace CatRecognizer.Droid
{
    [Activity(Label = "CatRecognizer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Instance = this;
        }

        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Dictionary<string, Stream>> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    Android.Net.Uri uri = data.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    Dictionary<string, Stream> dic = new Dictionary<string, Stream>();
                    dic.Add(uri.ToString(), stream);
                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(dic);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}