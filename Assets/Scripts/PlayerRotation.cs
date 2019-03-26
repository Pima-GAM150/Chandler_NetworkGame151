using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    public float rotateSpeed = 0f;
    public GameObject playerRotation;
    public Transform player;
    public Vector2 direction;
    public float angle;
    public Quaternion rotation;
    // Update is called once per frame
    void Update()
    {
        //itsRotation = player.transform.rotation;
        //Debug.Log(itsRotation.z);
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y + 1, direction.x + 1) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}