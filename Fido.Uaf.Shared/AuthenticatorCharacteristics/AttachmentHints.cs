using System;

namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The ATTACHMENT_HINT constants are flags in a bit field represented as a 32 bit long.
    /// They describe the method an authenticator uses to communicate with the FIDO User Device.
    /// These constants are reported and queried through the UAF Discovery APIs,
    /// and used to form Authenticator policies in UAF protocol messages. Because the connection
    /// state and topology of an authenticator may be transient, these values are only hints
    /// that can be used by server-supplied policy to guide the user experience,
    /// e.g. to prefer a device that is connected and ready for authenticating or confirming
    /// a low-value transaction, rather than one that is more secure but requires more user effort.
    /// </summary>
    /// <note>
    /// These flags are not a mandatory part of authenticator metadata and, when present,
    /// only indicate possible states that may be reported during authenticator discovery.
    /// </note>
    [Flags]
    public enum AttachmentHints
    {
        /// <summary>
        /// ATTACHMENT_HINT_INTERNAL 0x01
        /// 
        /// This flag MAY be set to indicate that the authenticator is permanently attached to the FIDO User Device.
        /// A device such as a smartphone may have authenticator functionality that is able to be used both locally
        /// and remotely. In such a case, the FIDO client MUST filter and exclusively report only the relevant bit
        /// during Discovery and when performing policy matching.
        /// 
        /// This flag cannot be combined with any other ATTACHMENT_HINT flags.
        /// </summary>
        AttachmentHintInternal = 0x01,

        /// <summary>
        /// ATTACHMENT_HINT_EXTERNAL 0x02
        /// 
        /// This flag MAY be set to indicate, for a hardware-based authenticator, that it is removable
        /// or remote from the FIDO User Device. A device such as a smartphone may have authenticator
        /// functionality that is able to be used both locally and remotely. In such a case,
        /// the FIDO UAF Client MUST filter and exclusively report only the relevant bit during discovery
        /// and when performing policy matching.
        /// </summary>
        AttachmentHintExternal = 0x02,

        /// <summary>
        /// ATTACHMENT_HINT_WIRED 0x04
        /// 
        /// This flag MAY be set to indicate that an external authenticator currently has an exclusive wired connection,
        /// e.g. through USB, Firewire or similar, to the FIDO User Device.
        /// </summary>
        AttachmentHintWired = 0x04,

        /// <summary>
        /// ATTACHMENT_HINT_WIRELESS 0x08
        /// 
        /// This flag MAY be set to indicate that an external authenticator communicates with the FIDO User Device
        /// through a personal area or otherwise non-routed wireless protocol, such as Bluetooth or NFC.
        /// </summary>
        AttachmentHintWireless = 0x08,

        /// <summary>
        /// ATTACHMENT_HINT_NFC 0x10
        /// 
        /// This flag MAY be set to indicate that an external authenticator is able to communicate by NFC
        /// to the FIDO User Device. As part of authenticator metadata, or when reporting characteristics
        /// through discovery, if this flag is set, the ATTACHMENT_HINT_WIRELESS flag SHOULD also be set as well.
        /// </summary>
        AttachmentHintNfc = 0x10,

        /// <summary>
        /// ATTACHMENT_HINT_BLUETOOTH 0x20
        /// 
        /// This flag MAY be set to indicate that an external authenticator is able to communicate using Bluetooth
        /// with the FIDO User Device.As part of authenticator metadata, or when reporting characteristics through
        /// discovery, if this flag is set, the ATTACHMENT_HINT_WIRELESS flag SHOULD also be set.
        /// </summary>
        AttachmentHintBluetooth = 0x20,

        /// <summary>
        /// ATTACHMENT_HINT_NETWORK 0x40
        /// 
        /// This flag MAY be set to indicate that the authenticator is connected to the FIDO User Device
        /// ver a non-exclusive network (e.g.over a TCP/IP LAN or WAN, as opposed to a PAN or point-to-point connection).
        /// </summary>
        AttachmentHintNetwork = 0x40,

        /// <summary>
        /// ATTACHMENT_HINT_READY 0x80
        /// 
        /// Thif flag MAY be set to indicate that an external authenticator is in a "ready" state.
        /// This flag is set by the ASM at its discretion.
        /// </summary>
        /// <note>
        /// Generally this should indicate that the device is immediately available to perform
        /// user verification without additional actions such as connecting the device or creating
        /// a new biometric profile enrollment, but the exact meaning may vary for different types
        /// of devices.For example, a USB authenticator may only report itself as ready when it is
        /// plugged in, or a Bluetooth authenticator when it is paired and connected,
        /// but an NFC - based authenticator may always report itself as ready.
        /// </note>
        AttachmentHintReady = 0x80,

        /// <summary>
        /// ATTACHMENT_HINT_WIFI_DIRECT 0x100
        /// 
        /// This flag MAY be set to indicate that an external authenticator is able to communicate
        /// using WiFi Direct with the FIDO User Device.As part of authenticator metadata and when
        /// reporting characteristics through discovery, if this flag is set,
        /// the ATTACHMENT_HINT_WIRELESS flag SHOULD also be set.
        /// </summary>
        AttachmentHintWifiDirect = 0x100,
    }
}
