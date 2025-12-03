using UnityEngine;
using System.Collections.Generic;
public class DeathBarrier : MonoBehaviour
{
    [SerializeField] private GameObject[] YoureGonnaDie;



    private List<GameObject> YouDied = new List<GameObject>();

    private void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Player"))
        {
            GameObject playerObj = other.gameObject;

           
            PlayerDeathSettings deathSettings = playerObj.GetComponent<PlayerDeathSettings>();

            if (deathSettings != null)
            {
                deathSettings.OnDeath();
            }

         
            YouDied.Add(playerObj);

            playerObj.transform.position += new Vector3(0, -50, 0);
            playerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            Debug.Log($"Player {playerObj.name} disabled and added to array. Total disabled: {YouDied.Count}");
        }
    }


    public void EnableAllPlayers()
    {
        foreach (GameObject player in YouDied)
        {
            if (player != null)
            {
                player.SetActive(true);
            }
        }
        YouDied.Clear();
    }

    public List<GameObject> GetDisabledPlayers()
    {
        return YouDied;
    }
}

