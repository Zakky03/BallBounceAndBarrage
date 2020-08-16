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

    // エフェクト
    [SerializeField]
    private EffectColor eff;

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

    ////ボールの状態の色(オブジェクトごとに持つのは無駄なのでstatic)
    //static Color[] ballColor = {
    //    new Color(60, 183, 72, 255) / 255,  //GREEN
    //    new Color(39, 104, 135, 255) / 255, //BLUE
    //    new Color(248, 110, 112, 255) / 255 //RED
    //};

    public bool isRinging { private set; get; }

    public BALL_STATE prevBallState;

    // Start is called before the first frame update
    void Awake()
    {
        cirCol2D = GetComponent<CircleCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // ボールを生成するときに呼び出す by tada
    public void Init()
    {
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
            // もしゲームが終了していたなら無視する
            if (Score.Finished)
            {
                gameObject.SetActive(false);
                return;
            }

            switch (ballState)
            {
                case BALL_STATE.GREEN:
                    //// エフェクトを出す
                    //EffectFactory.Play(transform.position, (int)BALL_STATE.RED);
                    StateAndColorSetter(BALL_STATE.RED);
                    break;

                case BALL_STATE.BLUE:
                    //// エフェクトを出す
                    //EffectFactory.Play(transform.position, (int)BALL_STATE.GREEN);
                    StateAndColorSetter(BALL_STATE.GREEN);
                    break;

                case BALL_STATE.RED:
                    // Destroy(gameObject);
                    gameObject.SetActive(false);
                    break;
            }

            if (!gameObject.activeSelf) return;
            Vector2 vec = transform.position;
            vec.y += tmp * 2;
            transform.position = vec;
            // エフェクトを出す
            EffectFactory.Play(eEffectType.Bubble, transform.position - new Vector3(0f, 0.2f, 0f), (int)ballState);
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
        //ぶつかったボールが既に鳴ってたらやらない
        if (col.gameObject.tag == "Ball" && col.gameObject.GetComponent<Ball>().isRinging) return;
        else
        {
            //ボールの二つ分音がならないためにやってる
            isRinging = true;
            audioSource.Play();
        }

        //プレイヤー以外のぶつかりは無視
        if (col.gameObject.tag != "Player") return;

        //OnCollisionEnter2Dの順番よくわからんからこれで色保持してる
        prevBallState = ballState;
        //ボールの色によって運命が決まる
        switch (ballState)
        {
            case BALL_STATE.GREEN:
                Score.ScoreAdder();
                //GameObject newBall = Instantiate(ball);
                Ball newBall = BallFactory.GetInstance(); // by tada
                if (newBall != null)
                {
                    newBall.transform.position = this.transform.position;
                    newBall.GetComponent<Rigidbody2D>().velocity =
                        Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * rigidbody2d.velocity.normalized * 10f;
                    newBall.PlayEffect();
                }
                StateAndColorSetter(BALL_STATE.BLUE);
                // 2つの玉からエフェクトを出す
                PlayEffect(); 
                break;

            case BALL_STATE.RED:

                // 死亡エフェクトを出す
                EffectFactory.Play(eEffectType.Burst,transform.position, (int)BALL_STATE.RED);
                Score.GameOver();
                // ボールを消す
                gameObject.SetActive(false);
                break;
        }
    }

    void StateAndColorSetter(BALL_STATE bState)
    {
        //ボールの色と状態を代入する
        ballState = bState;
        //spRenderer.color = ballColor[(int)bState];
        spRenderer.color = CustomColorTheme.GetColors()[(int)bState];
    }

    // by tada
    public void PlayEffect()
    {
        if (eff != null) eff.Play(ballState);
    }

    void OnDisable()
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
