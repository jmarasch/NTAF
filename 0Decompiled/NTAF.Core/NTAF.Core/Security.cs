using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NTAF.Core
{
	public static class Security
	{
		private static byte[] secIV
		{
			get
			{
				byte[] bytSalt = Encoding.ASCII.GetBytes("NewTerraGames");
				return (new PasswordDeriveBytes("NewTerraStudios", bytSalt)).GetBytes(16);
			}
		}

		private static byte[] secKey
		{
			get
			{
				byte[] bytSalt = Encoding.ASCII.GetBytes("NewTerraGames");
				return (new PasswordDeriveBytes("NewTerraStudios", bytSalt)).GetBytes(32);
			}
		}

		public static void cryptFile(string path, Security.CryptAction action)
		{
			FileStream Input = new FileStream(path, FileMode.Open, FileAccess.Read);
			FileStream Output = new FileStream(string.Concat(path, "~"), FileMode.Create, FileAccess.Write);
			CryptoStream csCryptoStream = null;
			try
			{
				try
				{
					Output.SetLength((long)0);
					byte[] bytBuffer = new byte[4096];
					long lngBytesProcessed = (long)0;
					long lngFileLength = Input.Length;
					int intBytesInCurrentBlock = 0;
					RijndaelManaged cspRijndael = new RijndaelManaged();
					switch (action)
					{
						case Security.CryptAction.encrypt:
						{
							csCryptoStream = new CryptoStream(Output, cspRijndael.CreateEncryptor(Security.secKey, Security.secIV), CryptoStreamMode.Write);
							break;
						}
						case Security.CryptAction.decrypt:
						{
							csCryptoStream = new CryptoStream(Output, cspRijndael.CreateDecryptor(Security.secKey, Security.secIV), CryptoStreamMode.Write);
							break;
						}
					}
					while (lngBytesProcessed < lngFileLength)
					{
						intBytesInCurrentBlock = Input.Read(bytBuffer, 0, 4096);
						csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock);
						lngBytesProcessed = lngBytesProcessed + (long)intBytesInCurrentBlock;
					}
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				csCryptoStream.Close();
				Input.Close();
				Output.Close();
			}
		}

		public static string Decrypt(string cipherText)
		{
			string passPhrase = "NewTerraPassPhrase";
			string saltValue = "5@1tV41u3";
			string hashAlgorithm = "SHA1";
			int passwordIterations = 2;
			string initVector = "f662vl3K4vhbfw98";
			int keySize = 256;
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged()
			{
				Mode = CipherMode.CBC
			};
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[(int)cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, (int)plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
		}

		public static string Encrypt(string plainText)
		{
			string passPhrase = "NewTerraPassPhrase";
			string saltValue = "5@1tV41u3";
			string hashAlgorithm = "SHA1";
			int passwordIterations = 2;
			string initVector = "f662vl3K4vhbfw98";
			int keySize = 256;
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged()
			{
				Mode = CipherMode.CBC
			};
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, (int)plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(cipherTextBytes);
		}

		public static MemoryStream StreamCrypt(Stream Input, Security.CryptAction action)
		{
			MemoryStream memoryStream;
			MemoryStream Output = new MemoryStream();
			CryptoStream csCryptoStream = null;
			try
			{
				try
				{
					Output.SetLength((long)0);
					byte[] bytBuffer = new byte[4096];
					long lngBytesProcessed = (long)0;
					long lngFileLength = Input.Length;
					int intBytesInCurrentBlock = 0;
					RijndaelManaged cspRijndael = new RijndaelManaged();
					switch (action)
					{
						case Security.CryptAction.encrypt:
						{
							csCryptoStream = new CryptoStream(Output, cspRijndael.CreateEncryptor(Security.secKey, Security.secIV), CryptoStreamMode.Write);
							break;
						}
						case Security.CryptAction.decrypt:
						{
							csCryptoStream = new CryptoStream(Output, cspRijndael.CreateDecryptor(Security.secKey, Security.secIV), CryptoStreamMode.Write);
							break;
						}
					}
					while (lngBytesProcessed < lngFileLength)
					{
						intBytesInCurrentBlock = Input.Read(bytBuffer, 0, 4096);
						csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock);
						lngBytesProcessed = lngBytesProcessed + (long)intBytesInCurrentBlock;
					}
					memoryStream = Output;
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
			}
			return memoryStream;
		}

		public enum CryptAction
		{
			encrypt,
			decrypt
		}
	}
}