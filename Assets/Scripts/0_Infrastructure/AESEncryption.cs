using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ProjectBlue.RepulserEngine.Infrastructure;
using UnityEngine;

public static class AesEncryption
{
    /// <summary>
    /// 対称鍵暗号を使って文字列を暗号化する
    /// </summary>
    /// <param name="text">暗号化する文字列</param>
    /// <returns>暗号化された文字列</returns>
    public static string Encrypt(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        using (var rijndael = new RijndaelManaged())
        {
            var info = new EncryptionInfo();

            rijndael.BlockSize = info.BlockSize;
            rijndael.KeySize = info.KeySize;
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;

            rijndael.IV = Encoding.UTF8.GetBytes(info.IV);
            rijndael.Key = Encoding.UTF8.GetBytes(info.Key);

            var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

            byte[] encrypted;
            using (var mStream = new MemoryStream())
            {
                using (var ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(ctStream))
                    {
                        sw.Write(text);
                    }

                    encrypted = mStream.ToArray();
                }
            }

            return (System.Convert.ToBase64String(encrypted));
        }
    }

    /// <summary>
    /// 対称鍵暗号を使って暗号文を復号する
    /// </summary>
    /// <param name="cipher">暗号化された文字列</param>
    /// <returns>復号された文字列</returns>
    public static string Decrypt(string cipher)
    {
        if (string.IsNullOrEmpty(cipher)) return "";

        using (var rijndael = new RijndaelManaged())
        {
            var info = new EncryptionInfo();

            rijndael.BlockSize = info.BlockSize;
            rijndael.KeySize = info.KeySize;
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;

            rijndael.IV = Encoding.UTF8.GetBytes(info.IV);
            rijndael.Key = Encoding.UTF8.GetBytes(info.Key);

            var decrypter = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

            var plain = string.Empty;
            using (var mStream = new MemoryStream(System.Convert.FromBase64String(cipher)))
            {
                using (var ctStream = new CryptoStream(mStream, decrypter, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(ctStream))
                    {
                        plain = sr.ReadLine();
                    }
                }
            }

            return plain;
        }
    }
}