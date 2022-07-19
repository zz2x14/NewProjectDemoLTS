using System.Collections;

public interface ILimitAction
{
    void StartLimitActionCor(float duration);
    IEnumerator LimitActionCor(float duration);
}