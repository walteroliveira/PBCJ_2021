/// <summary>
/// Função do cinemachine que realiza o arredondamento da camera 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32;        // numero de pixels base do jogo
    /* Função que armazena a base da camera do cinemachine que muda a posição da camera
     * Para arredondar subtrai a posição da camera arredondanda pela a posição da camera atual
     */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, 
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;  // Armazena a posição que a camera deve ir
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        }
    }
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit; // Retorna a posição da camera arredondada
    }
    
}
