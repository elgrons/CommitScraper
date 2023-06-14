namespace CommitScraper.Models
{
    public class PayloadModel
    {
        public string After { get; set; }
        public string Before { get; set; }
        public RepositoryModel Repository { get; set; }
        public List<CommitModel> Commits { get; set; }
    }
}