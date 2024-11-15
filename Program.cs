﻿using Sandbox.Game.EntityComponents;
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
        // START PROGRAM; CONFIG START
        bool ISTRACKER = true; // Whether this entity sends targetting data or not
        string CAMERA_NAME = "Cam_TGP"; // Name of camera to target with
        string ANTENNA_NAME = "Antenna"; // Name of antenna to broadcast with
        int TARGET_RANGE = 5000; // Maximum range at which a lock can be obtained
        string BROADCAST_TAG = "IGCG"; // Listen code for antenna broadcast
        // CONFIG END
        Vector3D targetDirection = Vector3D.Zero;
        Vector3D targetLocation = Vector3D.Zero;
        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
        }

        public IMyCameraBlock ActivateCamera(string name)
        {
            var cam = GridTerminalSystem.GetBlockWithName(name) as IMyCameraBlock;
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
            targetLocation = data.HitPosition ?? Vector3D.Zero;
            return targetLocation != Vector3D.Zero;
        }

        public Vector3D CalculateDirection(Vector3D myPos, Vector3D targetPos)
        {
            throw new Exception("Not implemented."); 
        }

        public bool Broadcast(string message, Vector3D targetCoords)
        {
            throw new Exception("Not implemented."); 
        }

        public void Save()
        {
            Storage = targetDirection.ToString();
        }

        public void Main(string argument, UpdateType updateSource)
        {
            if (ISTRACKER) { 
                var cam = ActivateCamera(CAMERA_NAME);
                if (cam == null)
                {
                    throw new ArgumentNullException($"{nameof(cam)} is null.");
                }
                cam.EnableRaycast = true;
                if (Scan(cam))
                {
                    targetDirection = CalculateDirection(cam.GetPosition(), targetLocation);
                    Broadcast("LOCK", targetLocation);
                }
                else
                {
                    Broadcast("NONE", Vector3D.Zero);
                }
            } else
            {
                throw new Exception("Not implemented."); 
            }
        }
        // END PROGRAM
    }
}
