using System.Collections.Generic;
using Xunit.Abstractions;

namespace Paradox.Cart.Test
{
    public class TestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        
        public TestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        protected void Print(List<string> messages)
        {
            foreach (var message in messages)
                _testOutputHelper.WriteLine(message);
        }
    }
}