using System.Net.Mail;

namespace GCook.Helpers;

public static class Helper
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mail = new(email);
            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
    }
}
