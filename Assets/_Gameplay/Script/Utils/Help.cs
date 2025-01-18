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

    public enum GameState
    {
        InGame,
        SelectCard,
        EndGame,
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
    }
}
