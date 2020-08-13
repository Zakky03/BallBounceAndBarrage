using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    float ω;
    float θ;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        θ = 0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        θ = 0f;
        if (Input.GetButton("Fire1"))
        {
            //θ += ω * Time.deltaTime;
            θ = ω;
        }
        if (Input.GetButton("Fire2"))
        {
            //θ -= ω * Time.deltaTime;
            θ = -ω;
        }
        //θ = Mathf.Clamp(θ, -90f, 90f);
        //transform.rotation = Quaternion.Euler(0, 0, θ);
        rigidbody2d.angularVelocity = θ;

        rigidbody2d.velocity += new Vector2(100f * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);
    }
}
