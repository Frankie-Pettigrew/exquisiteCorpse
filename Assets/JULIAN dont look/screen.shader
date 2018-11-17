// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "screen"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_tiling_offset("tiling_offset", Vector) = (0,0,0,0)
		_rgb_cell("rgb_cell", 2D) = "white" {}
		_pixels("pixels", Vector) = (0,0,0,0)
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _rgb_cell;
			uniform float2 _pixels;
			uniform sampler2D _MainTex;
			uniform float4 _tiling_offset;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv18 = i.ase_texcoord.xy * _pixels + float2( 0,0 );
				float2 appendResult7 = (float2(_tiling_offset.x , _tiling_offset.y));
				float2 appendResult8 = (float2(_tiling_offset.z , _tiling_offset.w));
				float2 uv5 = i.ase_texcoord.xy * appendResult7 + appendResult8;
				float pixelWidth9 =  1.0f / _pixels.x;
				float pixelHeight9 = 1.0f / _pixels.y;
				half2 pixelateduv9 = half2((int)(uv5.x / pixelWidth9) * pixelWidth9, (int)(uv5.y / pixelHeight9) * pixelHeight9);
				
				
				finalColor = ( tex2D( _rgb_cell, uv18 ) * tex2D( _MainTex, pixelateduv9 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15900
403;132;992;768;1899.883;779.2255;2.591543;True;False
Node;AmplifyShaderEditor.Vector4Node;6;-1280.836,352.0382;Float;False;Property;_tiling_offset;tiling_offset;1;0;Create;True;0;0;False;0;0,0,0,0;0.4,1,0.3,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;7;-1059.837,357.0382;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-1043.837,453.0383;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;10;-1075.597,153.2701;Float;False;Property;_pixels;pixels;3;0;Create;True;0;0;False;0;0,0;42,74;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-875.8271,318.0735;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.6,1;False;1;FLOAT2;0.2,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-877.6433,-13.0439;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.6,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCPixelate;9;-632.9095,174.4912;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT;30;False;2;FLOAT;20;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-518.8499,-117.4629;Float;True;Property;_rgb_cell;rgb_cell;2;0;Create;True;0;0;False;0;None;9e078d928ef154acfaabdfec027384ed;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-408.9654,103.9781;Float;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-57.44162,-19.61465;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;124.1567,-2.641633;Float;False;True;2;Float;ASEMaterialInspector;0;1;screen;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;1
WireConnection;7;1;6;2
WireConnection;8;0;6;3
WireConnection;8;1;6;4
WireConnection;5;0;7;0
WireConnection;5;1;8;0
WireConnection;18;0;10;0
WireConnection;9;0;5;0
WireConnection;9;1;10;1
WireConnection;9;2;10;2
WireConnection;11;1;18;0
WireConnection;1;1;9;0
WireConnection;12;0;11;0
WireConnection;12;1;1;0
WireConnection;3;0;12;0
ASEEND*/
//CHKSM=1CD90F7F235DB176B4941A928789306D72AC9B9D