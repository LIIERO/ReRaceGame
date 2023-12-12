using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTextComponent; // Komponent obiektu UI, wy�wietlaj�cy licznik czasu na ekranie

    public static float RaceTimer { get; private set; } = 0f; // Globalny czas, kt�ry up�yn�� od pocz�tku wy�cigu

    private bool timerActive = false;
    private bool timerDisplayActive = true;

    private void Start()
    {
        RaceTimer = 0f;
    }

    private void Update()
    {
        // Czas odmierzany przez dodawanie czasu, kt�ry up�yn�� od ostatniej klatki
        if (timerActive) RaceTimer += Time.deltaTime; 

        // Aktualizacja czasu na ekranie je�li wy�wietlacz jest aktywny
        if (timerDisplayActive) timerTextComponent.text = string.Format("{0:f2}", RaceTimer);
    }

    public void StartTimer()
    {
        timerActive = true;
        RaceTimer = 0f;
    }

    public void FreezeTimerDisplay()
    {
        timerDisplayActive = false;
    }


    private void OnEnable() // Menad�er czasu subskrybuje do wydarzenia rozpocz�cia i zako�czenia wy�cigu
    {
        EventManager.RaceStart += StartTimer; // Odliczanie czasu zaczyna si� przy rozpocz�ciu wy�cigu
        EventManager.RaceEnd += FreezeTimerDisplay;  // Odliczanie czasu zatrzymuje si� przy rozpocz�ciu wy�cigu
    }

    private void OnDisable() // Metoda usuwaj�ca subskrypcje - dobra praktyka programistyczna
    {
        EventManager.RaceStart -= StartTimer;
        EventManager.RaceEnd -= FreezeTimerDisplay;
    }
}

