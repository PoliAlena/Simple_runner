using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    public GameObject TownPrefab;
    public List<GameObject> _town = new List<GameObject>();
    public float _maxSpeed = 10f;
    private float _speed = 1f;
    public int _maxCountTowns = 5;

    void Start()
    {
        ResetLevel();
        //StartL();
    }

    void Update()
    {
        if (_speed == 0)  return; 

        foreach (GameObject obj in _town)
        { 
          obj.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }

        if (_town[0].transform.position.z < -15)
        {
            Destroy(_town[0]);
            _town.RemoveAt(0);

            NextTown();
        }
    }

    public void StartL()
    {
        _speed = _maxSpeed;
        SwipeManager.instance.enabled= true;
    }

    private void NextTown()
    {
        Vector3 _pos = Vector3.zero;
        if(_town.Count > 0 ) 
        {
          _pos = _town[_town.Count - 1].transform.position + new Vector3(0,0,15);
        }

        GameObject next = Instantiate(TownPrefab,_pos, Quaternion.identity);
        next.transform.SetParent(transform);
        _town.Add(next);
    }

    public void ResetLevel()
    {
        _speed= 0;
        while (_town.Count > 0 ) 
        {
            Destroy(_town[0]);
            _town.RemoveAt(0);
        }

        for (int i = 0; i < _maxCountTowns; i++)
        {
            NextTown();
        }

        SwipeManager.instance.enabled = false;
    }
}
