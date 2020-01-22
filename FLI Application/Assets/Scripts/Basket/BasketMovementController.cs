using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovementController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        // Move bucket to the right

        if (Input.GetKey(KeyCode.RightArrow))

        {
            Vector3 BucketPosition = transform.position;
            BucketPosition.x += 0.1f; //if =, does it only once. if += it repeats as long as arrow pressed 
            transform.position = BucketPosition;

        }

        // Move bucket to the left

        if (Input.GetKey(KeyCode.LeftArrow))

        {
            Vector3 BucketPosition = transform.position;
            BucketPosition.x -= 0.1f;
            transform.position = BucketPosition;

        }




    }
}
