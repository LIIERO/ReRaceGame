using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaceData
{

    // Enum Types
    public enum Difficuty { easy, normal, hard }

    // Race Data
    public static int NoHumanCars { get; set; }
    public static int NoAICars { get; set; }
    public static int NoLaps { get; set; } = 1;
    public static Difficuty DifficultyLevel { get; set; }
    
}
