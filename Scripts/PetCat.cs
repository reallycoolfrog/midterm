using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCat : MonoBehaviour
{
    [SerializeField] private Sprite[] idleArray;
    [SerializeField] private Sprite[] mouseoverArray;
    [SerializeField] private Sprite[] problemArray;
    [SerializeField] private Sprite[] mouseoverproblemArray;
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
        createGOTimer();
    }

    void createProblemTimer(){
    if (!problemtimerStarted && !problemState){ //Timer hasnt starter and not in problem state
        problemTimer = Random.Range(5.0f, 15.0f); //Pick a random amnt of time
        problemtimerStarted = true; //Set the timer
        }
    }

    void createGOTimer(){
    if (!GOtimerStarted){ //GO timer hasn't started
        gameoverTimer = 6f;
        GOtimerStarted = true; //GO timer set
        }
    }

    void Update() {
        if (!mouseOn){ //Mouse not over object
        Idle();
        }

        if (!problemState && problemtimerStarted){ //if not in problem state, and the timer starter
        problemTimer -= Time.deltaTime; //Timer counts down
        GOtimerStarted = false;
        }

        if(problemTimer <= 0){ //Once random timer reaches zero
        problemTimer = 0; //cant go below zero
        problemState = true; //Now in the problem state
        problemtimerStarted = false; //End timer
        }

        if(problemState && !GOtimerStarted){
            createGOTimer();
        } else if(problemState && GOtimerStarted){
            gameoverTimer -= Time.deltaTime; //Game over timer counts down
            Debug.Log(gameoverTimer);
            if (gameoverTimer <= 0) {
                gameoverTimer = 0; //can't go below zero
                GameOver(); // game over
            }
        }
    }

    void ResetStuff(){
        if(!problemState && !problemtimerStarted){ //ended problem state & timer
        createProblemTimer(); //Restart problem state timer
        problemtimerStarted = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "cattoy" && problemState){ //In problem state
            problemState = false; //end problem state!
            problemtimerStarted = false; //make sure timer is off....
            ResetStuff(); //Play this PLEASE
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
        if (!problemState) { //Obj not in the problem state
        timer += Time.deltaTime;
        if (timer >= framerate){
            timer -= framerate;
            currentFrame = (currentFrame + 1) % mouseoverArray.Length;
            myRenderer.sprite = mouseoverArray[currentFrame]; //Play normal animation
            }
        } else if (problemState){ //Obj in problem state
        timer += Time.deltaTime;
        if (timer >= framerate){
            timer -= framerate;
            currentFrame = (currentFrame + 1) % mouseoverproblemArray.Length;
            myRenderer.sprite = mouseoverproblemArray[currentFrame]; //Play problem anim
            }
        }
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