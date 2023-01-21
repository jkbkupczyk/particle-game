using System.Collections.Generic;

using System.Windows.Media;

using ParticleGame.Game.Controls;
using ParticleGame.Game.State;
using ParticleGame.Util;
using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Screen.Game
{
    public class DebugScreen : IScreen
    {
        private static readonly Color BACKGROUND_COLOR = ColorUtils.FromHex(0x70_767676);

        private readonly TextRenderable FpsText;
        private readonly TextRenderable TpsText;
        private readonly TextRenderable AllocationRateText;
        private readonly TextRenderable CurrentCpuNameText;
        private readonly TextRenderable CpuUsageText;
        private readonly TextRenderable RamUsedText;

        private float CpuUsage;
        private float UsedRam;
        private float AvailableRam;
        private float UsedRamPercentage;

        private readonly List<TextRenderable> AllDiags;

        private bool DebugVisible;

        public DebugScreen()
        {
            this.DebugVisible = false;

            this.FpsText = new TextRenderable("Fps: 0", null, 16, BACKGROUND_COLOR);
            this.TpsText = new TextRenderable("Tps: 0", null, 16, BACKGROUND_COLOR);
            this.AllocationRateText = new TextRenderable("Allocation rate: 0 Mb/s", null, 16, BACKGROUND_COLOR);
            this.CurrentCpuNameText = new TextRenderable("CPU Name: ", null, 16, BACKGROUND_COLOR);
            this.CpuUsageText = new TextRenderable("CPU Usage: 0%", null, 16, BACKGROUND_COLOR);
            this.RamUsedText = new TextRenderable("RAM used: 0/0 Mb, 0%", null, 16, BACKGROUND_COLOR);

            this.AllDiags = new List<TextRenderable>() {
                this.FpsText, this.TpsText, this.AllocationRateText, 
                this.CurrentCpuNameText, this.CpuUsageText, this.RamUsedText
            };
        }

        public void ToggleDebugScreen()
        {
            this.DebugVisible = !this.DebugVisible;
        }

        public void Init()
        {
            foreach (var diag in this.AllDiags)
            {
                diag.SetZIndex(int.MaxValue);
                diag.SetVisible(this.DebugVisible);
                diag.Init();
            }
        }

        
        public void Tick()
        {
            if (GlobalState.CurrentStateType == StateType.IN_GAME && KeyboardHandler.DEBUG_TOGGLE.PressedTick())
            {
                this.ToggleDebugScreen();
            }
        }

        public void Update()
        {
            HardwareDiag.GetCpuUsage().ContinueWith((v) => this.CpuUsage = v.Result);
            HardwareDiag.GetUsedRam().ContinueWith((v) => this.UsedRam = v.Result);
            HardwareDiag.GetAvailableRam().ContinueWith((v) => this.AvailableRam = v.Result);
            HardwareDiag.GetUsedRamPercentage().ContinueWith((v) => this.UsedRamPercentage = v.Result);
        }

        public void Render()
        {
            for (int i = 0; i < this.AllDiags.Count; i++)
            {
                this.AllDiags[i].SetPosition(0, i * this.AllDiags[i].Height);
            }

            this.FpsText.SetText($"Fps: {GlobalState.Frames}");
            this.TpsText.SetText($"Tps: {GlobalState.Ticks}");
            this.AllocationRateText.SetText($"Allocation rate: {string.Format("{0:0.0000}", HardwareDiag.GetAllocationRatePerSecondMb())} Mb/s");
            this.CurrentCpuNameText.SetText($"CPU Name: {HardwareDiag.GetCurrentProcessorName()}");
            this.CpuUsageText.SetText($"CPU Usage: {string.Format("{0:0.00}", this.CpuUsage)}%");
            this.RamUsedText.SetText($"RAM used: {this.UsedRam / 1000000}/{this.AvailableRam} Mb, {this.UsedRamPercentage}%");

            this.AllDiags.ForEach(diag => diag.SetVisible(this.DebugVisible));
            this.AllDiags.ForEach(diag => diag.Render());
        }

        public void Destroy()
        {
            this.AllDiags.ForEach(diag => diag.Destroy());
        }

    }
}
