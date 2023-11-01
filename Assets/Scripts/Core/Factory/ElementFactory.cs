using System;
using System.Collections.Generic;
using Core.Elements;
using Core.Utilities;
using Game.Gameplay.GameElements;
using BoardElement = Core.Elements.BoardElement;

namespace Core.Factory
{
    public class ElementFactory
    {
        private IElement CreateRedElement(Point point)
        {
            return new BoardElement(IdGenerator.Get(), point, ElementType.Red);
        }

        private IElement CreateGreenElement(Point point)
        {
            return new BoardElement(IdGenerator.Get(), point, ElementType.Green);
        }

        private IElement CreateYellowElement(Point point)
        {
            return new BoardElement(IdGenerator.Get(), point, ElementType.Yellow);
        }

        private IElement CreateBlueElement(Point point)
        {
            return new BoardElement(IdGenerator.Get(), point, ElementType.Blue);
        }

        private IElement CreatePurpleElement(Point point)
        {
            return new BoardElement(IdGenerator.Get(), point, ElementType.Purple);
        }
        
        public List<Func<Point, IElement>> GetCreateElementMethods()
        {
            return new List<Func<Point, IElement>>
            {
                CreateRedElement,
                CreateGreenElement,
                CreateYellowElement,
                CreateBlueElement,
                CreatePurpleElement
            };
        }
    }
}