﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.3f;

    private Vector3 velocity = Vector3.zero;


    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - 7f;
        pos.y = player.position.y + height;


        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
