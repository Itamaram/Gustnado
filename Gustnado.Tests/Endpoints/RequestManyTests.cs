using System.Linq;
using Gustnado.Endpoints;
using NUnit.Framework;

namespace Gustnado.Tests.Endpoints
{
    public class RequestManyTests
    {
        [Test]
        public void PaginationTest()
        {
            var users = SoundCloudApi.Users.Get(10).Execute(new TestClient()).Take(31).ToList();
            Assert.AreEqual(31, users.Count);
            CollectionAssert.AllItemsAreNotNull(users);
        }
    }
}