using UnityEngine;

public class TrajectorySwitch : MonoBehaviour
{
    public void TrajectoryOn()
    {
        GlobalParameters.ShowTrajectory = true;
    }

    public void TrajectoryOff()
    {
        GlobalParameters.ShowTrajectory = false;
    }
}
