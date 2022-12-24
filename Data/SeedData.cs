namespace PTOEQuiz.Data
{
    public static class SeedData
    {
        public static void Initialize(GameContext db)
        {

            db.SaveChanges();
        }
    }
}
