using System.Collections;
using System.Collections.Generic;
using System;

namespace CustomMath
{
    public struct Plane
    {
        private Vec3 planeNormal;
        private float planeDistance;

        public float distance
        {
            get { return planeDistance; }
            set { planeDistance = value; }
        }

        public Vec3 normal
        {
            get { return planeNormal; }
            set { planeNormal = value; }
        }

        public Plane flipped { get { return new Plane(-planeNormal, -planeDistance); } }

        public Plane(Vec3 inNormal, Vec3 inPoint)
        {
            planeNormal = inNormal.normalized;
            planeDistance = -Vec3.Dot(inNormal.normalized, inPoint);
        }

        public Plane(Vec3 inNormal, float d)
        {
            planeNormal = inNormal.normalized;
            planeDistance = d;
        }

        public Plane(Vec3 vecA, Vec3 vecB, Vec3 vecC)
        {
            planeNormal = (Vec3.Cross(vecB - vecA, vecC - vecA)).normalized;
            planeDistance = -Vec3.Dot(planeNormal, vecA);
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            float dot = Vec3.Dot(planeNormal, point) + planeDistance;
            return point - planeNormal * dot;
        }

        public void Flip()
        {
            planeNormal = -planeNormal;
            planeDistance = -planeDistance;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(planeNormal, point) + planeDistance;
        }

        public bool GetSide(Vec3 point)
        {
            return (double)Vec3.Dot(planeNormal, point) + (double)planeDistance > 0f;
        }

        public bool SameSide(Vec3 inPoint, Vec3 inPoint1)
        {
            float dToPoint = GetDistanceToPoint(inPoint);
            float dToPoint1 = GetDistanceToPoint(inPoint1);
            return (double)dToPoint > 0f && (double)dToPoint1 > 0f || (double)dToPoint <= 0f && (double)dToPoint1 <= 0f;
        }

        public void Set3Points(Vec3 vecA, Vec3 vecB, Vec3 vecC)
        {
            planeNormal = (Vec3.Cross(vecB - vecA, vecC - vecA)).normalized;
            planeDistance = -Vec3.Dot(planeNormal, vecA);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            planeNormal = inNormal.normalized;
            planeDistance = -Vec3.Dot(inNormal.normalized, inPoint);
        }

        public void Translate(Vec3 translation)
        {
            planeDistance += Vec3.Dot(planeNormal, translation);
        }

        public static Plane Translate(Plane plane, Vec3 translation)
        {
            return new Plane(plane.normal, plane.distance += Vec3.Dot(plane.normal, translation));
        }
    }
}