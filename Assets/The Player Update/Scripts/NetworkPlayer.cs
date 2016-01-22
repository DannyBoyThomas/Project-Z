using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkPlayer : NetworkBehaviour 
{
    public Animator anim;

    public bool is3rdPerson = false;

    public CharacterController controller;
    public FirstPersonController fpController;

    [SyncVar]
    public bool IsDead;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        fpController = GetComponent<FirstPersonController>();
    }

    public void Update()
    {
        if (isLocalPlayer)
            CmdSyncProperties(fpController.m_IsWalking, fpController.isCrouching, fpController.m_Horizontal, fpController.m_Speed);

        float speed = fpController.m_Speed;

        anim.SetFloat("Vertical", speed);
        anim.SetFloat("Horizontal", fpController.m_Horizontal);

        anim.SetBool("Run", !fpController.m_IsWalking);
        anim.SetBool("Crouch", fpController.isCrouching);

        if (!isLocalPlayer || is3rdPerson)
            anim.gameObject.SetActive(true);
        else
            if (isLocalPlayer && !is3rdPerson)
                anim.gameObject.SetActive(false);

      
    }


    [Command]
    private void CmdSyncProperties(bool isWalking, bool isCrouching, float horizontal, float vertical)
    {
        fpController.m_IsWalking = isWalking;
        fpController.isCrouching = isCrouching;
        fpController.m_Speed = vertical;
        fpController.m_Horizontal = horizontal;
    }

    
}
