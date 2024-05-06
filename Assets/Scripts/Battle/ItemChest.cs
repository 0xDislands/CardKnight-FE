using UnityEngine;
using Dislands;
using System.Collections.Generic;

public class ItemChest : Item
{
    public ParticleSystem appearEffect;
    public List<CardId> transformCards = new List<CardId>() { CardId.ItemHeal, CardId.ItemPoison, CardId.ItemShield };
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item heal");
        var data = new DamageData();
        data.damage = 1;
        hero.AddHP(data);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(appearEffect, transform.position);
        var card = GetComponent<Card>();
        var newCard = CardManager.Instance.SpawnCard(card.Pos, transformCards.RandomElement());
        newCard.MoveToPos(card.Pos);
        card.Disappear();
    }
}