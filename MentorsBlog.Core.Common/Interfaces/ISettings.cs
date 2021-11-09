namespace MentorsBlog.Core.Common.Interfaces
{
    public interface ISettings
    {
        string GetValue(string name);
        T GetValue<T>(string name);
        T GetSection<T>(string name) where T : class, new();
    }
}