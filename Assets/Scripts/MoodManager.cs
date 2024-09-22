using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    [Header("Animation States")]
    [SerializeField] private AnimationClip healthy;
    [SerializeField] private AnimationClip angry;
    //[SerializeField] private AnimationClip weak;
    private Animator anim;
    private string currentAnim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void setMood(string moodName)
    {
        switch (moodName) {
            case "healthy":
                changeAnim(healthy.name);
                break;
            case "angry":
                changeAnim(angry.name);
                break;
            default:break;
        }
    }

    private void changeAnim(string state)
    {
        if (currentAnim == state)
        {
            return;
        }
        else
        {
            anim.Play(state);
            currentAnim = state;
        }
    }
}
