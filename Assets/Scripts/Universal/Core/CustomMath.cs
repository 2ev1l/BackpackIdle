using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Universal.Core
{
    public static class CustomMath
    {
        #region fields & properties
        #endregion fields & properties

        #region methods
        public static IEnumerator WaitAFrame()
        {
            yield return null;
        }

        #region Geometry
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Result with math quarters 1 => (1, 1); 2 => (-1, 1); 3 => (-1, -1); 4 => (1, -1)</returns>
        public static Vector2 GetScreenSquare(Vector3 cameraPosition, Vector3 currentPosition) => (currentPosition - cameraPosition) switch
        {
            Vector3 vec when vec.x >= 0 && vec.y >= 0 => Vector2.right + Vector2.up,
            Vector3 vec when vec.x <= 0 && vec.y >= 0 => -Vector2.right + Vector2.up,
            Vector3 vec when vec.x <= 0 && vec.y <= 0 => -Vector2.right - Vector2.up,
            Vector3 vec when vec.x >= 0 && vec.y <= 0 => Vector2.right - Vector2.up,
            _ => -Vector2.right - Vector2.up
        };
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Result with math quarters 1 => (1, 1); 2 => (-1, 1); 3 => (-1, -1); 4 => (1, -1)</returns>
        public static Vector2 GetScreenSquare()
        {
            Vector2 screen2 = new(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;
            Vector2 result = Vector2.zero;
            result.x = (mousePosition.x > screen2.x) ? -1 : 1;
            result.y = (mousePosition.y > screen2.y) ? -1 : 1;
            return result;
        }
        #endregion Geometry
        /// <summary>
        /// The same as <see cref="Mathf.Lerp"/> but simplified with vector
        /// </summary>
        /// <param name="length"></param>
        /// <param name="t">0..1f</param>
        /// <returns></returns>
        public static float LerpVector(Vector2 length, float t) => Mathf.Lerp(length.x, length.y, t);
        /// <summary>
        /// The same as <see cref="Mathf.InverseLerp"/> but simplified with vector
        /// </summary>
        /// <param name="length"></param>
        /// <param name="vectorPosition">length.x..length.y</param>
        /// <returns></returns>
        public static float InverseLerpVector(Vector2 length, float vectorPosition) => Mathf.InverseLerp(length.x, length.y, vectorPosition);
        /// <summary>
        /// Transform value from old length to new, e.g. old:Vector(4, 1), old:2, new:Vector(0, 10) = 6.66f,
        /// </summary>
        /// <param name="oldLength"></param>
        /// <param name="oldLengthValue"></param>
        /// <param name="newLength"></param>
        /// <returns></returns>
        public static float ConvertValueFromTo(Vector2 oldLength, float oldLengthValue, Vector2 newLength)
        {
            float lerpedValue = InverseLerpVector(oldLength, oldLengthValue);
            return LerpVector(newLength, lerpedValue);
        }
        public static Vector2 ConvertVectorFromTo(Vector2 oldMinMax, Vector2 scaleable, Vector2 newMinMax)
        {
            return ConvertVectorFromTo(oldMinMax, oldMinMax, scaleable, newMinMax, newMinMax);
        }
        public static Vector2 ConvertVectorFromTo(Vector2 oldMinMaxX, Vector2 oldMinMaxY, Vector2 scaleable, Vector2 newMinMaxX, Vector2 newMinMaxY)
        {
            float x = ConvertValueFromTo(oldMinMaxX, scaleable.x, newMinMaxX);
            float y = ConvertValueFromTo(oldMinMaxY, scaleable.y, newMinMaxY);
            return new(x, y);
        }

        public static float ConvertToFloat(double value) => System.Convert.ToSingle(value);
        public static float ConvertToFloat(string value) => ConvertToFloat(System.Convert.ToDouble(value.Replace(".", ",")));
        public static double ConvertToDouble(string value) => System.Convert.ToDouble(value.Replace(".", ","));
        #endregion methods
    }
}