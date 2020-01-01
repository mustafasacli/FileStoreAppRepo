using System;

namespace FileStoreApp.Source.Util
{
    internal class SecurityHelper
    {
        internal static string EncryptSHA256(string data)
        {
            string result = string.Empty;
            try
            {
                result = Encode(data);
            }
            catch (Exception)
            {
            }
            return result;
        }

        internal static String EncodeString(String data)
        {
            string result = string.Empty;
            try
            {
                result = Encode(data);
            }
            catch (Exception)
            {
            }
            return result;
        }

        internal static String DecodeString(String data)
        {
            string result = string.Empty;
            try
            {
                result = Decode(data);
            }
            catch (Exception)
            {
            }
            return result;
        }

        internal static String Decode(String data)
        {
            String result = string.Empty;
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                Byte[] toDecodeByte = Convert.FromBase64String(data);
                Int32 charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                Char[] decodedChar = new Char[charCount];
                utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                result = new String(decodedChar);
            }
            catch (Exception)
            {
            }
            return result;
        }

        internal static String Encode(String data)
        {
            String encodedData = string.Empty;
            try
            {
                Byte[] encDataByte = new Byte[data.Length];
                encDataByte = System.Text.Encoding.UTF8.GetBytes(data);
                encodedData = Convert.ToBase64String(encDataByte);
            }
            catch (Exception)
            {
            }
            return encodedData;
        }
    }
}