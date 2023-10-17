using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinePool : MonoBehaviour
{
    private static CoroutinePool instance;
    public static CoroutinePool Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = GameObject.Find("CoroutinePool");
                if (obj == null)
                {
                    instance = new GameObject("CoroutinePool").AddComponent<CoroutinePool>();
                }
                else
                {
                    instance = obj.GetComponent<CoroutinePool>();
                }
            }
            return instance;
        }
    }
    private Dictionary<object, List<Coroutine>> CoroutineDic;
    private CoroutinePool()
    {
        CoroutineDic = new Dictionary<object, List<Coroutine>>();
    }
    public Coroutine StartCoroutine(IEnumerator routine, object obj)
    {
        Coroutine coroutine = base.StartCoroutine(routine);
        if (CoroutineDic.ContainsKey(obj))
        {
            CoroutineDic[obj].Add(coroutine);
        }
        else
        {
            CoroutineDic.Add(obj, new List<Coroutine>());
            CoroutineDic[obj].Add(coroutine);
        }
        return coroutine;
    }
    public void StopAllCoroutineInObject(object obj)
    {
        if (!CoroutineDic.ContainsKey(obj))
        {
            return;
        }
        foreach (Coroutine coroutine in CoroutineDic[obj])
        {
            StopCoroutine(coroutine);
        }
        CoroutineDic[obj].Clear();
    }
    public void StartAnimatorCallback(Animator anim,string stateName,Action action)
    {
        StartCoroutine(AnimatorCallback(anim, stateName, action));
    }
    private IEnumerator AnimatorCallback(Animator anim,string stateName,Action callback)
    {
        while(true)
        {
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if(!info.IsName(stateName)||!anim.enabled||!anim.gameObject.activeSelf)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(info.length);
                callback.Invoke();
                yield break;
            }
        }
    }
}
