using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Monads;
using Gustnado.Endpoints;
using Gustnado.RestSharp;
using NUnit.Framework;
using Gustnado.Extensions;

namespace Gustnado.Tests.Endpoints
{
    public class UsersEndpointTests
    {
        private static readonly SoundCloudHttpClient client = new TestClient();

        [Test]
        public void GetUsersTest()
        {
            var users = SoundCloudApi.Users.Get(50).Execute(client).Take(50).ToList();

            Assert.AreEqual(50, users.Count);
            CollectionAssert.AllItemsAreNotNull(users);
        }

        [Test]
        public void GetFilteredUsers()
        {
            var users = SoundCloudApi.Users.Get("cool", 50).Execute(client).Take(50).ToList();

            Assert.AreEqual(50, users.Count);
            foreach (var user in users)
                StringAssert.Contains("cool", user.GetAllStringPropertiesValues());
        }

        [Test]
        public void GetUserById()
        {
            var gustnado = SoundCloudApi.Users[Constants.GustnadoId].Get().Execute(client);
            Assert.AreEqual(Constants.GustnadoId, gustnado.Id);
        }

        [Test]
        public void GetUserTracks()
        {
            var tracks = SoundCloudApi.Users[Constants.ItamarId].Tracks.Get().Execute(client);
            CollectionAssert.Contains(tracks.Select(t => t.Title), "Moist 24 Samples 60BPM");
        }

        [Test]
        public void GetUserPlaylists()
        {
            var playlists = SoundCloudApi.Users[Constants.ItamarId].Playlists.Get().Execute(client);
            CollectionAssert.Contains(playlists.Select(t => t.Title), "TnZ Assbackwards");
        }

        [Test]
        public void GetUserFollowings()
        {
            var users = SoundCloudApi.Users[Constants.ItamarId].Followings.Get().Execute(client);
            CollectionAssert.Contains(users.Select(u => u.Id), Constants.GustnadoId);
        }
        
        [Test]
        [Ignore("Client not handling 303 redirect correctly")]
        public void GetUserFollowingById()
        {
            var user = SoundCloudApi.Users[Constants.ItamarId].Followings[Constants.GustnadoId].Get().Execute(client);
            Assert.AreEqual(Constants.GustnadoId, user.Id);
        }

        [Test]
        public void GetUserFollowers()
        {
            var users = SoundCloudApi.Users[Constants.ItamarId].Followers.Get().Execute(client);
            CollectionAssert.Contains(users.Select(u => u.Id), Constants.GustnadoId);
        }
        
        [Test]
        [Ignore("Client not handling 303 redirect correctly")]
        public void GetUserFollowerById()
        {
            var user = SoundCloudApi.Users[Constants.ItamarId].Followers[Constants.GustnadoId].Get().Execute(client);
            Assert.AreEqual(Constants.GustnadoId, user.Id);
        }

        [Test]
        public void GetUserComments()
        {
            var comments = SoundCloudApi.Users[Constants.ItamarId].Comments.Get().Execute(client);
            CollectionAssert.Contains(comments.Select(c => c.Body), "This track makes me moist");
        }

        [Test]
        public void GetUserFavorites()
        {
            var tracks = SoundCloudApi.Users[Constants.ItamarId].Favorites.Get().Execute(client);
            CollectionAssert.Contains(tracks.Select(t => t.Id), 215615250);
        }

        [Test]
        public void GetUserFavoriteById()
        {
            var track = SoundCloudApi.Users[Constants.ItamarId].Favorites[215615250].Get().Execute(client);
            Assert.AreEqual(215615250, track.Id);
        }

        //webprofiles
    }

    public class TestClient : SoundCloudHttpClient
    {
        public TestClient()
            : base(EnvironmentVariables.ClientId, EnvironmentVariables.ClientSecret, new GustnadoRestClient())
        {
        }

        public TestClient Authenticate()
        {
            Authenticate(EnvironmentVariables.Username, EnvironmentVariables.Password);
            return this;
        }
    }

    public static class StringAssert
    {
        public static void Contains(string expected, IEnumerable<string> caucuses) => Contains(expected, caucuses.ToArray());

        public static void Contains(string expected, params string[] caucuses)
        {
            caucuses
                .Where(c => c.Contains(expected, StringComparison.OrdinalIgnoreCase))
                .FirstOrNone()
                .WhenNone(() => Assert.Fail($"Couldn't find substring '{expected}' in any of {string.Join(", ", caucuses.Select(c => $"'{c}'"))}"));
        }
    }
}