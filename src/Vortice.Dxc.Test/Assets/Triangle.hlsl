#include "Common.hlsli"

PSInput VSMain(VSInput input) {
    PSInput result;
    result.Position = input.Position;
    result.Color = input.Color;
    return result;
}

float4 PSMain(PSInput input) : SV_TARGET{
    return input.Color;
}
