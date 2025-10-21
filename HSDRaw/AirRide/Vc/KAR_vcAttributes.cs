namespace HSDRaw.AirRide.Vc
{
    public class KAR_vcAttributes : HSDAccessor
    {
        public override int TrimmedSize => 0x1F0;

        public int KirbySitBoneIndex { get => _s.GetInt32(0x0); set => _s.SetInt32(0x0, value); }

        public int KirbyAdditionalBoneIndex { get => _s.GetInt32(0x4); set => _s.SetInt32(0x4, value); }

        public float ModelScaling { get => _s.GetFloat(0x8); set => _s.SetFloat(0x8, value); }

        public float BaseWeight_Multi { get => _s.GetFloat(0xc); set => _s.SetFloat(0xc, value); }

        public float StartCameraDistance { get => _s.GetFloat(0x10); set => _s.SetFloat(0x10, value); }

        public float ShadowHeight { get => _s.GetFloat(0x14); set => _s.SetFloat(0x14, value); }

        public float ShadowLengthFrontAndBehind { get => _s.GetFloat(0x18); set => _s.SetFloat(0x18, value); }

        public float ShadowLengthSides { get => _s.GetFloat(0x1c); set => _s.SetFloat(0x1c, value); }

        public float ShadowWidthTurning { get => _s.GetFloat(0x20); set => _s.SetFloat(0x20, value); }

        public int AnglePointerVisability { get => _s.GetInt32(0x24); set => _s.SetInt32(0x24, value); }

        public float AngelPointerVert_Distance { get => _s.GetFloat(0x28); set => _s.SetFloat(0x28, value); }

        public float AnglePointerHoriz_Distance { get => _s.GetFloat(0x2c); set => _s.SetFloat(0x2c, value); }

        public float Unknown4 { get => _s.GetFloat(0x30); set => _s.SetFloat(0x30, value); }
        public float Unknown5 { get => _s.GetFloat(0x34); set => _s.SetFloat(0x34, value); }
        public float HitKnockback { get => _s.GetFloat(0x38); set => _s.SetFloat(0x38, value); }
        public float HitKnockback_Resist { get => _s.GetFloat(0x3c); set => _s.SetFloat(0x3c, value); }
        public float HitKnockback_Hight { get => _s.GetFloat(0x40); set => _s.SetFloat(0x40, value); }
        public float HitKnockback_Decel { get => _s.GetFloat(0x44); set => _s.SetFloat(0x44, value); }
        public float MaxAngularDisplacementPerfectLand { get => _s.GetFloat(0x48); set => _s.SetFloat(0x48, value); }
        public float MaxRotDisPerfectLand { get => _s.GetFloat(0x4c); set => _s.SetFloat(0x4c, value); }
        public int TimeUntilOverheating { get => _s.GetInt32(0x50); set => _s.SetInt32(0x50, value); }
        public int OverheatingCoolDown { get => _s.GetInt32(0x54); set => _s.SetInt32(0x54, value); }
        public float Slipstream_Diameter { get => _s.GetFloat(0x58); set => _s.SetFloat(0x58, value); }
        public float Slipstream_TrailStart { get => _s.GetFloat(0x5c); set => _s.SetFloat(0x5c, value); }
        public int Slipstream_LifeExpectency { get => _s.GetInt32(0x60); set => _s.SetInt32(0x60, value); }
        public float Slipstream_AccelMulti { get => _s.GetFloat(0x64); set => _s.SetFloat(0x64, value); }
        public int Slipstream_ParticleID { get => _s.GetInt32(0x68); set => _s.SetInt32(0x68, value); }
        public float BaseHP { get => _s.GetFloat(0x6c); set => _s.SetFloat(0x6c, value); }
        public float RammingHitbox_Size { get => _s.GetFloat(0x70); set => _s.SetFloat(0x70, value); }
        public float RammingHitbox_DistX { get => _s.GetFloat(0x74); set => _s.SetFloat(0x74, value); }
        public float Ramming_Dmg { get => _s.GetFloat(0x78); set => _s.SetFloat(0x78, value); }
        public float Ramming_DmgMulti { get => _s.GetFloat(0x7c); set => _s.SetFloat(0x7c, value); }
        public float Ramming_KnockbackVelocity { get => _s.GetFloat(0x80); set => _s.SetFloat(0x80, value); }
        public float Ramming_KnockbackDist { get => _s.GetFloat(0x84); set => _s.SetFloat(0x84, value); }
        public float BaseOffense { get => _s.GetFloat(0x88); set => _s.SetFloat(0x88, value); }
        public float BaseDefense { get => _s.GetFloat(0x8c); set => _s.SetFloat(0x8c, value); }
        public float TopSpeedGround { get => _s.GetFloat(0x90); set => _s.SetFloat(0x90, value); }
        public float SpeedMultiplierUpSlope { get => _s.GetFloat(0x94); set => _s.SetFloat(0x94, value); }
        public float SpeedMultiplierDownSlope { get => _s.GetFloat(0x98); set => _s.SetFloat(0x98, value); }
        public float ChargeSpeed { get => _s.GetFloat(0x9c); set => _s.SetFloat(0x9c, value); }
        public float ChargeSpeedTurning { get => _s.GetFloat(0xa0); set => _s.SetFloat(0xa0, value); }
        public float ChargeDepleteSpeed { get => _s.GetFloat(0xa4); set => _s.SetFloat(0xa4, value); }
        public float SpeedGainUnchargedBoost { get => _s.GetFloat(0xa8); set => _s.SetFloat(0xa8, value); }
        public float SpeedGainFirstQuartTankBoost { get => _s.GetFloat(0xac); set => _s.SetFloat(0xac, value); }
        public float SpeedGainSecondHalfTankBoost { get => _s.GetFloat(0xb0); set => _s.SetFloat(0xb0, value); }
        public float SpeedGainSecondTankBoost { get => _s.GetFloat(0xb4); set => _s.SetFloat(0xb4, value); }
        public float SpeedGainSecondQuartSecondTankBoost { get => _s.GetFloat(0xb8); set => _s.SetFloat(0xb8, value); }
        public float SpeedGainThirdQuartSecondTankBoost { get => _s.GetFloat(0xbc); set => _s.SetFloat(0xbc, value); }
        public float SpeedGainThirdTankBoost { get => _s.GetFloat(0xc0); set => _s.SetFloat(0xc0, value); }
        public float SpeedGainSecondHalfThirdTankBoost { get => _s.GetFloat(0xc4); set => _s.SetFloat(0xc4, value); }
        public float SpeedGainThirdQuartThirdTankBoost { get => _s.GetFloat(0xc8); set => _s.SetFloat(0xc8, value); }
        public float SpeedGainFourthTankBoost { get => _s.GetFloat(0xcc); set => _s.SetFloat(0xcc, value); }
        public float SpeedGainFullTankBoost { get => _s.GetFloat(0xd0); set => _s.SetFloat(0xd0, value); }
        public float SpeedGainDriftingBoost { get => _s.GetFloat(0xd4); set => _s.SetFloat(0xd4, value); }
        public float BaseBoost { get => _s.GetFloat(0xd8); set => _s.SetFloat(0xd8, value); }
        public float SpeedGainSlidingBoost { get => _s.GetFloat(0xdc); set => _s.SetFloat(0xdc, value); }
        public float OutwardTurnAngle { get => _s.GetFloat(0xe0); set => _s.SetFloat(0xe0, value); }
        public float Machine_TurnChargeDrift { get => _s.GetFloat(0xe4); set => _s.SetFloat(0xe4, value); }
        public float Machine_GroundTilt { get => _s.GetFloat(0xe8); set => _s.SetFloat(0xe8, value); }
        public float Machine_GroundTiltSpeed { get => _s.GetFloat(0xec); set => _s.SetFloat(0xec, value); }
        public float Machine_RockingSpeed_Maybe { get => _s.GetFloat(0xf0); set => _s.SetFloat(0xf0, value); }
        public float Machine_MaxGroundBackLean { get => _s.GetFloat(0xf4); set => _s.SetFloat(0xf4, value); }
        public float Machine_MaxGroundFrontLean { get => _s.GetFloat(0xf8); set => _s.SetFloat(0xf8, value); }
        public float Machine_MaxBackLean { get => _s.GetFloat(0xfc); set => _s.SetFloat(0xfc, value); }
        public float Machine_MaxFrontLean { get => _s.GetFloat(0x100); set => _s.SetFloat(0x100, value); }
        public float Machine_TopSpeedReturnLand { get => _s.GetFloat(0x104); set => _s.SetFloat(0x104, value); }
        public float LollipopHitboxRadius { get => _s.GetFloat(0x108); set => _s.SetFloat(0x108, value); }
        public float LollipopRammingDamage { get => _s.GetFloat(0x10c); set => _s.SetFloat(0x10c, value); }
        public float LollipopRammingDamgMul { get => _s.GetFloat(0x110); set => _s.SetFloat(0x110, value); }
        public float LollipopAddKnockbackStrength { get => _s.GetFloat(0x114); set => _s.SetFloat(0x114, value); }
        public float LollipopAddKnockbackMulti_Maybe { get => _s.GetFloat(0x118); set => _s.SetFloat(0x118, value); }
        public float LandingHitboxSize { get => _s.GetFloat(0x11c); set => _s.SetFloat(0x11c, value); }
        public float LandingHitboxDistanceX { get => _s.GetFloat(0x120); set => _s.SetFloat(0x120, value); }
        public float ATap_Damg_VPlayer { get => _s.GetFloat(0x124); set => _s.SetFloat(0x124, value); }
        public float ATap_Damg_Mul_VPlayer { get => _s.GetFloat(0x128); set => _s.SetFloat(0x128, value); }
        public float ATap_AddKnockbackStrength_VPlayer { get => _s.GetFloat(0x12c); set => _s.SetFloat(0x12c, value); }
        public float ATap_KockbackMul_VPlayer { get => _s.GetFloat(0x130); set => _s.SetFloat(0x130, value); }
        public float QuickSpinTornadoSize { get => _s.GetFloat(0x134); set => _s.SetFloat(0x134, value); }
        public float TurnSpeedOnSlope { get => _s.GetFloat(0x138); set => _s.SetFloat(0x138, value); }
        public float InitialTakeOffSpeed { get => _s.GetFloat(0x13c); set => _s.SetFloat(0x13c, value); }
        public float InitialTakeOffSpeed_2 { get => _s.GetFloat(0x140); set => _s.SetFloat(0x140, value); }
        public float VertLaunch_HoldDown { get => _s.GetFloat(0x144); set => _s.SetFloat(0x144, value); }
        public float VertLaunch_HoldUp { get => _s.GetFloat(0x148); set => _s.SetFloat(0x148, value); }
        public float TopSpeedAir { get => _s.GetFloat(0x14c); set => _s.SetFloat(0x14c, value); }
        public float AirTurnUpdraft { get => _s.GetFloat(0x150); set => _s.SetFloat(0x150, value); }
        public float AerialLean { get => _s.GetFloat(0x154); set => _s.SetFloat(0x154, value); }
        public float Landing_OrientationCorrection { get => _s.GetFloat(0x158); set => _s.SetFloat(0x158, value); }
        public float Unknown52 { get => _s.GetFloat(0x15c); set => _s.SetFloat(0x15c, value); }
        public float FullChargeMidairSpeed { get => _s.GetFloat(0x160); set => _s.SetFloat(0x160, value); }
        public float DiveCrash_Lenicency { get => _s.GetFloat(0x164); set => _s.SetFloat(0x164, value); }
        public float WobbleIntensity_SemiDiveCrash { get => _s.GetFloat(0x168); set => _s.SetFloat(0x168, value); }
        public float WobbleIntensity_DiveCrash { get => _s.GetFloat(0x16c); set => _s.SetFloat(0x16c, value); }
        public float DiveCrash_CrashHeight { get => _s.GetFloat(0x170); set => _s.SetFloat(0x170, value); }
        public float GlidePointUpSpeed { get => _s.GetFloat(0x174); set => _s.SetFloat(0x174, value); }
        public float GlidePointUpAmount { get => _s.GetFloat(0x178); set => _s.SetFloat(0x178, value); }
        public float GlidePointDownSpeed { get => _s.GetFloat(0x17c); set => _s.SetFloat(0x17c, value); }
        public float GlidePointDownAmount { get => _s.GetFloat(0x180); set => _s.SetFloat(0x180, value); }
        public float Unknown57 { get => _s.GetFloat(0x184); set => _s.SetFloat(0x184, value); }
        public float MidairUpSnapbackSpeed { get => _s.GetFloat(0x188); set => _s.SetFloat(0x188, value); }
        public float MidairTurnSpeedUp { get => _s.GetFloat(0x18c); set => _s.SetFloat(0x18c, value); }
        public float MidairTurnSpeedStraight { get => _s.GetFloat(0x190); set => _s.SetFloat(0x190, value); }
        public float Unknown58 { get => _s.GetFloat(0x194); set => _s.SetFloat(0x194, value); }
        public float MidairSideSnapbackSpeed { get => _s.GetFloat(0x198); set => _s.SetFloat(0x198, value); }
        public float AerialTurnSpeed_Multi { get => _s.GetFloat(0x19c); set => _s.SetFloat(0x19c, value); }
        public float RailSpeed_Multi { get => _s.GetFloat(0x1a0); set => _s.SetFloat(0x1a0, value); }
        public float RailSpeed_UpSlope { get => _s.GetFloat(0x1a4); set => _s.SetFloat(0x1a4, value); }
        public float RailSpeed_DownSlope { get => _s.GetFloat(0x1a8); set => _s.SetFloat(0x1a8, value); }
        public float Unknown63 { get => _s.GetFloat(0x1ac); set => _s.SetFloat(0x1ac, value); }
        public float Unknown64 { get => _s.GetFloat(0x1b0); set => _s.SetFloat(0x1b0, value); }
        public float DismountMachineGravity { get => _s.GetFloat(0x1b4); set => _s.SetFloat(0x1b4, value); }
        public float DismountMachineVelocity { get => _s.GetFloat(0x1b8); set => _s.SetFloat(0x1b8, value); }
        public float Unknown67 { get => _s.GetFloat(0x1bc); set => _s.SetFloat(0x1bc, value); }
        public float RailStationAreaTurnSpeed { get => _s.GetFloat(0x1c0); set => _s.SetFloat(0x1c0, value); }
        public float WallDecelMulti { get => _s.GetFloat(0x1c4); set => _s.SetFloat(0x1c4, value); }
        public float WallDecelMulti_Air { get => _s.GetFloat(0x1c8); set => _s.SetFloat(0x1c8, value); }
        public float WallRicochet_CorrectionMulti { get => _s.GetFloat(0x1cc); set => _s.SetFloat(0x1cc, value); }
        public float Unknown71 { get => _s.GetFloat(0x1d0); set => _s.SetFloat(0x1d0, value); }
        public float StoppingSuspention { get => _s.GetFloat(0x1d4); set => _s.SetFloat(0x1d4, value); }
        public float Unknown73 { get => _s.GetFloat(0x1d8); set => _s.SetFloat(0x1d8, value); }
        public float Unknown74 { get => _s.GetFloat(0x1dc); set => _s.SetFloat(0x1dc, value); }
        public float Unknown75 { get => _s.GetFloat(0x1e0); set => _s.SetFloat(0x1e0, value); }
        public float Unknown76 { get => _s.GetFloat(0x1e4); set => _s.SetFloat(0x1e4, value); }
        public float Unknown77 { get => _s.GetFloat(0x1e8); set => _s.SetFloat(0x1e8, value); }
        public float Unknown78 { get => _s.GetFloat(0x1ec); set => _s.SetFloat(0x1ec, value); }
    }
}
