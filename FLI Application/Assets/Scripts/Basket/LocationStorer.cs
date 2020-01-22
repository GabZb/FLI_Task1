using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocationStorer : MonoBehaviour
{


    void CreateText()
    {

        // Path of the file

        string path = Application.dataPath + "/Location.txt";

        // Create file if it doesn't exist

        // if (!File.Exists(path)) {}


        File.WriteAllText(path, "Location of bucket \n\n");



        // Content of the file

        string content = "chosen location" + transform.position + "\n";

        // Add content to existing file

        File.AppendAllText(path, content); //writealltext will replace text

    }



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.X))

        {

            CreateText();

        }

    }
}
