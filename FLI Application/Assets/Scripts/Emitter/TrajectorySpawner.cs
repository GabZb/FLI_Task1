using UnityEngine;

public class TrajectorySpawner : MonoBehaviour
{
    public GameObject trajectoryPrefab;
    private float _prefabLife = 1.5f;


    private void Update()
    {
        if (GlobalParameters.ShowTrajectory)
        {
            // instantiate object to show trajectory
            GameObject _trajectory = Instantiate(trajectoryPrefab, this.transform.position, this.transform.rotation);

            // destroy object after fixed amount of time
            Destroy(_trajectory, _prefabLife);
        }
    }
}
