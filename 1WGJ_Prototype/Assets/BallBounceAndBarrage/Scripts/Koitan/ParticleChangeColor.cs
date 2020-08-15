using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChangeColor : MonoBehaviour
{
    ParticleSystem particle;
    [SerializeField]
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        particle.startColor = color;
        
    }
}
