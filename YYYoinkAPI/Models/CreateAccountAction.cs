namespace YYYoinkAPI.Models;

public class CreateAccountAction
{
    public Guid Uuid { get; }
    public string Action { get; }
    public DateTime Timestamp { get; }
    public string Topic { get; }
    public AccountData Data { get; }

    public CreateAccountAction(Guid uuid, string action, DateTime timestamp, string topic, AccountData data)
    {
        Uuid = uuid;
        Action = action;
        Timestamp = timestamp;
        Topic = topic;
        Data = data;
    }

    public class AccountData
    {
        public string Email { get; }
        public string Password { get; }

        public AccountData(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public string ISOTimestamp()
    {
        return Timestamp.ToString("o");
    }
}