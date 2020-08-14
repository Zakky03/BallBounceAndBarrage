using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
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
        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Z))
        {
            θ = ω;
        }
        if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.X))
        {
            θ = -ω;
        }
        rigidbody2d.angularVelocity = θ;

        //ボウの横移動
        rigidbody2d.velocity += new Vector2(100f * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

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
        if (bowScale <= 0f) Score.GameOver();
        Vector2 vec = iniScale;
        vec.x = bowScale * iniScale.x;
        transform.localScale = vec;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Ball") return;
        Debug.Log(col.gameObject.GetComponent<Ball>().prevBallState);
        if (col.gameObject.GetComponent<Ball>().prevBallState == Ball.BALL_STATE.GREEN)
        {
            bowScale = Mathf.Clamp(bowScale + 0.15f, 0f, 1f);
        }
    }
}
