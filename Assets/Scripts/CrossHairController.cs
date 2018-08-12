using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{

    public float radius = 1f;

    Transform rabbit;
    void Update()
    {
        rabbit = GetComponentInParent<Transform>();
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = radius;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(rabbit.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
