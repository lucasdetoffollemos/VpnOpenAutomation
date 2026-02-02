using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpnOpenAutomation;

namespace VpnOpenAutomationTests.UnitTests
{
    public class VpnServiceUnitTests
    {
        public Mock<ICredentialManager> _credentialManager;
        public VpnService _vpnService;
        public VpnServiceUnitTests()
        {
            _credentialManager = new Mock<ICredentialManager>();
            _vpnService = new VpnService(_credentialManager.Object);

        }

        #region setcredentials tests
        [Fact]
        public void VpnCredentials_InputCorrect_ShouldPass()
        {
            //arrange
            var userName = "teste";
            var password = "teste";

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            bool result = _vpnService.SetCredentials(userName, password);
            //assert

            Assert.True(result);
        }

        [Fact]
        public void VpnCredentials_InputUserNull_ShouldFail()
        {
            //arrange
            string? userName = null;
            var password = "teste";

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_InputUserEmpty_ShouldFail()
        {
            //arrange
            string? userName = string.Empty;
            var password = "teste";

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_InputPasswordNull_ShouldFail()
        {
            //arrange
            var userName = "teste";
            string? password = null;

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_InputPasswordEmpty_ShouldFail()
        {
            //arrange
            var userName = "teste";
            string? password = string.Empty;

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_InputBothNull_ShouldFail()
        {
            //arrange
            string? userName = null;
            string? password = null;

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_InputBothEmpty_ShouldFail()
        {
            //arrange
            var userName = string.Empty;
            var password = string.Empty;

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            var result = _vpnService.SetCredentials(userName, password);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void VpnCredentials_FailToSaveCredential_ShouldFail()
        {
            //arrange
            var userName = "teste";
            var password = "teste";

            _credentialManager.Setup(x => x.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            //act
            bool result = _vpnService.SetCredentials(userName, password);
            //assert

            Assert.False(result);
        }

        #endregion
    }
}
