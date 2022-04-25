using Android.Content;
using System.IO;
using System.Threading.Tasks;
using CatRecognizer.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace CatRecognizer.Droid
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<Stream> GetImageStreamAsync()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
            Intent.CreateChooser(intent, "Select Photo"),
            MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}