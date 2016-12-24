using UnityEngine;

namespace Assets.Gamelogic.Pirates.Camera
{
    public class LagRotation : MonoBehaviour
    {
        public float Speed = 10f;
        public float modifier = 0.1f;

        Transform ourTransform;
        Transform ourParent;
        Quaternion ourLocalRotation;
        Quaternion ourParentsRotation;

        private float yaw;
        private float pitch;
        public float speedH = 3.0f;
        public float speedV = 3.0f;

        void Start()
        {
            ourTransform = transform;
            ourParent = ourTransform.parent;
            ourLocalRotation = ourTransform.localRotation;
            ourParentsRotation = ourParent.rotation;
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                yaw = ourTransform.eulerAngles.y + speedH * Input.GetAxis("Mouse X");
                pitch = ourTransform.eulerAngles.x + speedV * -Input.GetAxis("Mouse Y");
                //Debug.Log(pitch);
                pitch = fixPitch(pitch);

                ourTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
            else {
                if (Input.GetMouseButtonUp(1))
                {
                    ourParentsRotation = Quaternion.Slerp(ourParentsRotation, ourParent.rotation, Time.deltaTime * Speed
                        * modifier);
                }
                else
                {
                    ourParentsRotation = Quaternion.Slerp(ourParentsRotation, ourParent.rotation, Time.deltaTime * Speed);
                }
                ourTransform.rotation = ourParentsRotation * ourLocalRotation;
            }
        }

        private float fixPitch(float pitch)
        {
            if (pitch < 330 && pitch > 90)
            {
                pitch = 330;
            }
            if (pitch > 40 && pitch < 90)
            {
                pitch = 40;
            }

            return pitch;
        }
    }
}