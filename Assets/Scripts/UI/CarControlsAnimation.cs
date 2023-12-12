using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarControlsAnimation : MonoBehaviour
{
    [SerializeField] private Sprite image1;
    [SerializeField] private Sprite image2;
    [SerializeField] private float timeBetweenImages = 1.5f;

    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.sprite = image1;

        InvokeRepeating(nameof(AltarnateImages), timeBetweenImages, timeBetweenImages);
    }

    private void AltarnateImages()
    {
        if (imageComponent.sprite == image1) imageComponent.sprite = image2;
        else imageComponent.sprite = image1;
    }
}
