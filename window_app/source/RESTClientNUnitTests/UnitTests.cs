using NUnit.Framework;
using RESTClient;
using System.Collections.Generic;

namespace RESTClientUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCollectionList()
        {
            string artistName = "Vincent van Gogh";
            string expecteObjectNumber = "SK-A-3262";
            RijksMuseumApi obj = new RijksMuseumApi();
            IList<ArtCollectionList> list = obj.GetCollectionsListByArtistName(artistName);
            Assert.AreEqual(expecteObjectNumber, list[0].ObjectNumber);
        }

        [Test]
        public void TestCollectionDetail()
        {
            string objectNumber = "SK-A-3262";
            string expectedGuid = "b9d83b68-40b3-42cf-8d5e-959201ddf4bf";
            RijksMuseumApi obj = new RijksMuseumApi();
            ArtCollectionDetail detail = obj.GetCollectionDetailByObjectNumber(objectNumber);
            Assert.AreEqual(expectedGuid, detail.Guid);
        }

        [Test]
        public void TestArtistListCount()
        {
            int expectedCount = 7000;
            int actualCount = 0;
            RijksMuseumApi obj = new RijksMuseumApi();
            actualCount = obj.GetArtistsCount();
            Assert.GreaterOrEqual(actualCount, expectedCount);
        }

    }
}