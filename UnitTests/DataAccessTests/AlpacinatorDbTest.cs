using Alpacinator.DataAccess.Base;
using Xunit;

namespace Alpacinator.UnitTests.DataAccessTests
{
    [Collection("Sequential")]
    public class AlpacinatorDbTest
    {
        [Fact]
        public void Initialize()
        {
            AlpacinatorDb alpacinatorDb = new AlpacinatorDb();
            alpacinatorDb.Initialize();

            Assert.Equal(2, alpacinatorDb.GetRowsCount("Alpaca"));
            Assert.Equal(4, alpacinatorDb.GetRowsCount("Farm"));
        }
    }
}