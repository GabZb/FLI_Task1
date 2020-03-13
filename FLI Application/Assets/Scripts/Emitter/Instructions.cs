using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    // reference to timer class
    public Timer timer;


    // specific instructions
    public GameObject ArrowsInstructions;
    public GameObject BasketInstructions;
    public GameObject EndEstimationInstructions;
    public GameObject EndInstructions;


    public GameObject FallInstructions;


    public GameObject emitter;

    public GameObject ball;
    private MeshRenderer _ballMeshRend;

    public GameObject instructionBall;
    private Rigidbody _instructionBallRigidbody;
    private MeshRenderer _instructionBallMeshRend;

    public GameObject basket;
    private MeshRenderer _basketMeshRend;

    public GameObject occluder;

    public GameObject background;

    //  private Rigidbody _basketRigidbody;

    // task state machine
    public enum State
    {
        Wait,
        Start,
        StaticFall,
        Arrows,
        Basket,
        EndEstimation,
        Questions,
        Stop

    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {

        // get timer component
        timer = GetComponent<Timer>();

        // get basket and ball component
        _basketMeshRend = basket.GetComponent<MeshRenderer>();
        _ballMeshRend = ball.GetComponent<MeshRenderer>();


        // get ball rigidbody and mesh renderer
        _instructionBallRigidbody = instructionBall.GetComponent<Rigidbody>();
        _instructionBallMeshRend = instructionBall.GetComponent<MeshRenderer>();


        state = State.Wait;

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Wait:
              if (Input.GetKeyDown(KeyCode.Space))
                state = State.Start;
                break;

            case State.Start:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.StaticFall;
                break;

            case State.StaticFall:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.Arrows;
                break;

            case State.Arrows:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.Basket;
                break;

            case State.Basket:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.EndEstimation;
                break;

            case State.EndEstimation:
            if (Input.GetKeyDown(KeyCode.Return))
                state = State.Questions;
                break;
        }



        if (state == State.Wait)
        {

            // show background
            background.SetActive(false);

            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);

            FallInstructions.SetActive(false);

            // make instructional ball invisible
            _instructionBallMeshRend.enabled = false;

        }

        if (state == State.Start)
        {

            // show basket
            _basketMeshRend.enabled = true;

            // hide occluder
            occluder.SetActive(false);

            // show background
            background.SetActive(false);

            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);


            FallInstructions.SetActive(true);

            // make child ball disappear 
            _ballMeshRend.enabled = false;

            // make instruction ball appear and stay static
            _instructionBallMeshRend.enabled = true;
            _instructionBallRigidbody.useGravity = false;

            emitter.transform.position = new Vector3(0, 4.5f, emitter.transform.position.z);
            instructionBall.transform.position = new Vector3(0, instructionBall.transform.position.y, instructionBall.transform.position.z);

        }

        if (state == State.StaticFall)
        {

            emitter.transform.position = new Vector3(0, 4.5f, emitter.transform.position.z);
            instructionBall.transform.position = new Vector3(0, instructionBall.transform.position.y, instructionBall.transform.position.z);


            // hide occluder
            occluder.SetActive(false);


            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);


            FallInstructions.SetActive(false);

            // make instruction ball fall 
            _instructionBallRigidbody.useGravity = true;


        }


        if (state == State.Arrows)
        {

            Destroy(instructionBall);


            // show correct instructions
            ArrowsInstructions.SetActive(true);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);


            FallInstructions.SetActive(false);


            // make occluder appears
            occluder.SetActive(true);

            timer.time = 5;

        }

        if (state == State.Basket)
        {

            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(true);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);

            basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, basket.transform.position.z);


            if (timer.time <= 4.5)

            {
                // set basket start position
                basket.transform.position = new Vector3(2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 4)

            {
                // set basket start position
                basket.transform.position = new Vector3(4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 3.5)

            {
                // set basket start position
                basket.transform.position = new Vector3(6f, basket.transform.position.y, basket.transform.position.z);

            }

            if (timer.time <= 3)

            {
                // set basket start position
                basket.transform.position = new Vector3(4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 2.5)

            {
                // set basket start position
                basket.transform.position = new Vector3(2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 2)

            {
                // set basket start position
                basket.transform.position = new Vector3(0f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 1.5)

            {
                // set basket start position
                basket.transform.position = new Vector3(-2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 1)

            {
                // set basket start position
                basket.transform.position = new Vector3(-4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 0.5)

            {
                // set basket start position
                basket.transform.position = new Vector3(-6f, basket.transform.position.y, basket.transform.position.z);
            }

        }

        if (state == State.EndEstimation)
        {

            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(true);
            EndInstructions.SetActive(false);


        }

        if (state == State.Questions)
        {


            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(true);

            _ballMeshRend.enabled = false;

            if (Input.GetKeyDown(KeyCode.Y))
            {
                state = State.Stop;
            }

        }

        if (state == State.Stop)
        {

            // show correct instructions
            ArrowsInstructions.SetActive(false);
            BasketInstructions.SetActive(false);
            EndEstimationInstructions.SetActive(false);
            EndInstructions.SetActive(false);

            _ballMeshRend.enabled = true;


        }
    }
}
