using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeniorProject
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            //Singleton pattern
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Game logic here
        }
    }
}

