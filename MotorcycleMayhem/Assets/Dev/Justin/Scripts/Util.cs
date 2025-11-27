using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class Util : MonoBehaviour
{
    [Serializable] public class TimerEndsEvent : UnityEvent { }

    public List<int> FindObjectsNotInList(int[] testList, int[] referenceList)
    {
        List<int> resultsList = new List<int>();
        foreach (int test in testList)
        {
            if (!resultsList.Contains(test))
            {
                resultsList.Add(test);
            }
        }
        foreach (int test in testList)
        {
            foreach (int reference in referenceList)
            {
                if (reference == test)
                {
                    resultsList.Remove(reference);
                }
            }
        }
        return resultsList;
    }

    public List<int> FindObjectsNotInList(List<int> testList, int[] referenceList)
    {
        List<int> resultsList = new List<int>();
        foreach (int test in testList)
        {
            if (!resultsList.Contains(test))
            {
                resultsList.Add(test);
            }
        }
        foreach (int test in testList)
        {
            foreach (int reference in referenceList)
            {
                if (reference == test)
                {
                    resultsList.Remove(reference);
                }
            }
        }
        return resultsList;
    }

    public List<int> FindObjectsNotInList(int[] testList, List<int> referenceList)
    {
        List<int> resultsList = new List<int>();
        foreach (int test in testList)
        {
            if (!resultsList.Contains(test))
            {
                resultsList.Add(test);
            }
        }
        foreach (int test in testList)
        {
            foreach (int reference in referenceList)
            {
                if (reference == test)
                {
                    resultsList.Remove(reference);
                }
            }
        }
        return resultsList;
    }

    public List<float> SortListOfNumbers(List<float> list)
    {
        List<float> sortedList = new List<float>();
        float currentLeast = Mathf.Infinity;
        int i = 0;

        while (list.Count > 0)
        {
            if (list[i] < currentLeast)
            {
                currentLeast = list[i];
            }
            i++;
            if (i >= list.Count)
            {
                i = 0;
                sortedList.Add(currentLeast);
                list.Remove(currentLeast);
                currentLeast = Mathf.Infinity;
            }
        }
        return sortedList;
    }

    public List<int> SortListOfNumbers(List<int> list)
    {
        List<int> sortedList = new List<int>();
        int currentLeast = list[0];
        int i = 0;

        while (list.Count > 0)
        {
            if (list[i] < currentLeast)
            {
                currentLeast = list[i];
            }
            i++;
            if (i >= list.Count)
            {
                i = 0;
                sortedList.Add(currentLeast);
                list.Remove(currentLeast);
                currentLeast = list[0];
            }
        }
        return sortedList;
    }
}
