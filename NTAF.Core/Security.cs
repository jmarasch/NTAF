/*
 * Coded in part in SharpDevelop
 * 
 * Code by: Jakob Marasch
 * 		 newterrastudios@sbcglobal.net
 * Date:    8/18/2009
 * Time:    3:36 PM
 * Project: NewTerra 
 * 
 * code in this file was modified from sample code obtained from...
 * 
 * http://www.obviex.com/Legal.aspx#Samples
 * Code Samples 
 * This Site contains code samples written by Obviex™, as well as links to code samples available from third parties.
 * All code samples that are provided or referenced on this Site are property of their respective owners. You may use
 * code samples copyrighted by Obviex™ without asking for explicit permission, provided that You leave the original
 * copyright notice in the source code. Obviex™ is not responsible, accountable, or liable for any damages, problems,
 * or difficulties resulting from use of the code samples available or referenced on this Site. 
 * 
 * Copyright (C) 2002 Obviex(TM). All rights reserved.
 * Symmetric key encryption and decryption using Rijndael algorithm.
 * http://www.obviex.com/samples/Encryption.aspx
 * 
 * sample code accessed: Tuesday, August 18 2009, 3:30pm CST
 */
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NTAF.Core {
    /// <summary>
    /// Description of Security.
    /// </summary>
    public static class Security {
        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt( string plainText ) {
            string   passPhrase         = "NewTerraPassPhrase"; // can be any string
            string   saltValue          = "5@1tV41u3";          // can be any string
            string   hashAlgorithm      = "SHA1";               // can be "MD5"
            int      passwordIterations = 2;                    // can be any number
            string   initVector         = "f662vl3K4vhbfw98";   // must be 16 bytes
            int      keySize            = 256;                  // can be 192 or 128

            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes( initVector );
            byte[] saltValueBytes  = Encoding.ASCII.GetBytes( saltValue );

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes  = Encoding.UTF8.GetBytes( plainText );

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations );

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes( keySize / 8 );

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream( memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write );
            // Start encrypting.
            cryptoStream.Write( plainTextBytes, 0, plainTextBytes.Length );

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String( cipherTextBytes );

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public static string Decrypt( string cipherText ) {
            string   passPhrase         = "NewTerraPassPhrase"; // can be any string
            string   saltValue          = "5@1tV41u3";          // can be any string
            string   hashAlgorithm      = "SHA1";               // can be "MD5"
            int      passwordIterations = 2;                    // can be any number
            string   initVector         = "f662vl3K4vhbfw98";   // must be 16 bytes
            int      keySize            = 256;                  // can be 192 or 128
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes( initVector );
            byte[] saltValueBytes  = Encoding.ASCII.GetBytes( saltValue );

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String( cipherText );

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations );

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes( keySize / 8 );

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged    symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream  memoryStream = new MemoryStream( cipherTextBytes );

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream  cryptoStream = new CryptoStream( memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read );

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read( plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length );

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString( plainTextBytes,
                                                       0,
                                                       decryptedByteCount );

            // Return decrypted string.   
            return plainText;
        }

        private static byte[] secKey {
            get {
                byte[] bytSalt = System.Text.Encoding.ASCII.GetBytes( "NewTerraGames" );
                PasswordDeriveBytes pdb = new PasswordDeriveBytes( "NewTerraStudios", bytSalt );

                return pdb.GetBytes( 32 );
            }
        }

        private static byte[] secIV {
            get {
                byte[] bytSalt = System.Text.Encoding.ASCII.GetBytes( "NewTerraGames" );
                PasswordDeriveBytes pdb = new PasswordDeriveBytes( "NewTerraStudios", bytSalt );

                return pdb.GetBytes( 16 );
            }
        }


        public enum CryptAction {
            encrypt,
            decrypt
        }
        //new file encryptor with keys
        public static void cryptFile( string path, CryptAction action ) {
            FileStream Input = new FileStream( path, FileMode.Open, FileAccess.Read );
            FileStream Output = new FileStream( path + "~", FileMode.Create, FileAccess.Write );
            CryptoStream csCryptoStream = null;
            try {
                Output.SetLength( 0 );

                byte[] bytBuffer = new byte[4096];

                long lngBytesProcessed = 0;

                long lngFileLength = Input.Length;

                int intBytesInCurrentBlock = 0;

                RijndaelManaged cspRijndael = new RijndaelManaged();

                switch ( action ) {
                    case CryptAction.encrypt:
                        csCryptoStream = new CryptoStream( Output, cspRijndael.CreateEncryptor( secKey, secIV ), CryptoStreamMode.Write );
                        break;
                    case CryptAction.decrypt:
                        csCryptoStream = new CryptoStream( Output, cspRijndael.CreateDecryptor( secKey, secIV ), CryptoStreamMode.Write );
                        break;
                }

                while ( lngBytesProcessed < lngFileLength ) {
                    intBytesInCurrentBlock = Input.Read( bytBuffer, 0, 4096 );
                    csCryptoStream.Write( bytBuffer, 0, intBytesInCurrentBlock );
                    lngBytesProcessed += ( long )intBytesInCurrentBlock;
                }
            }
            catch ( Exception ex ) { throw ex; }
            finally {
                csCryptoStream.Close();
                Input.Close();
                Output.Close();
            }
        }

        //new file encryptor with keys
        //public static void cryptFile( string path, CryptAction action )
        public static MemoryStream StreamCrypt( Stream Input, CryptAction action ) {
            //FileStream Input = new FileStream( path, FileMode.Open, FileAccess.Read );

            MemoryStream Output = new MemoryStream(); // new FileStream( path + "~", FileMode.Create, FileAccess.Write );
            CryptoStream csCryptoStream = null;
            
            try {
                Output.SetLength( 0 );

                byte[] bytBuffer = new byte[4096];

                long lngBytesProcessed = 0;

                long lngFileLength = Input.Length;

                int intBytesInCurrentBlock = 0;

                RijndaelManaged cspRijndael = new RijndaelManaged();

                switch ( action ) {
                    case CryptAction.encrypt:
                        csCryptoStream = new CryptoStream( Output, cspRijndael.CreateEncryptor( secKey, secIV ), CryptoStreamMode.Write );
                        break;
                    case CryptAction.decrypt:
                        csCryptoStream = new CryptoStream( Output, cspRijndael.CreateDecryptor( secKey, secIV ), CryptoStreamMode.Write );
                        break;
                }

                while ( lngBytesProcessed < lngFileLength ) {
                    intBytesInCurrentBlock = Input.Read( bytBuffer, 0, 4096 );
                    csCryptoStream.Write( bytBuffer, 0, intBytesInCurrentBlock );
                    lngBytesProcessed += ( long )intBytesInCurrentBlock;
                }
                return Output;
            }
            catch ( Exception ex ) { throw ex; }
            finally {
                //csCryptoStream.Close();
                //Input.Close();
                //Output.Close();
            }
        }
    }
}
