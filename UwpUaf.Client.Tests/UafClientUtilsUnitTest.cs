using Fido.Uaf.Shared;
using Fido.Uaf.Shared.Messages;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UwpUaf.Client.Tests
{
    [TestClass]
    public class UafClientUtilsUnitTest
    {
        private UafClientUtils uafClientUtils;

        [TestInitialize]
        public void Initialize()
        {
            uafClientUtils = new UafClientUtils();
        }

        [TestMethod]
        public void GetMessageOperation_PassRegistrationMessage_ReturnsCorrectRegOperation()
        {
            var message = uafClientUtils.GetUafV10Message(Samples.RegisterRequestJson);
            Assert.AreEqual(Operation.Reg, uafClientUtils.GetMessageOperation(message));
        }

        [TestMethod]
        public void GetMessageOperation_PassAuthenticationMessage_ReturnsCorrectAuthOperation()
        {
            var message = uafClientUtils.GetUafV10Message(Samples.AuthenticationRequestJson);
            Assert.AreEqual(Operation.Auth, uafClientUtils.GetMessageOperation(message));
        }

        [TestMethod]
        public void GetMessageOperation_PassDeregistrationMessage_ReturnsCorrectDeregOperation()
        {
            var message = uafClientUtils.GetUafV10Message(Samples.DeregistrationRequestJson);
            Assert.AreEqual(Operation.Dereg, uafClientUtils.GetMessageOperation(message));
        }
    }
}
