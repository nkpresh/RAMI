using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game
{
    [Serializable]
    public struct CameraBound
    {
        public float top;
        public float bottom;
        public float left;
        public float right;
    }

    public class CameraController : MonoBehaviour
    {

        public CameraFollowTarget target;
        public float followSpeed = 10;
        public float directionOffset = 1;

        public CameraBound cameraBounds;

        private Vector3 defaultPosition;
        private Camera _camera;

        private void Awake()
        {
            defaultPosition = transform.position;
            _camera = Camera.main;
        }

        void Update()
        {

        }

        private void LateUpdate()
        {
            Vector3 targetPos = target.transform.position;
            targetPos.z = defaultPosition.z;
            targetPos.y = defaultPosition.y;

            targetPos.x += (target.lookDirection * directionOffset);

            Vector3 pos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

            transform.position = pos;
            ClampCamera();
        }

        private void ClampCamera()
        {
            Vector3 clampMovement = transform.position;
            float CamSize = Camera.main.orthographicSize;
            float aspect = Camera.main.aspect;


            clampMovement.x = Mathf.Clamp(clampMovement.x, -cameraBounds.left + CamSize * aspect, cameraBounds.right - CamSize * aspect);
            clampMovement.y = Mathf.Clamp(clampMovement.y, -cameraBounds.bottom + CamSize, cameraBounds.top - CamSize);

            transform.position = clampMovement;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector3(-cameraBounds.left, cameraBounds.top), new Vector3(cameraBounds.right, cameraBounds.top));
            Gizmos.DrawLine(new Vector3(-cameraBounds.left, -cameraBounds.bottom), new Vector3(cameraBounds.right, -cameraBounds.bottom));

            Gizmos.DrawLine(new Vector3(-cameraBounds.left, cameraBounds.top), new Vector3(-cameraBounds.left, -cameraBounds.bottom));
            Gizmos.DrawLine(new Vector3(cameraBounds.right, cameraBounds.top), new Vector3(cameraBounds.right, -cameraBounds.bottom));
        }
    }

}

