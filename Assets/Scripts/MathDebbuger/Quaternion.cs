using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CustomMath
{
    public struct CustomQuaternion
    { 

        public float x;
        public float y;
        public float z;
        public float w;

        public const float kEpsilon = 1E-06F;

        public static CustomQuaternion Identity
        {
             get { return new CustomQuaternion(0, 0, 0, 1); }
        }

        public CustomQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static bool operator ==(CustomQuaternion lhs, CustomQuaternion rhs) => IsEqualUsingDot(Dot(lhs, rhs));

        public static bool operator !=(CustomQuaternion lhs, CustomQuaternion rhs) => !(lhs == rhs);

        public static CustomQuaternion operator *(CustomQuaternion lhs, CustomQuaternion rhs)
        {
            float w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
            float x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
            float z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;

            return new CustomQuaternion(x, y, z, w);
        }

        public static Vec3 operator *(CustomQuaternion rotation, Vec3 point)
        {
            float rotX = rotation.x * 2f;
            float rotY = rotation.y * 2f;
            float rotZ = rotation.z * 2f;

            float rotX2 = rotation.x * rotX;
            float rotY2 = rotation.y * rotY;
            float rotZ2 = rotation.z * rotZ;

            float rotXY = rotation.x * rotY;
            float rotXZ = rotation.x * rotZ;
            float rotYZ = rotation.y * rotZ;

            float rotWX = rotation.w * rotX;
            float rotWY = rotation.w * rotY;
            float rotWZ = rotation.w * rotZ;

            Vec3 result = Vec3.Zero;

            result.x = (1f - (rotY2 + rotZ2)) * point.x + (rotXY - rotWZ) * point.y + (rotXZ + rotWY) * point.z;
            result.y = (rotXY + rotWZ) * point.x + (1f - (rotX2 + rotZ2)) * point.y + (rotYZ - rotWX) * point.z;
            result.z = (rotXZ - rotWY) * point.x + (rotYZ + rotWX) * point.y + (1f - (rotX2 + rotY2)) * point.z;

            return result;
        }

        public static implicit operator Quaternion(CustomQuaternion quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public static implicit operator CustomQuaternion(Quaternion quaternion)
        {
            return new CustomQuaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public Vec3 EulerAngles
        {
            get { return ToEulerAngles(this) * Mathf.Rad2Deg; }
            set { this = ToQuaternion(value * Mathf.Deg2Rad); }
        }

        public CustomQuaternion Normalized => Normalize(this);

        public static CustomQuaternion Euler(float x, float y, float z) => ToQuaternion(new Vec3(x, y, z) * Mathf.Deg2Rad);

        public static CustomQuaternion Euler(Vec3 euler) => ToQuaternion(euler);

        private static CustomQuaternion ToQuaternion(Vec3 vec3)
        {
            float cz = Mathf.Cos(Mathf.Deg2Rad * vec3.z / 2);
            float sz = Mathf.Sin(Mathf.Deg2Rad * vec3.z / 2);
            float cy = Mathf.Cos(Mathf.Deg2Rad * vec3.y / 2);
            float sy = Mathf.Sin(Mathf.Deg2Rad * vec3.y / 2);
            float cx = Mathf.Cos(Mathf.Deg2Rad * vec3.x / 2);
            float sx = Mathf.Sin(Mathf.Deg2Rad * vec3.x / 2);

            CustomQuaternion quat = new CustomQuaternion();

            quat.w = cx * cy * cz + sx * sy * sz;
            quat.x = sx * cy * cz - cx * sy * sz;
            quat.y = cx * sy * cz + sx * cy * sz;
            quat.z = cx * cy * sz - sx * sy * cz;

            return quat;
        }

        private static Vec3 ToEulerAngles(CustomQuaternion quat)
        {
            //Link salvador a wikipedia: https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
            Vec3 angles;

            angles.x = Mathf.Atan2(2 * (quat.w * quat.x + quat.y * quat.z), 1 - 2 * (quat.x * quat.x + quat.y * quat.y));

            if (Mathf.Abs(2 * (quat.w * quat.y - quat.z * quat.x)) >= 1)
                angles.y = (Mathf.PI / 2) * Mathf.Sign(2 * (quat.w * quat.y - quat.z * quat.x));
            else
                angles.y = Mathf.Asin(2 * (quat.w * quat.y - quat.z * quat.x));

            angles.z = Mathf.Atan2(2 * (quat.w * quat.z + quat.x * quat.y), 1 - 2 * (quat.y * quat.y + quat.z * quat.z));

            return angles;
        }


        public static CustomQuaternion Inverse(CustomQuaternion rotation)
        {
            CustomQuaternion q;
            q.w = rotation.w;
            q.x = -rotation.x;
            q.y = -rotation.y;
            q.z = -rotation.z;
            return q;
        }

        public static CustomQuaternion Normalize(CustomQuaternion quat)
        {
            float sqrtDot = Mathf.Sqrt(Dot(quat, quat));

            if (sqrtDot < Mathf.Epsilon)
            {
                return Identity;
            }

            return new CustomQuaternion(quat.x / sqrtDot, quat.y / sqrtDot, quat.z / sqrtDot, quat.w / sqrtDot);
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public static CustomQuaternion Lerp(CustomQuaternion a, CustomQuaternion b, float t)
        {
            return LerpUnclamped(a, b, Mathf.Clamp01(t));
        }

        public static CustomQuaternion LerpUnclamped(CustomQuaternion a, CustomQuaternion b, float t)
        {
            CustomQuaternion r;
            float time = 1 - t;
            r.x = time * a.x + t * b.x;
            r.y = time * a.y + t * b.y;
            r.z = time * a.z + t * b.z;
            r.w = time * a.w + t * b.w;

            r.Normalize();

            return r;
        }


       
        public static CustomQuaternion Slerp(CustomQuaternion a, CustomQuaternion b, float t) => SlerpUnclamped(a, b, Mathf.Clamp01(t));

       
        public static CustomQuaternion SlerpUnclamped(CustomQuaternion a, CustomQuaternion b, float t)
        {
            CustomQuaternion r;

            float time = 1 - t;

            float wa, wb;

            float angle = Mathf.Acos(Dot(a, b));

            angle = Mathf.Abs(angle);

            float sn = Mathf.Sin(angle);

            wa = Mathf.Sin(time * angle) / sn;
            wb = Mathf.Sin((1 - time) * angle) / sn;

            r.x = wa * a.x + wb * b.x;
            r.y = wa * a.y + wb * b.y;
            r.z = wa * a.z + wb * b.z;
            r.w = wa * a.w + wb * b.w;

            r.Normalize();

            return r;
        }

        public static float Angle(CustomQuaternion a, CustomQuaternion b)
        {
            float dot = Dot(a, b);

            return IsEqualUsingDot(dot) ? 0f : (Mathf.Acos(Mathf.Min(Mathf.Abs(dot), 1f)) * 2f * Mathf.Rad2Deg);
        }

        private static bool IsEqualUsingDot(float dot)
        {
            return dot > 0.999999f;
        }

        public static float Dot(CustomQuaternion a, CustomQuaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static CustomQuaternion AngleAxis(float angle, Vec3 axis)
        {
            axis.Normalize();
            axis *= Mathf.Sin(angle * Mathf.Deg2Rad * 0.5f);
            return new CustomQuaternion(axis.x, axis.y, axis.z, Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f));
        }

        public static CustomQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            Vec3 axis = Vec3.Cross(fromDirection, toDirection);
            float angle = Vec3.Angle(fromDirection, toDirection);
            return AngleAxis(angle, axis.normalized);
        }

        public static CustomQuaternion LookRotation(Vec3 forward, Vec3 upwards)
        {
            Vec3 dir = (upwards - forward).normalized;
            Vec3 rotAxis = Vec3.Cross(Vec3.Forward, dir);
            float dot = Vec3.Dot(Vec3.Forward, dir);

            CustomQuaternion result;
            result.x = rotAxis.x;
            result.y = rotAxis.y;
            result.z = rotAxis.z;
            result.w = dot + 1;

            return result.Normalized;
        }

        public static CustomQuaternion LookRotation(Vec3 forward) 
        {
            return LookRotation (forward, Vec3.Up);
        }

        public static CustomQuaternion RotateTowards(CustomQuaternion from, CustomQuaternion to, float maxDegreesDelta)
        {
            float angle = Angle(from, to);

            if (angle == 0f)
            {
                return to;
            }

            return SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / angle));
        }

        public override bool Equals(object other)
        {
            if (!(other is CustomQuaternion))
            {
                return false;
            }

            return Equals((CustomQuaternion)other);
        }

        public bool Equals(CustomQuaternion other)
        {
            return x.Equals(other.x) &&
                   y.Equals(other.y) &&
                   z.Equals(other.z) &&
                   w.Equals(other.w);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }
    }
}