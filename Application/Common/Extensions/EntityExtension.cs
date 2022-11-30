namespace Application.Common.Extensions
{
    public static class EntityExtension
    {
        public static bool Exist(this object item)
        {
            return item != null;
        }
    }
}
