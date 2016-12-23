using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public enum ErrorCode
    {
        /// <summary>
        /// The operation completed with no error condition encountered. Upon receipt of this code,
        /// an application should no longer expect an associated UAFResponseCallback to fire.
        /// </summary>
        NoError = 0x0,

        /// <summary>
        /// Waiting on user action to proceed. For example, selecting an authenticator in the FIDO client user interface,
        /// performing user verification, or completing an enrollment step with an authenticator.
        /// </summary>
        WaitUserAction = 0x01,

        /// <summary>
        /// window.location.protocol is not "https" or the DOM contains insecure mixed content.
        /// </summary>
        InsecureTransport = 0x02,

        /// <summary>
        /// The user declined any necessary part of the interaction to complete the registration.
        /// </summary>
        UserCancelled = 0x03,

        /// <summary>
        /// The UAFMessage does not specify a protocol version supported by this FIDO UAF Client.
        /// </summary>
        UnsupportedVersion = 0x04,

        /// <summary>
        /// No authenticator matching the authenticator policy specified in the UAFMessage is available
        /// to service the request, or the user declined to consent to the use of a suitable authenticator.
        /// </summary>
        NoSuitableAuthenticator = 0x05,

        /// <summary>
        /// A violation of the UAF protocol occurred. The interaction may have timed out;
        /// the origin associated with the message may not match the origin of the calling DOM context,
        /// or the protocol message may be malformed or tampered with.
        /// </summary>
        ProtocolError = 0x06,

        /// <summary>
        /// The client declined to process the operation because the caller's calculated facet identifier was not found
        /// in the trusted list for the application identifier specified in the request message.
        /// </summary>
        UntrustedFacetId = 0x07,

        /// <summary>
        /// The UAuth key disappeared from the authenticator and canot be restored.
        /// <note>
        /// The RP App might want to re-register the authenticator in this case.
        /// </note>
        /// </summary>
        KeyDisappearedPermanently = 0x09,

        /// <summary>
        /// The authenticator denied access to the resulting request.
        /// </summary>
        AuthenticatorAccessDenied = 0x0c,

        /// <summary>
        /// Transaction content cannot be rendered, e.g. format doesn't fit authenticator's need.
        /// <note>
        /// The transaction content format requirements are specified in the authenticator's metadata statement.
        /// </note>
        /// </summary>
        InvalidTransactionContent = 0x0d,

        /// <summary>
        /// The user took too long to follow an instruction, e.g. didn't swipe the finger within the accepted time.
        /// </summary>
        UserNotResponsive = 0x0e,

        /// <summary>
        /// Insufficient resources in the authenticator to perform the requested task.
        /// </summary>
        InsufficientAuthenticatorResources = 0x0f,

        /// <summary>
        /// The operation failed because the user is locked out and the authenticator cannot automatically trigger an action to change that.
        /// For example, an authenticator could allow the user to enter an alternative password to re-enable the use of fingerprints
        /// after too many failed finger verification attempts.
        /// This error will be reported if such method either doesn't exist or the ASM / authenticator cannot automatically trigger it.
        /// </summary>
        UserLockout = 0x10,

        /// <summary>
        /// The operation failed because the user is not enrolled to the authenticator and the authenticator cannot automatically trigger user enrollment.
        /// </summary>
        UserNotEnrolled = 0x11,

        /// <summary>
        /// An error condition not described by the above-listed codes.
        /// </summary>
        Unknown = 0xff,
    }
}
