using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.U2D;

namespace Assets.Scripts.Stealth
{
    [System.Serializable]
    public class AnxietyMeter
    {
        [UnityEngine.SerializeField]
        float maxValue=1;
        [UnityEngine.SerializeField]
        float currentValue;

        [UnityEngine.SerializeField]
        int maxPixelZoom=48;
        [UnityEngine.SerializeField]
        int minPixelZoom = 16;

        [UnityEngine.SerializeField]
        /// <summary>
        /// Unity's pixel perfect camera.
        /// </summary>
        PixelPerfectCamera camera;

        /// <summary>
        /// The way to mess with the camera zoom level since pixel perfect doesn't like zoom
        /// </summary>
        public int zoomValue
        {
            get
            {
                return Math.Max((int)((currentValue) * (maxPixelZoom)),this.minPixelZoom);
            }
        }

        /// <summary>
        /// Create the object.
        /// </summary>
        /// <param name="Camera"></param>
        public AnxietyMeter(PixelPerfectCamera Camera)
        {
            this.currentValue = 0;
            this.maxValue = 100;
            this.minPixelZoom = 16;
            this.maxPixelZoom = 48;
            camera = Camera;
        }

        /// <summary>
        /// Gain some anxiety and zoom in the camera appropriately.
        /// </summary>
        /// <param name="amount"></param>
        public void gainAnxiety(float amount)
        {
            if (camera == null) return;
            if (this.currentValue >= maxValue)
            {
                camera.assetsPPU = this.zoomValue;
                tooMuchAnxiety();
                return;
            }
            this.currentValue += amount;
            if (this.currentValue >= maxValue) this.currentValue = maxValue;
            this.camera.assetsPPU = zoomValue;

        }

        public void tooMuchAnxiety()
        {
            //Do something here with too much anxiety.
        }

        /// <summary>
        /// Relax and lose some anxiety.
        /// </summary>
        /// <param name="amount"></param>
        public void relax(float amount)
        {
            if (camera == null) return;
            if (this.currentValue <= 0)
            {
                camera.assetsPPU = this.zoomValue;
                return;
            }
            this.currentValue -= amount;
            if (this.currentValue <= 0) this.currentValue = 0;
            camera.assetsPPU = this.zoomValue;
        }

        /// <summary>
        /// Resets the camera between sceenes.
        /// </summary>
        /// <param name="Camera"></param>
        public void setCamera(PixelPerfectCamera Camera)
        {
            this.camera = Camera;
        }

        public void caughtByGuard()
        {
            this.currentValue = 1f;
        }

    }
}
