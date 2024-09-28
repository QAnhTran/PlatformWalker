using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (GameManager.Instance.isMultiplayer)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, randomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(PlayerPrefab, randomPosition, Quaternion.identity);
        }
    }


}
