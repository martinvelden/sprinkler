﻿using UnityEngine;
using Cinemachine;

namespace Scripts
{
    public class HumanPlayer : MonoBehaviour
    {
        [HideInInspector]
        public bool isClimbing = false;

        private CinemachineFreeLook context;

        CharacterController characterController;
        public float movementSpeed = 3.0f;
        public float climbingSpeed = 2.0f;
        public float rotationSpeed = 100.0f;
        public float jumpHeight = 8.0f;
        public float gravity = 20f;
        private Vector3 moveDirection = Vector3.zero;


        private void Start()
        {
            context = GameManager.Instance.CameraHumanFollow.GetComponent<CinemachineFreeLook>();
            characterController = GetComponent<CharacterController>();
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Ladder")
            {
                isClimbing = true;
                gravity = 0f;
            }
        }


        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Ladder")
            {
                gravity = 20.0f;
                isClimbing = false;
            }
        }


        private void FixedUpdate()
        {
            if (GameManager.Instance.checkIfPlayerIsActive(ActivePlayer.Human))
            {
                if (isClimbing)
                {
                    moveDirection = new Vector3(0.0f, GameManager.Instance.getAxisForPlayer(ActivePlayer.Human, "Vertical", AxisType.Axis),
                        GameManager.Instance.getAxisForPlayer(ActivePlayer.Human, "Vertical", AxisType.Axis) * 0.5f);
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= climbingSpeed;
                    characterController.Move(moveDirection * Time.deltaTime);
                }
                else
                {
                    if (characterController.isGrounded)
                    {
                        moveDirection = new Vector3(GameManager.Instance.getAxisForPlayer(ActivePlayer.Human, "Horizontal", AxisType.Axis), 0.0f,
                            GameManager.Instance.getAxisForPlayer(ActivePlayer.Human, "Vertical", AxisType.Axis));
                        moveDirection = transform.TransformDirection(moveDirection);
                        moveDirection *= movementSpeed;

                        if (GameManager.Instance.getButtonPressForPlayer(ActivePlayer.Human, "Jump", ButtonPress.Press))
                        {
                            moveDirection.y = jumpHeight;
                        }
                    }
                    moveDirection.y -= gravity * Time.deltaTime;
                    characterController.Move(moveDirection * Time.deltaTime);

                    float translationRH = GameManager.Instance.getAxisForPlayer(ActivePlayer.Human, "Camera X", AxisType.AxisRaw) * rotationSpeed;
                    translationRH *= Time.deltaTime;
                    context.m_XAxis.Value += translationRH;
                    transform.Rotate(0, translationRH, 0);
                }
            }
        }

    }
}
