using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Assets
{
    public class Rotate: MonoBehaviour
    {
        public float xSelfRotata = 0f;
        public float ySelfRotata = 0f;
        public float zSelfRotata = 0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(xSelfRotata * Time.deltaTime, ySelfRotata * Time.deltaTime, zSelfRotata * Time.deltaTime);
        }
    }
}