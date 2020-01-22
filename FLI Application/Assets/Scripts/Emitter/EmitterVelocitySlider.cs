using UnityEngine;
using UnityEngine.UI;

public class EmitterVelocitySlider : MonoBehaviour
{
    public Slider slider;

    public void SetEmitterVelocity()
    {
        GlobalParameters.EmitterVelocity = slider.value;
    }
}
