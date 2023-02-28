using System;
using UnityEngine;

namespace Code.Logic.LevelTransition
{
    public class LevelRewardIcon : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
