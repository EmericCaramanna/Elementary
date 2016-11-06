using UnityEngine;
using System.Collections;
using System;

// work with a switch
public class ActivableScenes : MonoBehaviour, IActivateObject
{
    [SerializeField]
    AnimationClip animationClip;
    [SerializeField]
    float playAnimationInXSeconds = 0f;

    AnimationState state;
    bool play = false;
    bool first = true;

    float time;

    Animation anim;

    void Start ()
    {
        anim = GetComponent<Animation>();
        animationClip.wrapMode = WrapMode.PingPong;
        foreach (AnimationState s in anim)
        {
            if (s.name.Equals(animationClip.name))
            {
                state = s;
            }
        }
        if (playAnimationInXSeconds > 0f)
        {
            state.speed = state.length / playAnimationInXSeconds;
        }
	}

    void Update ()
    {
        if (anim.isPlaying)
        {
            time += Time.deltaTime;
            if (time >= state.length / state.speed)
                anim.Stop();
        }

        if (play)
        {
            if (!anim.isPlaying)
            {
                if (!first)
                {
                    state.time = state.length;
                }
                first = !first;
                anim.Play(animationClip.name);

                time = 0f;
            }
            else
            {
                first = false;
                while (state.time >= state.length)
                {
                    first = !first;
                    state.time -= state.length;
                }
                state.time = state.length - state.time;
                time = state.time;
                if (!first)
                    state.time += state.length;
                anim.Play(animationClip.name);
            }
            play = false;
        }
	}

    public void Activate()
    {
        play = true;
    }

    public void Desactivate()
    {
        play = true;
    }
}
