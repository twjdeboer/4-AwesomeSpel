using UnityEngine;
using System.Collections;

public static class Methods {

    /**
     * Sets Opacity for a material.
     * */
    public static void SetAlpha(GameObject go, float alpha)
    {
        Color color = go.renderer.material.color;
        color.a = alpha;
        go.renderer.material.color = color;
    }

    /**
 * Let the camera shake. Each component of a vector3 makes that axis shake.
 * Need to make an attribute that indicates time.
 **/
   public static void CameraShaker(Transform camera, Vector3 shakeSpeed, Vector3 amplitude, Vector3 intPos)
    {
        ResourceManager.t += Time.deltaTime;
        float t = ResourceManager.t;
        camera.transform.position = intPos + camera.transform.TransformDirection(new Vector3(amplitude.x * Mathf.Sin(shakeSpeed.x * t), amplitude.y * Mathf.Sin(shakeSpeed.y * t), amplitude.z * Mathf.Sin(shakeSpeed.z * t)));
    }

    /**
     * Checks whether a object is in a certain radius of another object.
     * */
    public static bool ReachedPosWithBuffer(Vector3 pos, Vector3 targetPos, Vector3 bufferRadius)
   {
       return (pos.x < targetPos.x + bufferRadius.x && pos.x > targetPos.x - bufferRadius.x
           && pos.y < targetPos.y + bufferRadius.y && pos.y > targetPos.y - bufferRadius.y
           && pos.z < targetPos.z + bufferRadius.z && pos.z > targetPos.z - bufferRadius.z);

   }


}
