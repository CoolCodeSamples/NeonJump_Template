using System;
using System.Collections.Generic;

public enum PlayerShapeType {
    Mandala,
    Tennis,
    Bat,
    Tower,
    Slime,
    Astronaut,
    Smiley,
    Fridolin,
    Octopus,
    Electro,
    Jellybelly,
    Ghosty,
}

public class PlayerShapes
{
    private static readonly Dictionary<PlayerShapeType, string[]> shapes = new()
    {
        {
            PlayerShapeType.Mandala,
            new string[] {
            "XXX XXX",
            "X X X X",
            "XX   XX",
            "       ",
            "XX   XX",
            "X X X X",
            "XXX XXX",
        }},
        {
            PlayerShapeType.Tennis,
            new string[] {
            "    XXX    ",
            "  XXXXXXX  ",
            "   XXXXX   ",
            " XX XXX XX ",
            "XXX XXX XXX",
            "XXX XXX XXX",
            "XXX XXX XXX",
            " XX XXX XX ",
            "   XXXXX   ",
            "  XXXXXXX  ",
            "    XXX    ",
        }},
        { PlayerShapeType.Bat, new string[] {
            "    XXX    ",
            "           ",
            "XX XXXXX XX",
            " XXX X XXX ",
            "  XXXXXXX ",
            "   XXXXX  ",
        }},
        { PlayerShapeType.Tower,
            new string[] {
            "   XXX   ",
            "  X X X  ",
            "   XXX   ",
            "   XXX   ",
            "  XXXXX  ",
            " XXXXXXX ",
            "XXXXXXXXX",
        }},
        { PlayerShapeType.Slime,
            new string[] {
            "    X    ",
            "   XXX   ",
            "  XXXXX  ",
            "XX XXX XX",
            "XXXXXXXXX",
            "XX XXX XX",
            "XXX   XXX",
            "XXXXXXXXX",
            "  X   X  ",
        }},
        { PlayerShapeType.Astronaut, new string[] {
            "XXXX",
            "X  X",
            "XXXX",
            "XXXX",
            "X  X",
        }},
        { PlayerShapeType.Smiley, new string[] {
            " X   X ",
            " X   X ",
            "       ",
            "X     X",
            "XXXXXXX",
        }}, {
            PlayerShapeType.Fridolin, new string[] {
            "   X     X ",
            "   XXXXXXX ",
            "  XXXXXXXXX",
            "  XX XXX XX",
            "  XXX   XXX",
            "  XXXXXXXXX",
            "   XXXXXXX ",
            "    XXXXX  ",
            "X X  XXX   ",
            " X  XXXX   ",
            "  XXXXX    ",
        }}, {
            PlayerShapeType.Octopus, new string[] {
            " X     X ",
            "  X   X  ",
            "  XXXXX  ",
            " XX   XX ",
            "XXX   XXX",
            "XXXXXXXXX",
            "XXX   XXX",
            " XXXXXXX ",
            "  X X X  ",
            " X  X  X ",
            " X X   X ",
        }}, {
            PlayerShapeType.Electro, new string[] {
            "    XXX    ",
            "     X     ",
            "X  XXXXX  X",
            "X XXXXXXX X",
            "XXXXXXXXXXX",
            "  X  X  X  ",
            "XXXXXXXXXXX",
            "X XXXXXXX X",
            "X  XXXXX  X",
            "   X   X   ",
            "   XX XX   ",
        }}, {
            PlayerShapeType.Ghosty, new string[] {
            "  XXX  ",
            "XXXXXXX",
            "XX XX X",
            "X  X  X",
            "X  X  X",
            "XXXXXXX",
            "X     X",
            "XXXXXXX",
            " X X X ",
        }}, {
            PlayerShapeType.Jellybelly, new string[] {
            "    XXXX    ",
            " XXXXXXXXXX ",
            "XXXX XXX XXX",
            "XXX  XX  XXX",
            "XXXXXXXXXXXX",
            "   XX  XX   ",
            "  XX XX XX  ",
            "XX        XX",
        }},
    };

    public static string[] GetShape(PlayerShapeType playerShape)
    {
        if (shapes.ContainsKey(playerShape))
        {
            return shapes[playerShape];
        }
        else
        {
            throw new ArgumentException($"Shape {playerShape} not defined.");
        }
    }
}
