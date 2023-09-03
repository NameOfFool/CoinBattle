using UnityEngine;

public interface IDagameable
{
    public float MaxHP { get; set; }
    public float CurrentHP { get; set; }

    public void OnHit(float damage);

}

