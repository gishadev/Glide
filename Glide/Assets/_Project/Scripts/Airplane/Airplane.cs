﻿using Gisha.Glide.Game;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    public class Airplane : MonoBehaviour
    {
        [SerializeField] private float fullWasteOfEnergyInSeconds = default;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp01(value);
        }
        float _energy = 1f;

        public bool IsCharged => Energy > 0;

        private void Update()
        {
            if (IsCharged)
                Energy -= Time.deltaTime / (fullWasteOfEnergyInSeconds + 0.001f);
            else
                Die();
        }

        public void Die()
        {
            Debug.Log("<color=purple>Airplane was destroyed!</color>");
            GameManager.ReloadScene();
        }

        public void ChargeUp() => Energy = 1f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle"))
                Die();
        }
    }
}