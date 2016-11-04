using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour
{

    public GameObject player;
    public float moveSpeed;
    private Animator anim;


    private Rigidbody2D rb;
    private PlayerController pc;
    private Vector2 currentHolder;
    private bool isMoving;


    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = player.GetComponent<PlayerController>();
    }



    void Update()
    {
        isMoving = false;



        /*
         if (pc.move.x > 0.5f  || pc.move.x < -0.5f){

                 rb.velocity = pc.lastMove * moveSpeed ;
             isMoving = true;
         }
         if (pc.move.y > 0.5f || pc.move.y < -0.5f)
         {
             rb.velocity = pc.lastMove * moveSpeed;
             isMoving = true;
         }
         if (pc.move.x < 0.5f && pc.move.x > -0.5f)
         {
             rb.velocity = new Vector2(0f, rb.velocity.y);
         }
         if (pc.move.y < 0.5f && pc.move.y > -0.5f)
         {
             rb.velocity = new Vector2( rb.velocity.x, 0f);
         }
         */
         


        anim.SetFloat("MoveX", pc.move.x);
        anim.SetFloat("MoveY", pc.move.y);
        anim.SetBool("IsMoving", pc.playerMoving);
        anim.SetFloat("LastMoveX", pc.lastMove.x);
        anim.SetFloat("LastMoveY", pc.lastMove.y);


        // transform.position = pc.lastMove * moveSpeed * Time.deltaTime ;



    }
}


