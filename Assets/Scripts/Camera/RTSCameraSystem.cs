using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.Cameras
{
    public class RTSCameraSystem : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCam;

        [Header("Options")]
        [SerializeField] private bool useEdgeScrolling = false;
        [SerializeField] private bool useDragPan = false;

        [Header("General Settings")]
        [SerializeField] private float moveSpeed = 50f;
        [SerializeField] private float rotateSpeed = 100f;
        [SerializeField] private float dragPanSpeed = 2f;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float zoomAmount = 3f;
        [SerializeField] private int edgeScrollSize = 20;

        [Header("Zoom By Changing FOV Settings")]
        [SerializeField] private float fovMin = 40;
        [SerializeField] private float fovMax = 50;

        [Header("Zoom By Lowering Height Settings")]
        [SerializeField] private float followOffsetMinY = 10f;
        [SerializeField] private float followOffsetMaxY = 50f;

        [Header("Zoom By Moving Foward Settings")]
        [SerializeField] private float followOffsetMin = 5f;
        [SerializeField] private float followOffsetMax = 50f;

        private float targetFOV;
        private bool dragPanMoveActive = false;
        private Vector2 lastMousePosition;        
        private Vector3 followOffset;

        private CinemachineTransposer cmTransposer;

        private void Awake()
        {
            followOffset = virtualCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
            cmTransposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Start()
        {
            targetFOV = 50f;
        }

        // Update is called once per frame
        void Update()
        {
            HandleCameraMovement();

            if (useEdgeScrolling)
            {
                HandleCameraMovementEdgeScrolling();
            }

            if (useDragPan)
            {
                HandleCameraMovementDragPan();
            }

            HandleCameraRotation();

            HandleCameraZoomByFOV();
            HandleCameraZoomByLowering();
        }

        public void HandleCameraMovement()
        {
            Vector3 inputDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) inputDir.z = 1f;
            if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
            if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
            if (Input.GetKey(KeyCode.D)) inputDir.x = 1f;

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        private void HandleCameraMovementEdgeScrolling()
        {
            Vector3 inputDir = Vector3.zero;

            if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
            if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = 1f;
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = 1f;

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        private void HandleCameraMovementDragPan()
        {
            Vector3 inputDir = Vector3.zero;

            if (Input.GetMouseButtonDown(1))
            {
                dragPanMoveActive = true;
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(1))
            {
                dragPanMoveActive = false;
            }

            if (dragPanMoveActive)
            {
                Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

                inputDir.x = mouseMovementDelta.x * dragPanSpeed;
                inputDir.z = mouseMovementDelta.y * dragPanSpeed;

                lastMousePosition = Input.mousePosition;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        public void HandleCameraRotation()
        {
            float rotateDir = 0f;
            if (Input.GetKey(KeyCode.Q)) rotateDir = 1f;
            if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

            transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
        }

        private void HandleCameraZoomByFOV()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFOV -= 5;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                targetFOV += 5;
            }

            targetFOV = Mathf.Clamp(targetFOV, fovMin, fovMax);
            virtualCam.m_Lens.FieldOfView = Mathf.Lerp(virtualCam.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }

        private void HandleCameraZoomByMoveForward()
        {
            Vector3 zoomDir = followOffset.normalized;

            if (Input.mouseScrollDelta.y > 0)
            {
                followOffset -= zoomDir * zoomAmount;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                followOffset += zoomDir * zoomAmount;
            }

            if (followOffset.magnitude < followOffsetMin)
            {
                followOffset = zoomDir * followOffsetMin;
            }
            if (followOffset.magnitude > followOffsetMax)
            {
                followOffset = zoomDir * followOffsetMax;
            }

            cmTransposer.m_FollowOffset = Vector3.Lerp(cmTransposer.m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
        }

        private void HandleCameraZoomByLowering()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                followOffset.y -= zoomAmount;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                followOffset.y += zoomAmount;
            }

            followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);
            
            cmTransposer.m_FollowOffset = Vector3.Lerp(cmTransposer.m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
        }
    }
}