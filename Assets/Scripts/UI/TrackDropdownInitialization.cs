using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackDropdownInitialization : MonoBehaviour
{
    // Zadaniem skryptu jest zachowywanie ostatniej u¿ytej wartoœci toru wyœcigowego, w obiekcie UI typu dropdown

    private TMP_Dropdown dropdownComponent;
    private void Start()
    {
        dropdownComponent = GetComponent<TMP_Dropdown>(); // Pobierz komponent
        dropdownComponent.value = GlobalGameManagerSingleton.SelectedRacingTrack; // Zmieñ wartoœæ
    }

}
