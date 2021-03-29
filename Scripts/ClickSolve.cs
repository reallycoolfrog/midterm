using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSolve : MonoBehaviour
{
    [SerializeField] private Sprite[] idleArray;
    [SerializeField] private Sprite[] problemArray;
    private int currentFrame;
    private float timer;
    
    public float framerate = .5f;

    private SpriteRenderer myRenderer;
    private bool mouseOn;
    
    private float problemTimer;
    private bool problemState = false;
    private bool problemtimerStarted = false;

    private float gameoverTimer;
    public GameObject gameover;
    private bool GOtimerStarted = false;


    // Start is called before the first frame update
    void Start(){
        mouseOn = false;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        problemState = false;
        problemtimerStarted = false;
        GOtimerStarted = false;
        createProblemTimer();
        Time.timeScale = 1;
    }

    void createProblemTimer(){
    if (!problemtimerStarted && !problemState){ //Timer hasnt starter and not in problem state
        problemTimer = Random.Range(5.0f, 15.0f); //Pick a random amnt of time
        problemtimerStarted = true; //Set the timer
        }
    }

    void createGOTimer(){
    if (!GOtimerStarted){ //GO timer hasn't started
        gameoverTimer = 5f;
        GOtimerStarted = true; //GO timer set
        }
    }

    void Update() {
        Idle();

        if (!problemState && problemtimerStarted){ //if not in problem state, and the timer starter
        problemTimer -= Time.deltaTime; //Timer counts down
        GOtimerStarted = false;
        }

        if(problemTimer <= 0){ //Once random timer reaches zero
        problemTimer = 0; //cant go below zero
        problemState = true; //Now in the problem state
        problemtimerStarted = false; //End timer
        }

        if (problemState && !GOtimerStarted){ //In problem state
        createGOTimer();
        } else if (problemState && GOtimerStarted){
        CheckClick(); //Play mouse click function
        gameoverTimer -= Time.deltaTime; //Game over timer counts down
        Debug.Log(gameoverTimer);
            if (gameoverTimer <= 0) {
                gameoverTimer = 0; //can't go below zero
                GameOver(); // game over
            }
        }

        if(!problemState && !problemtimerStarted){ //ended problem state
        createProblemTimer(); //Restart problem state timer
        problemtimerStarted = true;
        }
    }

    void CheckClick(){ //Check for clicking 
    if (mouseOn && Input.GetMouseButtonDown(0)){ //Click while mouseover & in problem state
            problemState = false; //end problem state
        }
    }

    void Idle(){ //Mouse is not over obj. plays idle animation
        if (!problemState) { //If not in problem state
            timer += Time.deltaTime;
            if (timer >= framerate){
                timer -= framerate;
                currentFrame = (currentFrame + 1) % idleArray.Length;
                myRenderer.sprite = idleArray[currentFrame];
            } //Play the normal idle
        } else if (problemState) { //In the problem state
            timer += Time.deltaTime;
            if (timer >= framerate){
                timer -= framerate;
                currentFrame = (currentFrame + 1) % problemArray.Length;
                myRenderer.sprite = problemArray[currentFrame]; //Play problem animation
            }
        }
    }

    void OnMouseOver() { //Mouse is over object, want to play specific animations
        mouseOn = true;
    }

    void OnMouseExit(){
        mouseOn = false; //Mouse no longer on object
    }

    public void GameOver(){
        if (GameObject.FindGameObjectWithTag("ShowonPause") == null){
        Instantiate(gameover, new Vector3(0, 0, 0), Quaternion.identity);
        }
        Time.timeScale = 0; //game pauses
    }
}
