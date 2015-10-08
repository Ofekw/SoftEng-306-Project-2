Shader "ColourBlindShader" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off

CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _RampTex;
uniform half _RampOffset;


fixed4 frag (v2f_img i) : SV_Target
{
	fixed4 orig = tex2D(_MainTex, i.uv);
	fixed grayscale = Luminance(orig.rgb);
	
	fixed rr = tex2D(_RampTex, orig.rr).r;
	fixed gg = tex2D(_RampTex, orig.gg).g;
	fixed bb = tex2D(_RampTex, orig.bb).b;

	fixed4 color;

	if(bb > gg && rr > gg){
		color = fixed4(rr*1.5, gg*0.5, bb*1.5, orig.a);
	}else if(gg > bb && gg > rr){
		color = fixed4(rr, gg, bb, orig.a);
	}else if(rr > bb && rr > gg && (bb+gg) < 100){
		color = fixed4(rr, gg*0.5, bb*0, orig.a);
	}else{
		color = fixed4(rr, gg, bb, orig.a);
	}

	return color;
}
ENDCG
	
	}
}

Fallback off

}
