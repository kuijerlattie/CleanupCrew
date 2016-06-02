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
        bool ishit = true;
        if (tagbased)
        {
            ishit = false;
            foreach (string tag in tags)
            {
                if (collision.gameObject.tag == tag)
                {
                    ishit = true;
                }
            }
        }

        if (layerbased)
        {
            //check if other had a layer
        }

        if (ishit)
        {
            hitpoints--;
            if (hitpoints <= 0)
            {
                GameObject.Destroy(this);
            }
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
