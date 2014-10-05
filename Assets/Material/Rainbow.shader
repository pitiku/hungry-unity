Shader "Custom/Rainbow" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
    	_Width ("Width", Range(0.0, 1.0)) = 1.0
	}
	
	SubShader 
	{
		LOD 200
		
		Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
        
        Pass {
        	Cull Off
            Lighting Off
            ZWrite Off
            Offset -1, -1
            Fog { Mode Off }
            ColorMask RGB
        	Blend SrcAlpha OneMinusSrcAlpha
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
			uniform float _Width;

			struct vertexInput {
	            float4 vertex : POSITION;
	            float4 texcoord : TEXCOORD0;
	         };
	         struct vertexOutput {
	            float4 pos : SV_POSITION;
	            float4 tex : TEXCOORD0;
	         };
	 
	         vertexOutput vert(vertexInput input) 
	         {
	            vertexOutput output;
	 
	            output.tex = input.texcoord;
	            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
	            return output;
	         }
	 
	         float4 frag(vertexOutput input) : COLOR
	         {
	            if (input.tex.x < (1 - _Width))
	            {
	            	return half4(0,0,0,0);
	            }
	            else
	            {
	            	return tex2D(_MainTex, float2(input.tex));
	            }
	         }

            ENDCG
        }
    }
}
