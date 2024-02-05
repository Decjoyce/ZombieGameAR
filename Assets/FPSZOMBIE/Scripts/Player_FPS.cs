using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FPS : MonoBehaviour
{
    public Transform cam;

    private void Update()
    {
        transform.position = cam.transform.position;
    }
}
