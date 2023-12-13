using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 1f)]
    public float speedMultiplier = 1f;

    private float gameSpeed;

    private float length;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        length = GetComponent<MeshRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameSpeed = GameManager.Instance._gameSpeed;

        distance += (Time.fixedDeltaTime * gameSpeed * speedMultiplier) / length;

        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        //transform.position = new Vector3(transform.position.x + Time.fixedDeltaTime * -GameManager.Instance._gameSpeed, transform.position.y, transform.position.z);
    }
}
