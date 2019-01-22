using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float distance = 20f;
    [SerializeField] Transform levelCenter;

    Transform player;

    private void Start()
    {
        GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
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
            GetPlayer();
        }
    }

    void GetPlayer()
    {
        player = FindObjectOfType<Rocket>().transform;
    }
}
