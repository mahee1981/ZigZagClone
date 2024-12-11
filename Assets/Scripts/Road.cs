using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Road : MonoBehaviour
{
    private Vector3 _offset;
    public GameObject roadPrefab;
    private float _blockSize = 1.0f;
    private Vector3 _lastBlockPosition;
    public Transform lastBlock;
    private Queue<GameObject> _roadParts = new Queue<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        _lastBlockPosition = lastBlock.position;
        // _blockSize = Vector3.Distance(_lastBlockPosition, gameObject.transform.GetChild(9).position);
        _offset = Vector3.forward * _blockSize;
    }

    public void GenerateRoad()
    {
        var chance = Random.Range(0f, 100f);
        Vector3 spawnLocation;
        if (chance < 50f)
        {
            spawnLocation = _lastBlockPosition + Quaternion.AngleAxis(45, Vector3.up) * _offset;
        }
        else
        {
            spawnLocation = _lastBlockPosition + Quaternion.AngleAxis(-45, Vector3.up) * _offset;
        }
        
        var clone = Instantiate(roadPrefab, spawnLocation, Quaternion.Euler(0.0f, 45.0f, 0.0f));
        if(chance > 30.0f && chance < 70.0f)
            clone.transform.GetChild(0).gameObject.SetActive(true);
        
        
        _lastBlockPosition = clone.transform.position;
        
        _roadParts.Enqueue(clone);
        
        if (_roadParts.Count > 25)
        {
            DestroyOldRoadParts();
        }
    }

    private void DestroyOldRoadParts()
    {
        var oldBlock = _roadParts.Dequeue();
        Destroy(oldBlock);
    }
}
