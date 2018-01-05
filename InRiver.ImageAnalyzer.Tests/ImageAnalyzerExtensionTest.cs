using System.Collections.Generic;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Log;
using Xunit;
using Xunit.Abstractions;

namespace InRiver.ImageAnalyzer.Tests
{
    public class ImageAnalyzerExtensionTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public ImageAnalyzerExtensionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void TestExtensionTestMethod()
        {
            IEntityListener extension = CreateExtension();

            string result = extension.Test();
            extension.Context.Log(LogLevel.Debug, result);
        }

        private ImageAnalyzerExtension CreateExtension()
        {
            var extension = new ImageAnalyzerExtension();
            extension.Context = new inRiverContext(null, new XUnitLogger(_outputHelper))
            {
                Settings = new Dictionary<string, string>
                {
                    { "AZURE_APIKEY", "" },
                    { "AZURE_ENDPOINT_URL", "" },
                    { "RESOURCE_ENTITY_TYPE_ID", "Resource" },
                    { "RESOURCE_FILEID_FIELD_ID", "ResourceFileId" },
                    { "RESOURCE_TYPE_FIELD_ID", "ResourceType" },
                    { "INCLUDED_RESOURCE_TYPES", "default,carousel" }
                }
            };

            return extension;
        }
    }
}
