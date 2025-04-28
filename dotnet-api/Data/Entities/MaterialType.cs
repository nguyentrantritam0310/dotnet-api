using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class MaterialType
    {
        public int ID { get; set; }
        public MaterialTypeEnum MaterialTypeName { get; set; }

        // Navigation properties
        public ICollection<Material> Materials { get; set; }
    }
}
