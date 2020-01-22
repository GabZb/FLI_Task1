using UnityEngine;

public class DropController : MonoBehaviour
{
    public bool addVelocity = true;

    public GameObject projectilePrefab;
    public Transform dropPoint;

    private float _prefabLife = 4f;


    public void Drop(Vector3 _emitterVelocity)
    {
        // instantiate projectile
        GameObject _projectile = Instantiate(projectilePrefab, dropPoint.position, this.transform.rotation);

        // get rigidbody so that force can be applied
        Rigidbody _projectileRigidbody = _projectile.GetComponent<Rigidbody>();

        // add emitter velocity if option selected
        if (GlobalParameters.AddVelocity)
            _projectileRigidbody.velocity = _emitterVelocity;

        // destroy prefab after fixed time
        Destroy(_projectile, _prefabLife);
    } 
}
