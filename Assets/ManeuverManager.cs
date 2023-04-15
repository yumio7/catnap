using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuverManager : MonoBehaviour
{
    [SerializeField, Tooltip("How many seconds does each maneuver last?")] private float changeAttack;
    
    private Component[] _maneuvers;
    
    void Start()
    {
        // grab all maneuvers in parent
        _maneuvers = gameObject.GetComponentsInParent(typeof(Maneuver));
        
        // start a coroutine that selects a random maneuver from the list of maneuvers every changeAttack seconds
        StartCoroutine(ChangeManeuver());
    }

    IEnumerator ChangeManeuver()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeAttack);
            int index = Random.Range(0, _maneuvers.Length);
            ((Maneuver)_maneuvers[index]).Activate();
        }
    }
}

