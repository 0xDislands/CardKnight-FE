using UnityEngine;
using Dislands;
using System.Collections.Generic;

public class ItemChestMiniGame : Item
{
    public ParticleSystem appearEffect;
    public List<CardId> transformCards = new List<CardId>() { CardId.ItemHeal, CardId.ItemPoison, CardId.ItemShield };
    public override void ApplyEffect(Hero hero)
    {
        var card = GetComponent<Card>();
        Gameplay.Instance.popupUnlockMiniGame.ShowMiniGame(card, transformCards.RandomElement());
    }
}