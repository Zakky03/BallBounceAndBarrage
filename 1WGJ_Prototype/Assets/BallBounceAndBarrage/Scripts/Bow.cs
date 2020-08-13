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
    Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        iniPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BowMove();
    }

    void BowMove()
    {
        //ボウを回転させる
        θ = 0f;
        if (Input.GetButton("Fire1"))
        {
            θ = ω;
        }
        if (Input.GetButton("Fire2"))
        {
            θ = -ω;
        }
        rigidbody2d.angularVelocity = θ;

        rigidbody2d.velocity += new Vector2(100f * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

        //ボウの高さを一定にする
        {
            Vector3 vec = transform.position;
            vec.y = iniPos.y;
            transform.position = vec;
        }
    }
}
