using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class FollowCam : MonoBehaviour
    {
        private Camera m_cam;

        // Start is called before the first frame update
        void Start()
        {
            m_cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(
                m_cam.transform.position.x,
                m_cam.transform.position.y,
                transform.position.z
                );
        }
    }
}
