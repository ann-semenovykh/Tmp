﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TriadCore;

namespace TriadNSim
{
    public class NSStructure : GraphBuilder 
    {
        private SimulationInfo simInfo;

        public NSStructure(SimulationInfo simInfo)
        {
            this.simInfo = simInfo;
        }

        public override Graph Build()
        {
            Boolean first = true;
            Graph network = new Graph();
            
            // цикл по всем вершинам
            foreach (NetworkObject node in simInfo.Nodes)
            {
                this.PushEmptyGraph();
                this.FirstInStackGraph.DeclareNode(new CoreName(node.Name));
                
                foreach (Polus pole in node.Routine.Poluses)
                {
                    //???
                    if (pole.Name.Contains("["))
                    {
                        Polus p = new Polus(pole.Name);
                        this.FirstInStackGraph.DeclarePolusInAllNodes(new CoreName(p.Name, p.UpperBounds.ToArray()));
                    }
                    else
                    //
                    this.FirstInStackGraph.DeclarePolusInAllNodes(new CoreName(pole.Name));
                }

                if (!first)
                {
                    this.SecondInStackGraph.Add(this.FirstInStackGraph);
                    this.PopGraph();
                }
                else
                    first = false;
            }

            foreach (PetriLink link in simInfo.Links)
            {
                this.PushEmptyGraph();

                NetworkObject objFrom = link.FromCP.Owner as NetworkObject;
                NetworkObject objTo = link.ToCP.Owner as NetworkObject;

                CoreName from, to;
                bool reverse=true;
                if (link.PolusFrom.Name.Contains("["))
                {
                    Polus p = link.PolusFrom;
                    from = new CoreName(p.Name, p.UpperBounds.ToArray());
                    reverse &= p.IsInput;
                }
                else
                {
                    
                    from =new CoreName( link.PolusFrom.Name);
                    reverse &= link.PolusFrom.IsInput;
                }
                if (link.PolusTo.Name.Contains("["))
                {
                    Polus p = link.PolusTo;
                    to = new CoreName(p.Name, p.UpperBounds.ToArray());
                    reverse &= p.IsOutput;
                }
                else
                {
                    
                    to = new CoreName(link.PolusFrom.Name);
                    reverse &= link.PolusTo.IsOutput;
                }
                //?????????????
                
                // добавляем полюса вершин
                this.FirstInStackGraph.DeclareNode(new CoreName(objFrom.Name), from);
                this.FirstInStackGraph.DeclareNode(new CoreName(objTo.Name), to);

                this.FirstInStackGraph.AddEdge(new CoreName(objFrom.Name), from,
                    new CoreName(objTo.Name), to,reverse);
                
                this.SecondInStackGraph.Add(this.FirstInStackGraph);
                this.PopGraph();
            }

            network = this.PopGraph();
            return network;
        }
    }
}
