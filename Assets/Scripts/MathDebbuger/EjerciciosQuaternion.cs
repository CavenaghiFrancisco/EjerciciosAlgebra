using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using MathDebbuger;

public class EjerciciosQuaternion : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField, Range(1, 3)] private int exercises = 1;

    Vec3 vectorA = new Vec3(10, 0, 0);
    Vec3 vectorB = new Vec3(10, 10, 0);
    Vec3 vectorC = new Vec3(20, 10, 0);
    Vec3 vectorD = new Vec3(20, 20, 0);

    Vec3 vecA;

    private void OnValidate() => SetExcersice(exercises);

    void Start()
    {
        vecA = CustomQuaternion.Euler(new Vec3(0, angle, 0)) * new Vec3(10, 0, 0);
        Vector3Debugger.AddVector(Vector3.zero, vectorA, Color.black, nameof(vectorA));
        Vector3Debugger.AddVector(vectorA, vectorB, Color.black, nameof(vectorB));
        Vector3Debugger.AddVector(vectorB, vectorC, Color.black, nameof(vectorC));
        Vector3Debugger.AddVector(vectorC, vectorD, Color.black, nameof(vectorD));
        Vector3Debugger.EnableEditorView();
    }

    void FixedUpdate()
    {
        HideAllVectors();

        switch (exercises)
        {
            case 1:
                ShowVector(nameof(vectorA));

                vectorA = CustomQuaternion.Euler(new Vec3(0, angle, 0)) * vectorA;

                Vector3Debugger.UpdatePosition(nameof(vectorA), vectorA);
                break;
            case 2:
                ShowVector(nameof(vectorA));
                ShowVector(nameof(vectorB));
                ShowVector(nameof(vectorC));

                vectorA = CustomQuaternion.Euler(new Vec3(0, angle, 0)) * vectorA;
                vectorB = CustomQuaternion.Euler(new Vec3(0, angle, 0)) * vectorB;
                vectorC = CustomQuaternion.Euler(new Vec3(0, angle, 0)) * vectorC;

                Vector3Debugger.UpdatePosition(nameof(vectorA), vectorA);
                Vector3Debugger.UpdatePosition(nameof(vectorB), vectorA, vectorB);
                Vector3Debugger.UpdatePosition(nameof(vectorC), vectorB, vectorC);

                break;
            case 3:

                ShowVector(nameof(vectorA));
                ShowVector(nameof(vectorB));
                ShowVector(nameof(vectorC));
                ShowVector(nameof(vectorD));

                vectorA = CustomQuaternion.Euler(new Vec3(angle, angle, 0)) * vectorA;
                vectorC = CustomQuaternion.Euler(new Vec3(-angle, -angle, 0)) * vectorC;

                Vector3Debugger.UpdatePosition(nameof(vectorA), vectorA);
                Vector3Debugger.UpdatePosition(nameof(vectorB), vectorA, vectorB);
                Vector3Debugger.UpdatePosition(nameof(vectorC), vectorB, vectorC);
                Vector3Debugger.UpdatePosition(nameof(vectorD), vectorC, vectorD);

                break;
        }
    }

    private void SetExcersice(int index)
    {
        exercises = Mathf.Clamp(exercises, 1, 3);

        vectorA = new Vec3(10, 0, 0);
        vectorB = new Vec3(10, 10, 0);
        vectorC = new Vec3(20, 10, 0);
        vectorD = new Vec3(20, 20, 0);
    }

    private void HideAllVectors()
    {
        HideVector(nameof(vectorA));
        HideVector(nameof(vectorB));
        HideVector(nameof(vectorC));
        HideVector(nameof(vectorD));
    }

    private void HideVector(string key)
    {
        Vector3Debugger.TurnOffVector(key);
        Vector3Debugger.DisableEditorView(key);
    }

    private void ShowVector(string key)
    {
        Vector3Debugger.TurnOnVector(key);
        Vector3Debugger.EnableEditorView(key);
    }
}