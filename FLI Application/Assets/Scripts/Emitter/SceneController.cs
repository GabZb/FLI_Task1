using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using System.IO;

public class SceneController : MonoBehaviour
{

    // settable parameters

    private float _basketStartPosition = 0f;

    // objects set in inspector
    public GameObject emitter;
    private Rigidbody _emitterRigidbody;

    public GameObject basket;
    private Rigidbody _basketRigidbody;

    public GameObject ball;
    private Rigidbody _ballRigidbody;

    public GameObject occluder;
    public GameObject background;

    // experiment parameters

    //private int subjectID;
    private int nrTrials = 5; // total number of trials
    private float estimationMaxDuration = 10f;

    // Lists to define heights, velocities, positions 

    private List<float> emitterHeight = new List<float> { 5f, 4.15f, 3f, 3.15f, 5f };
    private List<float> emitterVelocity = new List<float> { 5f, -3f, 4f, 2f, -6f };
    private List<float> emitterStartPosition = new List<float> { -10, 10, -10, -10, 10 }; // influences time it takes for emitter to appear (with velocity)
    private List<float> emitterDropPosition = new List<float> { -1, 7, -5, 3, 0 };

    // parameter tracking
    private int trialNr; // current trial number
    private float estimationLocation;
    private float estimationDuration;

    // reference to timer class
    public Timer timer;

    // state machine
    public enum State
    {
        Setup,
        GetReady,
        TransitionToTask,
        Task,
        TransitionToEstimation,
        Estimation,
        TransitionToRest,
        Rest
    }

    public State state;


    // script references
    // private DropController dropController; // ?

    // tracking parameters
    // private int dropCount; // ?

    // instructions
    public GameObject startInstruction;
    public GameObject restInstruction;
    public GameObject endInstruction;
    public GameObject estimationInstruction;

    public int side;

    private void Start()
    {

        // set the path
        GlobalParameters.Path = Application.dataPath + "/Responses.txt";

        // get reference to scripts
        // dropController = GetComponent<DropController>();

        // get emitter rigidbody 
        _emitterRigidbody = emitter.GetComponent<Rigidbody>();

        // get basket rigidbody 
        _basketRigidbody = basket.GetComponent<Rigidbody>();

        // get ball rigidbody 
        _ballRigidbody = emitter.GetComponent<Rigidbody>();

        // get timer component
        timer = GetComponent<Timer>();

        // create .txt file with date and time
        File.WriteAllText(GlobalParameters.Path, "\n\n\n" + "Date and time: " + DateTime.Now.ToString("yyyyMMdd - HHmmss") + "\n\n");

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?

        // initialize trialNr to 1
        trialNr = 1;

        // initialise state
        state = State.Setup;

    }



    private void Update()
    {

        if (state == State.Setup)
        {

            // show start instruction
            startInstruction.SetActive(true);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // hide end instruction
            endInstruction.SetActive(false);

            // hide background
            background.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // set basket start position
            basket.transform.position = new Vector3(_basketStartPosition, basket.transform.position.y, basket.transform.position.z);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero; // shorthand for writing Vector3(0,0,0)

            // initialise different parameters for each trial
            GlobalParameters.EmitterVelocity = emitterVelocity[trialNr - 1];
            emitter.transform.position = new Vector3(emitterStartPosition[trialNr - 1], emitterHeight[trialNr - 1], emitter.transform.position.z);

            // save side from which the emitter arrives
            if (emitterStartPosition[trialNr - 1] > 1)
            {
                side = 1;
            }

            else 
            {
                side = 0;
            }

            state = State.GetReady;
        }


        if (state == State.GetReady)
        {
            // if enter key is pressed transition to next state
            if (Input.GetKeyDown(KeyCode.Return))
            {
                state = State.TransitionToTask;
            }

        }


        if (state == State.TransitionToTask)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // set emitter velocity
            _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

            state = State.Task;

        }


        if (state == State.Task)
        {

            //if (Mathf.Approximately(emitter.transform.position.x,_emitterDropPosition))


            // if position emitter is at drop point, make occluder appear
            if (emitterVelocity[trialNr - 1] > 0)
               {

                  if (emitter.transform.position.x > emitterDropPosition[trialNr - 1]) // == <
                  {

                      state = State.TransitionToEstimation;

                  }
               }

            else

               {

                  if (emitter.transform.position.x < emitterDropPosition[trialNr - 1]) // == <
                  {

                      state = State.TransitionToEstimation;

                  }
               }

        }

        if (state == State.TransitionToEstimation)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // occlude screen
            occluder.SetActive(true);

            // set timer to duration of next state
            timer.time = estimationMaxDuration;

            state = State.Estimation;

        }


        if (state == State.Estimation)
        {

            if (timer.time <= 9.5)
            {
                // show estimation instruction
                estimationInstruction.SetActive(true);
            }

            // allow basket to be moved
            Vector3 basketPosition = basket.transform.position;

            if (Input.GetKey(KeyCode.RightArrow))

            {

                basketPosition.x += 0.1f;
                basket.transform.position = basketPosition;

            }

            if (Input.GetKey(KeyCode.LeftArrow))

            {

                basketPosition.x -= 0.1f;
                basket.transform.position = basketPosition;

            }

            // block bucket within screen boundaries


            // save data and transition to next state when enter is pressed
            if (Input.GetKey(KeyCode.Return))
            {
                double responseTime = estimationMaxDuration - timer.time;
                ResponseTime = responseTime;
                state = State.TransitionToRest;

            }

            else if (timer.time <= 0)
            {

                ResponseTime = 000;
                state = State.TransitionToRest;

            }
        }

        if (state == State.TransitionToRest)
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

                state = State.Rest;

        }

        if (state == State.Rest)
        {

            if (trialNr < nrTrials)
            {

                // show rest instruction
                restInstruction.SetActive(true);


                // if enter key is pressed transition to next state
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // update trial number
                    trialNr++;

                    // transition to GetReady state
                    state = State.Setup;
                }
            }

            else
            {

                // hide rest instruction
                restInstruction.SetActive(false);

                // show end instruction
                endInstruction.SetActive(true);

            }
        }


    }

    // make local variables accessible in various methods
    double Difference { get; set; }
    double LandingPosition { get; set; }
    double ResponseTime { get; set; }


    void SaveData()
    {

        // Content of the file
        string content = "trial : " + trialNr + "\n" + "emitter start location : " + side + "\n" + "correct location : " + LandingPosition + "\n" + "estimated location : "
            + basket.transform.position.x + "\n" + "difference : " + Difference + "\n" + "response time : " + ResponseTime + "\n\n";


        // Add content to existing file
        File.AppendAllText(GlobalParameters.Path, content); //writealltext will replace text

    }

    void CalculateEndPosition()
    {

        double gravity = 9.81;

        // equation for vertical part (- basket position because it is negative)
        double travelTime = Math.Sqrt((2 * (emitterHeight[trialNr - 1] - basket.transform.position.y)) / gravity);

        // equation for horizontal part
        double horDistance = ((emitterVelocity[trialNr - 1] * travelTime)); // horizontal distance travelled

        double landingPosition = emitterDropPosition[trialNr - 1] + horDistance;

        LandingPosition = landingPosition;

        double difference = basket.transform.position.x - landingPosition;

        Difference = difference;

    }


}

