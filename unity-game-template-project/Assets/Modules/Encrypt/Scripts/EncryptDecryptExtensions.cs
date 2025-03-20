using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Modules.Encrypt
{
    public static class EncryptDecryptExtensions
    {
        private static readonly int _keySize = 256;
        private static readonly int _blockSize = 128;
        private static readonly int _saltSize = 32;
        private static readonly int _bitesInByteCount = 8;

        public static string Encrypt(this string plainText, string password)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            byte[] salt = GenerateRandomBytes(_saltSize);

            using (Rfc2898DeriveBytes key = new(password, salt, 10000))
            {
                byte[] keyBytes = key.GetBytes(_keySize / _bitesInByteCount);
                byte[] ivBytes = key.GetBytes(_blockSize / _bitesInByteCount);

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = _keySize;
                    aes.BlockSize = _blockSize;
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))

                    using (MemoryStream memoryStream = new())
                    {
                        memoryStream.Write(salt, 0, salt.Length);

                        using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))

                        using (StreamWriter streamWriter = new(cryptoStream, Encoding.UTF8))
                        {
                            streamWriter.Write(plainText);
                        }

                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(this string cipherText, string password)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            byte[] salt = new byte[_saltSize];
            Array.Copy(cipherBytes, 0, salt, 0, _saltSize);
            int iterations = 10000;

            using (Rfc2898DeriveBytes key = new(password, salt, iterations))
            {
                byte[] keyBytes = key.GetBytes(_keySize / _bitesInByteCount);
                byte[] ivBytes = key.GetBytes(_blockSize / _bitesInByteCount);

                using (var aes = Aes.Create())
                {
                    aes.KeySize = _keySize;
                    aes.BlockSize = _blockSize;
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (MemoryStream memoryStream = new(cipherBytes, _saltSize, cipherBytes.Length - _saltSize))
                    using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))

                    using (StreamReader streamReader = new(cryptoStream, Encoding.UTF8))
                        return streamReader.ReadToEnd();
                }
            }
        }

        private static byte[] GenerateRandomBytes(int size)
        {
            using (RNGCryptoServiceProvider cryptoServiceProvider = new())
            {
                byte[] data = new byte[size];
                cryptoServiceProvider.GetBytes(data);

                return data;
            }
        }
    }
}
