namespace TccApp.Infraestructure.Helpers
{
    public static class GuidHelper
    {
        public static Guid GetGuidSequencial(this int id)
        {
            string novoId = "00000000-0000-0000-0000-" + id.ToString("000000000000");
            return Guid.Parse(novoId);         
        }

        public static Guid GetGuidSequencial(this int id, string text)
        {
            text += "00000000";
            string novoId = $"{text.Substring(0, 8)}-0000-0000-0000-{id:000000000000}";
            return Guid.Parse(novoId);
        }

        public static Guid? GetGuidSequencial(this int? id)
        {
            if (!id.HasValue) return null;

            string novoId = "00000000-0000-0000-0000-" + id.Value.ToString("000000000000");
            return Guid.Parse(novoId);
        }

        public static Guid GetGuidSequencial(this long id)
        {
            string novoId = "00000000-0000-0000-0000-" + id.ToString("000000000000");
            return Guid.Parse(novoId);
        }

        public static Guid? GetGuidSequencial(this long? id)
        {
            if (!id.HasValue) return null;

            string novoId = "00000000-0000-0000-0000-" + id.Value.ToString("000000000000");
            return Guid.Parse(novoId);
        }

        public static Guid GetGuidSequencial(this short id)
        {            
            string novoId = "00000000-0000-0000-0000-" + id.ToString("000000000000");
            return Guid.Parse(novoId);
        }

        public static Guid? GetGuidSequencial(this short? id)
        {
            if (!id.HasValue) return null;

            string novoId = "00000000-0000-0000-0000-" + id.Value.ToString("000000000000");
            return Guid.Parse(novoId);
        }
    }
}
