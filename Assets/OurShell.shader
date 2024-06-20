Shader "Custom/Water" {
    Properties
        {
            _ShellColor ("Fur Color", Color) = (1,1,1,1)
            _ShellLength ("Fur Length", Range(0.0, 1.0)) = 0.5
            _ShellCount ("Shell Count", Range(1, 32)) = 16
            _Thickness("Fur Thickness", Range(0.0, 1.0)) = 1.5
        }
	SubShader {
		Pass {
            Cull Off

            CGPROGRAM
            #pragma vertex vertexPass
			#pragma fragment fragmentPass

            struct VertexData {
                float3 normal : NORMAL;
				float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
			};

            struct VertexToFragment {
                float3 normal : TEXCOORD1;
				float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
				float3 worldPosition : TEXCOORD2;
			};

            int _ShellIndex;
			int _ShellCount;
			float _ShellLength;
			float _Density;
			float _NoiseMin, _NoiseMax;
			float _Thickness;
			float _Attenuation;
			float _OcclusionBias;
			float _ShellDistanceAttenuation;
			float _Curvature;
			float _DisplacementStrength;
			float3 _ShellColor;
			float3 _ShellDirection;

            float randomNumber(uint n) {
				n = (n ^ 61) ^ (n >> 16);
				n = n + (n << 3);
				n = n ^ (n >> 4);
				n = n * 0x27d4eb2d;
				n = n ^ (n >> 15);
				return float(n & uint(0x7fffffffU)) / float(0x7fffffff);
			}
        }
    }
}