using System.Diagnostics.CodeAnalysis;

namespace FileGenerator;

[ExcludeFromCodeCoverage]
public class StringFactory
{
    private readonly List<string> _dictionary;

    public StringFactory()
    {
        _dictionary = new List<string>
        {
            "Apple", "Banana is yellow", "Cherry is the best", "Something something something"
        };
    }

    public virtual string GetRandomString()
    {
        var localRandom = Random.Shared;
        return _dictionary[localRandom.Next(_dictionary.Count)];
    }
}