using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data.Services
{
	public static class Encryption
	{

		public static string Encrypt(this string text, string key)
		{
			try
			{
				if (string.IsNullOrEmpty(key))
					throw new ArgumentException("Key must have valid value.", nameof(key));
				if (string.IsNullOrEmpty(text))
					throw new ArgumentException("The text must have valid value.", nameof(text));

				var buffer = Encoding.UTF8.GetBytes(text);
				SHA512 sha = SHA512.Create();

				var aesKey = new byte[24];
				Buffer.BlockCopy(sha.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

				using var aes = Aes.Create();

				aes.Key = aesKey;

				using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				using var resultStream = new MemoryStream();
				using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
				using (var plainStream = new MemoryStream(buffer))
				{
					plainStream.CopyTo(aesStream);
				}

				var result = resultStream.ToArray();
				var combined = new byte[aes.IV.Length + result.Length];
				Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
				Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

				return Convert.ToBase64String(combined);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string Decrypt(this string encryptedText, string key)
		{
			try
			{
				if (string.IsNullOrEmpty(key))
					throw new ArgumentException("Key must have valid value.", nameof(key));
				if (string.IsNullOrEmpty(encryptedText))
					throw new ArgumentException("The encrypted text must have valid value.", nameof(encryptedText));

				var combined = Convert.FromBase64String(encryptedText);
				var buffer = new byte[combined.Length];
				SHA512 sha = SHA512.Create();

				var aesKey = new byte[24];
				Buffer.BlockCopy(sha.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

				using Aes aes = Aes.Create();

				aes.Key = aesKey;

				var iv = new byte[aes.IV.Length];
				var ciphertext = new byte[buffer.Length - iv.Length];

				Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
				Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

				aes.IV = iv;

				using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
				using var resultStream = new MemoryStream();
				using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
				using (var plainStream = new MemoryStream(ciphertext))
				{
					plainStream.CopyTo(aesStream);
				}

				return Encoding.UTF8.GetString(resultStream.ToArray());
			}
			catch (Exception)
			{
				return null;
			}
		}

	}
}
