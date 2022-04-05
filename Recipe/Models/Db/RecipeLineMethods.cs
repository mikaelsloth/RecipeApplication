namespace Recipe.Models.Db
{
    public static partial class RecipeLineMethods
    {
        public static void MergeWith(this RecipeLine existing, RecipeLine recipeLine)
        {
            existing.Evening = recipeLine.Evening;
            existing.MedicinName = recipeLine.MedicinName;
            existing.Midnight = recipeLine.Midnight;
            existing.Morning = recipeLine.Morning;
            existing.Noon = recipeLine.Noon;
            existing.Position = recipeLine.Position;
        }

        public static bool ValueEquals(this RecipeLine existing, RecipeLine recipeLine) =>
            existing.Position == recipeLine.Position &&
                existing.Evening == recipeLine.Evening &&
                existing.MedicinName == recipeLine.MedicinName &&
                existing.MedicinTypeId == recipeLine.MedicinTypeId &&
                existing.Midnight == recipeLine.Midnight &&
                existing.Morning == recipeLine.Morning &&
                existing.Noon == recipeLine.Noon &&
                existing.RecipeCardId == recipeLine.RecipeCardId &&
                existing.UnitsId == recipeLine.UnitsId;
    }
}
