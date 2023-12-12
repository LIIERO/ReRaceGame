using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManagerSingleton : MonoBehaviour
{
    // Liczba pojazd�w sztucznej inteligencji, kt�re maj� si� pojawi� w wy�cigu
    public static int NumberOfAICars { get; private set; } = 3; // 3 rywali to warto�� domy�lna
    // Wybrany tor wy�cigowy
    public static int SelectedRacingTrack { get; private set; } = 0; // 0 -> �atwiejszy tor, 1 -> trudniejszy tor

    public static void SetNumberOfAICars(int noAICars) { NumberOfAICars = noAICars; }
    public static void SetRacingTrack(int racingTrack) {  SelectedRacingTrack = racingTrack; }


    // Metody �aduj�ce sceny
    public static void StartGame() // Prze��czenie na scen� wy�cigu
    {
        SceneManager.LoadScene("Game");
    }

    public static void ReturnToMainMenu() // Prze��czenie na scen� menu g��wnego
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ExitGame() // Wyj�cie z gry
    {
        Application.Quit();
    }



    // Kod, kt�ry sprawia, �e ta klasa jest singletonem
    public static GlobalGameManagerSingleton Instance { get; private set; }
    private void Awake()
    {
        // Je�eli istnieje ju� inna instancja tej klasy, usuwa samego siebie. W przeciwnym wypadku instancja jest samym sob�
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}
