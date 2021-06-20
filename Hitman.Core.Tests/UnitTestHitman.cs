using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hitman.Core.Records;

namespace Hitman.Core.Tests
{
    [TestClass]
    public class HitmanTests
    {
        private readonly IConfiguration _configuration;
        private readonly Session _session;
        
        public HitmanTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<HitmanTests>();
                
            _configuration = builder.Build();

            _session = new Session(_configuration["Handle"], _configuration["Jsessionid"],
                _configuration["LiAt"]);
        }
        
        [TestMethod]
        public async Task TestSession()
        {
            using var hitman = new Hitman(_session);
            var sessionState = await hitman.IsSessionValidAsync();

            Assert.IsTrue(sessionState);
        }

        [TestMethod]
        public async Task TestGetNetworkInformation()
        {
            const string trackingUrn = "urn:li:member:251749025";

            using var hitman = new Hitman(_session);
            var willianGates = await hitman.GetNetworkInformationFromHandleAsync("williamhgates");

            Assert.IsTrue(
                willianGates.Followable &&
                willianGates.FollowingInfo.TrackingUrn == trackingUrn
            );
        }
        
        [TestMethod]
        public async Task TestGetPrivacySettings()
        {
            const string entityUrn = "urn:li:fs_privacySettings:ACoAAB7QFDgBHfmbpPTfwu1_RXh-88lhOTDvbyI";

            using var hitman = new Hitman(_session);
            var willianGatesPrivacySettings = await hitman.GetPrivacySettingsFromHandleAsync("williamhgates");

            Assert.IsTrue(
                willianGatesPrivacySettings.AllowOpenProfile &&
                willianGatesPrivacySettings.ShowPublicProfile &&
                willianGatesPrivacySettings.ShowPremiumSubscriberBadge &&
                willianGatesPrivacySettings.EntityUrn == entityUrn
            );
        }
    }
}