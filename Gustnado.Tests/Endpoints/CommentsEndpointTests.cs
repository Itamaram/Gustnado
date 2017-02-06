using System.Linq;
using Gustnado.Endpoints;
using Gustnado.Extensions;
using NUnit.Framework;

namespace Gustnado.Tests.Endpoints
{
    public class CommentsEndpointTests
    {
        [Test]
        public void GetComments()
        {
            SoundCloudApi.Comments.Get().Execute(new TestClient()).Take(50).ToList()
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(comment => Assert.NotNull(comment.Id));
        }

        [TestCase(328736442)]
        public void GetCommentById(int id)
        {
            SoundCloudApi.Comments[id].Get().Execute(new TestClient())
                .Do(comment => Assert.AreEqual(id, comment.Id));
        }
    }
}