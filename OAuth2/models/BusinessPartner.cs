public class BusinessPartner
{

     public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public string BusinessPartnerId { get; set; } = string.Empty;

    public DateTime dateTime {get;set;}= DateTime.Now;
    public bool IsActive { get; set; } = false;
    public string Avatar { get;  set; } = string.Empty;

    public BusinessPartner(string bpid) => BusinessPartnerId = bpid;


    public BusinessPartner()
 {
    
 }
}