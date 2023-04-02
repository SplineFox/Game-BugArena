Shader "Unlit/CircleCurtain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _CenterX("CenterX", Range(0.0, 1.0)) = 0.5
        _CenterY("CenterY", Range(0.0, 1.0)) = 0.5
        _Radius("Radius", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"= "Transparent" "Opaque" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            float _CenterX;
            float _CenterY;
            float _Radius;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            void drawCircle(in float2 uv, in float2 center, in float radius, in float smoothness, out float output)
            {
                float sqrDistance = pow(uv.x - center.x, 2) + pow(uv.y - center.y, 2);
                float sqrRadius = pow(radius, 2);

                if (sqrDistance < radius)
                {
                    output = smoothstep(sqrRadius, sqrRadius - smoothness, sqrDistance);
                }
                else
                {
                    output = 0;
                }
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(_CenterX, _CenterY);
                float smoothness = 0.005;
                float outputAlpha = 0;

                drawCircle(i.uv, center, _Radius, smoothness, outputAlpha);

                return fixed4(_Color.rgb, 1 - outputAlpha);
            }
            ENDCG
        }
    }
}
