using Core.Interface;

namespace Core
{
    public class StepInput : IStepInput
    {
        public Point fromPoint { get; }
        public Point toPoint { get; }

        public StepInput(Point fromPoint, Point toPoint)
        {
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
        }
    }
}
