﻿using System.IO;
using System.Reflection;
namespace EpsilonEngine
{
    public static class AssetLoader
    {
        public static Texture LoadImage(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(fileName);
            System.Drawing.Image loadedImage = System.Drawing.Image.FromStream(stream);
            System.Drawing.Bitmap loadedBitMap = new System.Drawing.Bitmap(loadedImage);
            Texture output = Texture.Create(loadedBitMap.Width, loadedBitMap.Height);
            for (int x = 0; x < loadedBitMap.Width; x++)
            {
                for (int y = 0; y < loadedBitMap.Height; y++)
                {
                    System.Drawing.Color systemColor = loadedBitMap.GetPixel(x, loadedBitMap.Height - y - 1);
                    if (systemColor.A != 255)
                    {
                        output.SetPixel(null, x, y);
                    }
                    else
                    {
                        Color epsilonColor = Color.Create(systemColor.R, systemColor.G, systemColor.B);
                        output.SetPixel(epsilonColor, x, y);
                    }
                }
            }
            return output;
        }
    }
}
