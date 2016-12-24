using Assets.Gamelogic.Pirates.Behaviours;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Camera
{
    // Enable this MonoBehaviour on client workers only
    [EngineType(EnginePlatform.Client)]
    public class ShipCamera : MonoBehaviour
    {
        public ShipController Controller;
        public AnimationCurve Distance;
        public AnimationCurve Angle;

        private Transform ourTransform;
        private float yaw;
        private float pitch;
        public float speedH = 2.0f;
        public float speedV = 2.0f;

        void Start ()
        {
            ourTransform = transform;
        }

        void Update ()
        {
            if (Controller != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Right Click");
                    yaw = ourTransform.eulerAngles.y + speedH * Input.GetAxis("Mouse X");
                    pitch = ourTransform.eulerAngles.x + speedV * Input.GetAxis("Mouse Y");

                    ourTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                }
                else
                {
                    Debug.Log("Here");
                    var speed = Controller.currentSpeed;
                    var rot = Quaternion.Euler(Angle.Evaluate(speed), 0f, 0f);
                    ourTransform.localPosition = rot * Vector3.back * Distance.Evaluate(speed);
                    ourTransform.localRotation = rot;
                }
            }
        }
    }
}