using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using System.IO;

public class SceneController : MonoBehaviour
{

    // experiment parameters
    int subjectID = 05;
    private int nrTrials = 60; // total number of trials (6 first are training)
    private float estimationMaxDuration = 10f;

    // objects set in inspector
    public GameObject emitter;
    private Rigidbody _emitterRigidbody;
//    private MeshRenderer _emitterMeshRend;

    public GameObject basket;
    private Rigidbody _basketRigidbody;

    public GameObject ball;
    private Rigidbody _ballRigidbody;
    private MeshRenderer _ballMeshRend;

    public GameObject occluder;
    public GameObject background;

    // parameter tracking
    private int trialNr; // current trial number
    private int side;

    // Lists to define heights, velocities, positions 
    private List<float> emitterStartPosition = new List<float> { 10, 10, -10, 10, -10, -10, 10, 10, -10, -10, 10, 10, 10, 10, 10, 10, 10, -10, -10, -10, -10, -10, 10, 10, -10, 10, -10, -10, -10, -10, -10, 10, -10, -10, -10, -10, -10, -10, 10, -10, -10, 10, -10, 10, 10, 10, 10, 10, 10, 10, -10, -10, 10, -10, 10, -10, 10, 10, 10, 10 }; // influences time it takes for emitter to appear (with velocity)
    private List<float> emitterVelocity = new List<float> { -3.5f, -2.5f, 3.5f, -1.5f, 3.5f, 3.5f, -1.5f, -3.5f, 2.5f, 3.5f, -3.5f, -1.5f, -1.5f, -3.5f, -3.5f, -3.5f, -2.5f, 3.5f, 3.5f, 2.5f, 1.5f, 1.5f, -2.5f, -1.5f, 2.5f, -1.5f, 2.5f, 3.5f, 1.5f, 1.5f, 2.5f, -2.5f, 3.5f, 3.5f, 2.5f, 3.5f, 1.5f, 3.5f, -3.5f, 2.5f, 1.5f, -3.5f, 2.5f, -2.5f, -1.5f, -1.5f, -3.5f, -2.5f, -1.5f, -3.5f, 3.5f, 2.5f, -2.5f, 1.5f, -2.5f, 1.5f, -2.5f, -1.5f, -1.5f, -2.5f };
    private List<float> emitterHeight = new List<float> { 1.5f, 1.5f, 3f, 1.5f, 4.5f, 1.5f, 1.5f, 4.5f, 4.5f, 4.5f, 1.5f, 3f, 3f, 3f, 1.5f, 3f, 4.5f, 4.5f, 3f, 3f, 1.5f, 1.5f, 1.5f, 3f, 1.5f, 1.5f, 1.5f, 4.5f, 4.5f, 4.5f, 1.5f, 4.5f, 3f, 1.5f, 3f, 1.5f, 3f, 3f, 3f, 4.5f, 1.5f, 4.5f, 3f, 4.5f, 1.5f, 4.5f, 4.5f, 3f, 4.5f, 1.5f, 1.5f, 4.5f, 3f, 3f, 1.5f, 3f, 3f, 4.5f, 4.5f, 1.5f };
    private List<float> emitterDropPosition = new List<float> { 2, -2, -2, -0, 0, -2, - 2, 2, -2, 2, 2, 2, -0, -2, -0, 2, -0, 0, 2, 0, -2, 2, -0, -2, 0, 2, -2, -2, -2, 0, 2, -2, -2, 2, 2, 0, 2, 0, -0, 2, 0, -0, -2, 2, -0, -0, -2, -0, 2, -2, -2, 0, 2, -2, -2, 0, -2, -2, -2, 2 };
    //private List<float> basketStartPosition = new List<float> { -3.3f, -3.3f, 3.3f, -3.3f, 3.3f, 3.3f, -3.3f, -3.3f, 3.3f, 3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, 3.3f, 3.3f, 3.3f, 3.3f, 3.3f, -3.3f, -3.3f, 3.3f, -3.3f, 3.3f, 3.3f, 3.3f, 3.3f, 3.3f, -3.3f, 3.3f, 3.3f, 3.3f, 3.3f, 3.3f, 3.3f, -3.3f, 3.3f, 3.3f, -3.3f, 3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, -3.3f, 3.3f, 3.3f, -3.3f, 3.3f, -3.3f, 3.3f, -3.3f, -3.3f, -3.3f, -3.3f };

    int basketStartPosition = 0;


    // for saving actualLandingPosition, which might be slightly different (depending on the velocity) to the pre-deptermined landingPosition
    private List<float> actualDropPosition = new List<float> { 10, 10, 10, 10, 10, 10, 10, 10, 10, -10, 10, -10, -10, -10, -10, 10, 10, 10, 10, 10, -10, 10, 10, -10, 10, -10, 10, -10, -10, 10, -10, 10, 10, -10, -10, 10, 10, -10, -10, -10, 10, -10, 10, -10, -10, 10, -10, 10, -10, 10, 10, 10, 10, 10, -10, -10, -10, -10, 10, -10 };


    // make local variables accessible in various methods
    double DifferenceLandingPosition { get; set; }
    double DifferenceDropPosition { get; set; }
    double LandingPosition { get; set; }
    double ResponseTime { get; set; }

    // reference to timer class
    public Timer timer;

    // task state machine
    public enum State
    {
        Wait,
        Setup,
        TransitionToTask,
        Task,
        TransitionToEstimation,
        Estimation,
        TransitionToSetup,
        EndTraining,
        Break
    }

    public State state;


    // script references
    // private DropController dropController; // ?

    // tracking parameters
    // private int dropCount; // ?

    // general instructions
    public GameObject startInstruction;
    public GameObject breakInstruction;
    public GameObject endInstruction;
    public GameObject estimationInstruction;
    public GameObject endTrainingInstruction;


    private void Start()
    {
        // set the path
        GlobalParameters.Path2 = Application.dataPath + "/Data/" + subjectID + "_task.txt";

        // create .txt file with date and time
        File.WriteAllText(GlobalParameters.Path2, "\n\n\n" + "Date and time: " + DateTime.Now.ToString("yyyyMMdd - HHmmss") + "\n\n" + "trial side height velocity dropPosition trueLandingPosition estimatedLandinPosition DifferenceLandingPosition responseTime DifferenceDropPosition");


        // get emitter rigidbody
        _emitterRigidbody = emitter.GetComponent<Rigidbody>();

        // get basket rigidbody 
        _basketRigidbody = basket.GetComponent<Rigidbody>();

        // get ball rigidbody & mesh renderer
        _ballRigidbody = emitter.GetComponent<Rigidbody>();
        _ballMeshRend = ball.GetComponent<MeshRenderer>();

        // get timer component
        timer = GetComponent<Timer>();

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?

        // initialize trialNr to 1
        trialNr = 1;

        // initialise state 
        state = State.Wait;

    }


    private void Update()
    {

        //waits for instructions to be over
        if (state == State.Wait)
        {

            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide end instruction
            endInstruction.SetActive(false);

            // hide break instruction
            breakInstruction.SetActive(false);

            // hide end training instruction
            endTrainingInstruction.SetActive(false);


            if (Input.GetKeyDown(KeyCode.Y)) 
            {
                state = State.Setup;
            }
        }


        // TASK:

        if (state == State.Setup)
        {

            // show start instruction
            startInstruction.SetActive(true);

            // hide break instruction
            breakInstruction.SetActive(false);

            // hide end training instruction
            endTrainingInstruction.SetActive(false);

            // hide background
            background.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // set basket start position
            basket.transform.position = new Vector3(basketStartPosition, basket.transform.position.y, basket.transform.position.z);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero; // shorthand for writing Vector3(0,0,0)

            // initialise different parameters for each trial
            GlobalParameters.EmitterVelocity = emitterVelocity[trialNr - 1];
            emitter.transform.position = new Vector3(emitterStartPosition[trialNr - 1], emitterHeight[trialNr - 1], emitter.transform.position.z);

            // save side from which the emitter arrives
            if (emitterStartPosition[trialNr - 1] > 0)
            {
                side = 1; // right
            }

            else 
            {
                side = 0; // left
            }


            state = State.TransitionToTask;
        }



        if (state == State.TransitionToTask)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide break instruction
            breakInstruction.SetActive(false);

            // set emitter velocity
            _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

            state = State.Task;

        }


        if (state == State.Task)
        {

            // if position emitter is past dropPoint, save actual position transition to next state
               if (side == 1)
               {

                  if (emitter.transform.position.x < emitterDropPosition[trialNr - 1]) // == <
                
                  {
                      actualDropPosition[trialNr - 1] = emitter.transform.position.x;

                      state = State.TransitionToEstimation;                    
                  }
            }

            else if (side == 0)
            {

                  if (emitter.transform.position.x > emitterDropPosition[trialNr - 1]) // == <
                  {
                      actualDropPosition[trialNr - 1] = emitter.transform.position.x;

                      state = State.TransitionToEstimation;
                  }
            }

        }

        if (state == State.TransitionToEstimation)
        {

            // hide start instruction
            startInstruction.SetActive(false);

            // hide break instruction
            breakInstruction.SetActive(false);

            // occlude entire screen
            occluder.SetActive(true);

            // set timer to duration of next state
            timer.time = estimationMaxDuration;

            state = State.Estimation;

        }


        if (state == State.Estimation)
        {

            // show estimation instruction
            estimationInstruction.SetActive(true);


            // allow basket to be moved
            Vector3 basketPosition = basket.transform.position;

            if (Input.GetKey(KeyCode.RightArrow))

            {

                basketPosition.x += 0.5f;
                basket.transform.position = basketPosition;

            }

            if (Input.GetKey(KeyCode.LeftArrow))

            {

                basketPosition.x -= 0.5f;
                basket.transform.position = basketPosition;

            }


            // save data and transition to next state when enter is pressed
            if (Input.GetKey(KeyCode.Return))
            {
                double responseTime = estimationMaxDuration - timer.time;
                ResponseTime = responseTime;
                state = State.TransitionToSetup;

            }

            else if (timer.time <= 0)
            {

                ResponseTime = 000;
                state = State.TransitionToSetup;

            }
        }

        if (state == State.TransitionToSetup)
        {

            CalculateEndPosition();
            SaveData();

            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // show background
            background.SetActive(true);

            // update trial number
            trialNr++;

           if (trialNr == 7)
            {
                state = State.EndTraining;
            }

            if (trialNr == 25)
            {
                state = State.Break;
            }

            if (trialNr == 43)
            {
                state = State.Break;
            }

            else if (trialNr < nrTrials && trialNr != 7 && trialNr != 25 && trialNr != 43)
            {
                state = State.Setup;

            }

            else if (trialNr == nrTrials)
            {
                // show end instruction
                endInstruction.SetActive(true);
            }

        }

        if (state == State.EndTraining)
        {
            // show background
            background.SetActive(true);

            // show endtraining instructions
            endTrainingInstruction.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Setup;
            }


        }

        if (state == State.Break)
        {

            // show break instruction
            breakInstruction.SetActive(true);

            // show background
            background.SetActive(true);

            // shide end instruction
            endInstruction.SetActive(false);

            // if space key is pressed transition to setup
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Setup;
            }


        }


    }

    void CalculateEndPosition()
    {

        double gravity = 9.81;

        // equation for vertical part (- basket position because it is negative)
        // double travelTime = Math.Sqrt((2 * (emitterHeight[trialNr - 1] - basket.transform.position.y)) / gravity);

        double travelTime = Math.Sqrt((2 * (emitterHeight[trialNr - 1] + 3.6f - 0.5f)) / gravity); // - 3.6 = position of basket relative to 0, 0.5 = position of ball relative to emitter

        // equation for horizontal part
        double horDistance = ((emitterVelocity[trialNr - 1] * travelTime)); // horizontal distance travelled

        double landingPosition = actualDropPosition[trialNr - 1] + horDistance; // changed here

        LandingPosition = landingPosition;

        double difference = basket.transform.position.x - landingPosition; // estimated - true -> if pos: estimated towards right, if neg: towards left
        DifferenceLandingPosition = difference;

        double differenceDrop = basket.transform.position.x - actualDropPosition[trialNr - 1]; // estimated - dropPos -> if almost 0 = straight-down belief
        DifferenceDropPosition = differenceDrop;

    }

    void SaveData()
    {

        // Content of the file
        //    string content = "trial : " + trialNr + "\n" + "emitter start location : " + side + "\n" + "correct location : " + LandingPosition + "\n" + "estimated location : "
        //        + basket.transform.position.x + "\n" + "difference : " + Difference + "\n" + "response time : " + ResponseTime + "\n\n";

        string content = "\n" + trialNr + " " + side + " " + (emitterHeight[trialNr - 1] + 3.6f - 0.5f) + " " + emitterVelocity[trialNr - 1] + " " + actualDropPosition[trialNr - 1] + " " + LandingPosition + " " + basket.transform.position.x + " " + DifferenceLandingPosition + " " + ResponseTime + " " + DifferenceDropPosition;
        // Add content to existing file
        File.AppendAllText(GlobalParameters.Path2, content); //writealltext will replace text

    }


}

