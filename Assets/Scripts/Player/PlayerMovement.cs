using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Animator animator;


    [Header("Shooting")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float shootingRange = 50f;
    [SerializeField] private LayerMask whatCanHit;

    [SerializeField] private GameObject explosionEffect;

    #region Animation_Names
    private string PLAYER_JUMP = "Player_Jump";
    private string PLAYER_IDLE = "Player_Idle";
    private string PLAYER_RUN = "Player_Run";
    #endregion

    private bool jump;
    private float horizontalMovement;
    private bool isGrounded;



    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && isGrounded)
        {
            isGrounded = false;
            jump = true;
        } else
        {
            jump = false;
        }

        controller.Move(horizontalMovement * speed * Time.deltaTime, jump);

        //Set animations
        if (!jump || isGrounded)
        {
            if (Mathf.Abs(horizontalMovement) >= 0.1)
            {
                animator.Play(PLAYER_RUN);
            }
            else
            {
                animator.Play(PLAYER_IDLE);
            }
        }

        //Shoot

        if ( Input.GetButtonDown("Shoot"))
        {
            RaycastHit2D shootingRay = Physics2D.Raycast(shootingPoint.position, transform.right, shootingRange, whatCanHit);
            if (shootingRay != null)
            {
                Instantiate(explosionEffect, shootingRay.point, transform.rotation);
                Debug.DrawLine(shootingPoint.position, shootingRay.point, Color.yellow, 2f);
            }
        }

    }

    public void OnLanding()
    {
        isGrounded = true;
    }
}
