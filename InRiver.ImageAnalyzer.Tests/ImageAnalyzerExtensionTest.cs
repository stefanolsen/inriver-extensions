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

        [Fact]
        public void TestResourceEntityUpdated_WithResourceFileIdField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(1, new[] { "ResourceFileId" });
        }

        [Fact]
        public void TestResourceEntityUpdated_WithResourceTypeField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(1, new[] { "ResourceType" });
        }

        private ImageAnalyzerExtension CreateExtension()
        {
            var extension = new ImageAnalyzerExtension();
            extension.Context = new inRiverContext(new FakeInRiverManager(), new XUnitLogger(_outputHelper))
            {
                Settings = new Dictionary<string, string>
                {
                    { "AZURE_APIKEY", "" },
                    { "AZURE_ENDPOINT_URL", "" },
                    { "RESOURCE_ENTITY_TYPE_ID", "Resource" },
                    { "RESOURCE_CAPTION_FIELD_ID", "ResourceCaption" },
                    { "RESOURCE_FILEID_FIELD_ID", "ResourceFileId" },
                    { "RESOURCE_TAGS_FIELD_ID", "ResourceTags" },
                    { "ADD_UNKNOWN_CVL_VALUES", "true" },
                    { "TAGS_MINIMUM_CONFICENDE", "0.5" }
                }
            };

            return extension;
        }
    }
}
