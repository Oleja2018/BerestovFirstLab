using System;

namespace BerestovFirstLab
{
    public class GetImage
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public GetImage(string name, string url)
        {
            FileName = name;
            Path = url;
        }
        public string GetInfoImage()
        {
            string result = Convert.ToString($"Name:{FileName}, Path: {Path}");
            return result;
        }
    }
}