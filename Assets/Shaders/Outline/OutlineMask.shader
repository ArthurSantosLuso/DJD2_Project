Shader "Custom/OutlineMask"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
    }

    SubShader{
        Tags{
           "Queue" = "Transparent+100"
           "RenderType" = "Transparent"        
        }

        pass{
            Name "Mask"
            Cull Offset
            ZTest [_ZTest]
            ZWrite Off
            ColorMask 0

            Stencil{
                Ref 1
                Pass Replace
            }
        }    
    }
}
