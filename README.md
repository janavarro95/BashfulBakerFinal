using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInput;

public class IceIceBaby : MonoBehaviour
{
    public float speed;
    public SpriteRenderer r;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        count = r.material.GetInt("_Counter");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * speed) + (InputControls.RightJoystickHorizontal / 20f), transform.position.y + (Input.GetAxis("Vertical")*speed) + (InputControls.RightJoystickVertical / 20), transform.position.z);

        count = r.material.GetInt("_Counter");
       // r.material.SetInt("Counter", 0);

       Debug.Log(count);
    }
}


Shader "Custom/cakeObj"
{
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Transparency("Transparency", Range(0.0,1)) = 1
		_Counter("Counter",int) = 0
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

			uniform int _Counter;


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
				int c = _Counter + 1;
				_Counter = c;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
