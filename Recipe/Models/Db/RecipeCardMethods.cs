namespace Recipe.Models.Db
{
    public static class RecipeCardMethods
    {
        public static void MergeWith(this RecipeCard existing, RecipeCard recipeCard)
        {
            existing.Date = recipeCard.Date;
            existing.Remarks = recipeCard.Remarks;
        }
    }
}
