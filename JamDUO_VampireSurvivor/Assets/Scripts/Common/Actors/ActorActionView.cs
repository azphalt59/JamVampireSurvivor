using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityCommon.Graphics.Actors
{
    /// <summary>
    /// View d'une actor action
    /// </summary>
    [Serializable]
    public class ActorActionView
    {
        public string Name;
        //public SpriteAnimationView Animation;
        public string Sound;
        //public float AnimationSpeed;
        public string NextAction;


        [HideInInspector]
        public float CurrentTimer;
    }

}