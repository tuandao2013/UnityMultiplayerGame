using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class TankPlayer : NetworkBehaviour
{
    [Header("References")]
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private SpriteRenderer minimapIconRenderer;
    [field:SerializeField]
    public Health Health { get; private set; }
    [field: SerializeField]
    public CoinWallet Wallet { get; private set; }

    [Header("Settings")]
    [SerializeField]
    private int ownerPriority = 15;
    [SerializeField]
    private Color ownerColor;

    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();

    public static event Action<TankPlayer> OnPlayerSpawned;
    public static event Action<TankPlayer> OnPlayerDespawned;


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            UserData userData = HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);
            PlayerName.Value = userData.userName;

            OnPlayerSpawned?.Invoke(this);
        }

        if (IsOwner)
        {
            virtualCamera.Priority = ownerPriority;

            minimapIconRenderer.color = ownerColor;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            OnPlayerDespawned?.Invoke(this);
        }
    }
}
