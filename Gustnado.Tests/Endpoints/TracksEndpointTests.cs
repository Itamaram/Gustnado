using System;
using System.Collections.Generic;
using System.Linq;
using Gustnado.Endpoints;
using Gustnado.Enums;
using Gustnado.Extensions;
using Gustnado.Objects;
using NUnit.Framework;

namespace Gustnado.Tests.Endpoints
{
    public class TracksEndpointTests
    {
        private static List<Track> ExecuteSearch(TracksRequestFilter filter)
        {
            return SoundCloudApi.Tracks.Get(filter, 20).Execute(new TestClient()).Take(20).ToList();
        }

        [TestCase("best")]
        public void FilterQ(string q)
        {
            ExecuteSearch(new TracksRequestFilter {Q = q})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track =>
                {
                    var fields = track.GetAllStringPropertiesValues().Concat(track.User.GetAllStringPropertiesValues());
                    StringAssert.Contains(q, fields);
                });
        }

        [TestCase("edm")]
        [TestCase("chill", "dope")]
        public void FilterTags(params string[] tags)
        {
            ExecuteSearch(new TracksRequestFilter {Tags = tags.ToList()})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track =>
                {
                    Assert.True(tags.Any(t => track.Tags.Contains(t, StringComparison.OrdinalIgnoreCase)),
                        $"'{track.Tags}' did not contain any of '{string.Join(", ", tags)}'");
                });
        }

        [TestCase(TrackVisibility.All)]
        [TestCase(TrackVisibility.Public)]
        public void FilterFilter(TrackVisibility visibility)
        {
            ExecuteSearch(new TracksRequestFilter {Filter = visibility})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.AreEqual(TrackVisibility.Public, track.Sharing));
        }

        //todo: Add a less silly test which includes auth and an actually private track
        [Test]
        public void FilterPrivate()
        {
            ExecuteSearch(new TracksRequestFilter {Filter = TrackVisibility.Private})
                .Do(CollectionAssert.IsEmpty);
        }

        [TestCase(License.NoRightsReserved, Ignore = "API returns 503")]
        [TestCase(License.AllRightsReserved, Ignore = "API returns 503")]
        [TestCase(License.AttributionNonCommercial, Ignore = "API returns 503")]
        [TestCase(License.Attribution)]
        [TestCase(License.AttributionNoDerivatives)]
        [TestCase(License.AttributionShareAlike)]
        [TestCase(License.AttributionNonCommercialNoDerivatives)]
        [TestCase(License.AttributionNonCommercialShareAlike)]
        public void FilterLicense(License license)
        {
            ExecuteSearch(new TracksRequestFilter {License = license})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.AreEqual(license, track.License));
        }

        [TestCase(120)]
        public void FilterBpmMin(int min)
        {
            ExecuteSearch(new TracksRequestFilter {BpmFrom = min})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.LessOrEqual(min, track.BPM));
        }

        [TestCase(60)]
        public void FilterBpmMax(int max)
        {
            ExecuteSearch(new TracksRequestFilter {BpmTo = max})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.GreaterOrEqual(max, track.BPM));
        }

        [TestCase(60 * 60 * 1000)]
        public void FilterDurationMin(int min)
        {
            ExecuteSearch(new TracksRequestFilter {DurationFrom = min})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.LessOrEqual(min, track.Duration));
        }

        [TestCase(5 * 1000)]
        public void FilterDurationMax(int max)
        {
            ExecuteSearch(new TracksRequestFilter {DurationTo = max})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.GreaterOrEqual(max, track.Duration));
        }

        [TestCase("1.00:00:00")]
        public void FilterCreatedAtFrom(string span)
        {
            var min = DateTime.Now - TimeSpan.Parse(span);

            ExecuteSearch(new TracksRequestFilter {CreateAtFrom = min})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.LessOrEqual(min, track.CreatedAt));
        }

        [TestCase("365.00:00:00")]
        public void FilterDurationTo(string span)
        {
            var max = DateTime.Now - TimeSpan.Parse(span);

            ExecuteSearch(new TracksRequestFilter {CreatedAtTo = max})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => Assert.GreaterOrEqual(max, track.CreatedAt));
        }

        [TestCase(215615250)]
        public void FilterIds(params int[] ids)
        {
            ExecuteSearch(new TracksRequestFilter {Ids = ids.ToList()})
                .Do(tracks => CollectionAssert.AreEquivalent(ids, tracks.Select(t => t.Id)));
        }

        [TestCase("edm")]
        //[TestCase("rap", "hiphop")] bug: Another SC API documentation lie?
        public void FilterGenres(params string[] genres)
        {
            ExecuteSearch(new TracksRequestFilter {Genres = genres.ToList()})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => StringAssert.Contains(track.Genre, genres));
        }

        [TestCase(TrackType.Original)]
        [TestCase(TrackType.Remix)]
        [TestCase(TrackType.Live)]
        [TestCase(TrackType.Recording)]
        [TestCase(TrackType.Spoken, Ignore = "API returns tracks with null TrackType")]
        [TestCase(TrackType.Podcast)]
        [TestCase(TrackType.Demo)]
        [TestCase(TrackType.InProgress)]
        [TestCase(TrackType.Stem, Ignore = "API returns 503")]
        [TestCase(TrackType.Loop)]
        [TestCase(TrackType.SoundEffect, Ignore = "API returns 503")]
        [TestCase(TrackType.Sample, Ignore = "API returns 503")]
        [TestCase(TrackType.Other)]
        [TestCase(TrackType.Original, TrackType.Recording)]
        public void FilterTypes(params TrackType[] types)
        {
            ExecuteSearch(new TracksRequestFilter {Types = types.ToList()})
                .Do(CollectionAssert.IsNotEmpty)
                .ForEach(track => CollectionAssert.Contains(types, track.TrackType));
        }

        private static readonly TrackEndpoint moist = SoundCloudApi.Tracks[Constants.MoistId];

        [Test]
        public void GetTrackById()
        {
            var track = moist.Get().Execute(new TestClient());
            //todo: deserialisation tests. Potentially here. Possibly not here.
            Assert.AreEqual(Constants.MoistId, track.Id);
        }

        [Test]
        public void GetTrackComments()
        {
            var comments = moist.Comments.Get().Execute(new TestClient());
            CollectionAssert.Contains(comments.Select(c => c.Id), 328736442);
        }

        [Test]
        public void GetTrackCommentById()
        {
            var comment = moist.Comments[328736442].Get().Execute(new TestClient());
            Assert.AreEqual(328736442, comment.Id);
        }

        [Test]
        public void GetTrackFavoriters()
        {
            var favoriters = moist.Favoriters.Get().Execute(new TestClient());
            CollectionAssert.Contains(favoriters.Select(f => f.Id), Constants.ItamarId);
        }

        [Test]
        [Ignore("Client not handling 303 redirect correctly")]
        public void GetTrackFavoriterById()
        {
            var favoriter = moist.Favoriters[Constants.ItamarId].Get().Execute(new TestClient());
            Assert.AreEqual(Constants.ItamarId, favoriter.Id);
        }

        //todo comment on a track goes here. But not uploading or favoriting?

        //todo SecretToken. Here? Or only on /Me?
    }
}