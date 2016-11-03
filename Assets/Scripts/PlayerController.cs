using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {


    public float moveSpeed;

    private Animator anim;
    public bool playerMoving;
    public Vector2 lastMove;
    public Vector2 move;
    public Stack<Vector2> moves;
 //   public Vector2 lastPosition;

    private Rigidbody2D myRigidbody;
    public GameObject tool;

    private bool tooling;
    private float timeTooling = 1.0f;
    private float timeToolingCounter;

    // Use this for initialization
    void Start () {
        anim = GetComponent < Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        moves = new Stack<Vector2>();
	}

    // Update is called once per frame
    void Update() {

        playerMoving = false;

        if (!tooling) { 

        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            moves.Push(lastMove);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            moves.Push(lastMove);
        }

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            timeToolingCounter = timeTooling;
            tool.SetActive(true);
            tooling = true;
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("useTool", true);


        }
    }
        if (timeToolingCounter >= 0)
        {
            timeToolingCounter -= Time.deltaTime;
        }
        if (timeToolingCounter < 0)
        {
            tooling = false;
            anim.SetBool("useTool", false);
            tool.SetActive(false);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
