using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManagerSingleton : MonoBehaviour
{
    // Liczba pojazdów sztucznej inteligencji, które maj¹ siê pojawiæ w wyœcigu
    public static int NumberOfAICars { get; private set; } = 3; // 3 rywali to wartoœæ domyœlna
    // Wybrany tor wyœcigowy
    public static int SelectedRacingTrack { get; private set; } = 0; // 0 -> ³atwiejszy tor, 1 -> trudniejszy tor

    public static void SetNumberOfAICars(int noAICars) { NumberOfAICars = noAICars; }
    public static void SetRacingTrack(int racingTrack) {  SelectedRacingTrack = racingTrack; }


    // Metody ³aduj¹ce sceny
    public static void StartGame() // Prze³¹czenie na scenê wyœcigu
    {
        SceneManager.LoadScene("Game");
    }

    public static void ReturnToMainMenu() // Prze³¹czenie na scenê menu g³ównego
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ExitGame() // Wyjœcie z gry
    {
        Application.Quit();
    }



    // Kod, który sprawia, ¿e ta klasa jest singletonem
    public static GlobalGameManagerSingleton Instance { get; private set; }
    private void Awake()
    {
        // Je¿eli istnieje ju¿ inna instancja tej klasy, usuwa samego siebie. W przeciwnym wypadku instancja jest samym sob¹
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}
