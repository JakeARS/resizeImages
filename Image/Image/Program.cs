using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Image
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bitmap sourceImage = new Bitmap("2.jpg");
            int width = sourceImage.Width;
            int heigth = sourceImage.Height;

            int k = 2;

            while (width % k != 0 || heigth % k != 0)
            {
                if (width%k <= k/2)
                {
                    k++;
                }
                else
                {
                    k--;
                }
            }

            int newWidth = Convert.ToInt16(Convert.ToDouble(sourceImage.Width)/k);
            int newHeigth = Convert.ToInt16(Convert.ToDouble(sourceImage.Height)/k);
            int[,] D = new int[width, heigth];
            //int Wx = min(W, sourceImage.Width);
            //int Hx = min(H, sourceImage.Height);

            Bitmap processedImage = new Bitmap(newWidth, newHeigth);

            processedImage.GetPixel(1, 1);

            int step = width/100;
            int prog = 0;
            int koef = 1;
            
            for (int i = 1; i < width; i += k)
            {
                for (int j = 1; j < heigth; j += k)
                {
                    //int A = sourceImage.GetPixel(i, j).R;
                    //A = sourceImage.GetPixel(i, j).G;
                    //A = sourceImage.GetPixel(i, j).B;

                    //рассчет цветов должен быть на основе матрицы размерности k x k
                    int A = (sourceImage.GetPixel(i, j).A + sourceImage.GetPixel(i - 1, j).A +
                             sourceImage.GetPixel(i, j - 1).A +
                             sourceImage.GetPixel(i - 1, j - 1).A)/4;
                    int R = (sourceImage.GetPixel(i, j).R + sourceImage.GetPixel(i - 1, j).R +
                             sourceImage.GetPixel(i, j - 1).R +
                             sourceImage.GetPixel(i - 1, j - 1).R)/4;
                    int G = (sourceImage.GetPixel(i, j).G + sourceImage.GetPixel(i - 1, j).G +
                             sourceImage.GetPixel(i, j - 1).G +
                             sourceImage.GetPixel(i - 1, j - 1).G)/4;
                    int B = (sourceImage.GetPixel(i, j).B + sourceImage.GetPixel(i - 1, j).B +
                             sourceImage.GetPixel(i, j - 1).B +
                             sourceImage.GetPixel(i - 1, j - 1).B)/4;
                    Color myColor = Color.FromArgb(A, R, G, B);

                    processedImage.SetPixel((i - 1)/k, (j - 1)/k, myColor);
                    //A = processedImage.GetPixel((i - 1)/3, (j - 1)/3).R;
                    //  processedImage.SetPixel(i - 1, j - 1,
                    //       Color(sourceImage.GetPixel(i, j) + sourceImage.GetPixel(i - 1, j) + sourceImage.GetPixel(i, j - 1) +
                    //   sourceImage.GetPixel(i - 1, j - 1)));
                }
                if (i < koef*(width / 100))
                {
                    Console.Clear();
                    Console.Write(prog + "%"); //проценты отображаются криво для некоторых изображений, надо править
                }
                else
                {
                    prog++;
                    koef++;
                }
            }

            processedImage.Save("processed.jpg");

            Console.WriteLine("\nИзображение обработано");
            Console.ReadLine();

            //    for (int i = 0; i < heigth; i++)
            //    {
            //        for (int j = 0; j < width; j++)
            //        {
            //            Console.WriteLine(sourceImage.GetPixel(i, j).R);
            //            D[i, j] = sourceImage.GetPixel(i, j).R;
            //        }
            //    }
            //Console.ReadLine();
            //    Rectangle rect = new Rectangle(0, 0, width, heigth);
            //    BitmapData bmpData = sourceImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //    // Get the address of the first sourceImagene.
            //    IntPtr ptr = bmpData.Scan0;

            //    // Declare an array to hold the bytes of the bitmap.
            //    // This code is specific to a bitmap with 24 bits per pixels.
            //    int bytes = width * heigth * 3;
            //    byte[] rgbValues = new byte[bytes];

            //    // Copy the RGB values into the array.
            //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


            /*int counter = 0;
            int p;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {

                    p = new int();

                    //counter = (i + j * W) * 3;
                    counter = (i * W + j) * 3;
                    p.R = rgbValues[counter + 2];
                    p.G = rgbValues[counter + 1];
                    p.B = rgbValues[counter + 0];


                    D[i, j] = p;

                }*/
        }
    }
}
