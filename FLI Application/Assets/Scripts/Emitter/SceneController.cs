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


    //private float _emitterEndPosition = 12f;
    private float _emitterDropPosition;
    //private float _emitterStartPosition;

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

    private int subjectID;
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

    //private List<float> estimationLocationList; ?
    //private List<float> stimationDurationList; ?

    // reference to timer class
    public Timer timer;

    // state machine
    public enum State
    {
        GetReady,
        Task,
        Estimation,
        Rest,
        End,

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


    private void Start()
    {

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

        // initialise obackground to false
        background.SetActive(false);

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?

// initialise emitter velocity to 5f 
      //  GlobalParameters.EmitterVelocity = 0.03f; // ?


        // initialize trialNr to 1
        trialNr = 1;

        // initialise state
        state = State.GetReady;
    }



    private void Update()
    {
        if (state == State.GetReady)


        {
            // initialise different emitter velocity for each trial

            GlobalParameters.EmitterVelocity = emitterVelocity[trialNr-1];
            emitter.transform.position = new Vector3(emitterStartPosition[trialNr-1], emitter.transform.position.y, emitter.transform.position.z);
            _emitterDropPosition = emitterDropPosition[trialNr-1];

            Debug.Log(trialNr);
            Debug.Log(emitter.transform.position);
            Debug.Log(emitter.transform.position.x);
            Debug.Log(GlobalParameters.EmitterVelocity);
            // show start instruction
            startInstruction.SetActive(true);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // hide end instruction
            endInstruction.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // hide background
            background.SetActive(false);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero; // shorthand for writing Vector3(0,0,0)

            // set emitter position
            //emitter.transform.position = new Vector3(_emitterStartPosition, emitter.transform.position.y, emitter.transform.position.z);

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


            //   Debug.Log(emitter.transform.position.x);

            // if position emitter is at drop point, make occluder appear

            //if (Mathf.Approximately(emitter.transform.position.x,_emitterDropPosition))
            if (emitterVelocity[trialNr - 1] > 0)
            {

                if (emitter.transform.position.x > _emitterDropPosition) // == <
                {
                    state = State.Estimation;

                    // set timer to duration of next state
                    timer.time = estimationMaxDuration;

                }
            }

            else if (emitterVelocity[trialNr - 1] < 0)

            {

                if (emitter.transform.position.x < _emitterDropPosition) // == <
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


            if (trialNr < nrTrials)
            {

                if (Input.GetKey(KeyCode.Return))
                {
                    CreateTextFile();
                    state = State.Rest;
                }

                else if (timer.time <= 0)
                {
                    state = State.Rest;
                }
            }

            else

            {

                if (Input.GetKey(KeyCode.Return))

                {
                    CreateTextFile();
                    state = State.End;
                }

                else if (timer.time <= 0)
                {
                    state = State.End;
                }

            }
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



        if (state == State.End)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            // show end instruction
            endInstruction.SetActive(true);

            // hide occluder
            occluder.SetActive(false);

            // show background
            background.SetActive(true);

        }

    }

    void CreateTextFile()
    {

        // Path of the file

        string path = Application.dataPath + "/Location.txt";

        // Create file if it doesn't exist


        File.WriteAllText(path, "Location of bucket \n\n");

        // Content of the file

        string content = "chosen location" + transform.position + "\n";

        // Add content to existing file

        File.AppendAllText(path, content); //writealltext will replace text

    }


}

   // public void SetTrial()
   // {
   //     // transition to initialisation of trial state
   //     state = State.Setup;
   //}

