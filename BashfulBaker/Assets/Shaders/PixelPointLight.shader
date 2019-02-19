Shader "Custom/PixelPointLight"
//Very busted.
{
    Properties
    {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData]_CenterX("CenterX",int)= 8
		[PerRendererData]_CenterY("CenterY",int)= 8
		[PerRendererData]_Radius("Radius",float)= 3
		[PerRendererData]_Color ("Color", Color) = (1,1,1,1)
		[PerRendererData]_FadePerDistance ("FadePerDistance", float) = 0.1
		[PerRendererData]_TextureWidth ("TextureWidth", int) = 16
		[PerRendererData]_TextureHeight ("TextureHeight", float) = 16

		[PerRendererData]_SquareSize ("SquareSize", float) = .04

		//_Color ("Tint", Color) = (1,1,1,1)
		//[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0

    }
    SubShader
    {
	Blend SrcAlpha OneMinusSrcAlpha
	        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Pass
        {



            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

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


			float _TextureWidth;
			float _TextureHeight;
			float _CenterX;
			float _CenterY;
			float _FadePerDistance;
			float _Radius;
			float _SquareSize;

			fixed4 calculateDiamondDistance(v2f IN){

				float2 unit=float2(1.0/_TextureWidth,1.0/_TextureHeight);
				float2 relatvePosition=float2(IN.texcoord.x,IN.texcoord.y);
				float2 relativeCenter=float2( (_CenterX/_TextureWidth) ,(_CenterY/_TextureHeight));
				float2 distance=relativeCenter-relatvePosition;

			    if(distance.x<0){
					distance.x=distance.x*-1;
				}
				if(distance.y<0){
					distance.y=distance.y*-1;
				}

				float actualDistance=distance.x+distance.y;

				actualDistance=actualDistance*_Radius;

				//float dropOff=actualDistance;

				return _Color * (1.0-(actualDistance));
			}


						float4 squareShader(v2f IN){
			
				float idk= (IN.texcoord.x);
				idk=idk-(idk%_SquareSize);

				float idy= (IN.texcoord.y);
				idy=idy-(idy%_SquareSize);

				float2 relativeCenter=float2( (_CenterX/_TextureWidth) ,(_CenterY/_TextureHeight));
				float relatvePosition=(relativeCenter.x+relativeCenter.y)/2;
				relatvePosition=relatvePosition-(relatvePosition%_SquareSize);

				float distance=0.0;

				if(idk<relatvePosition.x){
					idk=idk+relatvePosition.x;
					
				
				}
				else{
					idk= ((1-idk)+relatvePosition.x);	
						
					
				}

				if(idy<relatvePosition){
					idy=idy+relatvePosition.x;
				}
				else{
					idy= ((1-idy)+relatvePosition.x);
				}
				float val=(idy+idk)/2;
				float4 color=float4(val,val,val,.5);


				return color;

				/*
				distance=float2(distance.x-(distance.x%10),distance.y-(distance.y%10));
				
				float actualDistance=distance.x+distance.y;
				*/
			
			}

			//
			//Reference from: https://stackoverflow.com/questions/14470235/rendering-a-circle-with-a-pixel-shader-in-directx
			fixed4 circleShader(v2f IN){
				float2 relativeCenter=float2( (_CenterX/_TextureWidth) ,(_CenterY/_TextureHeight));
				
				float pct = 0.0;

				// a. The DISTANCE from the pixel to the center
				pct = distance(IN.texcoord.xy,relativeCenter);
				float color=1.0;
				if(pct<.5){
					return squareShader(IN);
				}
				else{
					color=0;
					return float4(color,color,color,0);
				}

				

				//return float4(pct,pct,pct,1);
				/*
				if(okDist < .1)
				return float4(0, 0, 0, 1);
				else
				return float4(1, 1, 1, 1);
				*/
			}


			float4 gradientShader(v2f IN){
			
				float idk= (IN.texcoord.x+IN.texcoord.y)/2;
				idk=idk-(idk%.10);

				/*
				distance=float2(distance.x-(distance.x%10),distance.y-(distance.y%10));
				
				float actualDistance=distance.x+distance.y;
				*/
				float4 color=float4(idk,idk,idk,1);
				return color;
			}



            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                
				fixed4 col=circleShader(i);
				
				return col;
            }
            ENDCG
        }
    }
}
