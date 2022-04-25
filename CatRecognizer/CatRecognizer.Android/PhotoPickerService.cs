﻿using Android.Content;
using System.IO;
using System.Threading.Tasks;
using CatRecognizer.Droid;
using Xamarin.Forms;
using System.Collections.Generic;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace CatRecognizer.Droid
{
    public class PhotoPickerService : IPhotoPickerService
    {
        Task<Dictionary<string, Stream>> IPhotoPickerService.GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Dictionary<string, Stream>>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}