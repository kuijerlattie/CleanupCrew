using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroyable : MonoBehaviour {

    bool tagbased = false;
    bool layerbased = false;

    int hitpoints = 1;

    private List<string> tags = new List<string>();
    private List<string> layers = new List<string>();
    
    void OnCollisionEnter(Collision collision)
    {
        bool taghit = false;
        bool layerhit = false;
        if (tagbased)
        {
            if (tags.Contains(collision.gameObject.tag)) taghit = true;
        }

        if (layerbased)
        {
            if (layers.Contains(LayerMask.LayerToName(collision.gameObject.layer))) layerhit = true;
        }

        if ((!tagbased && !layerbased) ||((tagbased || layerbased) && (taghit || layerhit))) //if not tagbased or layerbased return true; if tagbased or layerbased and taghit or layerhit return true; else return false;
        {
            hitpoints--;
            if (hitpoints <= 0)
            {
                GameObject.Destroy(this);
            }
            else
            {
                EventManager.TriggerEvent("HitDestroyable", this.gameObject, hitpoints);
            }
        }
        else
        {
            EventManager.TriggerEvent("FalseHitDestroyable", this.gameObject, 0);
        }
    }


    public void AddTag(string tag)
    {
        if (!tags.Contains(tag))
        {
            tags.Add(tag);
            tagbased = true;
        }
    }

    public void RemoveTag(string tag)
    {
        if (tags.Contains(tag))
        {
            tags.Remove(tag);
            if (tags.Count == 0) tagbased = false;
        }
    }

    public void AddLayer(string layername)
    {
        if (!layers.Contains(layername))
        {
            layers.Add(layername);
            layerbased = true;
        }
    }

    public void RemoveLayer(string layername)
    {
        if (layers.Contains(layername))
        {
            layers.Remove(layername);
            if (layers.Count == 0) layerbased = false;
        }
    }

    public void SetHitToDestroy(int hits)
    {
        hitpoints = hits;
    }

    void OnDestroy()
    {
        EventManager.TriggerEvent("DestroyedDestroyable", this.gameObject, 0);
    }
}
