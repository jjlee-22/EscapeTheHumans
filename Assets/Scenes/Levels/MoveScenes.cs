using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenes : MonoBehaviour
{
    [SerializeField] private string newLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ObjectiveManager om;
            try
            {
                om = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
                if (om.levelComplete)
                {
                    SceneManager.LoadScene(newLevel);
                    SoundManagerScript.PlaySound("door");
                }
            }
            catch
            {
                // To prevent scenes with no objectives from being able to continue
                SceneManager.LoadScene(newLevel);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
