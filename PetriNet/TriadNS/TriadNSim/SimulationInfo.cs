﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TriadCore;

namespace TriadNSim
{
    public class SimulationInfo
    {
        public ArrayList Nodes { get; private set; }
        public ArrayList Links { get; private set; }
        public ArrayList SpyNodes { get; private set; }
        public List<ICondition> SimContitons;
        public int TerminateTime { get; set; }

        public Dictionary<NetworkObject, List<PetriLink>> NodeLinks;

        public SimulationInfo(ArrayList shapes, int terminateTime)
        {
            this.TerminateTime = terminateTime;
            Init(shapes);
        }

        void Init(ArrayList shapes)
        {
            Nodes = new ArrayList();
            Links = new ArrayList();
            SpyNodes = new ArrayList();
            SimContitons = new List<ICondition>();
            NodeLinks = new Dictionary<NetworkObject, List<PetriLink>>();
            foreach (object obj in shapes)
            {
                if (obj is PetriLink)
                {
                    PetriLink link = obj as PetriLink;
                    Links.Add(link);
                    if (!NodeLinks.ContainsKey(link.FromCP.Owner as NetworkObject))
                        NodeLinks[link.FromCP.Owner as NetworkObject] = new List<PetriLink>();
                    if (!NodeLinks.ContainsKey(link.ToCP.Owner as NetworkObject))
                        NodeLinks[link.ToCP.Owner as NetworkObject] = new List<PetriLink>();
                    NodeLinks[link.FromCP.Owner as NetworkObject].Add(link);
                    NodeLinks[link.ToCP.Owner as NetworkObject].Add(link);
                }
                else if (obj is NetworkObject)
                {
                    NetworkObject NetObj = obj as NetworkObject;
                    if (!NodeLinks.ContainsKey(NetObj))
                        NodeLinks[NetObj] = new List<PetriLink>();
                    Nodes.Add(NetObj);
                    if (NetObj.ConnectedIPs.Count > 0)
                        SpyNodes.Add(NetObj);
                }
            }
        }
    }
}
