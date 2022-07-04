using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float angle = 2.0f;
    [SerializeField] Quaternion quaternion;
    [SerializeField] Quaternion quaternion2;
    [SerializeField] Quaternion result;
    // Update is called once per frame


    void Update()
    {
        //rot = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0.0f);
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    transform.position = new Vector3(transform.position.x * rot.x - transform.position.y * rot.y,
        //                                     transform.position.y * rot.x + transform.position.x * rot.y,
        //                                     0.0f);
        //}

        result.w = (quaternion.w * quaternion2.w - quaternion.x * quaternion2.x - quaternion.y * quaternion2.y - quaternion.z * quaternion2.z);
        result.x = (quaternion.w * quaternion2.x + quaternion.x * quaternion2.w + quaternion.y * quaternion2.z - quaternion.z * quaternion2.y);
        result.y = (quaternion.w * quaternion2.y - quaternion.x * quaternion2.z + quaternion.y * quaternion2.w + quaternion.z * quaternion2.x);
        result.z = (quaternion.w * quaternion2.z + quaternion.x * quaternion2.y - quaternion.y * quaternion2.x + quaternion.z * quaternion2.w);


    }
}
