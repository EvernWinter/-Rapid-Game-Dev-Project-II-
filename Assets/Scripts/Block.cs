using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Ordinary,
    Copper,
    Silver,
    Gold,
    LapisLazuli,
    Diamond
};

public class Block : MonoBehaviour
{
    [SerializeField] public BlockType blockType;
    [SerializeField] public bool isBlockInteractable;
}
