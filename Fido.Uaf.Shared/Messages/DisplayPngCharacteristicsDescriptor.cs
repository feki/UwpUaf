using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public class DisplayPngCharacteristicsDescriptor
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("width", Required = Required.Always)]
        public ulong Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("height", Required = Required.Always)]
        public ulong Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("bitDepth", Required = Required.Always)]
        public string BitDepth { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("colorType", Required = Required.Always)]
        public string ColorType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("compression", Required = Required.Always)]
        public string Compression { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("filter", Required = Required.Always)]
        public string Filter { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("interlace", Required = Required.Always)]
        public string Interlace { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("plte", NullValueHandling = NullValueHandling.Ignore)]
        RgbPalletteEntry[] RgbPallette { get; set; }
    }
}