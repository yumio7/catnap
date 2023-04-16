using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManeuverManager : MonoBehaviour
{
    [SerializeField, Tooltip("How many seconds does each maneuver last?")] private float changeAttack;
    
    private Component[] _maneuvers;
    private bool _waiting;
    
    void Start()
    {
        // grab all maneuvers in parent
        _maneuvers = gameObject.GetComponentsInParent(typeof(Maneuver));

        print(_maneuvers.Length);
        
        //StartCoroutine(ChangeManeuver());
    }

    private void Update()
    {
        // don't run the coroutine if the last instance is still waiting
        if (_waiting) return;
        
        // start a coroutine that selects a random maneuver from the list of maneuvers every changeAttack seconds
        StartCoroutine(ChangeManeuver());
    }

    IEnumerator ChangeManeuver()
    {
        _waiting = true;
        int index = Random.Range(0, _maneuvers.Length); 
        ((Maneuver)_maneuvers[index]).Activate();
        yield return new WaitForSecondsRealtime(changeAttack);
        _waiting = false;
    }
}

