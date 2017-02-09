using System.Collections.Generic;
using System.Linq;
using Gustnado.Endpoints;
using Gustnado.Extensions;
using Gustnado.Objects;
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

        private static readonly SoundCloudHttpClient authed = new TestClient().Authenticate();
        private const string Name = "Gustnado Test Playlist";

        private static void AssertPlaylistTracks(Playlist playlist, params int[] ids)
        {
            CollectionAssert.AreEqual(ids, playlist.Tracks.Select(t => t.Id));
        }

        private static Playlist GetTestPlaylist()
        {
            return SoundCloudApi.Me.Playlists.Get().Execute(authed).First(p => p.Title == Name);
        }

        [Test, Order(0)]
        public void CreatePlaylist()
        {
            SoundCloudApi.Playlists.Create(new Playlist
            {
                Title = Name,
                Tracks = new List<Track>
                    {
                        new Track {Id = Constants.MoistId}
                    }
            }).Execute(authed)
                .Do(playlist => Assert.IsNotNull(playlist.Id))
                .Do(playlist => AssertPlaylistTracks(playlist, Constants.MoistId));

            GetTestPlaylist()
                .Do(playlist => AssertPlaylistTracks(playlist, Constants.MoistId));
        }

        [Test, Order(1)]
        public void UpdatePlaylist()
        {
            GetTestPlaylist()
                .Map(p => SoundCloudApi.Playlists[p.Id.Value].Update(new Playlist
                {
                    Tracks = new List<Track>
                    {
                        new Track {Id = 54826163},
                        new Track {Id = Constants.MoistId}
                    }
                }))
                .Execute(authed)
                .Do(playlist => AssertPlaylistTracks(playlist, 54826163, Constants.MoistId));

            GetTestPlaylist()
                .Do(playlist => AssertPlaylistTracks(playlist, 54826163, Constants.MoistId));
        }

        [Test, Order(2)]
        public void DeletePlaylist()
        {
            GetTestPlaylist()
                .Map(playlist => SoundCloudApi.Playlists[playlist.Id.Value].Delete())
                .Execute(authed)
                .Do(r => Assert.AreEqual("202 - Accepted", r.Status));

            //We do not assert the playlist no longer exist as we cannot guarentee when the request will be processed
        }
    }
}