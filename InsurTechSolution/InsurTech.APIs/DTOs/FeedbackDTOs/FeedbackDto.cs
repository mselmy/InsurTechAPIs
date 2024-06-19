using InsurTech.Core.Entities;

public class FeedbackDto
{
    public int Id { get; set; }
    public Rating Rating { get; set; }
    public string Comment { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int InsurancePlanId { get; set; }
    public string InsurancePlanName { get; set; }
}
