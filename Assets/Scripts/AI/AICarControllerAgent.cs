using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AICarControllerAgent : Agent
{

    protected CarPhysics CarScript;
    protected Rigidbody2D CarBody;
    private GameInput currentInput = new();
    public Vector3 startPosition;

    public int acc = 0;
    public int turn = 0;
    public int reward = 0;

    private void Start()
    {
        // Referencje do obiektu i skryptu pojazdu przypisanego dla kontrolera
        CarScript = GetComponent<CarPhysics>();
        CarBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

    }

    public override void OnEpisodeBegin()
    {
        reward = 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);
        //Debug.Log(actions.DiscreteActions[1]);

        int acceleration = actions.DiscreteActions.Array[0];
        int direction = actions.DiscreteActions.Array[1];

        acc = acceleration;
        turn = direction;

        //Debug.Log(acc);
        //Debug.Log(turn);
    }

    protected void ModifyCarInput(GameInput input) // Metoda wywo³ywana w ka¿dej klatce gry
    {

        if (turn == 1) input.left = 1f;
        else if (turn == 2) input.left = 0f;
        else if (turn == 0) input.left = 0f;

        if (turn == 2) input.right = 1f;
        else if (turn == 1) input.right = 0f;
        else if (turn == 0) input.right = 0f;

        if (acc == 1) input.accelerate = 1f;
        else if (acc == 0) input.accelerate = 0f;
    }

    protected virtual void Update()
    {
        ModifyCarInput(currentInput);

        CarScript.CarInput = currentInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Reward Point"))
        {
            //Debug.Log("Reward");

            reward = reward + 1;
            //Debug.Log(reward);

            AddReward(+1f);
        }


        if (collision.gameObject.CompareTag("TrackEdge"))
        {
            Debug.Log("DeadZone");

            reward = reward - 5;
            //Debug.Log(reward);

            AddReward(-5f);
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("FINISH");

            reward = reward + 5;
            //Debug.Log(reward);

            AddReward(+5f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("StartPosition"))
        {
            Debug.Log("StartPosition");

            reward = 0;
            //Debug.Log(reward);

            SetReward(0f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(CarBody.velocity);
    }
}
