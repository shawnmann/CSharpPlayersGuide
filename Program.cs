using System.Data;
using System.Runtime.CompilerServices;

string name;

// Setup the console
Console.Title = "Bread Baker 3000";
Console.BackgroundColor = ConsoleColor.DarkYellow;
Console.ForegroundColor = ConsoleColor.Black;
Console.Clear(); // Clears all text, but also makes it so the background color applies to the whole window

Console.WriteLine("Press any key to start baking bread...");
Console.ReadKey(true);  // Pass true to not display the key pressed in the console

Console.WriteLine("Bread is ready!");
Console.Write("Who is this bread for? ");
name = Console.ReadLine();
Console.WriteLine(string.IsNullOrWhiteSpace(name) ? "No bread for no name!" : $"Gave bread to {name}!");

string fortuneInput;
Console.Write("Now you have your bread, time to pick a fortune (1-3): ");
fortuneInput = Console.ReadKey().KeyChar.ToString();

// Validate input: parse to int and ensure it's between 1 and 3.
// If parsing fails or value is outside the range, the switch falls back to the default case.
int fortuneChoice = 0;
if (!int.TryParse(fortuneInput, out fortuneChoice) || fortuneChoice < 1 || fortuneChoice > 3)
{
    fortuneChoice = 0; // anything not 1..3 goes to default
}

string response = fortuneChoice switch
{
    0 => "You didn't follow directions, may your bread be burnt!",
    1 => "You will find great success in your career.",
    2 => "A surprise awaits you later today.",
    3 => "Sharing bread brings joy to all around you.",
    _ => "The best fortune is the one you create yourself."
};
Console.WriteLine(); // New line after ReadKey
Console.WriteLine($"Your fortune: {response}");

// Do some array stuff
int[] scores = new int[] { 85, 92, 78, 90, 88 };
int[] firstThreeScores = scores[..3]; // Slices the first three elements
int[] lastTwoScores = scores[^2..]; // Slices the last two elements
int[] middleScores = scores[1..4]; // Slices from index 1 to 3
Console.WriteLine("First three scores: " + string.Join(", ", firstThreeScores));

// Jagged array (array of arrays where the child arrays can be various sizes)
int[][] jaggedMatrix = new int[3][];
jaggedMatrix[0] = new int[] { 1, 2 };
jaggedMatrix[1] = new int[] { 3, 4, 5 };
jaggedMatrix[2] = new int[] { 6, 7, 8, 9 };
Console.WriteLine("Jagged matrix element [0][1]: " + jaggedMatrix[0][1]);

// Multidimensional array (rectangular array)
int[,] multiDimensionalArray = new int[3,2] {
    { 1, 2 },
    { 3, 4 },
    { 5, 6 }
};
Console.WriteLine("Multidimensional array element [1,1]: " + multiDimensionalArray[1,1]);

CountToTen();

/// <summary>
/// Counts to 10 and prints each number to the console.
/// </summary>
void CountToTen()
{
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine(i);
    }
}

// Tuples for dummies...
(string, int, int) score = ("Shawn", 100, 10);
Console.WriteLine($"{score.Item1} scored {score.Item2} by getting to level {score.Item3}");
(string Name, int Score, int Level) score2 = ("Britty", 250, 25);
Console.WriteLine($"{score2.Name} scored {score2.Score} by getting to level {score2.Level}");
var tile = (Row: 2, Column: 4, Type: "Forest");
Console.WriteLine($"Tile at row {tile.Row}, column {tile.Column} is a {tile.Type} tile");
// Tuple deconstruction
var (playerName, playerScore, playerLevel) = score2;
// Ignoring elements with discards
var (row, column, _) = tile;

// Enums for dummies...
Season currentSeason = Season.Fall;
Console.WriteLine($"The current season is {currentSeason}");

Score myScore = new Score("Ted", 2000, 1);
Console.WriteLine($"{myScore.Name} scored {myScore.Points} in {myScore.Level} level(s). Star? {myScore.EarnedStar()}");

Rectangle rect = new Rectangle() { Width = 5, Height = 10 };

// Anonymous types for dummies...
var anonGuy = new { Name = "Anonymous Guy", Age = 30 };
Console.WriteLine($"This guy's name is {anonGuy.Name} and he is {anonGuy.Age} years old.");

// Static factory method call
Rectangle square = Rectangle.CreateSquare(7);

// Null reference
//  Reference types can be null - they aren't referncing any object in memory (on the heap)
string nullableString = null;
// Error: Cannot convert null to 'int' because it is a non-nullable value type
//  and value types hold their data directly (on the stack)
// int nullInt = null;

// Indicate if we will allow this variable to be null
string nullTest1; // Expected not to be null
nullTest1 = null; // Warning: Possible null reference assignment
string? nullTest2; // Nullable reference type - explicitly may be null
nullTest2 = null; // No warning, string? allows null
// This is static code analysis to help avoid null reference exceptions at runtime

// Null-conditional
//Console.WriteLine(nullTest1.Length); // Runtime error if null
Console.WriteLine(nullTest2?.Length); // Safe access with null-conditional operator

// Null coalescing
string displayName = nullTest2 ?? "Default name";

// Null-forgiving operator
nullTest2 = "Now not null";
int length = nullTest2!.Length; // Tells compiler we are sure nullTest2 is not null

// Nullable value type
int? age = null; // int? allows null
// This is short for:
Nullable<int> age2 = new Nullable<int>();
// This allows value types to be null
// int must always have a number
// int? can be null or have a number

// Casting
GameEntity entity = new GameEnemy(100, 50, "Goblin");
// Explicit cast - we are promising that entity is a GameEnemy
GameEnemy enemy = (GameEnemy)entity;

// Use GetType and typeof to check type at runtime
if (entity.GetType() == typeof(GameEnemy))
{
    Console.WriteLine("Entity is a GameEnemy!");
}

// Use is pattern matching to check type, and then cast in one step
if (entity is GameEnemy ge)
{
    // We have cast entity into a GameEnemy named ge
    Console.WriteLine($"Entity is a GameEnemy named {ge.Name}!");
}

// You can skip the name if you don't need to use it...
if (entity is GameEnemy)
{
    Console.WriteLine("Entity is a GameEnemy still!");
}

// Use as to type check and convert
//GameEntity entity1 = new GameEntity(100, 10);
GameEntity entity1 = new GameEnemy(150, 75, "Orc");
GameEnemy? enemy1 = entity1 as GameEnemy; // enemy1 could be null

// Records have a nice .ToString:
Console.WriteLine(new PointRecord(3.5f, 7.2f).ToString());
// Can deconstruct Records also
var (x, y) = new PointRecord(1.1f, 2.2f);
PointRecord pr1 = new PointRecord(4.4f, 5.5f);
PointRecord pr2 = pr1 with { X = 6.6f }; // 'with' expression to create a copy with modifications
PointRecord pr3 = pr1 with { X = 8.5f, Y = 7.2f }; // Can do multiple properties with commas

// Generics
Pair<int, int> intPair = new Pair<int, int>(10, 5);
Pair<string, string> strPair = new Pair<string, string>("Shawn", "Britty");
var (firstInt, secondInt) = intPair; // This will work because we added a Deconstruct to Pair
Console.WriteLine($"firstInt: {firstInt}, secondInt: {secondInt}");
Tuple<int, int, int> intTuple = new Tuple<int, int, int>(1, 2, 3);
Console.WriteLine(intTuple.ToString()); // Call the override ToString method

// Default parameters
Console.WriteLine($"Rolled die result: {RollDie().ToString()}");

// Named arguments
// Can pass the names with the params, for clarity, or in a different order
Math.Clamp(value: 15, min: 0, max: 10); // value is 10 after clamping

// Variable number of parameters
int total = SumNumbers(1, 2, 3, 4, 5);
Console.WriteLine($"Total sum: {total}");

// *** PASS BY REFERENCE
// Pass by reference
// Because the parameter is ref, the method receives a reference to the
//  caller's storage for the integer (not a copy).
void DisplayNumberByRef(ref int x)
{
    Console.WriteLine($"DisplayNumberByRef: {x}");
    // Modify the caller's variable since it was passed by reference
    x += 10;
}
int yRef = 3;
DisplayNumberByRef(ref yRef);
Console.WriteLine($"yRef after DisplayNumber: {yRef}"); // yRef is now 13

// Pass by value (default behavior)
void DisplayNumber(int x)
{
    Console.WriteLine($"DisplayNumber: {x}");
    x += 10; // This modifies only the local copy
}
int yVal = 3;
DisplayNumber(yVal);
Console.WriteLine($"yVal after DisplayNumber: {yVal}"); // yVal is still 3

// *** OUTPUT PARAMETERS
// Can output multiple values from a method using out parameters
void SetupNumber(bool useBigNumber, out double value, out string valueString)
{
    value = useBigNumber ? 1_000_000 : 1;
    valueString = value.ToString("N0"); // Format with commas
}
double xOutput;
//string xOutputStr; // Don't need to declare these ahead of time in C# 7 and later
SetupNumber(true, out xOutput, out string xOutputStr);
Console.WriteLine($"xOutput: {xOutput}, xOutputStr: {xOutputStr}");

// Extension Methods
string sample = "Hello, World!";
Console.WriteLine($"Reverse {sample} into {sample.ReverseString()}");

// Method with optional parameter
int RollDie(int sides = 6)
{
    Random rand = new Random();
    return rand.Next(sides) + 1; // Starts at index 0
}

// Variable number of parameters
int SumNumbers(params int[] numbers) // Add params modifier to allow variable number of args
{
    int sum = 0;
    foreach (int num in numbers)
    {
        sum += num;
    }
    return sum;
}

enum Season
{
    Winter,
    Spring,
    Summer,
    Fall
};

class Score
{
    public string Name = "Default Name"; // Field initializers are allowed in C# 12
    public int Points;
    public int Level;

    // Constructors
    public Score()
    {
        Name = "Unknown";
        Points = 0;
        Level = 1;
    }
    public Score(string name, int points, int level)
    {
        Name = name;
        Points = points;
        Level = level;
    }
    // You can also call one constructor from another
    // public Score(string name) : this()

    public bool EarnedStar()
    {
        return (Points / Level) > 1000;
    }

    // Static method - no instance needed to call, static
    //  members belong to the class itself
    // This might be a bad example, but imagine the Add method on a Math static class...
    public static void DisplayScore(Score score)
    {
        Console.WriteLine($"{score.Name} scored {score.Points} in level {score.Level}.");
    }

    // Can also do generic methods
    public void GenericMethod<T>(T item)
    {
        Console.WriteLine($"Generic method called with item: {item}");
    }
}

public class Rectangle
{
    public int Width { get; set; }  // Auto-implemented property
    public int Height { get; set; } // Auto-implemented property
    public int Area => Width * Height; // Expression-bodied property

    public Rectangle()
    {
        
    }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    // Static factory method
    public static Rectangle CreateSquare(int sideLength)
    {
        return new Rectangle(sideLength, sideLength);
    }
}

public static class Helper
{
    public static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public static int Add(int a, int b) => a + b;
} 

public abstract class GameEntity
{
    public int Health { get; set; }
    public int Mana { get; set; }
    // Protected properties can only be set by this class and its derived classes
    //  Derived classes inherit the original class
    public int ProtectedProperty { get; protected set; }

    public GameEntity(int health, int mana)
    {
        Health = health;
        Mana = mana;
    }

    // virtual methods can (optionally) be overridden in derived classes
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    // Derived classes MUST implement abstract methods
    //  And we also needed to make the class abstract to support this...
    // Abstract classes can include implementation
    public abstract void PerformAction();
}

public class  GameEnemy : GameEntity
{
    public string Name { get; set; }
    public GameEnemy(int health, int mana, string name) : base(health, mana)
    {
        Name = name;
    }

    public override void TakeDamage(int damage)
    {
        // Custom behavior for enemies when taking damage
        damage -= 1;
        base.TakeDamage(damage);
    }

    // We were forced to implement this method because it's abstract in the base class
    public override void PerformAction()
    {
        Console.WriteLine($"{Name} attacks with a fierce strike!");
    }
}

public sealed class FinalClass
{
    // This class cannot be inherited from because it's sealed!
}

// *** INTERFACES:
// As long as two things implement the same interface,
//  they can be used interchangeably
public enum TerrainType {
    Desert,
    Forest,
    Mountain,
    Plains,
    Hills
}

public class Level
{
    public int Width { get; }
    public int Height { get; }

    public Level(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public TerrainType GetTerrainAt(int x, int y)
    {
        return TerrainType.Desert;
    }
}

// Interfaces define the boundary/shape/contract (this is what abstract means),
//  but no implementations. Derived classes will supply the implementation
// Members of an interface are public and abstract by default (duh)
public interface ILevelBuilder
{
    Level BuildLevel(int width, int height);
}

// Implement the interface: build a class that does the job described in the interface
public class FixedLevelBuilder : ILevelBuilder
{
    public Level BuildLevel(int width, int height)
    {
        Level level = new Level(width, height);
        // ...do some stuff to build the level
        return level;
    }
}


// *** STRUCTS
// A class can have only one base class that it derives from, but it
//  can implement any number of interfaces

// Classes are a way to make custom reference types
// Structs are a way to make custom value types
public struct Point
{
    public float X { get; }
    public float Y { get; }
    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }
}

// This also works
public struct PairOfInts
{
    public int A;
    public int B;
}

// *** RECORDS
// Ultra-simplified syntax for immutable data objects
public record PointRecord(float X, float Y);

// *** GENERICS
// This is a generic type that holds two values that don't need to be
//  explicitly defined. So instead of making a "PairOfStrings", "PairOfInts",
//  "PairOfBools" you can just make a "Pair" generic type that will work in place
//  of all of the other classes...
public class Pair<TFirst, TSecond>
{
    public TFirst First { get; }
    public TSecond Second { get; }
    public Pair(TFirst first, TSecond second)
    {
        First = first;
        Second = second;
    }

    // Deconstruct:
    // Could write it like this -
    //public void Deconstruct(out TFirst first, out TSecond second) =>
    //    (first, second) = (First, Second);
    public void Deconstruct(out TFirst first, out TSecond second)
    {
        // Verbose:
        //first = First;
        //second = Second;
        // Or succinct, with deconstruction
        (first, second) = (First, Second);
    }
}

// You can derive generic types from other generic types
public class Tuple<TFirst, TSecond, TThird> : Pair<TFirst, TSecond>
{
    public TThird Third { get; }
    public Tuple(TFirst first, TSecond second, TThird third): base(first, second)
    {
        Third = third;
        // Or you could load base like this:
        //base.First = first;
        //base.Second = second;
    }

    // Deconstruct into three values: (first, second, third) = tuple;
    public void Deconstruct(out TFirst first, out TSecond second, out TThird third)
    {
        first = First;
        second = Second;
        third = Third;
    }

    // Helpful debug representation
    public override string ToString()
    {
        return $"({First}, {Second}, {Third})";
    }
}

// *** EXTENSION METHODS
// Extension methods allow you to "add" methods to existing types
public static class StringExtensions
{
    // This is an extension method for the string type
    //  The "this" keyword before the first parameter indicates
    //  that this is an extension method for that type
    public static string ReverseString(this string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}