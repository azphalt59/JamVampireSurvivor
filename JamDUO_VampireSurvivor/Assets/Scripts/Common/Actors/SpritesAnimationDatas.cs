using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCommon.Graphics
{
    [CreateAssetMenu]
    public class SpritesAnimationDatas : ScriptableObject
    {
        public int EntityIndex;
        public List<AnimationDatas> Animations;

        [Serializable]
        public class AnimationDatas
        {
            public string Name;
            public int FPS;
            public List<Sprite> Sprites;
        }

        public AnimationDatas GetAnimation( string name )
        {
            if ( m_animations == null )
            {
                 m_animations = new Dictionary<string, AnimationDatas>();

                foreach (var item in Animations)
                {
                    m_animations.Add(item.Name, item);
                }
            }

            AnimationDatas animation;

            if ( m_animations.TryGetValue(name , out animation) == false )
            {
                Debug.LogError("Can't find animation " + name + " in " + this.name);

                return null;
            }

            return animation;
        }

        Dictionary<string, AnimationDatas> m_animations;
    }

}