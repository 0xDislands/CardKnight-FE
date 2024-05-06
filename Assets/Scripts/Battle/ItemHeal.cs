using UnityEngine;
using DG.Tweening;

public class ItemHeal : Item
{
    public int healAmount;
    public ParticleSystem healEffect;
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item heal");
        var data = new DamageData();
        data.damage = 1;
        hero.AddHP(data);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(healEffect, hero.transform.position);
        effect.transform.SetParent(hero.transform);
        effect.transform.DOLocalMove(effect.transform.localPosition + new Vector3(0, 150f), 1f);
        var card = GetComponent<Card>();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}