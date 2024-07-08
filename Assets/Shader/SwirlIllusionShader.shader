Shader "Custom/TileIllusionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TileCount ("Tile Count", Range(1, 20)) = 10
        _ScrollSpeed ("Scroll Speed", Range(0.1, 5.0)) = 0.25
        _BorderShade ("Border Shade", Range(0, 1)) = 0.5
        _BorderWidth ("Border Width", Range(0.01, 0.1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TileCount;
            float _ScrollSpeed;
            float _BorderShade;
            float _BorderWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 vCoord = i.uv;
                vCoord.x *= _ScreenParams.x / _ScreenParams.y;

                float2 vTilePos = vCoord * _TileCount;
                float fScroll = _Time.y * _ScrollSpeed;

                float fIsOddRow = (fmod(vTilePos.y, 2.0) > 1.0) ? 0.0 : 1.0;
                vTilePos.x += fScroll * (fIsOddRow * 2.0 - 1.0);

                float fShade = (fmod(vTilePos.x, 2.0) > 1.0) ? 0.0 : 1.0;
                float2 vTileFract = frac(vTilePos);

                fShade = lerp(fShade, _BorderShade, step(vTileFract.x, _BorderWidth));
                fShade = lerp(fShade, _BorderShade, step(vTileFract.y, _BorderWidth));

                return float4(fShade, fShade, fShade, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
