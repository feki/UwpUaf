using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.AuthenticatorCharacteristics;
using Fido.Uaf.Shared.Tlv;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared.Op.Processor
{
    public class ReqAssertionBuilder
    {
        private readonly IAuthenticator authenticator;
        private readonly IBuffer publicKeyBuffer;
        private readonly IBuffer fcParams;

        public ReqAssertionBuilder(IAuthenticator authenticator, IBuffer publicKeyBuffer, IBuffer fcParams)
        {
            this.authenticator = authenticator;
            this.publicKeyBuffer = publicKeyBuffer;
            this.fcParams = fcParams;
        }

        public async Task<string> GetAssertions()
        {
            using (var s = new MemoryStream()) 
            {
                using (var bw = new BinaryWriter(s))
                {
                    var regAssertion = await GetRegAssertionAsync();

                    bw.Write(EncodeInt((int)TagTypes.TagUafv1RegAssertion));
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

        private async Task<byte[]> GetRegAssertionAsync()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    bw.Write(EncodeInt((int)TagTypes.TagUafv1Krd));
                    var signedDataValue = GetSignedData();
                    bw.Write(EncodeInt(signedDataValue.Length));
                    bw.Write(signedDataValue);

                    bw.Write(EncodeInt((int)TagTypes.TagAttestationBasicFull));
                    var value = await GetAttestationBasicFullAsync(signedDataValue);
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
                    // 2 bytes - Vendor assigned authenticator version; 1 byte Authentication Mode; 2 bytes Sig Alg; 2 bytes Pub Key Alg
                    //value = new byte[] { 0x00, 0x00, 0x01, 0x01, 0x00, 0x00, 0x01 };
                    using (var m = new MemoryStream())
                    {
                        using (var w = new BinaryWriter(m))
                        {
                            // 2 bytes - Vendor assigned authenticator version;
                            w.Write(new byte[] { 0x00, 0x00 });
                            // 1 byte Authentication Mode;
                            w.Write((byte)0x01);
                            w.Write(EncodeInt((int)AuthenticationAlgorithms.UafAlgSignSecp256r1EcdsaSha256Der));
                            w.Write(EncodeInt((int)PublicKeyRepresentationFormats.UafAlgKeyEccX962Der));
                        }

                        value = m.ToArray();
                    }
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagFinalChallenge));
                    value = GetFcSha256Hash(fcParams);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagKeyId));
                    value = Encoding.UTF8.GetBytes(authenticator.KeyId);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagCounters));
                    value = GetCounters();
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagPubKey));
                    value = GetPubKeyRawBytes();
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);
                }

                return s.ToArray();
            }
        }

        private byte[] GetPubKeyRawBytes()
        {
            byte[] ret;
            CryptographicBuffer.CopyToByteArray(publicKeyBuffer, out ret);

            return ret;
        }

        private byte[] GetCounters()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    bw.Write(EncodeInt(0));
                    bw.Write(EncodeInt(1));
                    bw.Write(EncodeInt(0));
                    bw.Write(EncodeInt(1));
                }

                return s.ToArray();
            }
        }

        private byte[] GetFcSha256Hash(IBuffer fcParams)
        {
            var objHash = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256).CreateHash();
            objHash.Append(fcParams);
            var hashBuffer = objHash.GetValueAndReset();
            byte[] ret;
            CryptographicBuffer.CopyToByteArray(hashBuffer, out ret);

            return ret;
        }

        private byte[] GetAaid()
        {
            return Encoding.ASCII.GetBytes(this.authenticator.Aaid);
        }

        private async Task<byte[]> GetAttestationBasicFullAsync(byte[] signedDataValue)
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    byte[] value;
                    int length;

                    bw.Write(EncodeInt((int)TagTypes.TagSignature));
                    value = await GetSignatureAsync(signedDataValue);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagAttestationCert));
                    var buffer = authenticator.GetCertificate();
                    CryptographicBuffer.CopyToByteArray(buffer, out value);
                    length = value.Length;
                    bw.Write(EncodeInt(length));
                    bw.Write(value);
                }

                return s.ToArray();
            }
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
