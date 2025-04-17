namespace FileGenerator.Tests;

public class StringFactoryStub : StringFactory
{
    private readonly string[] _values;
    private int _index;

    public StringFactoryStub(string[] values)
    {
        _values = values;
    }

    public override string GetRandomString()
    {
        var value = _values[_index % _values.Length];
        _index++;
        return value;
    }
}