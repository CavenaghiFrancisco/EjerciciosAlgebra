using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Matrix4x4
    {
        public float m00;
        public float m10;
        public float m20;
        public float m30;

        public float m01;
        public float m11;
        public float m21;
        public float m31;

        public float m02;
        public float m12;
        public float m22;
        public float m32;

        public float m03;
        public float m13;
        public float m23;
        public float m33;

        private static readonly Matrix4x4 Zero = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

        private static readonly Matrix4x4 Identity = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));

        public Vec3 lossyScale => GetLossyScale();
        private Vec3 GetLossyScale()
        {
            return new Vec3(m00, m11, m22);
        }       

        public CustomQuaternion rotation => GetRotation();

        private CustomQuaternion GetRotation()
        {
            CustomQuaternion xRotation = new CustomQuaternion(1, Mathf.Cos(m11) - Mathf.Sin(m12), Mathf.Sin(m21) + Mathf.Cos(m22), 1);
            CustomQuaternion yRotation = new CustomQuaternion(Mathf.Cos(m00) + Mathf.Sin(m02), 1, -Mathf.Sin(m20) + Mathf.Cos(m22), 1);
            CustomQuaternion zRotation = new CustomQuaternion(Mathf.Cos(m00) - Mathf.Sin(m01), Mathf.Sin(m10) + Mathf.Cos(m11), 1, 1);
            return xRotation * yRotation * zRotation;
        }

        public float this[int index]
        {
            get
            {
                return index switch
                {
                    0 => m00,
                    1 => m10,
                    2 => m20,
                    3 => m30,
                    4 => m01,
                    5 => m11,
                    6 => m21,
                    7 => m31,
                    8 => m02,
                    9 => m12,
                    10 => m22,
                    11 => m32,
                    12 => m03,
                    13 => m13,
                    14 => m23,
                    15 => m33,
                    _ => throw new IndexOutOfRangeException("Invalid matrix index!"),
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m00 = value;
                        break;
                    case 1:
                        m10 = value;
                        break;
                    case 2:
                        m20 = value;
                        break;
                    case 3:
                        m30 = value;
                        break;
                    case 4:
                        m01 = value;
                        break;
                    case 5:
                        m11 = value;
                        break;
                    case 6:
                        m21 = value;
                        break;
                    case 7:
                        m31 = value;
                        break;
                    case 8:
                        m02 = value;
                        break;
                    case 9:
                        m12 = value;
                        break;
                    case 10:
                        m22 = value;
                        break;
                    case 11:
                        m32 = value;
                        break;
                    case 12:
                        m03 = value;
                        break;
                    case 13:
                        m13 = value;
                        break;
                    case 14:
                        m23 = value;
                        break;
                    case 15:
                        m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                return this[row + column * 4];
            }
            set
            {
                this[row + column * 4] = value;
            }
        }

        public Matrix4x4 transpose => Transpose(this);

        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            float aux;

            aux = m.m01;
            m.m01 = m.m10;
            m.m10 = aux;

            aux = m.m02;
            m.m02 = m.m20;
            m.m20 = aux;

            aux = m.m03;
            m.m03 = m.m30;
            m.m30 = aux;

            aux = m.m12;
            m.m12 = m.m21;
            m.m21 = aux;

            aux = m.m13;
            m.m13 = m.m31;
            m.m31 = aux;

            aux = m.m23;
            m.m23 = m.m32;
            m.m32 = aux;

            return m;
        }

        
        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;
            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;
            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 result = default(Matrix4x4);
            result.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
            result.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
            result.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
            result.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
            result.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
            result.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
            result.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
            result.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
            result.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
            result.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
            result.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
            result.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
            result.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
            result.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
            result.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
            result.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
            return result;
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
        {
            Vector4 result = default(Vector4);
            result.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
            result.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
            result.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
            result.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
            return result;
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return lhs.GetColumn(0) == rhs.GetColumn(0) &&
                   lhs.GetColumn(1) == rhs.GetColumn(1) &&
                   lhs.GetColumn(2) == rhs.GetColumn(2) &&
                   lhs.GetColumn(3) == rhs.GetColumn(3);
        }

        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs) => !(lhs == rhs);


        public Vector4 GetColumn(int number)
        {
            return number switch
            {
                0 => new Vector4(m00, m10, m20, m30),
                1 => new Vector4(m01, m11, m21, m31),
                2 => new Vector4(m02, m12, m22, m32),
                3 => new Vector4(m03, m13, m23, m33),
                _ => throw new IndexOutOfRangeException("Invalid column index!"),
            };
        }

        

        public static Matrix4x4 Rotate(CustomQuaternion q)
        {
            float x = q.x * 2f;
            float y = q.y * 2f;
            float z = q.z * 2f;
            float x2 = q.x * x;
            float y2 = q.y * y;
            float z2 = q.z * z;
            float xy = q.x * y;
            float xz = q.x * z;
            float yz = q.y * z;
            float wx = q.w * x;
            float wy = q.w * y;
            float wz = q.w * z;

            Matrix4x4 result = default(Matrix4x4);

            result.m00 = 1f - (y2 + z2);
            result.m10 = xy + wz;
            result.m20 = xz - wy;
            result.m30 = 0f;
            result.m01 = xy - wz;
            result.m11 = 1f - (x2 + z2);
            result.m21 = yz + wx;
            result.m31 = 0f;
            result.m02 = xz + wy;
            result.m12 = yz - wx;
            result.m22 = 1f - (x2 + y2);
            result.m32 = 0f;
            result.m03 = 0f;
            result.m13 = 0f;
            result.m23 = 0f;
            result.m33 = 1f;

            return result;
        }

        public static Matrix4x4 Scale(Vec3 vector)
        {
            Matrix4x4 result = default(Matrix4x4);
            result.m00 = vector.x;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;
            result.m10 = 0f;
            result.m11 = vector.y;
            result.m12 = 0f;
            result.m13 = 0f;
            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = vector.z;
            result.m23 = 0f;
            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
            return result;
        }

        public static Matrix4x4 Translate(Vec3 vector)
        {
            Matrix4x4 result = default(Matrix4x4);
            result.m00 = 1f;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = vector.x;
            result.m10 = 0f;
            result.m11 = 1f;
            result.m12 = 0f;
            result.m13 = vector.y;
            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = 1f;
            result.m23 = vector.z;
            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
            return result;
        }
        

        public static Matrix4x4 TRS(Vec3 translation, CustomQuaternion rotation, Vec3 scale)
        {
            Matrix4x4 t = Matrix4x4.Translate(translation);
            Matrix4x4 r = Matrix4x4.Rotate(rotation);
            Matrix4x4 s = Matrix4x4.Scale(scale);

            return t * r * s;
        }

       
        public override string ToString()
        {
            return $"{m00:00.00}\t{m01:00.00}\t{m02:00.00}\t{m03:00.00}\n" +
                   $"{m10:00.00}\t{m11:00.00}\t{m12:00.00}\t{m13:00.00}\n" +
                   $"{m20:00.00}\t{m21:00.00}\t{m22:00.00}\t{m23:00.00}\n" +
                   $"{m30:00.00}\t{m31:00.00}\t{m32:00.00}\t{m33:00.00}";
        }
    }
}