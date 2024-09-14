
using System.Drawing;

namespace CharacterConsole;

public class CharacterManager
{
    private readonly IInput _input;
    private readonly IOutput _output;
    private readonly string _filePath = "input.csv";

    private string[] lines;

    private CharacterReader characterReader;
    private List<Character> Characters;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;

        characterReader = new CharacterReader();
        Characters = characterReader.CharactersList ?? new List<Character>();
       
    }

    public void Run()
    {
        _output.WriteLine("Welcome to Character Management",Color.AliceBlue);

        lines = File.ReadAllLines(_filePath);
        characterReader.CharacterLines = lines;
        characterReader.LoadCharacters();

        while (true)
        {
            _output.WriteLine("Menu:",Color.HotPink);
            _output.WriteLine("1. Display Characters",Color.AliceBlue);
            _output.WriteLine("2. Add Character",Color.AliceBlue);
            _output.WriteLine("3. Level Up Character",Color.AliceBlue);
            _output.WriteLine("4. Find Character",Color.AliceBlue);
            _output.WriteLine("5. Exit", Color.DarkRed);
            _output.Write("Enter your choice: ",Color.AliceBlue);
            var choice = _input.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    FindACharacter();
                    break;
                case "5":
                    CharacterWriter characterWriter = new CharacterWriter();
                    characterWriter.CharacterWriterList = Characters;
                    characterWriter.WriteOutCharacters();
                    return;
                default:
                    _output.WriteLine("Invalid choice. Please try again.", Color.DarkRed);
                    break;
            }
        }
    }

    public void DisplayCharacters()
    {
        characterReader.DisplayCharacters();
    }

    public void AddCharacter()
    {
        // TODO: Implement logic to add a new character
    
    Console.Write("Enter the name for your new character: ", Color.BlueViolet);
    string newCharacter = Console.ReadLine();

    Console.Write("Enter the equipment for your new character: ", Color.BlueViolet);
    string customEquipment = Console.ReadLine();

    Console.Write("Enter the class for your new character: ", Color.BlueViolet);
    string newClass = Console.ReadLine();

    
    string pipeDelimitedChoicesString = string.Join("|"); // Assuming this is some data related to the character
    string lineToAppend = $"{newCharacter},{newClass},{customEquipment},{5},{25},{pipeDelimitedChoicesString}"; 

 
    _output.WriteLine(lineToAppend, Color.AliceBlue);

    lines = lines.Append(lineToAppend).ToArray();

    characterReader.AddCharacter(newCharacter, newClass, customEquipment);
    }

    public void LevelUpCharacter()
    {
        _output.WriteLine("Select the character to level up: ", Color.AliceBlue);

        characterReader.DisplayCharacterNamesMenu();
        
        Console.Write("Enter Your Choice: ");
        string choice = Console.ReadLine();
                
        var foundCharacter = characterReader.FindCharacter(choice);

        if (foundCharacter != null)
        {
             _output.WriteLine($"You've Chosen to Level Up {foundCharacter.CharacterName}", Color.AliceBlue);
            foundCharacter.CharacterLevel++;
        }
        else
        {
            _output.WriteLine("Character not found.", Color.DarkRed);
        }
        
    }

    public void FindACharacter()
    {
        characterReader.DisplayCharacterNamesMenu();
        
        Console.Write("Enter Your Choice: ");
        string choice = Console.ReadLine();
                
        var foundCharacter = characterReader.FindCharacter(choice);

        if (foundCharacter != null)
        {
            foundCharacter.DisplayCharacterInformation();
        }
        else
        {
            _output.WriteLine("Character not found.", Color.DarkRed);
        }
    }
}

public class Character
{
    public string CharacterName {get;set;}
    public string CharacterClass {get;set;}
    public int CharacterLevel {get;set;}
    public int CharacterHitPoints {get;set;}
    public string CharacterEquipment {get;set;}

    public void DisplayCharacterInformation()
    {
        Console.WriteLine($"Name: {CharacterName}\nClass: {CharacterClass}\nLevel: {CharacterLevel}\nHit Points: {CharacterHitPoints}\nEquipment: {string.Join("|", CharacterEquipment)}");
    }

}