﻿using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;

    private Animator anim;
    public QuickBar quickBar;
    public Inventaire inventaire;

    ItemDatabase database;

    public bool playerMoving;
    public Vector2 lastMove;
    public Vector2 move;
    public Stack<Vector2> moves;
    //   public Vector2 lastPosition;

    private Rigidbody2D myRigidbody;
    public GameObject tool;
    public ObjectController objectC;

    private bool tooling;
    private float timeTooling = 0.3f;
    private float timeToolingCounter;

    public GameObject shop;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        moves = new Stack<Vector2>();
        objectC = GetComponent<ObjectController>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
    }

    // Update is called once per frame
    void Update()
    {

        playerMoving = false;

        if (!tooling)
        {

            //deplacement du joueur
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                moves.Push(lastMove);
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
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

            //touche espace pour utiliser outils ou object 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeToolingCounter = timeTooling;
                tool.SetActive(true);
                tooling = true;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("useTool", true);
                objectC.useObject(lastMove);
            }
            // touche entré rammasse object
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("return"))
            {
                Item recolt = Map.collectPlant(new Vector3(transform.position.x + lastMove.x, transform.position.y + lastMove.y, 0));
                Debug.Log("nn " + recolt.itemName + " " + recolt.itemPower);
                FM_SonScript.sonQualite(recolt.itemPower);
                if (recolt != null)
                {
                    if (recolt.itemPower >= 75) {
                        this.SendMessage("showGood","");
                    } else if (recolt.itemPower >= 50) {
                        this.SendMessage("showMedium","");
                    } else if (recolt.itemPower < 25) {
                        this.SendMessage("showGood","");
                        this.SendMessage("showBad","");
                    }
                    
                    quickBar.addItem(database.addItem(recolt));

                }
            }
        }
        // mise a jour de l'animation des outils
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
        FM_SonScript.sonPas(playerMoving, transform.position );
        //mise a jour de l'animation de deplacement
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }


    void OnCollisionStay2D(Collision2D collider)
    {
        // collision avec le puit et touche espace pour remplir l'arrosoire
        if (collider.gameObject.name == "Puit" && Input.GetKeyDown(KeyCode.Space))
        {
            if (objectC.objectCurrent.itemName == "WateringCan")
            {
                objectC.objectCurrent.itemPower = 41;
                FM_SonScript.sonOutil("event:/Outil/remplirArrosoir");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Shop") //collision avec le magasin
        {
            FM_SonScript.magasin();
            shop.SetActive(true);
            shop.transform.GetChild(1).gameObject.SetActive(true);
            shop.transform.GetChild(2).gameObject.SetActive(false);
            shop.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
