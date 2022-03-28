using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Ejercicios : MonoBehaviour
{
    [SerializeField, Range(1, 10)] int excersice = 1;
    [SerializeField] private Vec3 A = new Vec3(0, 0, 0);
    [SerializeField] private Vec3 B = new Vec3(0, 0, 0);
    [SerializeField] private Vec3 result = new Vec3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + A, Color.yellow, "A");
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + B, Color.blue, "B");
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + result, Color.green, "result");
        MathDebbuger.Vector3Debugger.EnableEditorView();
    }

    private void Update()
    {
        switch (excersice)
        {
            case 1:
                result = A + B;
                break;
            case 2:
                result = B - A;
                break;
            case 3:
                result = new Vec3(A.x * B.x, A.y * B.y, A.z * B.z);
                break;
            case 4:
                result = -Vec3.Cross(A, B);
                break;
            case 5:
                result = Vec3.Lerp(A, B, Time.time % 1);
                break;
            case 6:
                result = Vec3.Max(A, B);
                break;
            case 7:
                result = Vec3.Project(A, B);
                break;
            case 8:
                break;
            case 9:
                result = Vec3.Reflect(A, B.normalized);
                break;
            case 10:
                result = Vec3.LerpUnclamped(A, B, Time.time % 1);

                break;
        }
        MathDebbuger.Vector3Debugger.UpdatePosition("A", transform.position, A + transform.position);
        MathDebbuger.Vector3Debugger.UpdatePosition("B", transform.position, B + transform.position);
        MathDebbuger.Vector3Debugger.UpdatePosition("result", transform.position, result + transform.position);
    }
}
