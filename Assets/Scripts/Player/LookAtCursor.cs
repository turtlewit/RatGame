using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    [SerializeField]
    Transform target;
    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        Vector3 pos = cam.WorldToScreenPoint(target.position);
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 direction = mouse_pos - pos;
        direction = new Vector3(direction.x, 0, direction.y).normalized;
        target.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
