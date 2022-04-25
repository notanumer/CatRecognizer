using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CatRecognizer
{
    public interface IPhotoPickerService
    {
        Task<Dictionary<string, Stream>> GetImageStreamAsync();
    }
}