using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Stopwatch : MonoBehaviour
{
    private int seconds = 0;
    private void Start()
    {
        StartCoroutine(Watch());
    }

    private IEnumerator Watch()
    {
        while (true)
        {
            print(seconds);
            yield return new WaitForSeconds(1);
            seconds++;
        }
    }
}
