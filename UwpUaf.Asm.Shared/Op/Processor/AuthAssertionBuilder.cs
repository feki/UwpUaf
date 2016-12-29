using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Tlv;
using Org.BouncyCastle.Security;
using UwpUaf.Shared;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared.Op.Processor
{
    class AuthAssertionBuilder
    {
        readonly IAuthenticator authenticator;
        readonly IBuffer fcParams;

        public AuthAssertionBuilder(IAuthenticator authenticator, IBuffer fcParams)
        {
            this.authenticator = authenticator;
            this.fcParams = fcParams;
        }

        byte[] Counters
        {
            get
            {
                using (var s = new MemoryStream())
                {
                    using (var bw = new BinaryWriter(s))
                    {
                        bw.Write(EncodeInt(0));
                        bw.Write(EncodeInt(1));
                    }

                    return s.ToArray();
                }
            }
        }

        internal async Task<string> GetAssertionsAsync()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    var authAssertion = await GetAuthAssertionAsync();

                    bw.Write(EncodeInt((int)TagTypes.TagUafv1AuthAssertion));
                    bw.Write(EncodeInt(authAssertion.Length));
                    bw.Write(authAssertion);
                }

                //return Base64 url save encoded string of s.ToArray()
                var base64UrlString = s.ToArray().ConvertToBase64UrlString();
                return base64UrlString;
            }
        }

        static byte[] EncodeInt(int id)
        {
            var bytes = new byte[2];
            bytes[0] = (byte)(id & 0x00ff);
            bytes[1] = (byte)((id & 0xff00) >> 8);

            return bytes;
        }

        static byte[] GenerateNonce()
        {
            var nonce = new byte[32];

            var random = new SecureRandom();
            random.NextBytes(nonce);

            return nonce;
        }

        static byte[] GetFcSha256Hash(IBuffer fcp)
        {
            var objHash = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256).CreateHash();
            objHash.Append(fcp);
            var hashBuffer = objHash.GetValueAndReset();
            byte[] ret;
            CryptographicBuffer.CopyToByteArray(hashBuffer, out ret);

            return ret;
        }

        byte[] GetAaid()
        {
            return Encoding.ASCII.GetBytes(this.authenticator.Aaid);
        }

        async Task<byte[]> GetAuthAssertionAsync()
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

        async Task<byte[]> GetSignatureAsync(byte[] signedDataValue)
        {
            var signature = await authenticator.SignAsync(CryptographicBuffer.CreateFromByteArray(signedDataValue));
            byte[] ret;
            CryptographicBuffer.CopyToByteArray(signature, out ret);

            return ret;
        }

        byte[] GetSignedData()
        {
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    byte[] value;

                    bw.Write(EncodeInt((int)TagTypes.TagAaid));
                    value = GetAaid();
                    bw.Write(EncodeInt(value.Length));
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
                            w.Write(EncodeInt((int)authenticator.GetAuthenticatorInfo().AuthenticationAlgorithm));
                        }

                        value = m.ToArray();
                    }
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagAuthenticatorNonce));
                    value = GenerateNonce();
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagFinalChallenge));
                    value = GetFcSha256Hash(fcParams);
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagTransactionContentHash));
                    bw.Write(EncodeInt(0));

                    bw.Write(EncodeInt((int)TagTypes.TagKeyId));
                    value = Encoding.UTF8.GetBytes(authenticator.KeyId);
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);

                    bw.Write(EncodeInt((int)TagTypes.TagCounters));
                    value = Counters;
                    bw.Write(EncodeInt(value.Length));
                    bw.Write(value);
                }

                return s.ToArray();
            }
        }
    }
}
