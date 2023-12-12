using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void RaceEvent();
    public delegate void CarEvent(GameObject car);

    public static event RaceEvent RaceStart; // Wydarzenie rozpocz�cia wy�cigu
    public static event RaceEvent RaceEnd; // Wydarzenie zako�czenia wy�cigu (kiedy gracz dotrze do mety)

    public static event CarEvent CarReachedGoal; // Wydarzenie uko�czenia wy�cigu przez pojazd


    public static void InvokeRaceStartEvent()
    {
        if (RaceStart != null) { RaceStart(); }
    }

    public static void InvokeRaceEndEvent()
    {
        if (RaceEnd != null) { RaceEnd(); }
    }

    public static void InvokeCarReachedGoalEvent(GameObject car)
    {
        if (CarReachedGoal != null) { CarReachedGoal(car); }
    }

}
