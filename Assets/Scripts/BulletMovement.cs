using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 0.35f;

    public string bulletType;

    void Update()
    {
        float posX = transform.position.x + speed;
        transform.position = new Vector3(posX,transform.position.y,transform.position.z);
    }
}
