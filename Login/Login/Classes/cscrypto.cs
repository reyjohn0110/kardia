using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Login.Classes
{
	public class AES
	{
		public byte[] EncryptAES(byte[] input, string key)
		{
			byte[] KeyBytes = new UTF8Encoding().GetBytes(key);
			Aes AESImplementation = Aes.Create("AES");
			AESImplementation.Key = KeyBytes;
			AESImplementation.Mode = CipherMode.ECB;
			ICryptoTransform CryptoTransform = AESImplementation.CreateEncryptor();
			return CryptoTransform.TransformFinalBlock(input, 0, input.Length);
		}

		public byte[] EncryptAES(string input, string key)
		{
			byte[] StringBytes = new UTF8Encoding().GetBytes(input);
			return EncryptAES(StringBytes, key);
		}
		public string EncryptAESToString(byte[] input, string key)
		{
			return Convert.ToBase64String(EncryptAES(input, key));
		}
		public string EncryptAESToString(string input, string key)
		{
			return Convert.ToBase64String(EncryptAES(input, key));
		}
		public byte[] DecryptAES(byte[] input, string key)
		{
			byte[] KeyBytes = new UTF8Encoding().GetBytes(key);
			Aes AESImplementation = Aes.Create("AES");
			AESImplementation.Key = KeyBytes;
			AESImplementation.Mode = CipherMode.ECB;

			ICryptoTransform CryptoTransform = AESImplementation.CreateDecryptor();
			return CryptoTransform.TransformFinalBlock(input, 0, input.Length);
		}
		public byte[] DecryptAESFromString(string input, string key)
		{
			return DecryptAES(Convert.FromBase64String(input), key);
		}
		public string DecryptAESToString(byte[] input, string key)
		{
			return new UTF8Encoding().GetString(DecryptAES(input, key));
		}
		public string DecryptAESToString(string input, string key)
		{
			return new UTF8Encoding().GetString(DecryptAESFromString(input, key));
		}
	}
}
