using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    //スクロールする距離を測るのにつかう
    CircleCollider2D cirCol2D;
    //色を変えるのに使う
    SpriteRenderer spRenderer;
    //跳ね返るボールの方向取るのに使う
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;

    //分裂するボールのプレハブ
    [SerializeField]
    GameObject ball;

    //ボールの状態
    [SerializeField]
    public enum BALL_STATE
    {
        GREEN,
        BLUE,
        RED
    }
    //自分のボールの状態
    [SerializeField]
    public BALL_STATE ballState;

    //[SerializeField]
    //bool isFirstBall = false;

    //ボールの状態の色
    Color[] ballColor = new Color[(int)BALL_STATE.RED + 1];

    public bool isRinging { private set; get; }

    public BALL_STATE prevBallState;

    // Start is called before the first frame update
    void Start()
    {
        cirCol2D = GetComponent<CircleCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        //if (isFirstBall) rigidbody2d.velocity = Vector2.up * 10f;

        //色を定義(staticで持った方がいいかも)
        ballColor[(int)BALL_STATE.GREEN] = new Color(60, 183, 72, 255) / 255;
        ballColor[(int)BALL_STATE.BLUE] = new Color(39, 104, 135, 255) / 255;
        ballColor[(int)BALL_STATE.RED] = new Color(248, 110, 112, 255) / 255;

        //最初はBLUE状態から
        StateAndColorSetter(BALL_STATE.BLUE);

        //シーンのボールの個数増やす
        Score.ballNum++;
    }

    // Update is called once per frame
    void Update()
    {
        isRinging = false;
        //上下に行きすぎたら位置をスクロールする
        BallScroll();
    }

    void BallScroll()
    {
        //ボールが上下に過ぎ去った時
        float tmp = 5 + cirCol2D.radius * transform.localScale.x;
        //落ちた時ボールの色に応じて状態が変化する
        while (transform.position.y <= -tmp)
        {
            switch (ballState)
            {
                case BALL_STATE.GREEN:
                    StateAndColorSetter(BALL_STATE.RED);
                    //Score.scoreAdder(-1);
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
        //あがった時は変化なし
        while (transform.position.y >= tmp)
        {
            Vector2 vec = transform.position;
            vec.y -= tmp * 2;
            transform.position = vec;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<Ball>().isRinging) return;
        else
        {
            //Debug.Log("Ringed");
            isRinging = true;
            audioSource.Play();
        }
        //プレイヤー以外のぶつかりは無視
        if (col.gameObject.tag != "Player") return;

        prevBallState = ballState;
        //ボールの色によって運命が決まる
        switch (ballState)
        {
            case BALL_STATE.GREEN:
                Score.ScoreAdder();
                GameObject newBall = Instantiate(ball);
                newBall.GetComponent<Rigidbody2D>().velocity =
                    Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * rigidbody2d.velocity.normalized * 10f;
                StateAndColorSetter(BALL_STATE.BLUE);
                break;

            case BALL_STATE.RED:

                Score.GameOver();
                break;
        }
    }

    void StateAndColorSetter(BALL_STATE bState)
    {
        //ボールの色と状態を代入する
        ballState = bState;
        spRenderer.color = ballColor[(int)bState];
    }

    void OnDestroy()
    {
        //死んだときボールの数減らす
        Score.ballNum--;
        //ボール0のときGameOver
        if (Score.ballNum == 0)
        {
            Score.GameOver();
        }
    }
}
