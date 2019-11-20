using UnityEngine;

public class SceneController : MonoBehaviour
{
    // settable parameters
    private float _emitterStartPosition = -12f;
    private float _emitterEndPosition = 12f;

    private int dropLimit = 1;

    // objects set in inspector
    public GameObject emitter;
    private Rigidbody _emitterRigidbody;
    
    // state machine
    public enum State
    {
        Setup,  
        Wait, 
        Start,  
        Run,  
        Sandbox 
    }

    public State state;

    // script references
    private DropController dropController;

    // tracking parameters
    private int dropCount;

    // start instruction
    public GameObject startInstruction;


    private void Start()
    {
        // get reference to scripts
        dropController = GetComponent<DropController>();

        // get emitter rigidbody 
        _emitterRigidbody = emitter.GetComponent<Rigidbody>();

        // initialise add velocity to true - make sure switch prefab is consistent
        GlobalParameters.AddVelocity = true;

        // initialise show trajectory to false - make sure switch prefab is consistent
        GlobalParameters.ShowTrajectory = false;

        // initialise show trajectory to false - make sure switch prefab is consistent
        GlobalParameters.EmitterVelocity = 5f;

        // initialise no key control
        GlobalParameters.KeyControl = false;

        // initialise state
        state = State.Setup;
    }


    private void Update()
    {
        if (state == State.Setup)
        {
            // show start instruction
            startInstruction.SetActive(true);

            // set emitter velocity (angular velocities locked)
            _emitterRigidbody.velocity = Vector3.zero;

            // set emitter position
            emitter.transform.position = new Vector3(_emitterStartPosition, emitter.transform.position.y, emitter.transform.position.z);

            // reset drop count
            dropCount = 0;

            // transition to next state
            state = State.Wait;
        }

        if (state == State.Wait)
        {
            // if enter key is pressed transition to next state
            if (Input.GetKeyDown(KeyCode.Return))
                state = State.Start;
        }

        if (state == State.Start)
        {
            // hide start instruction
            startInstruction.SetActive(false);

            // set emitter velocity
            _emitterRigidbody.AddForce(new Vector3 (GlobalParameters.EmitterVelocity, 0f, 0f), ForceMode.VelocityChange);

            // transition to next state
            state = State.Run;
        }

        if (state == State.Run)
        {
            if (dropCount < dropLimit && Input.GetKeyDown(KeyCode.Space))
            {
                dropController.Drop(_emitterRigidbody.velocity);
                dropCount++;
            }
            
            // if emitter position is greater than end position, transition to next state
            if (emitter.transform.position.x > _emitterEndPosition)
                state = State.Setup;
        }

        if (state == State.Sandbox)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                dropController.Drop(_emitterRigidbody.velocity);
        }

    }

    public void SetSandbox()
    {
        // hide start instruction
        startInstruction.SetActive(false);

        // set emitter velocity (angular velocities locked)
        _emitterRigidbody.velocity = Vector3.zero;

        // set emitter position
        emitter.transform.position = new Vector3(0f, emitter.transform.position.y, emitter.transform.position.z);

        // enable key control
        GlobalParameters.KeyControl = true;

        // transition to sanbox state
        state = State.Sandbox;
    }

    public void SetTrial()
    {
        // transition to initialisation of trial state
        state = State.Setup;
    }
}
