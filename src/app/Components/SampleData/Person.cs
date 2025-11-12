namespace Components.SampleData;

public class Person
{
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }

    public Person(string firstName, string lastName, string middleName = null)
    {
        FirstName = firstName;
        LastName = lastName;    
        MiddleName = middleName ?? string.Empty;
    }

    public string DisplayName => string.IsNullOrEmpty(MiddleName)
        ? $"{FirstName}, {LastName}"
        : $"{FirstName} {MiddleName}, {LastName}";

    public override string ToString() => DisplayName;
}
