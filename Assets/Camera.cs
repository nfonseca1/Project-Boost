using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float distance = 20f;
    [SerializeField] Transform levelCenter;
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        if (levelCenter)
        {
            Vector3 midpoint = (levelCenter.position + player.position) / 2;
            transform.position = new Vector3(
                midpoint.x,
                midpoint.y,
                midpoint.z - distance
            );
        }
        else
        {
            transform.position = new Vector3(
                player.position.x,
                player.position.y + 2f,
                player.position.z - distance
            );
        }
    }
}
