using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class CarAgent : Agent
{
    private CarController carController;
    public bool finished = false; 

    //called once at the start
    public override void Initialize()
    {
        carController = GetComponent<CarController>();
    }

    //Called each time it has timed-out or has reached the goal
    public override void OnEpisodeBegin()
    {
        // Debug.Log("end");
        // checkpointManager.ResetCheckpoints();
        // SceneManager.LoadScene(1);
        //   carController.Respawn();
    }

    //Collecting extra Information that isn't picked up by the RaycastSensors
    public override void CollectObservations(VectorSensor sensor)
    {
        if (!finished)
        {
            AddReward(-0.001f);
        }
    }

    //Processing the actions received
    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;
        carController.Steer(input[0]);
        carController.ApplyAcceleration(input[1]);
    }

    //For manual testing with human input, the actionsOut defined here will be sent to OnActionRecieved
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }
}
