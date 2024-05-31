#ifndef __CUSTOM_FUNC_HLSL__
#define __CUSTOM_FUNC_HLSL__


void CustomFunc_float(out float4 baseColor) {
	#if defined(_BASECOLORMODE_RED)
	baseColor = float4(1.0, 0.0, 0.0, 1.0);
	#elif defined(_BASECOLORMODE_BLUE)
	baseColor = float4(0.0, 0.7, 0.7, 1.0);
	#else
	baseColor = float4(0.5, 0.5, 0.5, 1.0);
	#endif
}


#endif