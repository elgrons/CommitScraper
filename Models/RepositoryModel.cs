namespace CommitScraper.Models
{
    public class RepositoryModel
    {
        public string Repository { get; set; }
        public OwnerModel Owner { get; set; }
        public string Name { get; set; }
    }
}