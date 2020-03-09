using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using System.IO;

public class Control : MonoBehaviour
{
    // reference to timer class
    public Timer timer;

    // objects set in inspector
    public GameObject emitter;
    private Rigidbody _emitterRigidbody;

    public GameObject ball;
    private Rigidbody _ballRigidbody;
    private MeshRenderer _ballMeshRend;

    public GameObject basket;
    private MeshRenderer _basketMeshRend;

    public GameObject occluder;

    public GameObject background;

    // control experiment parameters
    int subjectID = 01;
    private int nrTrials = 6; // total number of trials

    // parameter tracking
    private int trialNr;
    private int side;

    // make local variables accessible in various methods
    double Difference { get; set; }
    double EstimatedExitPosition { get; set; }


    // Lists to define heights, velocities, positions 
    private List<float> emitterStartPosition = new List<float>  {10, -10, 10, -10, 10, -10};
    private List<float> emitterVelocity = new List<float> {-1.5f, 3.5f, -3.5f, 3.5f, -3.5f, 3.5f};
    private List<float> emitterHeight = new List<float> {1.5f, 3f, 1.5f, 1.5f, 1.5f, 4.5f};
    private List<float> emitterDropPosition = new List<float> {-0, -2, 2, -2, -2, 0 };
    private List<float> emitterRealExitPosition = new List<float> { -8, 8, -8, 8, -8, 8 };


    // control state machine
    public enum State
    {
        GeneralInstructions,
        Emitter,
        Occluder,
        ControlInstructions,
        ControlSetup,
        ControlTask,
        EndControlInstructions,
        TransitionToSetup,
        Stop
    }

    public State state;

    // instructions
    public GameObject generalInstructions;
    public GameObject emitterInstructions;
    public GameObject occluderInstructions;

    public GameObject controlInstruction;
    public GameObject endControlInstruction;

    // Start is called before the first frame update
    void Start()
    {
        // get timer component
        timer = GetComponent<Timer>();

        // set the path
        GlobalParameters.Path = Application.dataPath + "/Data/" + subjectID + "_control.txt";

        // create .txt file with date and time
        File.WriteAllText(GlobalParameters.Path, "\n\n\n" + "Date and time: " + DateTime.Now.ToString("yyyyMMdd - HHmmss") + "\n\n" + "realExitPosition estimatedExitPosition difference");

        // get ball, emitter and basket components
        _emitterRigidbody = emitter.GetComponent<Rigidbody>();
        _ballRigidbody = ball.GetComponent<Rigidbody>();
        _basketMeshRend = basket.GetComponent<MeshRenderer>();
        _ballMeshRend = ball.GetComponent<MeshRenderer>();

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?

        // initialize trialNr to 1
        trialNr = 1;

        state = State.GeneralInstructions;

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            case State.GeneralInstructions:
                if (Input.GetKeyDown(KeyCode.Return))
                    state = State.Emitter;
                break;

            case State.Emitter:
                if (Input.GetKeyDown(KeyCode.Return))
                    state = State.Occluder;
                break;

            case State.Occluder:
                if (Input.GetKeyDown(KeyCode.Return))
                    state = State.ControlInstructions;
                break;

            case State.ControlInstructions:
                if (Input.GetKeyDown(KeyCode.X))
                    state = State.ControlSetup;
                break;

        }



        if (state == State.GeneralInstructions)
        {

            // hide basket
            _basketMeshRend.enabled = false;

            // show background
            background.SetActive(true);

            // hide occluder
            occluder.SetActive(false);

            // show right instructions
            generalInstructions.SetActive(true);
            emitterInstructions.SetActive(false);
            occluderInstructions.SetActive(false);
            controlInstruction.SetActive(false);
            endControlInstruction.SetActive(false);

            timer.time = 5.5f;

        }

        if (state == State.Emitter)
        {

            // make ball visible
            _ballMeshRend.enabled = true;

            // hide background
            background.SetActive(false);

            // hide occluder
            occluder.SetActive(false);

            // show right instructions
            generalInstructions.SetActive(false);
            emitterInstructions.SetActive(true);
            occluderInstructions.SetActive(false);
            controlInstruction.SetActive(false);
            endControlInstruction.SetActive(false);


            emitter.transform.position = new Vector3(-10, emitter.transform.position.y, emitter.transform.position.z);


            if (timer.time <= 4.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-8f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 4)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-7f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 3.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-6f, emitter.transform.position.y, emitter.transform.position.z);

            }

            if (timer.time <= 3)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-5f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 2.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-4f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 2)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-3f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 1.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-2f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 1)

            {
                // set basket start position
                emitter.transform.position = new Vector3(-1f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 0.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(0f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= 0)

            {
                // set basket start position
                emitter.transform.position = new Vector3(1f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -0.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(2f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -1)

            {
                // set basket start position
                emitter.transform.position = new Vector3(3f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -1.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(4f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -2)

            {
                // set basket start position
                emitter.transform.position = new Vector3(5f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -2.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(6f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -3)

            {
                // set basket start position
                emitter.transform.position = new Vector3(7f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -3.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(8f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -4)

            {
                // set basket start position
                emitter.transform.position = new Vector3(9f, emitter.transform.position.y, emitter.transform.position.z);
            }

            if (timer.time <= -4.5)

            {
                // set basket start position
                emitter.transform.position = new Vector3(10f, emitter.transform.position.y, emitter.transform.position.z);
            }
        }

        if (state == State.Occluder)
        {

            // show occluder
            occluder.SetActive(true);

            // show right instructions
            generalInstructions.SetActive(false);
            emitterInstructions.SetActive(false);
            occluderInstructions.SetActive(true);
            controlInstruction.SetActive(false);
            endControlInstruction.SetActive(false);

        }


        if (state == State.ControlInstructions)
        {

            // hide occluder
            occluder.SetActive(false);

            // show background
            background.SetActive(true);

            // show right instructions
            generalInstructions.SetActive(false);
            emitterInstructions.SetActive(false);
            occluderInstructions.SetActive(false);
            controlInstruction.SetActive(true);
            endControlInstruction.SetActive(false);

        }


        if (state == State.ControlSetup)
        {


            // make ball visible
            _ballMeshRend.enabled = true;

            // hide occluder
            occluder.SetActive(false);

            // hide background
            background.SetActive(false);

            // hide all instructions
            generalInstructions.SetActive(false);
            emitterInstructions.SetActive(false);
            occluderInstructions.SetActive(false);
            controlInstruction.SetActive(false);
            endControlInstruction.SetActive(false);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero; // shorthand for writing Vector3(0,0,0)

            // initialise different parameters for each trial
            GlobalParameters.EmitterVelocity = emitterVelocity[trialNr - 1];
            emitter.transform.position = new Vector3(emitterStartPosition[trialNr - 1], emitterHeight[trialNr - 1], emitter.transform.position.z);

            // set emitter velocity
            _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

            // save side from which the emitter arrives
            if (emitterStartPosition[trialNr - 1] > 0)
            {
                side = 1; // right
            }

            else
            {
                side = 0; // left
            }

            // initialise state 
            state = State.ControlTask;
        }


        if (state == State.ControlTask)
        {


            // make ball visible
            _ballMeshRend.enabled = true;

            // if position emitter is past dropPoint, occlude screen
            if (side == 1)
            {
                if (emitter.transform.position.x < emitterDropPosition[trialNr - 1]) 

                {
                    // occlude entire screen
                    occluder.SetActive(true);

                }
            }

            else if (side == 0)
            {
                if (emitter.transform.position.x > emitterDropPosition[trialNr - 1]) 
                {
                    // occlude entire screen
                    occluder.SetActive(true);

                }
            }


            // if space key pressed, save data and transition to next state

            if (Input.GetKeyDown(KeyCode.Return))
            {

                 double estimatedExitPosition = emitter.transform.position.x;
                 EstimatedExitPosition = estimatedExitPosition;
                 double difference = emitterRealExitPosition[trialNr - 1] - EstimatedExitPosition;
                 Difference = difference;

                 SaveData();
                 state = State.TransitionToSetup;
            }
                

        }

        if (state == State.TransitionToSetup)
        {
            // update trial number
            trialNr++;

            if (trialNr <= nrTrials)
            {
                state = State.ControlSetup;
            }

            else if (trialNr > nrTrials)
            {

                state = State.EndControlInstructions;
            }
        }

        if (state == State.EndControlInstructions)
        {

            // show background
            background.SetActive(true);

            // show right instructions
            generalInstructions.SetActive(false);
            emitterInstructions.SetActive(false);
            occluderInstructions.SetActive(false);
            controlInstruction.SetActive(false);
            endControlInstruction.SetActive(true);


            _emitterRigidbody.velocity = Vector3.zero;
            emitter.transform.position = new Vector3(0, 4.5f, emitter.transform.position.z);

         //   _ballMeshRend.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Stop;
            }

        }

        if (state == State.Stop)
        {

            // hide background
            background.SetActive(false);

            // hide end control instructions
            endControlInstruction.SetActive(false);

        }


        void SaveData()
        {


            string content = "\n\n" + trialNr + " " + emitterRealExitPosition[trialNr - 1] + " " + EstimatedExitPosition + " " + Difference;

            // Add content to existing file
            File.AppendAllText(GlobalParameters.Path, content); //writealltext will replace text

        }

    }
}
