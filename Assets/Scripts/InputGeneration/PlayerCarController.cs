using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCarController : MonoBehaviour
{
    public static readonly float InputRegisterThreshold = 0.001f;

    public KeyCode kcAccelerate = KeyCode.W;
    public KeyCode kcLeft = KeyCode.A;
    public KeyCode kcRight = KeyCode.D;

    public KeyCode kcReset = KeyCode.R;

    private CarPhysics carScript;

    private void Start()
    {
        carScript = GetComponent<CarPhysics>();
    }

    private void Update()
    {
        ModifyCarInput(carScript.CarInput);
    }

    private void ModifyCarInput(GameInput input) // Metoda wywo³ywana w ka¿dej klatce gry
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        

        if (horizontalInput < -InputRegisterThreshold) input.left = -horizontalInput;
        else input.left = 0f;

        if (horizontalInput > InputRegisterThreshold) input.right = horizontalInput;
        else input.right = 0f;

        if (verticalInput > InputRegisterThreshold) input.accelerate = verticalInput;
        else input.accelerate = 0f;


        // Stary system wejœcia gracza
        /*if (Input.GetKeyDown(kcLeft)) input.left = 1;
        else if (Input.GetKeyUp(kcLeft)) input.left = 0;

        if (Input.GetKeyDown(kcRight)) input.right = 1;
        else if (Input.GetKeyUp(kcRight)) input.right = 0;

        if (Input.GetKeyDown(kcAccelerate)) input.accelerate = 1;
        else if (Input.GetKeyUp(kcAccelerate)) input.accelerate = 0;*/
    }
}
