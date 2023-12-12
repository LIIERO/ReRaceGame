using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarController : MonoBehaviour
{
    protected CarPhysics CarScript;
    private GameInput currentInput = new();

    private void Start()
    {
        // Referencja do skryptu pojazdu przypisanego dla kontrolera
        CarScript = transform.GetChild(0).gameObject.GetComponent<CarPhysics>();
    }

    protected abstract void ModifyCarInput(GameInput input); // szablon metody modyfikuj¹cej pole CurrentInput w ka¿dej klatce gry

    protected virtual void Update()
    {
        ModifyCarInput(currentInput);
        CarScript.CarInput = currentInput;
    }
}
