using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReductorAnimationScript : MonoBehaviour
{
    public float animationSpeed = 1f;
    public UnityEvent extraEventsWhenOpen;
    public UnityEvent extraEventsWhenClosed;
    private bool isOpening;
    private Animator animator;
    private Coroutine coroutine;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartAction()
    {
        isOpening = !isOpening;
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ReductorAnimation());
    }
    IEnumerator ReductorAnimation()
    {
        if (isOpening)
        {
            while (animator.GetFloat("Opening") < 1f)
            {
                animator.SetFloat("Opening", animator.GetFloat("Opening") + Time.deltaTime * animationSpeed);
                yield return new WaitForEndOfFrame();
            }
            extraEventsWhenOpen?.Invoke();
        }
        else 
        {
            extraEventsWhenClosed?.Invoke();
            while (animator.GetFloat("Opening") > 0f)
            {
                animator.SetFloat("Opening", animator.GetFloat("Opening") - Time.deltaTime * animationSpeed);
                yield return new WaitForEndOfFrame();
            }
        } 
    }
}
