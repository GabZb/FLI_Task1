using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Speed = 10f;

    private Rigidbody _emitterRigidbody;
    // public Rigidbody _ballRigidbody; /// 
    private Vector3 _inputs = Vector3.zero;

    void Start()
    {
        _emitterRigidbody = GetComponent<Rigidbody>();
       //  _ballRigidbody = GetComponent<Rigidbody>(); ///
    }

    void Update()
    {
        // only allow external control of emitter if key control is enabled (sandbox)
        if (GlobalParameters.KeyControl)
        {
            _inputs = Vector3.zero;
            _inputs.x = Input.GetAxis("Horizontal");

            if (_inputs != Vector3.zero)
                transform.right = _inputs;
        }
    }

    void FixedUpdate()
    {
        // only allow external control of emitter if key control is enabled (sandbox)
        if (GlobalParameters.KeyControl)
            _emitterRigidbody.AddForce(_inputs * Speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
