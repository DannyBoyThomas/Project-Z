using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : NetworkBehaviour
    {
        [SyncVar][SerializeField] public bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField]
        private float m_CrouchSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        public Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private bool m_IsOnLadder = false;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        public bool isClimbing = false;

        public float LeanAngle;
        private float CurrentLeanAngle;

        public Transform LeanPoint;

        [SyncVar]
        public bool isCrouching = false;

        public float crouchingHeight;
        public float standingHeight;

        public float cameraStandingHeight;
        public float cameraCrouchingHeight;

        public float currentHeight;

        [SyncVar] public float m_Speed;
        [SyncVar] public float m_Horizontal;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = GetComponentInChildren<Camera>();
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            if (!isLocalPlayer)
                Destroy(m_Camera.gameObject);
            else
                m_Camera.gameObject.SetActive(true);
        }


        // Update is called once per frame
        private void Update()
        {
            if (!isLocalPlayer)
                return;

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                CmdPlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }

            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            if (CrossPlatformInputManager.GetButtonDown("Crouch"))
                isCrouching = !isCrouching;

            if (Input.GetKey(KeyCode.E))
            {
                CurrentLeanAngle = Mathf.LerpAngle(CurrentLeanAngle, -LeanAngle, 5f * Time.deltaTime);
            }
            else
                if (Input.GetKey(KeyCode.Q))
                {
                    CurrentLeanAngle = Mathf.LerpAngle(CurrentLeanAngle, LeanAngle, 5f * Time.deltaTime);
                }
                else
                    CurrentLeanAngle = Mathf.LerpAngle(CurrentLeanAngle, 0, 5f * Time.deltaTime);


            Vector3 rotation = LeanPoint.localEulerAngles;
            rotation.z = CurrentLeanAngle;
            LeanPoint.localEulerAngles = rotation;

            float characterHeight = isCrouching ? crouchingHeight : standingHeight;

            float newHeight = Mathf.MoveTowards(m_CharacterController.height, characterHeight, 3f * Time.deltaTime);

            float change = newHeight - m_CharacterController.height;

            m_CharacterController.height = newHeight;


            Vector3 center = m_CharacterController.center;
            center.y += change * 0.5f;
            if(change > 0)
                transform.position += Vector3.up * change;
            m_CharacterController.center = center;

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
            
        }


        [ClientRpc]
        private void RpcPlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        [Command]
        private void CmdPlayLandingSound()
        {
            RpcPlayLandingSound();
        }


        private void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;

            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, ~0, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded && !isClimbing)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    CmdPlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                if(!isClimbing)
                    m_MoveDir += Vector3.down * m_GravityMultiplier * 9.81f * Time.deltaTime;
            }


            if (!isClimbing)
                m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
            else
                m_MoveDir = Vector3.zero;

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
           
        }


        [ClientRpc]
        private void RpcPlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        [Command]
        private void CmdPlayJumpSound()
        {
            RpcPlayJumpSound();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            CmdPlayFootStepAudio();
        }

        [Command]
        private void CmdPlayFootStepAudio()
        {
            RpcPlayFootStepAudio();
        }

        [ClientRpc]
        private void RpcPlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {

               
                //Vector3 headBob = m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                //newCameraPosition =  (m_OriginalCameraPosition - headBob);

                //newCameraPosition.y =  m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }


            Vector3 cameraPos = m_Camera.transform.localPosition;
            float cameraHeight = isCrouching ? cameraCrouchingHeight : cameraStandingHeight;
            cameraPos.y = Mathf.MoveTowards(cameraPos.y, cameraHeight, 6f * Time.deltaTime);

            
            newCameraPosition = cameraPos;

            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);

#endif

            // set the desired speed to be walking or running
            speed = isCrouching ? m_CrouchSpeed : m_IsWalking ? m_WalkSpeed : m_RunSpeed;

            m_Input = new Vector2(horizontal, vertical);
            m_Speed = m_Input.y;
            m_Horizontal = m_Input.x;
            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
        
            }

        

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

      
    }
}
