// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Unlit/Transparent" {
Properties {
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_NoiseTexture("Base (RGB) Trans (A)", 2D) = "white" {}
	_TimeMultiplier("Time Multiplier", Float) = 1.0
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

			sampler2D _MainTex;
			sampler2D _NoiseTexture;
            float4 _MainTex_ST;
			float _TimeMultiplier;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float get_offset(float time)
			{
				return time / 2.5 * 0.5;
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float x_offset = 1 * _Time.x * _TimeMultiplier;
				float y_offset = 0 * _Time.x * _TimeMultiplier;

				float time = fmod(_Time.x, 5);

				if (time < 2.5)
				{
					x_offset = get_offset(time);
				}
				else
				{
					x_offset = get_offset(5 - time);
				}

				x_offset *= _TimeMultiplier;

				fixed2 xyOffset = tex2D(_NoiseTexture, i.texcoord + float2(x_offset, y_offset)).rg;
                fixed4 col = tex2D(_MainTex, i.texcoord + xyOffset);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
        ENDCG
    }
}

}
