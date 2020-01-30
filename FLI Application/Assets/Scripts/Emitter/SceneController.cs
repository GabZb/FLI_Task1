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
    private List<float> emitterVelocity = new List<float> { 0.03f, -0.1f, 0.01f, 0.1f, -0.2f };
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
        SaveData,
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

        // initialise occluder to false
        occluder.SetActive(false);

        // initialise background to false
        background.SetActive(false);

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?


        // initialize trialNr to 1
        trialNr = 1;

        if (trialNr == 1)
        {

            CreateTextFile();

        }


        // hide end instruction
        endInstruction.SetActive(false);


        // initialise state
        state = State.GetReady;


        if (emitterStartPosition[trialNr - 1] > 1)
        {
            side = 1;
        }

        if (emitterStartPosition[trialNr - 1] < 1)
        {
            side = 0;
        }

    }



    private void Update()
    {

        if (state == State.GetReady)


        {


            // initialise different parameters for each trial

            GlobalParameters.EmitterVelocity = emitterVelocity[trialNr - 1];
            emitter.transform.position = new Vector3(emitterStartPosition[trialNr - 1], emitterHeight[trialNr - 1], emitter.transform.position.z);

            // show start instruction
            startInstruction.SetActive(true);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // hide background
            background.SetActive(false);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero; // shorthand for writing Vector3(0,0,0)


            // set basket start position
            basket.transform.position = new Vector3(_basketStartPosition, basket.transform.position.y, basket.transform.position.z);

            // if enter key is pressed transition to next state
            if (Input.GetKeyDown(KeyCode.Return))
            {
                state = State.Task;
            }


        }


        if (state == State.Task)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);


            // set emitter velocity
            _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);


            //Debug.Log(emitter.transform.position);
            //Debug.Log(emitter.transform.position.x);
            //Debug.Log(GlobalParameters.EmitterVelocity);

            // if position emitter is at drop point, make occluder appear

            //if (Mathf.Approximately(emitter.transform.position.x,_emitterDropPosition))
            if (emitterVelocity[trialNr - 1] > 0)
            {

                if (emitter.transform.position.x > emitterDropPosition[trialNr - 1]) // == <
                {
                    state = State.Estimation;

                    // set timer to duration of next state
                    timer.time = estimationMaxDuration;

                }
            }

            else if (emitterVelocity[trialNr - 1] < 0)

            {

                if (emitter.transform.position.x < emitterDropPosition[trialNr - 1]) // == <
                {
                    state = State.Estimation;

                    // set timer to duration of next state
                    timer.time = estimationMaxDuration;

                }
            }

        }

        if (state == State.Estimation)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // occlude screen
            occluder.SetActive(true);

            if (timer.time <= 9.5)
            {

                // show estimation instruction
                estimationInstruction.SetActive(true);

            }

            // allow basket to be moved

            if (Input.GetKey(KeyCode.RightArrow))

            {

                Vector3 basketPosition = basket.transform.position;
                basketPosition.x += 0.1f;
                basket.transform.position = basketPosition;
            }

            if (Input.GetKey(KeyCode.LeftArrow))

            {

                Vector3 basketPosition = basket.transform.position;
                basketPosition.x -= 0.1f;
                basket.transform.position = basketPosition;

            }


    //        if (trialNr < nrTrials)
    //        {

                if (Input.GetKey(KeyCode.Return))
                {
                    double responseTime = estimationMaxDuration - timer.time;
                    ResponseTime = responseTime;

                    CalculateEndPosition();
                    SaveData();

                    state = State.Rest;

                }

                else if (timer.time <= 0)
                {

                    ResponseTime = 000;
                    CalculateEndPosition();
                    SaveData();

                    state = State.Rest;

                }
    //        }

    //        else

    //        {

    //            if (Input.GetKey(KeyCode.Return))

    //            {
    //                double responseTime = estimationMaxDuration - timer.time;
    //                ResponseTime = responseTime;
    //                Debug.Log(responseTime);

    //                CalculateEndPosition();
    //                SaveData();

    //                state = State.End;

    //            }

     //           else if (timer.time <= 0)
     //           {
     //               CalculateEndPosition();
     //               SaveData();

     //               state = State.End;

      //          }

     //       }
        }



        if (state == State.Rest)

        {

            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // show background
            background.SetActive(true);

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
                    state = State.GetReady;
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

    void CreateTextFile()

    {
        
        File.WriteAllText(GlobalParameters.Path, "\n\n\n" + "Date and time: " + DateTime.Now.ToString("yyyyMMdd - HHmmss") + "\n\n");

    }

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
        double landingPosition = emitterVelocity[trialNr - 1] * travelTime;

        LandingPosition = landingPosition;

        double difference = basket.transform.position.x - landingPosition;

        Difference = difference;


    }


}

