using UnityEngine;

public class Timer : MonoBehaviour
{

    public float time;

    void Update()
	{
		time -= Time.deltaTime;
	}
}
