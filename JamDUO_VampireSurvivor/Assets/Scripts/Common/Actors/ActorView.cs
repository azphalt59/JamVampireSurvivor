using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCommon.Graphics.Actors
{
    public class ActorView : MonoBehaviour
    {
        public GameObject ParentActor;

        public List<ActorActionView> Actions = new List<ActorActionView>();
        public List<ActorStateView> States = new List<ActorStateView>();

        public SpritesAnimationDatas AnimationsDatas;

        public int CurrentStateID
        {
            get { return CurrentState.ID; }
        }


        [HideInInspector]
        public float StateTimer;

        public ActorActionView CurrentAction
        {
            get
            {
                return m_currentAction;
            }
        }

        [HideInInspector]
        public ActorStateView CurrentState
        {
            get
            {
                return m_currentState;
            }
        }


        public bool HasState(string name)
        {
            foreach (var item in States)
            {
                if (item.Name == name)
                    return true;
            }

            return false;
        }

        private void Awake()
        {
            int index = 0;
            foreach (var state in States)
            {
                state.ID = index;
                index++;
            }

            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            SetState(0);

            m_currentFrame = UnityEngine.Random.Range(0, GetAnimation(m_currentState.Name).Sprites.Count);
            m_spriteRenderer.sprite = GetAnimation(m_currentState.Name).Sprites[m_currentFrame];
        }

        public void Update()
        {
            StateTimer += Time.deltaTime;

            if (m_currentAction != null)
            {
                m_currentAction.CurrentTimer += Time.deltaTime;

                SpritesAnimationDatas.AnimationDatas animation = GetAnimation(m_currentAction.Name);
                if (animation != null)
                {
                    if (m_currentAction.CurrentTimer >= (1.0f / animation.FPS) * (float)animation.Sprites.Count)
                    {
                        string nextAction = m_currentAction.NextAction;

                        m_currentAction.CurrentTimer = 0;
                        m_currentAction = null;

                        EndAction();

                        if (string.IsNullOrEmpty(nextAction) == false)
                            PlayAction(nextAction);
                    }
                }
                else
                {

                }
            }

            UpdateView();

        }


        SpritesAnimationDatas.AnimationDatas GetAnimation(string name )
        {
            return AnimationsDatas.GetAnimation(name);
        }

        void UpdateView()
        {
            if (m_currentAction != null)
            {

                int frame = (int)(m_currentAction.CurrentTimer * GetAnimation(m_currentAction.Name).FPS);

                if (frame >= GetAnimation(m_currentAction.Name).Sprites.Count)
                    frame = GetAnimation(m_currentAction.Name).Sprites.Count - 1;

                if (frame != m_currentFrame)
                {
                    m_currentFrame = frame;
                    m_spriteRenderer.sprite = GetAnimation(m_currentAction.Name).Sprites[m_currentFrame];
                }

            }
            else if (m_currentState != null)
            {
                int frame = (int)(((StateTimer * GetAnimation(m_currentState.Name).FPS)) % GetAnimation(m_currentState.Name).Sprites.Count);

                if (frame != m_currentFrame)
                {
                    m_currentFrame = frame;

                    if (GetAnimation(m_currentState.Name).Sprites.Count > m_currentFrame && m_currentFrame >= 0)
                    {
                        m_spriteRenderer.sprite = GetAnimation(m_currentState.Name).Sprites[m_currentFrame];
                    }
                }
            }
        }

        void EndAction()
        {
            m_currentAction = null;
        }



        public bool HasAction()
        {
            return m_currentAction != null;
        }

        public void SetState(int stateID)
        {
            if (m_currentState != null && m_currentState.ID == stateID)
                return;

            m_currentFrame = -1;
            StateTimer = 0;

            m_currentState = States[stateID];

        }

        public void SetState(string name)
        {
            ActorStateView state = GetState(name);

            if (state == null)
            {
                Debug.LogError("can't find state " + name);
            }
            else
            {
                SetState(state.ID);
            }

        }

        ActorStateView GetState(string name)
        {
            foreach (var item in States)
            {
                if (item.Name == name)
                    return item;
            }

            return null;
        }



        public void PlayAction(string actionName)
        {
            ActorActionView action = GetAction(actionName);
            if (action == null)
            {
                Debug.LogError("can't find action " + actionName);
                return;
            }

            SpritesAnimationDatas.AnimationDatas animation = GetAnimation(action.Name);

            if ( animation == null )
            {
                Debug.LogError("can't find animation " + actionName);
                return;
            }

            m_currentAction = action;
            m_currentAction.CurrentTimer = 0;
        }



        ActorActionView GetAction(int index)
        {
            if (index < 0 || index >= Actions.Count)
                return null;

            return Actions[index];
        }

        ActorActionView GetAction(string actionName)
        {
            foreach (var action in Actions)
            {
                if (action.Name == actionName)
                    return action;
            }
            return null;
        }

        public void SetParent(GameObject parent)
        {
            ParentActor = parent;
        }



        int m_currentFrame;
        SpriteRenderer m_spriteRenderer;
        ActorActionView m_currentAction;
        ActorStateView m_currentState;
    }
}