using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimateSelectCharacterCanvas : MonoBehaviour
{
    [SerializeField] RectTransform topSubmenu, downSubmenu;
    [SerializeField] private float oneMoveTime = 0.2f;
    private float xMove = 2000f;

    public void AnimateTopSubmenu(bool next)
    {
        int direction;
        if (next) direction = 1;
        else direction = -1;

        topSubmenu.DOLocalMoveX(-xMove * direction, oneMoveTime).SetEase(Ease.OutSine).OnComplete(() =>
        {
            topSubmenu.DOLocalMoveX(xMove * direction, 0);
            topSubmenu.DOLocalMoveX(0, oneMoveTime).SetEase(Ease.InSine);
        });
    }
}
