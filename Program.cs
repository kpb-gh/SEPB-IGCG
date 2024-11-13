using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        // CONFIG START
        string CAMERA_NAME = "Cam_TGP"; // Name of camera to target with
        string ANTENNA_NAME = "Antenna"; // Name of antenna to broadcast with
        int TARGET_RANGE = 5000; // Maximum range at which a lock can be obtained
        string BROADCAST_TAG = "IGCG"; // Listen code for antenna broadcast
        // CONFIG END
        Vector3D targetDirection = Vector3D.Zero;
        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
        }

        public IMyCameraBlock ActivateCamera(string name)
        {
            var cam = GridTerminalSystem.GetBlockWithName(name) as IMyCameraBlock;
            cam.EnableRaycast = true;
            return cam;
        }

        public bool Scan(IMyCameraBlock cam)
        {
            MyDetectedEntityInfo data;
            if (targetDirection != Vector3D.Zero)
            {
                data = cam.Raycast(TARGET_RANGE, targetDirection);
            } else 
            { 
                data = cam.Raycast(TARGET_RANGE); 
            }
            targetDirection = data.HitPosition ?? Vector3D.Zero;
            return targetDirection != Vector3D.Zero;
        }

        public bool Broadcast(string message, Vector3D targetCoords)
        {
            throw new Exception("Not implemented."); 
        }

        public void Save()
        {
            throw new Exception("Not implemented.");
        }

        public void Main(string argument, UpdateType updateSource)
        {
            var cam = ActivateCamera(CAMERA_NAME);
            if (cam == null) 
            { 
                throw new ArgumentNullException($"{nameof(cam)} is null."); 
            }
            if (Scan(cam))
            {
                Broadcast("LOCK", targetDirection);
            } else
            {
                Broadcast("NONE", Vector3D.Zero);
            }
        }
    }
}
