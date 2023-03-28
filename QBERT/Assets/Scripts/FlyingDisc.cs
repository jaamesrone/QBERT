using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDisc : MonoBehaviour
{

    public Transform a;
    public Transform b;

    public float HeightLimit = 5;
    public float speed;
    private Transform current;
    private Transform target;
    private float sinTime;

    private void Start()
    {

    }

    private void Update()
    {
        if (target!=null&&transform.position != target.position) 
        {
            sinTime += Time.deltaTime * speed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = evaluate(sinTime);
            transform.position = Vector3.Lerp(current.position, target.position, t);
            GameManager.Instance.Player.transform.position = Vector3.Lerp(current.position, target.position, t);
        }
        else if (gameObject.transform.localPosition.y >= HeightLimit)
        {
            gameObject.SetActive(false);
        }
    }

    public float evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            current = a;
            target = b;
            transform.position = current.position;

        }
    }
}
