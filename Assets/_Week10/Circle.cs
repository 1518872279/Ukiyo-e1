using UnityEngine;

/*
 * 
 *  Writer:June
 * 
 *  Date: 2019.06.26
 * 
 *  Function:让物体做圆周运动、椭圆运动、双曲线运动
 * 
 *  Remarks:圆的参数方程 x=a+r cosθ y=b+r sinθ（θ∈ [0，2π) ） (a,b) 为圆心坐标，r 为圆半径，θ 为参数，(x,y) 为经过点的坐标
 *          椭圆的参数方程 x=a cosθ　 y=b sinθ（θ∈[0，2π）） a为长半轴长 b为短半轴长 θ为参数
 *          双曲线的参数方程 x =a secθ （正割） y=b tanθ a为实半轴长 b为虚半轴长 θ为参数    secθ （正割）即1/cosθ 
 */


public class Circle : MonoBehaviour
{
    /// <summary>
    /// 要移动的物体
    /// </summary>
    [Header("要移动的物体"), SerializeField]
    private GameObject go;
    /// <summary>
    /// 长轴长
    /// </summary>
    [Header("长轴长"), SerializeField]
    private float Ellipse_a;
    /// <summary>
    /// 短轴长
    /// </summary>
    [Header("短轴长"), SerializeField]
    private float Ellipse_b;
    /// <summary>
    /// 角度
    /// </summary>
    private float angle;
    /// <summary>
    /// 圆半径
    /// </summary>
    [Header("圆半径"), SerializeField]
    private float Circular_R;
    /// <summary>
    /// 原点
    /// </summary>
    [Header("原点"), SerializeField]
    private GameObject Point;
    /// <summary>
    /// 双曲线实轴
    /// </summary>
    [Header("双曲线实轴"), SerializeField]
    private float Hyperbola_a;
    /// <summary>
    /// 双曲线虚轴
    /// </summary>
    [Header("双曲线虚轴"), SerializeField]
    private float Hyperbola_b;

    [Header("Speed"), SerializeField]
    private float Speed = 0;

    private void Update()
    {
        //角度
        angle += Time.deltaTime / 50f;
        //Move(Hyperbola_X(Hyperbola_a, angle), Hyperbola_Y(Hyperbola_b, angle));
        Move(Ellipse_X(Ellipse_a, angle), Ellipse_Y(Ellipse_b, angle));
        if (Input.GetKey(KeyCode.A))
        {
            //椭圆运动
            Move(Ellipse_X(Ellipse_a, angle), Ellipse_Y(Ellipse_b, angle));
        }

        if (Input.GetKey(KeyCode.S))
        {
            //圆运动
            Move(Circular_X(0, Circular_R, angle), Circular_Y(0, Circular_R, angle));
        }

        if (Input.GetKey(KeyCode.D))
        {
            //双曲线运动
            Move(Hyperbola_X(Hyperbola_a, angle), Hyperbola_Y(Hyperbola_b, angle));
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void Move(float x, float y)
    {
        go.transform.position = new Vector3(x + Point.transform.position.x, y + Point.transform.position.y, 0) ;
    }


    /// <summary>
    /// 椭圆x坐标
    /// </summary>
    /// <param name="a">长半轴</param>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float Ellipse_X(float a, float angle)
    {
        return a * Mathf.Cos(angle * Speed * Mathf.Rad2Deg);
    }

    /// <summary>
    /// 椭圆y坐标
    /// </summary>
    /// <param name="b">短半轴</param>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float Ellipse_Y(float b, float angle)
    {
        return b * Mathf.Sin(angle * Speed * Mathf.Rad2Deg);
    }


    /// <summary>
    /// 圆x坐标
    /// </summary>
    /// <param name="a">圆心x坐标</param>
    /// <param name="r">半径</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    private float Circular_X(float a, float r, float angle)
    {
        return (a + r * Mathf.Cos(angle * Mathf.Rad2Deg));
    }

    /// <summary>
    /// 圆y坐标
    /// </summary>
    /// <param name="b">圆心y坐标</param>
    /// <param name="r">半径</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    private float Circular_Y(float b, float r, float angle)
    {
        return (b + r * Mathf.Sin(angle * Mathf.Rad2Deg));
    }

    /// <summary>
    /// 双曲线x坐标
    /// </summary>
    /// <param name="a">实轴</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    private float Hyperbola_X(float a, float angle)
    {
        return a / Mathf.Cos(angle * Mathf.Rad2Deg);
    }

    /// <summary>
    /// 双曲线y坐标
    /// </summary>
    /// <param name="a">虚轴</param>
    /// <param name="angle">角度</param>
    /// <returns></returns>
    private float Hyperbola_Y(float b, float angle)
    {
        return b * Mathf.Tan(angle * Mathf.Rad2Deg);
    }
}