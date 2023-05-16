using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityCommon.Graphics.Actors
{
    /// <summary>
    /// View d'un state d'actor
    /// </summary>
    [Serializable]
    public class ActorStateView
    {
        public string Name;
        //public SpriteAnimationView Animation;
       // public float AnimationSpeed = 20;

        [HideInInspector]
        public int ID;

    }

}