Shader "Custom/Object_Occlusion"
{
    SubShader
    {
        Tags { "Queue" = "Transparent+1" }

        Pass
        {
            Blend Zero One
        }
    }
}
