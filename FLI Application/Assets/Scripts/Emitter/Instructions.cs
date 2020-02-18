using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // reference to timer class
    public Timer timer;

    private float instructionDuration = 80f;

    // task state machine
    public enum State
    {
        Start,
        Emitter,
        Arrows,
        Basket,
        EndEstimation,
        Questions,
        EndInstructions

    }

    public State state;

    // specific instructions
    public GameObject Instructions_0;
    public GameObject Instructions_1;
    public GameObject Instructions_2;
    public GameObject Instructions_3;
    public GameObject Instructions_4;
    public GameObject Instructions_5;
    public GameObject Instructions_11;

    public GameObject background;

    public GameObject basket;

    public GameObject emitter;
    private MeshRenderer _emitterMeshRend;

    public GameObject ball;
    private MeshRenderer _ballMeshRend;

    //  private Rigidbody _basketRigidbody;


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

        state = State.Start;

    }

    // Update is called once per frame
    void Update()
    {

        /// instruction states

        if (state == State.Start)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                timer.time = instructionDuration;
                state = State.Emitter;
            }

        }

        if (state == State.Emitter)
        {
            float time1 = timer.time;

            // hide background
            background.SetActive(false);

            // show correct instructions
            Instructions_0.SetActive(false);
            Instructions_1.SetActive(true);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            if (timer.time <= 72)
            {

                    // make emitter disappear
                    _emitterMeshRend.enabled = false;

                    //make ball disappear
                    _ballMeshRend.enabled = false;

                    // make instructions appear
                    Instructions_1.SetActive(false);
                    Instructions_11.SetActive(true);

            }


            if (timer.time <= 65)
            {
                state = State.Arrows;
            }

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



            if (timer.time <= 60)
            {
                state = State.Basket;
            }

        }

        if (state == State.Basket)
        {

            Instructions_0.SetActive(false);
            Instructions_1.SetActive(false);
            Instructions_2.SetActive(true);
            Instructions_3.SetActive(true);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);

            Instructions_11.SetActive(false);

            basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y, basket.transform.position.z);


            if (timer.time <= 55)

            {
                // set basket start position
                basket.transform.position = new Vector3(2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 54)

            {
                // set basket start position
                basket.transform.position = new Vector3(4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 53)

            {
                // set basket start position
                basket.transform.position = new Vector3(6f, basket.transform.position.y, basket.transform.position.z);

            }

            if (timer.time <= 52)

            {
                // set basket start position
                basket.transform.position = new Vector3(4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 51)

            {
                // set basket start position
                basket.transform.position = new Vector3(2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 50)

            {
                // set basket start position
                basket.transform.position = new Vector3(0f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 49)

            {
                // set basket start position
                basket.transform.position = new Vector3(-2f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 48)

            {
                // set basket start position
                basket.transform.position = new Vector3(-4f, basket.transform.position.y, basket.transform.position.z);
            }

            if (timer.time <= 47)

            {
                // set basket start position
                basket.transform.position = new Vector3(-6f, basket.transform.position.y, basket.transform.position.z);
            }


            if (timer.time <= 45)
            {
                state = State.EndEstimation;
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
            Instructions_11.SetActive(false);

            if (timer.time <= 37)
            {
                state = State.Questions;
            }

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
            Instructions_11.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
            {

                state = State.EndInstructions;

            }

        }

        if (state == State.EndInstructions)
        {

            Instructions_0.SetActive(false);

            Instructions_1.SetActive(false);
            Instructions_2.SetActive(false);
            Instructions_3.SetActive(false);
            Instructions_4.SetActive(false);
            Instructions_5.SetActive(false);
            Instructions_11.SetActive(false);
        }
    }
}
