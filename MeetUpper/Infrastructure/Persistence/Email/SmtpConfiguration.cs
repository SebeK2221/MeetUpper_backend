using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Persistence.Email;

public class SmtpConfiguration
{
    public string SMTPHost { get; set; }
    
    public int SMTPPort { get; set; }
    
    public string SMTPFrom { get; set; }
    
    public string SMTPUser { get; set; }
    
    public string SMTPPass { get; set; }
}