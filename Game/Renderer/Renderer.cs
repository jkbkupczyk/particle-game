using System.Windows.Controls;

using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Renderer
{
    public static class Renderer
    {
        private static readonly GameCanvas GameCanvas;

        public static double Width { get; set; }
        public static double Height { get; set; }

        static Renderer()
        {
            GameCanvas = new GameCanvas();
        }

        public static Canvas GetCanvas() => GameCanvas.GetCanvas();

        public static void InitElement(RenderableObject renderable) => GameCanvas.InitElement(renderable);

        public static void RenderElement(RenderableObject renderable) => GameCanvas.RenderElement(renderable);

        public static void DestroyElement(RenderableObject renderable) => GameCanvas.DestroyElement(renderable);

        public static void RenderFrame() => GameCanvas.RenderFrame();

        public static void Clear() => GameCanvas.Clear();

        public static void Resize(double newWidth, double newHeight) {
            Width = newWidth;
            Height = newHeight;
            GameCanvas.Resize(newWidth, newHeight);
        }

    }
}
