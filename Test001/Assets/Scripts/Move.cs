using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool isUI;

    //internal int test; // 프로젝트 내에서만 사용가능

    private void Update()
    {
        if(isUI == true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            transform.Translate(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
        }
        else if(isUI == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            transform.Translate(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.R)) transform.Rotate(0, 0, 10 * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.T)) transform.Rotate(0, 10 * Time.deltaTime * speed, 0);

        if (Input.GetKey(KeyCode.Y)) transform.Rotate(10 * Time.deltaTime * speed, 0, 0);

        if (Input.GetKey(KeyCode.C))
        {
            Vector3 scale = transform.localScale;
            scale += new Vector3(0.01f, 0.01f, 0.01f);
            transform.localScale = scale;
        }

        if (Input.GetKey(KeyCode.V))
        {
            Vector3 scale = transform.localScale;
            scale -= new Vector3(0.01f, 0.01f, 0.01f);
            transform.localScale = scale;
        }
    }
}
