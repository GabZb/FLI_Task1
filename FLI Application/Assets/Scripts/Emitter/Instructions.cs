using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    // reference to timer class
    public Timer timer;


    // specific instructions
    public GameObject Instructions_0;
    public GameObject Instructions_1;
    public GameObject Instructions_2;
    public GameObject Instructions_3;
    public GameObject Instructions_4;
    public GameObject Instructions_5;
    public GameObject Instructions_11;
    public GameObject Instructions_12;
    public GameObject Instructions_13;
    public GameObject Instructions_14;
    public GameObject Instructions_15;


    public GameObject background;

    public GameObject basket;

    public GameObject emitter;
    private MeshRenderer _emitterMeshRend;

    public GameObject ball;
    private MeshRenderer _ballMeshRend;


    public GameObject occluder;

    //  private Rigidbody _basketRigidbody;

    // task state machine
    public enum State
    {
        GeneralInstructions,
        Start,
        Emitter1,
        Emitter2,
        Emitter3,
        Emitter4,
        Arrows,
        Basket,
        EndEstimation,
        Questions,
        EndInstructions

    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {


        _emitterMeshRend = emitter.GetComponent<MeshRenderer>();
        _ballMeshRend = ball.GetComponent<MeshRenderer>();

        // get timer component
        timer = GetComponent<Timer>();

        // show background
        background.SetActive(true);

        // show instructions
        Instructions_0.SetActive(true);
        Instructions_1.SetActive(false);
        Instructions_2.SetActive(false);
        Instructions_3.SetActive(false);
        Instructions_4.SetActive(false);
        Instructions_5.SetActive(false);


        Instructions_11.SetActive(false);

        state = State.GeneralInstructions;

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            //    if (Input.GetKeyDown(KeyCode.Return))
            //    {
            case State.GeneralInstructions:
              if (Input.GetKeyDown(KeyCode.Return))
                    state = State.Start;
                break;

            case State.Start:
              if (Input.GetKeyDown(KeyCode.Return))
                state = State.Emitter1;
                break;

            case State.Emitter1:
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

        //    }
        /// instruction states

        if (state == State.GeneralInstructions)
        {
            // show background
            background.SetActive(true);

            // show instructions
            Instructions_0.SetActive(true);

            timer.time = 5.5f;

        }

        if (state == State.Start)
        {
            // show background
            background.SetActive(false);

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(true);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(false);
            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

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
                Instructions_11.SetActive(true);
            }

        }

        if (state == State.Emitter1)
        {

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(true);
            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make emitter disappear
            _emitterMeshRend.enabled = false;

            //make ball disappear
            _ballMeshRend.enabled = false;

        }


        if (state == State.Emitter2)
        {


            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(false);
            Instructions_13.SetActive(true);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make occluder appear 
            occluder.SetActive(true);
        }


        if (state == State.Emitter3)
        {

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(false);
            Instructions_13.SetActive(false);
            Instructions_14.SetActive(true);
            Instructions_15.SetActive(false);

            // make occluder disappear
            occluder.SetActive(false);

            // make emitter disappear
            _emitterMeshRend.enabled = false;

            //make ball appear
            _ballMeshRend.enabled = true;
        }


        if (state == State.Emitter4)
        {

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(false);
            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(true);

            // make occluder appear
            occluder.SetActive(true);

            //make ball appear
            _ballMeshRend.enabled = true;
        }


        if (state == State.Arrows)
        {


            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(true);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);
            Instructions_12.SetActive(false);
            Instructions_13.SetActive(false);
            Instructions_14.SetActive(false);
            Instructions_15.SetActive(false);

            // make occluder disappear
            occluder.SetActive(false);

            //make ball disappear
            _ballMeshRend.enabled = false;

            timer.time = 5;

        }

        if (state == State.Basket)
        {

            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(true);
            Instructions_3.SetActive(true);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_12.SetActive(false);

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
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(true);
            Instructions_5.SetActive(false);
            Instructions_12.SetActive(false);

        }

        if (state == State.Questions)
        {


            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(true);
            Instructions_12.SetActive(false);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.EndInstructions;
            }


        }

        if (state == State.EndInstructions)
        {

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);
            Instructions_12.SetActive(false);
        }
    }
}
