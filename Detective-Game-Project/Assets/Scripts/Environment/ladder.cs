﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Kid")
        {
            print("boink");
            collision.gameObject.GetComponent<KidPlayer>().is_climbing = true;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Kid")
        {
            collision.gameObject.GetComponent<KidPlayer>().is_climbing = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}