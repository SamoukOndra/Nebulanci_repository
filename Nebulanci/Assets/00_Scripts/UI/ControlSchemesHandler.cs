using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSchemesHandler : MonoBehaviour
{
    private Dictionary<int, int> pairs = new();

    //on dropdown value change
    public void UpdatePair(int player, int controls)
    {
        if(pairs.TryGetValue(player, out int _value))
        {
            pairs.Remove(player);
        }

        pairs.Add(player, controls);
    }

    //blokne dropdown.values
    public List<int> ControlsTaken(int player)
    {
        List<int> takenControls = new();
        Dictionary<int, int> _pairs = pairs;
        _pairs.Remove(player);
        
        Dictionary<int, int>.ValueCollection schemes = _pairs.Values;
        foreach(int scheme in schemes)
        {
            takenControls.Add(scheme);
        }

        return takenControls;
    }
}
