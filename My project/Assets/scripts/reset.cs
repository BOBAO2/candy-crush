using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the scene by reloading it
            // This assumes you have a scene named "SampleScene" in your project
            // Make sure to replace "SampleScene" with the actual name of your scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}