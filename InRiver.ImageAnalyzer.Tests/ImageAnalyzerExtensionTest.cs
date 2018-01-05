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
                    { "ITEM_ENTITY_TYPE_ID", "Item" },
                    { "PRODUCT_ENTITY_TYPE_ID", "Product" },
                    { "PRODUCTITEM_LINK_TYPE_ID", "ProductItem" },
                    { "BRAND_FIELD_TYPE_ID", "ProductBrand" },
                    { "COLORID_FIELD_TYPE_ID", "ItemColorId" },
                    { "TABLE_NAME", "ColorGroups" }
                }
            };

            return extension;
        }
    }
}
