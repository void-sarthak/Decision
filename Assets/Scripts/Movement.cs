using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float zLimit = -50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward *  speed * Time.deltaTime);

        if(transform.position.z < zLimit)
        {
            Destroy(gameObject);
        }
    }
}
