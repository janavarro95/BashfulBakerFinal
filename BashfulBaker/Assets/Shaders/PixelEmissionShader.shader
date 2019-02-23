//Original Source: https://nielson.io/2016/04/2d-sprite-outlines-in-unity
Shader "Custom/PixelLightEmission"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		// Add values to determine if outlining is enabled and outline color.
		[PerRendererData]_Outline("Outline", Float) = 0
		[PerRendererData]_OutlineColor("Outline Color", Color) = (1,1,1,1)
		_OutlineSize("Outline Size", int) = 1
		_OutlineMultiplier("OutlineMultiplier",float)=1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			float _Outline;
			fixed4 _OutlineColor;
			int _OutlineSize;
			float4 _MainTex_TexelSize;

			float _OutlineMultiplier;


			
			

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			//The original work in which the emission effect was based off of taken from the above tutorial.
			fixed4 outline(v2f IN){
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

				// If outline is enabled and there is a pixel, try to draw an outline.
				if (_Outline > 0 && c.a != 0) {
					float totalAlpha = 1.0;

					[unroll(16)]
					for (int i = 1; i < _OutlineSize + 1; i++) {
						fixed4 pixelUp = tex2D(_MainTex, IN.texcoord + fixed2(0, i * _MainTex_TexelSize.y));
						fixed4 pixelDown = tex2D(_MainTex, IN.texcoord - fixed2(0,i *  _MainTex_TexelSize.y));
						fixed4 pixelRight = tex2D(_MainTex, IN.texcoord + fixed2(i * _MainTex_TexelSize.x, 0));
						fixed4 pixelLeft = tex2D(_MainTex, IN.texcoord - fixed2(i * _MainTex_TexelSize.x, 0));

						totalAlpha = totalAlpha * pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a;
					}
					

					if (totalAlpha == 0) {
						c.rgba = fixed4(1, 1, 1, 1) * _OutlineColor;
					}
				}

				c.rgb *= c.a;

				return c;
			}

			//Sprite emission effect
			fixed4 emission(v2f IN){
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

				// If outline is enabled and there is not a pixel, try to draw an emission outline.
				if (_Outline > 0 && c.a == 0) {
					float4 totalColor=float4(0,0,0,0);

					[unroll(16)]
					for (int i = 1; i < _OutlineSize + 1; i++) {
						fixed4 pixelUp = tex2D(_MainTex, IN.texcoord + fixed2(0, i * _MainTex_TexelSize.y));
						fixed4 pixelDown = tex2D(_MainTex, IN.texcoord - fixed2(0,i *  _MainTex_TexelSize.y));
						fixed4 pixelRight = tex2D(_MainTex, IN.texcoord + fixed2(i * _MainTex_TexelSize.x, 0));
						fixed4 pixelLeft = tex2D(_MainTex, IN.texcoord - fixed2(i * _MainTex_TexelSize.x, 0));

						int contributeAmount=0;
						if(pixelUp.r!=0 && pixelUp.g!=0 && pixelUp.b!=0){
							contributeAmount=contributeAmount+1;
						}

						if(pixelRight.r!=0 && pixelRight.g!=0 && pixelRight.b!=0){
							contributeAmount=contributeAmount+1;
						}
						if(pixelLeft.r!=0 && pixelLeft.g!=0 && pixelLeft.b!=0){
							contributeAmount=contributeAmount+1;
						}
						if(pixelDown.r!=0 && pixelDown.g!=0 && pixelDown.b!=0){
							contributeAmount=contributeAmount+1;
						}


						//Normalize contributing pixels;
						totalColor = totalColor+( pixelUp + pixelDown + pixelRight + pixelLeft)/contributeAmount;
					}
						totalColor=totalColor*_OutlineMultiplier*_OutlineColor;
					
						c.rgba = totalColor/_OutlineSize;
					
					
					
				}

				c.rgb *= c.a;

				return c;
			}


			fixed4 frag(v2f IN) : SV_Target
			{
				return emission(IN);
			}
		ENDCG
		}
	}
}
