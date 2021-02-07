using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private enum eInputType
    {
        Keyboard,
        Touch,
    }

    [SerializeField]
    private eInputType inputType;

    [SerializeField]
    private Joystick joyStick;

    [SerializeField]
    private TadaLib.CustomButton buttonL;
    [SerializeField]
    private TadaLib.CustomButton buttonR;

    [SerializeField]
    float ω;
    float θ;
    //回転の速度入れるのにつかう
    Rigidbody2D rigidbody2d;
    //上下の高さ固定につかう
    Vector2 iniPos;

    float bowScale;

    Vector2 iniScale;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        iniPos = transform.position;
        bowScale = 1f;
        iniScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        BowMove();

        BowScaler();
    }

    void BowMove()
    {
        //ボウを回転させる
        θ = 0f;
        if (inputType == eInputType.Keyboard)
        {
            if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Z))
            {
                θ = ω;
            }
            if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.X))
            {
                θ = -ω;
            }
        }
        else if(inputType == eInputType.Touch)
        {
            if(buttonL.IsPushed) θ += ω;
            if(buttonR.IsPushed) θ -= ω;
        }

        rigidbody2d.angularVelocity = θ;

        //ボウの横移動
        float moveX = (inputType == eInputType.Keyboard) ? Input.GetAxis("Horizontal") : joyStick.Horizontal;
        rigidbody2d.velocity += new Vector2(100f * moveX * Time.deltaTime, 0f);

        //ボウの高さを一定にする
        {
            Vector3 vec = transform.position;
            vec.y = iniPos.y;
            transform.position = vec;
        }
    }

    void BowScaler()
    {
        bowScale -= 0.1f * Time.deltaTime;
        if (bowScale <= 0f)
        {
            Score.GameOver();
            return;
        }
        Vector2 vec = iniScale;
        vec.x = bowScale * iniScale.x;
        transform.localScale = vec;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Ball") return;
        //Debug.Log(col.gameObject.GetComponent<Ball>().prevBallState);
        if (col.gameObject.GetComponent<Ball>().prevBallState == Ball.BALL_STATE.GREEN)
        {
            bowScale = Mathf.Clamp(bowScale + 0.15f, 0f, 1f);
        }
    }
}
