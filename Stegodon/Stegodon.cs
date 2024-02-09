using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Stegodon
{
    public class Stegodon
    {

        // PREPOCESSING STARTS HERE

        public static Bitmap NormalizeImage(Bitmap originalImage)
        {
            Bitmap normalizedImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics graphics = Graphics.FromImage(normalizedImage))
            {
                graphics.DrawImage(originalImage, Point.Empty);
            }

            for (int y = 0; y < normalizedImage.Height; y++)
            {
                for (int x = 0; x < normalizedImage.Width; x++)
                {
                    Color pixel = normalizedImage.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(
                        pixel.R % 2 != 0 ? pixel.R - 1 : pixel.R, // if odd make even
                        pixel.G % 2 != 0 ? pixel.G - 1 : pixel.G,
                        pixel.B % 2 != 0 ? pixel.B - 1 : pixel.B);

                    normalizedImage.SetPixel(x, y, newPixel);
                }
            }
            return normalizedImage;
        }


        // PREPOCESSING ENDS HERE

        // ENCRYPTION STARTS HERE

        public static string Txt2Bin(string text)
        {
            StringBuilder binaryMessage = new StringBuilder();

            foreach (char character in text)
            {
                string binaryChar = Convert.ToString(character, 2);

                // pad with 0 until binaryChar == 1 byte
                binaryChar = binaryChar.PadLeft(8, '0');

                binaryMessage.Append(binaryChar).Append(' ');
            }

            if (binaryMessage.Length > 0)
            {
                binaryMessage.Length--; // remove the last char space ( empty space ) 
            }

            return binaryMessage.ToString();
        }

        private static Bitmap InjectBinMsg(Bitmap normalizedImage, string binaryMessage)
        {
            Bitmap encryptedImage = new Bitmap(normalizedImage);

            binaryMessage = binaryMessage.Replace(" ", ""); // remove emptys

            int bitIndex = 0; // pointer

            for (int y = 0; y < encryptedImage.Height; y++)
            {
                for (int x = 0; x < encryptedImage.Width; x++)
                {
                    if (bitIndex < binaryMessage.Length) // check
                    {
                        Color pixel = encryptedImage.GetPixel(x, y);

                        Color newPixel = Color.FromArgb( // update the least significant bit of each color channel with a bit from the string
                            (pixel.R & 0xFE) | ((bitIndex < binaryMessage.Length) ? ((binaryMessage[bitIndex] - '0') & 1) : 0),
                            (pixel.G & 0xFE) | (((bitIndex + 1) < binaryMessage.Length) ? ((binaryMessage[bitIndex + 1] - '0') & 1) : 0),
                            (pixel.B & 0xFE) | (((bitIndex + 2) < binaryMessage.Length) ? ((binaryMessage[bitIndex + 2] - '0') & 1) : 0));

                        encryptedImage.SetPixel(x, y, newPixel);

                        bitIndex += 3; // shift to the next 3 bits -/- pixel
                    }
                    else
                    {
                        return encryptedImage; // IF all bits are used then copy//recycle the remaining pixels
                    }
                }
            }

            return encryptedImage;
        }


        public static Bitmap Encrypt(Bitmap originalImage, string inputString)
        {
            Bitmap normalizedImage = NormalizeImage(originalImage);

            string binaryMessage = Txt2Bin(inputString); // goat --> 01100111 01101111 01100001 01110100 00001010

            Bitmap encryptedImage = InjectBinMsg(normalizedImage, binaryMessage);

            return encryptedImage;
        }

        // ENCRYPTION ENDS HERE



        // DECRYPTION STARTS HERE

        private static string Bin2Txt(string binaryMessage)
        {
            StringBuilder messageBuilder = new StringBuilder();

            for (int i = 0; i < binaryMessage.Length; i += 8) // 8bit
            {
                if (i + 8 <= binaryMessage.Length) // at least 8 characters remaining in the binary to convert to ASCII
                {
                    string byteStr = binaryMessage.Substring(i, 8);
                    int charCode = Convert.ToInt32(byteStr, 2);
                    char character = (char)charCode;
                    messageBuilder.Append(character);
                }
                else
                {
                    break;
                }
            }

            return messageBuilder.ToString();
        }


        public static string Decrypt(Bitmap image)
        {
            StringBuilder messageBuilder = new StringBuilder();

            for (int y = 0; y < image.Height; y++) // pixel iteration and exctract least significant bit from each channel
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    messageBuilder.Append(pixel.R & 1);
                    messageBuilder.Append(pixel.G & 1);
                    messageBuilder.Append(pixel.B & 1);
                }
            }

            string binaryMessage = messageBuilder.ToString(); // 0110011101101111011000010111010000001010??????... - ? as img data
            string message = Bin2Txt(binaryMessage); // 01100111 01101111 01100001 01110100 00001010 --> goat

            return message;
        }

        // DECRYPTION ENDS HERE
    }
}

