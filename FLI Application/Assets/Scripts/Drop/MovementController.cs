using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Speed = 10f;

    private Rigidbody _emitterRigidbody;
    private Vector3 _inputs = Vector3.zero;

    void Start()
    {
        _emitterRigidbody = GetComponent<Rigidbody>();
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
