using DG.Tweening;
using UnityEngine;

public class TweenHolder : MonoBehaviour
{
    public Tween MoveTween;
}

public static class TweenHolderExtensions
{
    public static TweenHolder WithTweenHolder<T>(this T obj) where T : Component
    {
        if (!obj.TryGetComponent<TweenHolder>(out var holder))
        {
            // this addComponent will only be run at most once per gameObject per lifetime
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            holder = obj.gameObject.AddComponent<TweenHolder>();
        }

        return holder;
    }

    public static TweenHolder ClearTween(this TweenHolder holder, bool doComplete)
    {
        if (holder.MoveTween != null && holder.MoveTween.IsActive()) holder.MoveTween.Kill(doComplete);

        return holder;
    }

    public static Tween HoldTween(this TweenHolder holder, Tween tween, bool completeLastTween)
    {
        if (holder.MoveTween != null && holder.MoveTween.IsActive()) holder.ClearTween(completeLastTween);
        holder.MoveTween = tween;

        return tween;
    }
}