using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    [SerializeField]
    private Transform turretTransforrm;
    [SerializeField]
    private InputReader inputReader;

    private void LateUpdate() // update everyframe but happens after other updates
    {
        if (!IsOwner) return;
        Vector2 aimScreenPosition = inputReader.AimPosition;
        Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScreenPosition);

        turretTransforrm.up = new Vector2(
            aimWorldPosition.x - turretTransforrm.position.x,
            aimWorldPosition.y - turretTransforrm.position.y
        );
    }
}
