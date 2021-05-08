using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Internal;

namespace CBZ.API.Math
{
    public static class Math
    {
        public static float PI { get => Mathf.PI; }

        public static float PIRound { get => 3.14f; }
        public static float Epsilon { get => Mathf.Epsilon; }

        public static float Rad2Deg { get => Mathf.Rad2Deg; }

        public static float Rad2DegRound { get => 57.3f; }

        public static float Deg2Rad { get => Mathf.Deg2Rad; }

        public static float Deg2RadRound { get => 0.02f; }

        public static float Infinity { get => float.PositiveInfinity; }

        public static float NegativeInfinity { get => float.NegativeInfinity; }

        public static float PositiveInfinity { get => float.PositiveInfinity; }


        public static float Clamp (float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float Clamp01(float value)
        {
            return Mathf.Clamp01(value);
        }

        public static float Cos (float value)
        {
            return Mathf.Cos(value);
        }

        public static float Acos(float value)
        {
            return Mathf.Acos(value);
        }

        public static float Asin(float value)
        {
            return Mathf.Asin(value);
        }

        public static float Sin(float value)
        {
            return Mathf.Sin(value);
        }

        public static float Tan(float value)
        {
            return Mathf.Tan(value);
        }

        public static float ATan(float value)
        {
            return Mathf.Atan(value);
        }

        public static float ATan2(float x, float y)
        {
            return Mathf.Atan2(x, y);
        }

        public static float Lerp(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, t);
        }

        public static float LerpAngle(float a, float b, float t)
        {
            return Mathf.LerpAngle(a, b, t);
        }

        public static float InverseLerp(float a, float b, float value)
        {
            return Mathf.InverseLerp(a, b, value);
        }

        public static float LerpUnClamp(float a, float b, float t)
        {
            return Mathf.LerpUnclamped(a, b, t);
        }

        public static float Abs(float value)
        {
            return Mathf.Abs(value);
        }

        public static float Abs(int value)
        {
            return Mathf.Abs(value);
        }

        public static float Log(float value)
        {
            return Mathf.Log(value);
        }

        public static float Log(int value)
        {
            return Mathf.Log(value);
        }

        public static float Log10(int value)
        {
            return Mathf.Log10(value);
        }

        public static float Log10(float value)
        {
            return Mathf.Log10(value);
        }

        public static float Round(float value)
        {
            return Mathf.Round(value);
        }

        public static float Floor(float value)
        {
            return Mathf.Floor(value);
        }
        public static int FloorInt(float value)
        {
            return Mathf.FloorToInt(value);
        }

        public static float PerlinNoise (float x,  float y)
        {
            return Mathf.PerlinNoise(x, y);
        }

        public static float Sqrt(float value)
        {
            return Mathf.Sqrt(value);
        }

        public static float Gamma(float value, float absmax, float gamma)
        {
            return Mathf.Gamma(value, absmax, gamma);
        }

        public static float MaxValueOnArray (params float[] values)
        {
            return Mathf.Max(values);
        }

        public static float MaxValue(float a, float b)
        {
            return Mathf.Max(a, b);
        }

        public static int MaxValue(int a, int b)
        {
            return Mathf.Max(a, b);
        }


        public static float MinValueOnArray(params float[] values)
        {
            return Mathf.Min(values);
        }

        public static float MinValue(float a, float b)
        {
            return Mathf.Min(a, b);
        }

        public static int MinValue(int a, int b)
        {
            return Mathf.Min(a, b);
        }

        public static int ClosestPowerOfTwo(int value)
        {
            return Mathf.ClosestPowerOfTwo(value);
        }

        public static bool IsPowerOfTwo(int value)
        {
            return Mathf.IsPowerOfTwo(value);
        }



        public static float Exp(float value)
        {
            return Mathf.Exp(value);
        }

       
        public static  bool IsEven(int value)
        {
            return (value % 2) == 0;
        }

        public static bool IsEven(float value)
        {
            return (value % 2) == 0;
        }

        public static float ProcentOfValue (float current, float procent)
        {
            return current * procent / 100;
        }

        public static int ProcentOfValue(int current, int procent)
        {
            return current * procent / 100;
        }

        public static float Sign(float value)
        {
            return Mathf.Sign(value);
        }

        public static float PingPong (float t, float length)
        {
            return Mathf.PingPong(t, length);
        }

        public static float Repeat(float t, float length)
        {
            return Mathf.Repeat(t, length);
        }

        public static float SmoothStep(float from, float to, float t)
        {
            return Mathf.SmoothStep(from, to, t);
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            return Mathf.MoveTowards(current, target, maxDelta);
        }

        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            return Mathf.MoveTowardsAngle(current, target, maxDelta);
        }

        public static int NextPowerOfTwo(int value)
        {
            return Mathf.NextPowerOfTwo(value);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
        {
            return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
        {
            return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
        }

        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
        {
            return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed);
        }

        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
        {
            return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
        {
            return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static float Cell (float value)
        {
            return Mathf.Ceil(value);
        }

        public static int CellInt(float value)
        {
            return Mathf.CeilToInt(value);
        }

        public static bool Approximately(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static Point RandomSpherePoint (Point center, float radius)
        {
           Vector3 vector = UnityEngine.Random.insideUnitSphere * radius + center.ToVector3();


            return new Point(vector.x, vector.y, vector.z);
        }

        public static int[] Fibonathi(int[] startArray, int countNumbers)
        {
            List<int> list = new List<int>(0);

            for (int i = 0; i < startArray.Length; i++)
            {
                try
                {
                  int num =  FibonathiCalculate(startArray[i], startArray[i + 1]);
                    list.Add(num);
                }
                catch 
                {

                }
            }
            for (int i = 0; i < countNumbers; i++)
            {
                try
                {
                    int num = FibonathiCalculate(list[i], list[i + 1]);
                    list.Add(num);
                }
                catch
                {

                }
            }

            return list.ToArray();
        }



        private static int FibonathiCalculate (int a, int b)
        {
            return a + b;
        }

    }
}