using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    CircleCollider2D cirCol2D;
    SpriteRenderer spRenderer;
    //Rigidbody2D rigidbody2d;

    [SerializeField]
    GameObject ball;

    [SerializeField]
    public enum BALL_STATE
    {
        GREEN,
        BLUE,
        RED
    }
    [SerializeField]
    public BALL_STATE ballState;

    Color[] ballColor = new Color[3];

    // Start is called before the first frame update
    void Start()
    {
        cirCol2D = GetComponent<CircleCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        //rigidbody2d = GetComponent<Rigidbody2D>();

        ballColor[(int)BALL_STATE.GREEN] = new Color(60, 183, 72, 255) / 255;
        ballColor[(int)BALL_STATE.BLUE] = new Color(39, 104, 135, 255) / 255;
        ballColor[(int)BALL_STATE.RED] = new Color(248, 110, 112, 255) / 255;

        StateAndColorSetter(BALL_STATE.BLUE);

        Score.ballNum++;
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = 5 + cirCol2D.radius * transform.localScale.x;
        while (transform.position.y <= -tmp)
        {
            switch (ballState)
            {
                case BALL_STATE.GREEN:
                    StateAndColorSetter(BALL_STATE.RED);
                    Score.scoreAdder(-1);
                    break;

                case BALL_STATE.BLUE:
                    StateAndColorSetter(BALL_STATE.GREEN);
                    break;

                case BALL_STATE.RED:
                    Destroy(gameObject);
                    break;
            }

            Vector2 vec = transform.position;
            vec.y += tmp * 2;
            transform.position = vec;
        }
        while (transform.position.y >= tmp)
        {
            Vector2 vec = transform.position;
            vec.y -= tmp * 2;
            transform.position = vec;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player") return;
        
        switch (ballState)
        {
            case BALL_STATE.GREEN:
                Score.scoreAdder(2);
                GameObject newBall = Instantiate(ball);
                newBall.GetComponent<Rigidbody2D>().velocity =
                    Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * Vector2.up * 10f;
                StateAndColorSetter(BALL_STATE.BLUE);
                //rigidbody2d.velocity = Vector2.up * 10f;
                break;

            case BALL_STATE.RED:
                Score.GameOver();
                break;
        }
    }

    void StateAndColorSetter(BALL_STATE bState)
    {
        ballState = bState;
        spRenderer.color = ballColor[(int)bState];
    }

    void OnDestroy()
    {
        Score.ballNum--;
        if (Score.ballNum == 0)
        {
            Score.GameOver();
        }
    }
}
