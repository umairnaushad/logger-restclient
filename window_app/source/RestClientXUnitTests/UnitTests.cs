using RESTClient;
using System;
using System.Collections.Generic;
using Xunit;

namespace RestClientXUnitTests
{
    public class UnitTests
    {
        [Fact]
        public void TestCollectionList()
        {
            string artistName = "Vincent van Gogh";
            string expecteObjectNumber = "SK-A-3262";
            RijksMuseumApi obj = new RijksMuseumApi();
            IList<ArtCollectionList> list = obj.GetCollectionsListByArtistName(artistName);
            Assert.Equal(expecteObjectNumber, list[0].ObjectNumber);
        }
        [Fact]
        public void TestCollectionDetail()
        {
            string objectNumber = "SK-A-3262";
            string expectedGuid = "b9d83b68-40b3-42cf-8d5e-959201ddf4bf";
            RijksMuseumApi obj = new RijksMuseumApi();
            ArtCollectionDetail detail = obj.GetCollectionDetailByObjectNumber(objectNumber);
            Assert.Equal(expectedGuid, detail.Guid);
        }
    }
}
