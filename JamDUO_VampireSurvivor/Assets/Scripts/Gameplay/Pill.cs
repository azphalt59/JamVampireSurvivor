using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 240f * Time.deltaTime, 0);
    }
}
