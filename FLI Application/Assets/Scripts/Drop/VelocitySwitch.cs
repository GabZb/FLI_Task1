using UnityEngine;

public class VelocitySwitch : MonoBehaviour
{
    public void VelocityOn()
    {
        GlobalParameters.AddVelocity = true;
    }

    public void VelocityOff()
    {
        GlobalParameters.AddVelocity = false;
    }
}
