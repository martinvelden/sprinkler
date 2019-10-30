﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class SwitchPlayer : MonoBehaviour
    {
        public GameObject grandpa;
        public GameObject kid;

        [HideInInspector]
        public static ActivePlayer active_player;
        // Start is called before the first frame update
        private void Start()
        {
            //grandpa = GameObject.FindGameObjectWithTag("Grandpa");
            //kid = GameObject.FindGameObjectWithTag("Kid");
            grandpa.GetComponent<GrandpaPlayer>().is_active_player = true;
            grandpa.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
            active_player = ActivePlayer.Grandpa;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (active_player == ActivePlayer.Grandpa)
                {
                    grandpa.GetComponent<GrandpaPlayer>().is_active_player = false;
                    grandpa.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                    kid.GetComponent<KidPlayer>().is_active_player = true;
                    kid.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                    active_player = ActivePlayer.Kid;
                }
                else if (active_player == ActivePlayer.Kid)
                {
                    kid.GetComponent<KidPlayer>().is_active_player = false;
                    kid.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                    grandpa.GetComponent<GrandpaPlayer>().is_active_player = true;
                    grandpa.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                    active_player = ActivePlayer.Grandpa;
                }
            }
        }
    }
}