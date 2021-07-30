using System;
using UnityEngine;

namespace Script.Character
{
    [RequireComponent(typeof(CharacterAnimator))]

    public class CharacterRenderer : MonoBehaviour
    {
        private CharacterAnimator _characterAnimator;

        private void Awake()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
        }
    }
}