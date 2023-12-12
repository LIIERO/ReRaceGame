using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : CarController
{

    protected override void ModifyCarInput(GameInput input) // Metoda wywo³ywana w ka¿dej klatce gry
    {
        input.accelerate = 1;
        input.left = 0;
        input.right = 0;
    }

}
