using System.Drawing;
using Colorful;
namespace CharacterConsole;

public interface IOutput
{
    void WriteLine(string message, Color color);
    void Write(string message, Color color);
}