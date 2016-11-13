using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour
{

    public GameObject player;
    public float moveSpeed;
    private Animator anim;



    private PlayerController pc;
    private Vector2 currentHolder;



    void Start()
    {

        anim = GetComponent<Animator>();

        pc = player.GetComponent<PlayerController>();
    }



    void Update()
    {
      
         


        anim.SetFloat("MoveX", pc.move.x);
        anim.SetFloat("MoveY", pc.move.y);
        anim.SetBool("IsMoving", pc.playerMoving);
        anim.SetFloat("LastMoveX", pc.lastMove.x);
        anim.SetFloat("LastMoveY", pc.lastMove.y);


        // transform.position = pc.lastMove * moveSpeed * Time.deltaTime ;



    }
}


