using Gustnado.Endpoints;
using NUnit.Framework;

namespace Gustnado.Tests.Endpoints
{
    public class MeEndpointTests
    {
        private static readonly SoundCloudHttpClient client = new TestClient().Authenticate();

        [Test]
        public void GetMe()
        {
            var me = SoundCloudApi.Me.Get().Execute(client);
            Assert.AreEqual(Constants.GustnadoId, me.Id);
        }
    }
}