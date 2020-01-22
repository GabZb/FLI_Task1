using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class SceneController : MonoBehaviour
{
    // settable parameters
    private float _emitterStartPosition = -12f;
    private float _emitterEndPosition = 12f;
    private float _emitterDropPosition = 3f;

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
    private int nrTrials = 3; // total number of trials
    private float estimationMaxDuration = 10f;

    // Lists to define heights, position, velocity

    private List<float> emitterHeight = new List<float> { 5f, 4.15f, 3f };
    private List<float> emitterVelocity = new List<float> { 0.03f, 0.1f, 0.01f };

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
        GlobalParameters.EmitterVelocity = 0.03f; // ?

        // initialise state
        state = State.GetReady;

        // initialize trialNr to 1
        trialNr = 1;
    }



    private void Update()
    {
            if (state == State.GetReady)


                {
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
                    emitter.transform.position = new Vector3(_emitterStartPosition, emitter.transform.position.y, emitter.transform.position.z);

                    // set basket start position
                    basket.transform.position = new Vector3(_basketStartPosition, basket.transform.position.y, basket.transform.position.z);



            // if enter key is pressed transition to next state
            if (Input.GetKeyDown(KeyCode.Return))
                    {
                        state = State.Task;
                    }
                }

            //        // transition to next state
            //        state = State.Wait;
            //    }

            //    if (state == State.Wait)
            //    {
            //        // if enter key is pressed transition to next state
            //        if (Input.GetKeyDown(KeyCode.Return))
            //            state = State.Task;
            //    }

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

                /// set a timer, condition works if time < delay set manually, but countdown doesn't seem to work:
                //float time = 3;
                // decrease the value of 'timer' by deltaTime 
                //time -= Time.deltaTime;
                //float delay = 2;
                //if (time < delay)

                //   Debug.Log(emitter.transform.position.x);

                // if position emitter is at drop point, make occluder appear

                //if (Mathf.Approximately(emitter.transform.position.x,_emitterDropPosition))
                if (emitter.transform.position.x > _emitterDropPosition) // ==
                {
                    state = State.Estimation;

                    // set timer to duration of next state
                    timer.time = estimationMaxDuration;

                }



                // if emitter position is greater than end position, transition to next state
                //  if (emitter.transform.position.x > _emitterEndPosition)
                // {
                //    state = State.Rest;


            }

            if (state == State.Estimation)
            {
                // hide start instruction
                startInstruction.SetActive(false);

                // hide rest instruction
                restInstruction.SetActive(false);

                // occlude screen
                occluder.SetActive(true);

            if (timer.time <= 5.5)
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




            // save the data into participant folder WHEN ENTER IS PRESSED

            // transition to next state when enter pressed or time up


            if (trialNr < nrTrials)
            {
                    if (timer.time <= 0)
                    {
                        state = State.Rest;
                    }
            }

            else

            {
                if (timer.time <= 0)
                {
                    state = State.End;
                }
            }

            //   Debug.Log(emitter.transform.position.x);

            // if position emitter is at drop point, make occluder appear



            // if emitter position is greater than end position, transition to next state
            //  if (emitter.transform.position.x > _emitterEndPosition)
            // {
            //    state = State.Rest;


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

    }

   // public void SetTrial()
   // {
   //     // transition to initialisation of trial state
   //     state = State.Setup;
   //}

