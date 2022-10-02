using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlock : MonoBehaviour
{
    [SerializeField] private GameObject go;
    [SerializeField] private float Ellipse_a;
    [SerializeField] private float Ellipse_b;
     private float angle;
     //private float Circular_R;
    [SerializeField] private GameObject Point;
    [SerializeField] private float Speed = 0;

    
    private void Update()
    {
        angle += Time.deltaTime / 50f;
        Move(Ellipse_X(Ellipse_a, angle), Ellipse_Y(Ellipse_b, angle));
        
    }

    private void Move(float x, float y)
    {
        go.transform.position = new Vector3(x + Point.transform.position.x, y + Point.transform.position.y, 0);
    }


    private float Ellipse_X(float a, float angle)
    {
        return a * Mathf.Cos(angle * Speed * Mathf.Rad2Deg);
    }


    private float Ellipse_Y(float b, float angle)
    {
        return b * Mathf.Sin(angle * Speed * Mathf.Rad2Deg);
    }
}
