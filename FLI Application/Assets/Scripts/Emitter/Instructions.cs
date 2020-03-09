using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    // reference to timer class
    public Timer timer;


    // specific instructions
    public GameObject Instructions_2;
    public GameObject Instructions_3;
    public GameObject Instructions_4;
    public GameObject Instructions_5;
    public GameObject Instructions_13;
    public GameObject Instructions_14;
    public GameObject Instructions_15;

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
        Emitter2,
        Emitter3,
        Emitter4,
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

        // make instructional ball invisible
        //        _instructionBallMeshRend.enabled = false;
        //        _instructionBallRigidbody.useGravity = false;

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
                state = State.Emitter2;
                break;


            case State.Emitter2:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.Emitter3;
                break;

            case State.Emitter3:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.Emitter4;
                break;

            case State.Emitter4:
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
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make instructional ball invisible
            _instructionBallMeshRend.enabled = false;
//            _instructionBallRigidbody.useGravity = false;

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
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

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
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);


            FallInstructions.SetActive(false);

            // make instruction ball fall 
            _instructionBallRigidbody.useGravity = true;


        //    emitter.transform.position = new Vector3(0, emitter.transform.position.y, emitter.transform.position.z);


        }


        if (state == State.Emitter2)
        {


            // show correct instructions
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);


            Instructions_13.SetActive(true);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make occluder appear 
            occluder.SetActive(true);
        }


        if (state == State.Emitter3)
        {

            // show correct instructions
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);


            Instructions_13.SetActive(false);
            Instructions_14.SetActive(true);
            Instructions_15.SetActive(false);

            // make occluder disappear
            occluder.SetActive(false);

        }


        if (state == State.Emitter4)
        {

            // show correct instructions
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(true);

        }


        if (state == State.Arrows)
        {


            // show correct instructions
            Instructions_2.SetActive(true);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make occluder disappear
            occluder.SetActive(false);

            timer.time = 5;

        }

        if (state == State.Basket)
        {

            Instructions_2.SetActive(true);
            Instructions_3.SetActive(true);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);



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
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(true);
            Instructions_5.SetActive(false);


        }

        if (state == State.Questions)
        {


            // show correct instructions
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(true);



            if (Input.GetKeyDown(KeyCode.X))
            {
                state = State.Stop;
            }

        }

        if (state == State.Stop)
        {

            // show correct instructions
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            _ballMeshRend.enabled = false;


        }
    }
}
