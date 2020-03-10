using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sentence
{
    public string sentence;
    public bool isFinal;
    /** This float will dictate the amount that the stress bar will be effected.
     * Range between -1.0 and 1.0. Will automatically be rounded to be within
     * the min and max values when retrieved using the get method.
     */
    public float effectOnStress;
    private float minStressRange = -1.0f, maxStressRange = 1.0f;

    /**
     * Getter for the effectOnStress float. This method will automatically
     * round the amount needed to be within the correct range of -1.0 to 1.0.
     */
    public float getEffectOnStress()
    {
        return Mathf.Clamp(effectOnStress, minStressRange, maxStressRange);
    }
}
