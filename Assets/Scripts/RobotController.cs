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
        if (pc.move.x > 0.5f || pc.move.x < -0.5f)
        {
            //   if  (targetDirection.magnitude > moveSpeed)
            rb.velocity = pc.moves.Pop() * moveSpeed;
        }
        if (pc.move.y > 0.5f || pc.move.y < -0.5f)
        {
            rb.velocity = pc.lastMove * moveSpeed;
        }
        if (pc.move.x < 0.5f && pc.move.x > -0.5f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (pc.move.y < 0.5f && pc.moves.Pop().y > -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        */


       
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


        anim.SetFloat("MoveX", pc.move.x);
        anim.SetFloat("MoveY", pc.move.y);
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("LastMoveX", pc.lastMove.x);
        anim.SetFloat("LastMoveY", pc.lastMove.y);


        // transform.position = pc.lastMove * moveSpeed * Time.deltaTime ;


        /*range = Vector2.Distance(transform.position, transPlayer.position);
        if (range > minDistance)
        {
            Debug.Log(range);
            transform.Translate (Vector2.MoveTowards(transform.position, transPlayer.position,  moveSpeed * Time.deltaTime))
                ;

        }*/

        //Vector3 targetDirection = player.transform.position - transform.position;
        //transform.position += targetDirection * moveSpeed * Time.deltaTime;
        /*
        Vector3 targetDirection = transPlayer.position - transform.position;
        float moveDistance = moveSpeed * Time.deltaTime;
        if ( targetDirection.magnitude > moveSpeed)
        {
            transform.position += targetDirection.normalized * moveDistance;
        }
        else
        {
            transform.position = transPlayer.position;
        }
        rb.velocity = new Vector2();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        //transform.position += targetDirection.normalized * moveSpeed * Time.deltaTime;
        */
    }
}


