using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public delegate void EventGame();

    public enum TypeCharacter
    {
        Player,
        Monster,
    }

    public enum NameCharacter
    {

    }

    public enum TypeEffect
    {
        posion
    }
    public static class Common
    {
        public static float InitVel(float distance, float angle = 45, float time = 3)
        {
            return distance / (Mathf.Cos(angle) * time);
        }

        public static void ClampPosition(this Transform t, float minX, float maxX, float minY, float maxY)
        {
            Vector3 position = t.position;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            t.position = position;
        }
    }
}
