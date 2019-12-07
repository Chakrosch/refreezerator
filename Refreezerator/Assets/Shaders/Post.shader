Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_PixelOffset("Pixel Offset", Range(-1, 1)) = 0.65
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float _PixelOffset;
			float _OrthoSize;
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
				
				float pixelsY = _OrthoSize * 2 * 32;
				float pixelsX = (pixelsY * _ScreenParams.x) / _ScreenParams.y;
				
				float2 gridSize = float2(pixelsX, pixelsY);
				
				float2 cameraSize = _ScreenParams.xy;
				float2 pixelSize = cameraSize / gridSize;
				
				float2 uv = (i.uv * cameraSize) / pixelSize;
				uv = (float2(int(uv.x), int(uv.y)) * pixelSize + _PixelOffset)/cameraSize;
				
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
