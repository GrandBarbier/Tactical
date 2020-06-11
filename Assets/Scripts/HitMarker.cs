using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    public AnimationClip anim;
    
    void Update()
    {
        StartCoroutine(Boum(anim.length));
    }


    IEnumerator Boum(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
