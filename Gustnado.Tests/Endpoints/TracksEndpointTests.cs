using System;
using System.Collections;
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
        [TestCaseSource(nameof(tests))]
        public void TrackSearch(Constraint[] constraints)
        {
            var filter = constraints.Aggregate(new TracksRequestFilter(), (f, c) =>
            {
                c.Poke(f);
                return f;
            });

            var tracks = SoundCloudApi.Tracks.Get(filter).Execute(new TestClient()).Take(50).ToList();

            CollectionAssert.IsNotEmpty(tracks);

            foreach (var track in tracks)
                foreach (var constraint in constraints)
                    constraint.AssertTrack(track);
        }

        private static readonly IEnumerable tests = new object[]
        {
            new Constraint[] {new Query("best")},
            new Constraint[] {new Tags("chill")},
            new Constraint[] {new Tags("chill", "dope")},
            new Constraint[] {new Filter(TrackVisibility.All)},
            new Constraint[] {new Filter(TrackVisibility.Public)},
            //new Constraint[] {new Filter(TrackVisibility.Private)}, obviously returns nothing
            //new Constraint[] {new LicenseConstraint(License.AllRightsReserved)}, SC API bug
            new Constraint[] {new LicenseConstraint(License.AttributionShareAlike)},
            new Constraint[] {new LicenseConstraint(License.Attribution)},
            new Constraint[] {new LicenseConstraint(License.AttributionNoDerivatives)},
            //new Constraint[] {new LicenseConstraint(License.AttributionNonCommercial)}, SC API bug
            new Constraint[] {new LicenseConstraint(License.AttributionNonCommercialNoDerivatives)},
            new Constraint[] {new LicenseConstraint(License.AttributionNonCommercialShareAlike)},
            //new Constraint[] {new LicenseConstraint(License.NoRightsReserved)}, SC API bug
            new Constraint[] {new BpmMin(120)},
            new Constraint[] {new BpmMax(60)},
            new Constraint[] {new DurationMin(60*60*1000)},
            new Constraint[] {new DurationMax(5*1000)},
            new Constraint[] {new CreatedFrom(DateTime.Now.AddDays(-1))},
            new Constraint[] {new CreatedBy(DateTime.Now.AddYears(-1))}
        };
    }

    public interface Constraint
    {
        void AssertTrack(Track track);
        void Poke(TracksRequestFilter filter);
    }

    public class Query : Constraint
    {
        private readonly string q;

        public Query(string q)
        {
            this.q = q;
        }

        public void AssertTrack(Track track)
        {
            track.GetAllStringPropertiesValues().Concat(track.User.GetAllStringPropertiesValues())
                .Do(props => StringAssert.Contains(q, props));
        }

        public void Poke(TracksRequestFilter filter) => filter.Q = q;
    }

    public class Tags : Constraint
    {
        private readonly IEnumerable<string> tags;

        public Tags(params string[] tags)
        {
            this.tags = tags;
        }

        public void AssertTrack(Track track)
        {
            Assert.True(tags.Any(t => track.Tags.Contains(t, StringComparison.OrdinalIgnoreCase)),
                $"'{track.Tags}' did not contain any of '{string.Join(", ", tags)}'");
        }

        public void Poke(TracksRequestFilter filter) => filter.Tags = tags.ToList();
    }

    public class Filter : Constraint
    {
        private readonly TrackVisibility visibility;

        public Filter(TrackVisibility visibility)
        {
            this.visibility = visibility;
        }

        public void AssertTrack(Track track)
        {
            switch (visibility)
            {
                case TrackVisibility.All:
                    break;
                case TrackVisibility.Public:
                case TrackVisibility.Private:
                    Assert.AreEqual(track.Sharing, visibility);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Poke(TracksRequestFilter filter) => filter.Filter = visibility;
    }

    public class LicenseConstraint : Constraint
    {
        private readonly License license;

        public LicenseConstraint(License license)
        {
            this.license = license;
        }

        public void AssertTrack(Track track) => Assert.AreEqual(license, track.License);

        public void Poke(TracksRequestFilter filter) => filter.License = license;
    }

    public class BpmMin : Constraint
    {
        private readonly double min;

        public BpmMin(double min)
        {
            this.min = min;
        }

        public void AssertTrack(Track track) => Assert.GreaterOrEqual(track.BPM, min);

        public void Poke(TracksRequestFilter filter) => filter.BpmFrom = min;
    }

    public class BpmMax : Constraint
    {
        private readonly double max;

        public BpmMax(double max)
        {
            this.max = max;
        }

        public void AssertTrack(Track track) => Assert.GreaterOrEqual(track.BPM, max);

        public void Poke(TracksRequestFilter filter) => filter.BpmFrom = max;
    }

    public class DurationMin : Constraint
    {
        private readonly int min;

        public DurationMin(int min)
        {
            this.min = min;
        }

        public void AssertTrack(Track track) => Assert.GreaterOrEqual(track.Duration, min);

        public void Poke(TracksRequestFilter filter) => filter.DurationFrom = min;
    }

    public class DurationMax : Constraint
    {
        private readonly int max;

        public DurationMax(int max)
        {
            this.max = max;
        }

        public void AssertTrack(Track track) => Assert.LessOrEqual(track.Duration, max);

        public void Poke(TracksRequestFilter filter) => filter.DurationTo = max;
    }

    public class CreatedFrom : Constraint
    {
        private readonly DateTime from;

        public CreatedFrom(DateTime from)
        {
            this.from = from;
        }

        public void AssertTrack(Track track) => Assert.LessOrEqual(from, track.CreatedAt);

        public void Poke(TracksRequestFilter filter) => filter.CreateAtFrom = from;
    }

    public class CreatedBy : Constraint
    {
        private readonly DateTime to;

        public CreatedBy(DateTime to)
        {
            this.to = to;
        }

        public void AssertTrack(Track track) => Assert.GreaterOrEqual(to, track.CreatedAt);

        public void Poke(TracksRequestFilter filter) => filter.CreatedAtTo = to;
    }

    public class Ids : Constraint
    {
        private readonly IEnumerable<int> ids;

        public Ids(params int[] ids)
        {
            this.ids = ids;
        }

        public void AssertTrack(Track track) => CollectionAssert.Contains(ids, track.Id);

        public void Poke(TracksRequestFilter filter) => filter.Ids = ids.ToList();
    }

    public class Genres : Constraint
    {
        private readonly IEnumerable<string> genres;

        public Genres(params string[] genres)
        {
            this.genres = genres;
        }

        public void AssertTrack(Track track)
        {
            StringAssert.Contains(track.Genre, genres);
        }

        public void Poke(TracksRequestFilter filter)
        {
            filter.Genres = genres.ToList();
        }
    }

    public class Types : Constraint
    {
        private readonly IEnumerable<TrackType> types;

        public Types(params TrackType[] types)
        {
            this.types = types;
        }

        public void AssertTrack(Track track)
        {
            CollectionAssert.Contains(types, track.TrackType);
        }

        public void Poke(TracksRequestFilter filter)
        {
            filter.Types = types.ToList();
        }
    }
}