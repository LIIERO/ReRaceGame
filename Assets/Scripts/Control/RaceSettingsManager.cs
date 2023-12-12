using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSettingsManager : MonoBehaviour
{
    [SerializeField] private bool carRespawnOnStartPos;

    private void Awake()
    {
        DeadZone.ResetCarTransformOnDeadZone = carRespawnOnStartPos;
    }
}
