using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {


    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemocved;


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    private float spawnPleteTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update() {
        spawnPleteTimer += Time.deltaTime;
        if (spawnPleteTimer > spawnPlateTimerMax) {
            spawnPleteTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is empty handed
            if (platesSpawnedAmount > 0) {
                // There's at least one plate hare 
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemocved?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}


