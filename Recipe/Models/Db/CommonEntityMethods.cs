namespace Recipe.Models.Db
{
    public static class CommonEntityMethods
    {
        public static void MergeWith(this CommonEntity existing, CommonEntity commonEntity) => existing.RftData = commonEntity.RftData;
    }
}
