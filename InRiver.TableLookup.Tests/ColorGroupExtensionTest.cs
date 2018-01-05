using System.Collections.Generic;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Log;
using Xunit;
using Xunit.Abstractions;

namespace InRiver.TableLookup.Tests
{
    public class ColorGroupExtensionTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public ColorGroupExtensionTest(ITestOutputHelper outputHelper)
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
        public void TestItemEntityCreated()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityCreated(1);
        }

        [Fact]
        public void TestItemEntityUpdated_WithColorIdField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(1, new[] {"ItemColorId"});
        }

        [Fact]
        public void TestItemEntityUpdated_WithNameField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(1, new[] { "ItemColorId" });
        }

        [Fact]
        public void TestProductEntityCreated()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityCreated(2);
        }

        [Fact]
        public void TestProductEntityUpdated_WithBrandField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(2, new[] { "ProductBrand" });
        }

        [Fact]
        public void TestProductEntityUpdated_WithNameField()
        {
            IEntityListener extension = CreateExtension();

            extension.EntityUpdated(2, new[] { "ProductName" });
        }

        [Fact]
        public void TestLinkCreated()
        {
            ILinkListener extension = CreateExtension();

            extension.LinkCreated(3, 2, 1, "ProductItem", null);
        }

        [Fact]
        public void TestLinkUpdated()
        {
            ILinkListener extension = CreateExtension();

            extension.LinkUpdated(3, 2, 1, "ProductItem", null);
        }

        private ColorGroupExtension CreateExtension()
        {
            var extension = new ColorGroupExtension();
            extension.Context = new inRiverContext(new FakeInRiverManager(), new XUnitLogger(_outputHelper))
            {
                Settings = new Dictionary<string, string>
                {
                    { "ITEM_ENTITY_TYPE_ID", "Item" },
                    { "PRODUCT_ENTITY_TYPE_ID", "Product" },
                    { "PRODUCTITEM_LINK_TYPE_ID", "ProductItem" },
                    { "BRAND_FIELD_TYPE_ID", "ProductBrand" },
                    { "COLORID_FIELD_TYPE_ID", "ItemColorId" },
                    { "COLORGROUP_FIELD_TYPE_ID", "ItemColorGroup" },
                    { "TABLE_CONNECTION_STRING", "UseDevelopmentStorage=true" },
                    { "TABLE_NAME", "ColorGroups" }
                }
            };

            return extension;
        }
    }
}
