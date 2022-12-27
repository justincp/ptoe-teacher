namespace PTOEQuiz.Data
{
    public static class SeedData
    {
        public static void Initialize(QuizContext db)
        {

            db.SaveChanges();
        }
    }
}
