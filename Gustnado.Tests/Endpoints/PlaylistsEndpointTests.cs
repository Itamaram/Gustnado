using System.Linq;
using Gustnado.Endpoints;
using Gustnado.Extensions;
using NUnit.Framework;

namespace Gustnado.Tests.Endpoints
{
    public class PlaylistsEndpointTests
    {
        [Test]
        public void GetPlaylists()
        {
            SoundCloudApi.Playlists.Get(10).Execute(new TestClient()).Take(11)
                .Do(playlists => Assert.AreEqual(11, playlists.Count()));
        }

        [Test]
        public void GetCompactPlaylists()
        {
            SoundCloudApi.Playlists.Get(PlaylistRepresentation.Compact, 10).Execute(new TestClient()).Take(10).ToList()
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(playlist => Assert.IsNull(playlist.Tracks));
        }

        [Test]
        public void GetTrackIdsOnlyPlaylists()
        {
            SoundCloudApi.Playlists.Get(PlaylistRepresentation.Id, 10).Execute(new TestClient()).Take(10).ToList()
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(playlist => playlist.Tracks.ForEach(track =>
                {
                    Assert.IsNotNull(track.Id);
                    CollectionAssert.IsEmpty(track.GetAllStringPropertiesValues());
                }));
        }

        [TestCase("best")]
        public void GetFilteredPlaylists(string q)
        {
            SoundCloudApi.Playlists.Get(q, 10).Execute(new TestClient()).Take(10).ToList()
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(playlist => StringAssert.Contains(q, playlist.GetAllStringPropertiesValues()));
        }

        [TestCase(205510527)]
        public void GetPlaylistById(int id)
        {
            SoundCloudApi.Playlists[id].Get().Execute(new TestClient())
                .Do(playlist => Assert.AreEqual(id, playlist.Id));
        }
    }
}