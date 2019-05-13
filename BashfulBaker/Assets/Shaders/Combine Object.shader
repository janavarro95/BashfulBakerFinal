Shader "Custom/Combine Object" {
    Properties {
        _Color  ("Darkness Color", Color) = (0,0,0,1)
        _Color1 ("Guard Color", Color) = (0,0,0,1)
        _Color2 ("Window Color", Color) = (0,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Transparency("Darkness Transparency", Range(0.0,0.9)) = 0.25
        _Transparency1("Guard Transparency", Range(0.0,0.9)) = 0.25
        _Transparency2("Window Transparency", Range(0.0,0.9)) = 0.25
        
        _PixelCountU("Pixel Count U", float) = 100
        _PixelCountV("Pixel Count V", float) = 100
        _mX("Mouse X", Float) = 0
        _mY("Mouse Y", Float) = 0
    }
    SubShader {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil {
            Ref 0
            Comp Equal
        }
        
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
            float4 _Color;
            float _Transparency;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

            v2f vert(appdata v)
            {
                v2f o;
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.a = _Transparency;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
        
        Stencil {
            Ref 1
            Comp Equal
        }

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
            float4 _Color1;
            float _Transparency1;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

            v2f vert(appdata v)
            {
                v2f o;
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color1;
                col.a = _Transparency1;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
        
        Stencil {
            Ref 2
            Comp Equal
        }

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
            float4 _Color2;
            float _Transparency2;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

            v2f vert(appdata v)
            {
                v2f o;
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color2;
                col.a = _Transparency2;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
        
        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _PixelCountU;
            float _PixelCountV;
            uniform float _mX;
            uniform float _mY;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.uv = v.texcoord.xy;
                o.pos = UnityObjectToClipPos(v.vertex);

                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                float pixelWidth = 5.0f / _mX;
                float pixelHeight = 5.0f / _mX;

                half2 uv = half2((int)(i.uv.x / pixelWidth) * pixelWidth, (int)(i.uv.y / pixelHeight) * pixelHeight);
                half4 col = tex2D(_MainTex, uv);

                return col * _Color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
