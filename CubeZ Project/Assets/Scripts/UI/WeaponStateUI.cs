using System;
using System.Collections;
using UnityEngine;

    public class WeaponStateUI : MonoBehaviour
    {
    [Header("Ячейка предмета Main Canvas")]
    [SerializeField] private ItemCellMainCanvas cellWeapon;
        // Use this for initialization
        void Start()
        {
            if (PlayerManager.Manager == null)
            {
                throw new WeaponStateUIException("Player manager is null");
            }

            if (PlayerManager.Manager.Player == null)
            {
                throw new WeaponStateUIException("Player not found");
            }
        if (cellWeapon == null)
        {
            throw new WeaponStateUIException("cell weapon not seted");
        }
        SetStateVisibleWeapon(false);
            PlayerManager.Manager.Player.onWeaponChanged += UpdateWeaponInfo;
        }

        private void UpdateWeaponInfo(ItemBaseData data)
        {
        if (data == null)
        {
            SetStateVisibleWeapon(false);

        }

        else
        {
            SetStateVisibleWeapon(true);
            cellWeapon.SetData(data);
        }
        }


    private void SetStateVisibleWeapon (bool state) {

        cellWeapon.gameObject.SetActive(state);
    }
    }