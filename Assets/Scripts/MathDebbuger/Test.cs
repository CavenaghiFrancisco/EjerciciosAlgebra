using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = new Vector3(1, 2, 3);
        Vector3 v1 = new Vector3(8, 4, 2);
        Vec3 cv = new Vec3(1, 2, 3);
        Vec3 cv1 = new Vec3(8, 4, 2);

        Debug.Log(v.ToString());
        Debug.Log(cv.ToString());

        Debug.Log(Vector3.Angle(v, v1));
        Debug.Log(Vec3.Angle(cv, cv1));

        Debug.Log(Vector3.ClampMagnitude(v, 2));
        Debug.Log(Vec3.ClampMagnitude(cv, 2));

        Debug.Log(Vector3.Magnitude(v));
        Debug.Log(Vec3.Magnitude(cv));

        Debug.Log(Vector3.Cross(v, v1));
        Debug.Log(Vec3.Cross(cv, cv1));

        Debug.Log(Vector3.Distance(v, v1));
        Debug.Log(Vec3.Distance(cv, cv1));

        Debug.Log(Vector3.Dot(v, v1));
        Debug.Log(Vec3.Dot(cv, cv1));

        Debug.Log("Lerp" + Vector3.Magnitude(Vector3.Lerp(v, v1,1f)));
        Debug.Log(Vec3.Magnitude(Vec3.Lerp(cv, cv1,1f)));

        Debug.Log(Vector3.Magnitude(Vector3.LerpUnclamped(v, v1, 1f)));
        Debug.Log(Vec3.Magnitude(Vec3.LerpUnclamped(cv, cv1, 1f)));

        Debug.Log(Vector3.Max(v, v1));
        Debug.Log(Vec3.Max(cv, cv1));

        Debug.Log(Vector3.Min(v, v1));
        Debug.Log(Vec3.Min(cv, cv1));

        Debug.Log(Vector3.SqrMagnitude(v));
        Debug.Log(Vec3.SqrMagnitude(cv));

        Debug.Log("P"+ Vector3.Project(v,v1));
        Debug.Log(Vec3.Project(cv,cv1));

        Debug.Log(Vector3.Reflect(v, v1));
        Debug.Log(Vec3.Reflect(cv, cv1));
        Debug.Log(v.normalized);
        v.Normalize();
        Debug.Log(v.ToString());
    }

    
}
