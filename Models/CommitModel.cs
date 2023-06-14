namespace CommitScraper.Models
{
  public class CommitModel
  {
    public string Id { get; set; }
    public CommitDetailModel Commit { get; set; }
  }

  public class CommitDetailModel
  {
    public string Message { get; set; }
    public CommitterModel Committer { get; set; }
  }

  public class CommitterModel
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
  }
}