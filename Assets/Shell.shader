Shader "Custom/Furr" {
	Properties
			{									// wartości domyślne
				_shColor ("Fur Color", Color) = (0,0,0)					// wartość koloru włosa
				_shLength ("Fur Length", Range(0.0, 1.0)) = 0.08
				_shThickness("Fur shThickness", Range(0.0, 1.0)) = 1.2
			}
	SubShader {
		Pass {
            Cull Off
			CGPROGRAM
			#pragma vertex vertexPass
			#pragma fragment fragmentPass
			#include "UnityPBSLighting.cginc"
            #include "AutoLight.cginc"

			struct vertexData {						// struktura zawierająca
				float4 vertex : POSITION;  			// pozycję pojedynczego wierzchołka
				float3 normal : NORMAL;				// mapę symulującą wypukłości obiektu
                float2 uv : TEXCOORD0;				// koordynaty płaszczyzny 2d nakładanej na obiekt 3d
			};

			struct vertexToFragment {				// struktura zawierająca
				float4 pos : SV_POSITION;			// pozycję pojedynczego wierzchołka
                float3 normal : TEXCOORD1;			// mapę symulującą wypukłości obiektu
				float2 uv : TEXCOORD0;				// koordynaty płaszczyzny 2d nakładanej na obiekt 3d
				float3 worldPos : TEXCOORD2;		// pozycję na scenie
			};
			
			float hash(uint n) {					// funkcja haszująca
				n = (n ^ 61) ^ (n >> 16);
				n = n + (n << 3);
				n = n ^ (n >> 4);
				n = n * 0x27d4eb2d;
				n = n ^ (n >> 15);
				return float(n & uint(0x7fffffffU)) / float(0x7fffffff);
			};

            int _shIndex; 					// indeks/wysokość warstwy
			int _shCount; 					// ilość warstw/segmentów włosa
			float _shLength; 				// odległość pomiędzy warstwami	
			float _shDensity;  				// zagęszczenie włosów na płaszczyźnie
			float _minNoise, _maxNoise;		// granice wartości liczb losowych (normalizacja)
			float _shThickness; 			// grubość włosa
			float _shAttenuation; 			// parametr odpowiedzialny za ustalenie szerokości włosa na podstawie odległości od płaszczyzny
			float _shDistanceAttenuation; 	// parametr odpowiedzialny za jasność poszczególnych warstw w zależności od odległości od płaszczyzny
			float3 _shColor; 				

			vertexToFragment vertexPass(vertexData v) {														// funkcja przekazująca pojedynczy wierzchołek 
				float shellHeight = pow(((float)_shIndex / (float)_shCount), _shDistanceAttenuation);
				v.vertex.xyz += v.normal.xyz * _shLength * shellHeight;
				
				vertexToFragment i;
                i.pos = UnityObjectToClipPos(v.vertex);
				i.normal = normalize(UnityObjectToWorldNormal(v.normal));
				i.worldPos = mul(unity_ObjectToWorld, v.vertex);
                i.uv = v.uv;
				
				return i;
			}

			float4 fragmentPass(vertexToFragment i) : SV_TARGET {			// funkcja przekazująca zbiór wierzchołków
				float2 newUV = i.uv * _shDensity;
				float2 localUV = frac(newUV) * 2 - 1;						// translacja pojedynczego włosa od jego lokalnego środka
				float localDistanceFromCenter = length(localUV);
                uint2 tid = newUV;
				
                float rand = lerp(_minNoise, _maxNoise, hash(tid.x + 100 * tid.y + 100 * 10));
                float h = (float)_shIndex / (float)_shCount;
				
				if ((localDistanceFromCenter) > (_shThickness * (rand - h)) && _shIndex > 0) discard;      // jeżeli w danym miejscu nie ma włosa, nie renderuj
				float ndotl = DotClamped(i.normal, _WorldSpaceLightPos0) * 0.5f + 0.5f;
				float ambientOcclusion = pow(h, _shAttenuation);
				
                return float4(_shColor * ndotl*ndotl * saturate(ambientOcclusion), 1.0);
			}
			ENDCG
		}
	}
}