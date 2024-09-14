using System.Drawing;

namespace CharacterConsole.Tests;

public class MockOutput : IOutput
{
    public string Output { get; private set; } = string.Empty;

    public void WriteLine(string message, Color color)
    {
        Output += message + "\n";
    }

    public void Write(string message, Color color)
    {
        Output += message;
    }
}