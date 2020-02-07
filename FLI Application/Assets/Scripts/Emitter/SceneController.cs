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
    private MeshRenderer _emitterMeshRend;

    public GameObject basket;
    private Rigidbody _basketRigidbody;

    public GameObject ball;
    private Rigidbody _ballRigidbody;
    private MeshRenderer _ballMeshRend;

    public GameObject occluder;
    public GameObject background;

    // experiment parameters

    int subjectID = 01; // make it inputable
    private int nrTrials = 3; // total number of trials
    private float estimationMaxDuration = 10f;

    // Lists to define heights, velocities, positions 

    //  private List<float> emitterHeight = new List<float> { 4.15f, 4.15f, 4.15f, 4.15f, 4.15f };
    //  private List<float> emitterVelocity = new List<float> { 1f, -3f, 5f, 2f, -6f };
    //   private List<float> emitterStartPosition = new List<float> { -10, 10, -10, -10, 10 }; // influences time it takes for emitter to appear (with velocity)
    //   private List<float> emitterDropPosition = new List<float> { -1, 7, -5, 3, 0 };

    private List<float> emitterStartPosition = new List<float> { -10, 10, 10, 10, 10, -10, -10, -10, 10, 10, 10, 10, -10, -10, -10, 10, -10, 10, 10, 10, -10, 10, -10, 10, -10, -10, 10, 10, 10, 10, -10, -10, -10, 10, 10, 10, -10, 10, 10, -10, -10, 10, -10, -10, -10, -10, -10, 10, 10, 10, -10, -10, 10, 10, 10, -10, -10, -10, -10, 10 }; // influences time it takes for emitter to appear (with velocity)
    private List<float> emitterVelocity = new List<float> { 4.5f, -4.5f, -1.5f, -1.5f, -3f, 3f, 1.5f, 4.5f, -4.5f, -1.5f, -1.5f, -3f, 4.5f, 1.5f, 3f, -1.5f, 1.5f, -4.5f, -3f, -1.5f, 4.5f, -4.5f, 4.5f, -4.5f, 3f, 1.5f, -3f, -3f, -4.5f, -3f, 1.5f, 3f, 4.5f, -3f, -1.5f, -4.5f, 1.5f, -3f, -1.5f, 3f, 3f, -4.5f, 1.5f, 4.5f, 4.5f, 4.5f, 3f, -1.5f, -4.5f, -4.5f, 4.5f, 3f, -3f, -1.5f, -3f, 1.5f, 1.5f, 1.5f, 3f, -3f };
    private List<float> emitterHeight = new List<float> { 1f, 5f, 5f, 1f, 3f, 5f, 3f, 5f, 3f, 5f, 1f, 3f, 5f, 5f, 3f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 1f, 5f, 5f, 3f, 1f, 5f, 1f, 3f, 3f, 5f, 1f, 5f, 3f, 3f, 3f, 5f, 5f, 3f, 1f, 3f, 1f, 5f, 3f, 5f, 3f, 1f, 3f, 1f, 1f, 1f, 3f, 1f, 1f, 5f, 5f, 1f };
    private List<float> emitterDropPosition = new List<float> { -3, 0, -3, 2, 1, 1, 1, 2, -0, -2, -3, -0, -2, -2, -1, -1, 2, 2, -1, 1, 0, 1, 3, -2, 2, 3, -2, 2, 1, -3, -2, -2, 0, 1, 3, -3, 0, 0, 0, -1, 2, -2, -1, 3, -1, -1, 1, 2, -3, 3, 2, -3, -2, 1, -1, 3, -3, -1, 3, 3 };

    // parameter tracking
    private int trialNr; // current trial number

    // reference to timer class
    public Timer timer;

    // task state machine
    public enum State
    {
        Wait,
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

    // general instructions
    public GameObject startInstruction;
    public GameObject restInstruction;
    public GameObject endInstruction;
    public GameObject estimationInstruction;

    public int side;

    private void Start()
    {

        // set the path
        GlobalParameters.Path = Application.dataPath + "/Data/" + subjectID + "_responses.txt";

        // get reference to scripts
        // dropController = GetComponent<DropController>();

        // get emitter rigidbody & mesh renderer
        _emitterRigidbody = emitter.GetComponent<Rigidbody>();
        _emitterMeshRend = emitter.GetComponent<MeshRenderer>();

        // get basket rigidbody 
        _basketRigidbody = basket.GetComponent<Rigidbody>();

        // get ball rigidbody & mesh renderer
        _ballRigidbody = emitter.GetComponent<Rigidbody>();
        _ballMeshRend = ball.GetComponent<MeshRenderer>();

        // get timer component
        timer = GetComponent<Timer>();

        // create .txt file with date and time
        File.WriteAllText(GlobalParameters.Path, "\n\n\n" + "Date and time: " + DateTime.Now.ToString("yyyyMMdd - HHmmss") + "\n\n" + "trial side height velocity trueLandingPosition estimatedLandinPosition difference responseTime");

        // initialise add velocity to true 
        GlobalParameters.AddVelocity = true; // ?

        // initialize trialNr to 1
        trialNr = 1;

        // initialise state 
        state = State.Wait;

    }



    private void Update()
    {

        

        /// task states:
        
        //waits for instructions to be over
        if (state == State.Wait)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // hide estimation instruction
            estimationInstruction.SetActive(false);

            // hide end instruction
            endInstruction.SetActive(false);

            // hide rest instruction
            restInstruction.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Setup;
            }
        }

        if (state == State.Setup)
        {


            //            //make emitter visible
            _emitterMeshRend.enabled = true;

//            //make ball visible
            _ballMeshRend.enabled = true;

            // show start instruction
            startInstruction.SetActive(true);

            // hide rest instruction
            restInstruction.SetActive(false);

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


            // if position emitter is at drop point,
            if (emitterVelocity[trialNr - 1] > 0)
               {

                  if (emitter.transform.position.x > emitterDropPosition[trialNr - 1]) // == <
                  {

                      state = State.TransitionToEstimation;

                    //make emitter and ball freeze
                    GlobalParameters.EmitterVelocity = -emitterVelocity[trialNr - 1];
                    _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

                    // make emitter disappear
                    _emitterMeshRend.enabled = false;

////                    //make ball disappear
                   _ballMeshRend.enabled = false;

 //                  occluder.SetActive(true);

                    
                }
            }

            else

               {

                  if (emitter.transform.position.x < emitterDropPosition[trialNr - 1]) // == <
                  {

                      state = State.TransitionToEstimation;

                    //make emitter and ball freeze
                    GlobalParameters.EmitterVelocity = - emitterVelocity[trialNr - 1];
                    _emitterRigidbody.AddForce(new Vector3(GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

                    // make emitter disappear
                    _emitterMeshRend.enabled = false;

////                   //make ball disappear
                    _ballMeshRend.enabled = false;

 //                   occluder.SetActive(true);


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
//            occluder.SetActive(true);

            // set timer to duration of next state
            timer.time = estimationMaxDuration;

            state = State.Estimation;

        }


        if (state == State.Estimation)
        {

         //   if (timer.time <= 9.5)
         //   {
                // show estimation instruction
                estimationInstruction.SetActive(true);
         //   }

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
    //    string content = "trial : " + trialNr + "\n" + "emitter start location : " + side + "\n" + "correct location : " + LandingPosition + "\n" + "estimated location : "
    //        + basket.transform.position.x + "\n" + "difference : " + Difference + "\n" + "response time : " + ResponseTime + "\n\n";

        string content = "\n\n" + trialNr + " " + (emitterHeight[trialNr - 1] + 3.6f - 0.5f) + " " + side + " " + emitterVelocity[trialNr - 1] + " " + LandingPosition + " " + basket.transform.position.x + " " + Difference + " " + ResponseTime;

        // Add content to existing file
        File.AppendAllText(GlobalParameters.Path, content); //writealltext will replace text

    }

    void CalculateEndPosition()
    {

        double gravity = 9.81;

        // equation for vertical part (- basket position because it is negative)
        // double travelTime = Math.Sqrt((2 * (emitterHeight[trialNr - 1] - basket.transform.position.y)) / gravity);

        double travelTime = Math.Sqrt((2 * (emitterHeight[trialNr - 1] + 3.6f - 0.5f)) / gravity); // - 3.6 = position of basket relative to 0, 0.5 = position of ball relative to emitter

        // equation for horizontal part
        double horDistance = ((emitterVelocity[trialNr - 1] * travelTime)); // horizontal distance travelled

        double landingPosition = emitterDropPosition[trialNr - 1] + horDistance;

        LandingPosition = landingPosition;

        double difference = basket.transform.position.x - landingPosition;

        Difference = difference;

    }


}

