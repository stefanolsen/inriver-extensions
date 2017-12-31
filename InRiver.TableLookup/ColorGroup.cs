using Microsoft.WindowsAzure.Storage.Table;

namespace InRiver.TableLookupExtension
{
    public class ColorGroup : TableEntity
    {
        [IgnoreProperty]
        public string BrandName
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        [IgnoreProperty]
        public string ColorCode
        {
            get => RowKey;
            set => RowKey = value;
        }

        public string CvlKey { get; set; }
    }
}
