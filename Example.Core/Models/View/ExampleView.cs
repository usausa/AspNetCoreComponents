namespace Example.Models.View;

using System.Diagnostics.CodeAnalysis;

public class ExampleView
{
    public int No { get; set; }

    [AllowNull]
    public string Name { get; set; }
}
