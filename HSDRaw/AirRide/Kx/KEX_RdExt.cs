using HSDRaw.AirRide.Rd;
using HSDRaw.Common;
using System.ComponentModel;
using System.Drawing;

namespace HSDRaw.AirRide.Kx
{
    public class KEX_RdExt : KAR_RdData
    {
    }

    public class KEX_KirbyColor : HSDAccessor
    {
        public override int TrimmedSize => 0x10;

        [DisplayName("Main Color"), Description("")]
        public Color MainColor { get => _s.GetColorRGBA(0x0); set => _s.SetColorRGBA(0x0, value); }

        [DisplayName("Dark Color"), Description("")]
        public Color DarkColor { get => _s.GetColorRGBA(0x4); set => _s.SetColorRGBA(0x4, value); }

        [DisplayName("Bright Color"), Description("")]
        public Color BrightColor { get => _s.GetColorRGBA(0x8); set => _s.SetColorRGBA(0x8, value); }

        [DisplayName("HUD Color"), Description("")]
        public Color HUDColor { get => _s.GetColorRGBA(0xC); set => _s.SetColorRGBA(0xC, value); }
    }

    public class KEX_KirbyColors : HSDAccessor
    {
        public override int TrimmedSize => 0x8;

        public int Num { get => _s.GetInt32(0x00); set => _s.SetInt32(0x00, value); }

        public HSDArrayAccessor<KEX_KirbyColor> Colors { get => _s.GetReference<HSDArrayAccessor<KEX_KirbyColor>>(0x04); set => _s.SetReference(0x04, value); }
    }

}
