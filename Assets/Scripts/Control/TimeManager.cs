using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTextComponent; // Komponent obiektu UI, wyœwietlaj¹cy licznik czasu na ekranie

    public static float RaceTimer { get; private set; } = 0f; // Globalny czas, który up³yn¹³ od pocz¹tku wyœcigu

    private bool timerActive = false;
    private bool timerDisplayActive = true;

    private void Start()
    {
        RaceTimer = 0f;
    }

    private void Update()
    {
        // Czas odmierzany przez dodawanie czasu, który up³yn¹³ od ostatniej klatki
        if (timerActive) RaceTimer += Time.deltaTime; 

        // Aktualizacja czasu na ekranie jeœli wyœwietlacz jest aktywny
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


    private void OnEnable() // Menad¿er czasu subskrybuje do wydarzenia rozpoczêcia i zakoñczenia wyœcigu
    {
        EventManager.RaceStart += StartTimer; // Odliczanie czasu zaczyna siê przy rozpoczêciu wyœcigu
        EventManager.RaceEnd += FreezeTimerDisplay;  // Odliczanie czasu zatrzymuje siê przy rozpoczêciu wyœcigu
    }

    private void OnDisable() // Metoda usuwaj¹ca subskrypcje - dobra praktyka programistyczna
    {
        EventManager.RaceStart -= StartTimer;
        EventManager.RaceEnd -= FreezeTimerDisplay;
    }
}

