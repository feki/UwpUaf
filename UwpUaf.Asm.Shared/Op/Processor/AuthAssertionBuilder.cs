using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.AuthenticatorCharacteristics;
using Fido.Uaf.Shared.Tlv;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared.Op.Processor
{
    internal class AuthAssertionBuilder
    {
        private IAuthenticator authenticator;
        private IBuffer publicKey;
        private IBuffer signedChallenge;

        public AuthAssertionBuilder(IAuthenticator authenticator, IBuffer publicKey, IBuffer signedChallenge)
        {
            this.authenticator = authenticator;
            this.publicKey = publicKey;
            this.signedChallenge = signedChallenge;
        }

        internal async Task<string> GetAssertions()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    var regAssertion = await GetAuthAssertionAsync();

                    bw.Write(EncodeInt((int)TagTypes.TagUafv1AuthAssertion));
                    bw.Write(EncodeInt(regAssertion.Length));
                    bw.Write(regAssertion);
                }

                //return Base64 url save encoded string of s.ToArray()
                return Convert.ToBase64String(s.ToArray())
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
            }
        }

        private async Task<byte[]> GetAuthAssertionAsync()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    bw.Write(EncodeInt((int)TagTypes.TagUafv1Krd));
                    var signedDataValue = GetSignedData();
                    bw.Write(EncodeInt(signedDataValue.Length));
                    bw.Write(signedDataValue);

                    bw.Write(EncodeInt((int)TagTypes.TagSignature));
                    var value = await GetSignatureAsync(signedDataValue);
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);
                }

                return s.ToArray();
            }
        }

        private byte[] GetSignedData()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    byte[] value;
                    int length;

                    bw.Write(EncodeInt((int)TagTypes.TagAaid));
                    value = GetAaid();
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagAssertionInfo));
                    // 2 bytes - Vendor assigned authenticator version; 1 byte Authentication Mode; 2 bytes Sig Alg;
                    //value = new byte[] { 0x00, 0x00, 0x01, 0x01, 0x00 };
                    using (var m = new MemoryStream())
                    {
                        using (var w = new BinaryWriter(m))
                        {
                            // 2 bytes - Vendor assigned authenticator version;
                            w.Write(new byte[] { 0x00, 0x00 });
                            // 1 byte Authentication Mode;
                            w.Write((byte)0x01);
                            w.Write(EncodeInt((int)AuthenticationAlgorithms.UafAlgSignSecp256r1EcdsaSha256Der));
                        }

                        value = m.ToArray();
                    }
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    //TODO: nonce

                    bw.Write(EncodeInt((int)TagTypes.TagFinalChallenge));
                    //value = GetFcSha256Hash(fcParams);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagKeyId));
                    value = Encoding.UTF8.GetBytes(authenticator.KeyId);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagCounters));
                    //value = GetCounters();
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagPubKey));
                    //value = GetPubKeyRawBytes();
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);
                }

                return s.ToArray();
            }
        }

        private byte[] GetAaid()
        {
            return Encoding.ASCII.GetBytes(this.authenticator.Aaid);
        }

        private async Task<byte[]> GetSignatureAsync(byte[] signedDataValue)
        {
            var signature = await authenticator.SignAsync(CryptographicBuffer.CreateFromByteArray(signedDataValue));
            byte[] ret;
            CryptographicBuffer.CopyToByteArray(signature, out ret);

            return ret;
        }

        private static byte[] EncodeInt(int id)
        {
            var bytes = new byte[2];
            bytes[0] = (byte)(id & 0x00ff);
            bytes[1] = (byte)((id & 0xff00) >> 8);

            return bytes;
        }
    }
}
