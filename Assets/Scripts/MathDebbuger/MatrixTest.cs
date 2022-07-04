using CustomMath;
using UnityEditor;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    enum QuaternionType { Unity, Custom }

    enum LerpType { LERP, SLERP }

    [SerializeField] QuaternionType quaternionType = QuaternionType.Unity;
    [SerializeField] LerpType lerpType = LerpType.LERP;
    [SerializeField] bool clamped = false;
    [SerializeField, Range(0, 2)] float lerpValue = 0;
    [SerializeField] Transform rotationFirst;
    [SerializeField] Transform rotationSecond;
    [SerializeField] Transform origin;

    private void OnValidate()
    {
        switch (quaternionType)
        {
            case QuaternionType.Unity:
                switch (lerpType)
                {
                    case LerpType.LERP:
                        origin.rotation = clamped ? Quaternion.Lerp(rotationFirst.rotation, rotationSecond.rotation, lerpValue) : Quaternion.LerpUnclamped(rotationFirst.rotation, rotationSecond.rotation, lerpValue);
                        break;
                    case LerpType.SLERP:
                        origin.rotation = clamped ? Quaternion.Slerp(rotationFirst.rotation, rotationSecond.rotation, lerpValue) : Quaternion.SlerpUnclamped(rotationFirst.rotation, rotationSecond.rotation, lerpValue);
                        break;
                }
                break;
            case QuaternionType.Custom:
                switch (lerpType)
                {
                    case LerpType.LERP:
                        origin.rotation = clamped ? CustomQuaternion.Lerp(rotationFirst.rotation, rotationSecond.rotation, lerpValue) : CustomQuaternion.LerpUnclamped(rotationFirst.rotation, rotationSecond.rotation, lerpValue);
                        break;
                    case LerpType.SLERP:
                        origin.rotation = clamped ? CustomQuaternion.Slerp(rotationFirst.rotation, rotationSecond.rotation, lerpValue) : CustomQuaternion.SlerpUnclamped(rotationFirst.rotation, rotationSecond.rotation, lerpValue);
                        break;
                }
                break;
        }
        
    }

    private void OnDrawGizmos()
    {
        if(rotationFirst && rotationSecond && origin)
        {
            Handles.color = Color.black;
            Handles.matrix = rotationFirst.localToWorldMatrix;
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.identity, 1,EventType.Repaint);

            Handles.color = Color.black;
            Handles.matrix = rotationSecond.localToWorldMatrix;
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.identity, 1, EventType.Repaint);

            Handles.color = Color.black;
            Handles.matrix = origin.localToWorldMatrix;
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.identity, 1, EventType.Repaint);
        }
    }
}

