using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarDropdownInitialization : MonoBehaviour
{
    // Zadaniem skryptu jest zachowywanie ostatniej u¿ytej wartoœci liczby rywali, w obiekcie UI typu dropdown

    private TMP_Dropdown dropdownComponent;
    private void Start()
    {
        dropdownComponent = GetComponent<TMP_Dropdown>(); // Pobierz komponent
        dropdownComponent.value = GlobalGameManagerSingleton.NumberOfAICars; // Zmieñ wartoœæ
    }

}
