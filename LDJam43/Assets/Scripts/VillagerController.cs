using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVillagerState { idle, moving, woodCutting }

public class VillagerController : MonoBehaviour {

    private Stack<EVillagerState> stateStack = new Stack<EVillagerState>();
    public EVillagerState currentState;

    [Header("Movement options")]
    public bool facingRight = true;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    public float newPosRange;
    public Vector2 initPosition;
    public Vector2 newPosition;

    public float timeBTWStateChange;
    private float currentTimeBtwStateChange;
    private bool CanChangeSate = true;

    private bool justChangedStates = false;

    // Use this for initialization
    void Start ()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        currentTimeBtwStateChange = 0f ;
        initPosition = transform.position;
        GetNewPosition();

        currentState = EVillagerState.idle;

        stateStack.Push(currentState);
    }
	
	// Update is called once per frame
	void Update () {

        UpdateState();
        if(stateStack.Count > 0)
        {
            currentState = stateStack.Peek();
            switch (currentState)
            {
                case EVillagerState.idle:
                    Idle();
                    break;
                case EVillagerState.moving:
                    Move();
                    break;
                case EVillagerState.woodCutting:
                    break;
                default:
                    break;
            }
        }
        else
        {
            stateStack.Push(EVillagerState.idle);
        }
    }

    public void Idle()
    {
        if (justChangedStates)
        {
            anim.SetTrigger("Idle");
        }
        else
        {
            justChangedStates = false;
        }
    }

    public void Move()
    {
        Vector2 direction = (newPosition - rb.position).normalized;

        if (justChangedStates)
        {
            anim.SetTrigger("Move");
            if (direction.x > 0)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
        }
        else
        {
            justChangedStates = false;
        }

        sr.flipX = !facingRight;

        rb.velocity = direction * speed;
    }

    public void GetNewPosition()
    {
        newPosition = initPosition + Random.insideUnitCircle * newPosRange;
        while((newPosition - rb.position).magnitude < 0.3)
        {
            newPosition = initPosition + Random.insideUnitCircle * newPosRange;
        }
    }

    public void UpdateState()
    {
        currentTimeBtwStateChange -= Time.deltaTime;
        if (currentTimeBtwStateChange <= 0 || (newPosition - rb.position).magnitude < 0.2 || CanChangeSate)
        {
            CanChangeSate = false;

            currentState = (EVillagerState) Random.Range(0, sizeof(EVillagerState) - 1);
            ChangeState(currentState);

            switch (currentState)
            {
                case EVillagerState.idle:
                    break;
                case EVillagerState.moving:
                    GetNewPosition();
                    break;
                case EVillagerState.woodCutting:
                    break;
                default:
                    break;
            }

            currentTimeBtwStateChange = Random.Range(0.5f, 1.5f) * timeBTWStateChange;
        }
    }

    public void ChangeState(EVillagerState newState)
    {
        stateStack.Pop();
        stateStack.Push(newState);
        justChangedStates = true;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(initPosition, newPosRange);
        Gizmos.DrawSphere((newPosition), 0.5f);
    }
    */

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Building")
        {
            GetNewPosition();
        }
    }
}
