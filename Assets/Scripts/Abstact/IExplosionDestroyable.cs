using System;

public interface IExplosionDestroyable
{
    event Action<IExplosionDestroyable> OnBlowUp;
    void BlowUp();
}

