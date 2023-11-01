using Core;
using Core.Elements;
using Core.Extensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Gameplay.GameElements
{
    public class BoardElementItem : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private int _animationsCounter;
        private IElement _element;

        public string id => _element.id;
        public Point point { get; private set; }
        public bool isGravity { get; private set; }

        public void SetElement(IElement element)
        {
            _element = element;
            SetPoint(element.point);
            SetColorByType(element.type);
        }

        public async UniTask MoveTo(Point toPoint)
        {
            AddAnimationCounter();
            SetPoint(toPoint);
            
            _spriteRenderer.sortingOrder++;
            await transform.DOMove(toPoint.ToVector2(), 0.15f).OnComplete(() => _spriteRenderer.sortingOrder--).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask MoveFrom(Point fromPoint)
        {
            AddAnimationCounter();
            SetPoint(fromPoint);

            _spriteRenderer.sortingOrder--;
            await transform.DOMove(fromPoint.ToVector2(), 0.15f).OnComplete(() => _spriteRenderer.sortingOrder++).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderEmpty()
        {
            AddAnimationCounter();

            await transform.DOScale(0.6f, 0.05f).SetLoops(2, LoopType.Yoyo).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderAppearing(Point toPoint)
        {
            AddAnimationCounter();
            SetPoint(toPoint);
            transform.position = toPoint.ToVector2();
            transform.localScale = Vector3.zero;

            await transform.DOScale(1, Constants.GRAVITY_DURATION_IN_CELL).SetEase(Ease.OutBack).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderDelete()
        {
            AddAnimationCounter();

            await transform.DOScale(0, 0.1f).SetEase(Ease.InBack).ToUniTask();

            RemoveAnimationCounter();
        }

        public async UniTask RenderGravity(Point toPoint)
        {
            AddAnimationCounter();
            var rowsDelta = Mathf.Abs(this.point.rowIndex - toPoint.rowIndex);
            SetPoint(toPoint);
            isGravity = true;

            await transform.DOMove(toPoint.ToVector2(), Constants.GRAVITY_DURATION_IN_CELL * rowsDelta).ToUniTask();

            isGravity = false;
            RemoveAnimationCounter();
        }

        private void AddAnimationCounter()
        {
            _animationsCounter++;
        }

        private void RemoveAnimationCounter()
        {
            _animationsCounter = Mathf.Max(0, _animationsCounter - 1);
        }
        
        private void SetPoint(Point givenPoint)
        {
            point = givenPoint;
        }

        private void SetColorByType(ElementType elementType)
        {
            _spriteRenderer.color = elementType switch
            {
                ElementType.Red => Color.red,
                ElementType.Green => Color.green,
                ElementType.Blue => Color.blue,
                ElementType.Yellow => Color.yellow,
                ElementType.Purple => Color.magenta,
                _ => Color.white
            };
        }
    }
}
