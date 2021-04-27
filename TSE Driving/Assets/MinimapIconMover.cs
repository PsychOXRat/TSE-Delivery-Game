using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconMover : MonoBehaviour
{
    public GameObject objectForRotate;
    public GameObject icon;

    // Update is called once per frame
    void Update()
    {
        icon.transform.rotation = Quaternion.Euler(90, 0, -objectForRotate.transform.rotation.eulerAngles.y);
    }
}
