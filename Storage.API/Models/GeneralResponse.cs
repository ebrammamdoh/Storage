namespace Storage.API.Models;

public class GeneralResponse
{
    public bool IsSuccess
    {
        get
        {
            return Code == "200";
        }
    }
    public string Code { get; set; }
    public string Message { get; set; }

}
