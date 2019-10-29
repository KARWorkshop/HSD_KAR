﻿using System.Windows.Forms;
using HSDRaw;
using System.Collections.Generic;
using System;
using HSDRaw.Common;
using HSDRaw.Common.Animation;
using System.Linq;
using HSDRaw.Melee.Gr;
using HSDRaw.Melee.Pl;
using HSDRaw.AirRide.Gr.Data;
using HSDRaw.Melee.Ef;

namespace HSDRawViewer
{
    public class DataNode : TreeNode
    {
        public bool IsArrayMember { get; internal set; } = false;
        private string ArrayName { get; set; }
        private int ArrayIndex { get; set; }
        public HSDAccessor Accessor { get; set; }

        private static Dictionary<Type, string> typeToImageKey = new Dictionary<Type, string>()
        {
            { typeof(HSD_JOBJ), "jobj" },
            { typeof(HSD_DOBJ), "dobj" },
            { typeof(HSD_MOBJ), "mobj" },
            { typeof(HSD_POBJ), "pobj" },
            { typeof(HSD_TOBJ), "tobj" },
            { typeof(HSD_AOBJ), "aobj" },
            { typeof(HSD_IOBJ), "iobj" },
            { typeof(HSD_SOBJ), "sobj" },
            { typeof(HSD_Camera), "cobj" },
            { typeof(HSD_LOBJ), "lobj" },
            { typeof(SBM_Coll_Data), "coll" },
            { typeof(KAR_grCollisionNode), "coll" },
            { typeof(HSD_AnimJoint), "anim_joint" },
            { typeof(HSD_MatAnimJoint), "anim_material" },
            { typeof(HSD_TEXGraphicBank), "group" },
            { typeof(HSD_TexGraphic), "anim_texture" },
            { typeof(HSD_TexAnim), "anim_texture" },
            { typeof(SBM_Map_Head), "group" },
            { typeof(SBM_GeneralPoints), "group" },
            { typeof(SBM_Model_Group), "group" },
            { typeof(SBM_EffectModel), "group" },
            { typeof(SBM_EffectTable), "table" },
            { typeof(SBM_SubActionTable), "table" },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool CheckGenericType(HSDAccessor o)
        {
            if(o.GetType().IsGenericType)
            {
                if (o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(HSDArrayAccessor<>))
                    || o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(HSDNullPointerArrayAccessor<>)))
                {
                    ImageKey = "folder";
                    SelectedImageKey = "folder";
                    return true;
                }
            }
            return false;
        }

        public DataNode(string Text, HSDAccessor accessor)
        {
            this.Text = Text;
            Accessor = accessor;

            if (Accessor is HSD_JOBJ jobj && jobj.ClassName != null)
                Text = jobj.ClassName + ":" + Text;

            if (typeToImageKey.ContainsKey(accessor.GetType()))
            {
                ImageKey = typeToImageKey[accessor.GetType()];
                SelectedImageKey = typeToImageKey[accessor.GetType()];
            }
            else
            if (CheckGenericType(accessor))
            {

            }
            else
            if(accessor.GetType() != typeof(HSDAccessor) )
            {
                ImageKey = "known";
                SelectedImageKey = "known";
            }

            // add dummy only if this node has references or if there is an array in the accessor's properties
            if(accessor._s.References.Count != 0 || Accessor.GetType().GetProperties().ToList().Find(e=>e.PropertyType.IsArray) != null)
                Nodes.Add(new TreeNode()); // dummy
        }

        private void AddNext(HSDAccessor access, int index)
        {
            foreach (var prop in access.GetType().GetProperties())
            {
                if (prop.Name == "Next")
                {
                    var acc = (HSDAccessor)prop.GetValue(access);
                    if (acc != null)
                    {
                        Nodes.Add(new DataNode(prop.PropertyType.Name + "_" + index, acc));
                        AddNext(acc, index+1);
                    }
                }

            }
        }

        public void Refresh()
        {
            if (Parent != null && Parent is DataNode parent)
            {
                if (IsArrayMember)
                {
                    var prop = parent.Accessor.GetType().GetProperty(ArrayName);
                    var arr = prop.GetValue(parent.Accessor) as HSDAccessor[];
                    arr[ArrayIndex] = Accessor;
                    prop.SetValue(parent.Accessor, arr);

                }
                //parent.Refresh();
            }

            Collapse();
            Expand();
        }

        public void ExpandData()
        {
            if (MainForm.Instance.IsOpened(this))
            {
                MessageBox.Show("Error: This node is currently open in an editor");
                Collapse();
                return;
            }

            HashSet<HSDStruct> strucs = new HashSet<HSDStruct>();

            foreach(var prop in Accessor.GetType().GetProperties())
            {
                if (prop.Name.Equals("Item") || prop.Name.Equals("Children"))
                    continue;

                if (prop.PropertyType.IsArray)
                {
                    var acc = prop.GetValue(Accessor) as HSDAccessor[];

                    if (acc != null)
                    {
                        int index = 0;
                        foreach (var a in acc)
                        {
                            if (a == null) continue;
                            strucs.Add(a._s);
                            Nodes.Add(
                                new DataNode
                                (prop.Name + (typeToImageKey.ContainsKey(acc.GetType()) ? "" : $"_{index}:\t" + prop.PropertyType.Name), a)
                            {
                                IsArrayMember = true,
                                ArrayName = prop.Name,
                                ArrayIndex = index
                            });
                            // add substructs too so they don't get appended at the end
                            foreach (var ss in a._s.References)
                                strucs.Add(ss.Value);
                            AddNext(a, 1);
                            index++;
                        }
                    }

                }
                if (prop.PropertyType.IsSubclassOf(typeof(HSDAccessor)))
                {
                    var acc = prop.GetValue(Accessor) as HSDAccessor;
                    
                    if (acc != null && acc._s != Accessor._s)
                    {
                        strucs.Add(acc._s);
                        if (prop.Name != "Next")
                        {
                            Nodes.Add(new DataNode(prop.Name + (typeToImageKey.ContainsKey(acc.GetType()) ? "" : ":\t" + prop.PropertyType.Name), acc));
                            AddNext(acc, 1);
                        }
                    }
                }
            }

            // appends structs without labels
            foreach (var v in Accessor._s.References)
            {
                if(!strucs.Contains(v.Value))
                    Nodes.Add(new DataNode("0x" + v.Key.ToString("X6"), Accessor._s.GetReference<HSDAccessor>(v.Key)));
            }
        }

        public void Export()
        {
            using (SaveFileDialog f = new SaveFileDialog())
            {
                f.Filter = "HSD (*.dat)|*.dat";
                f.FileName = Text;

                if(f.ShowDialog() == DialogResult.OK)
                {
                    HSDRawFile r = new HSDRawFile();
                    HSDRootNode root = new HSDRootNode();
                    root.Data = Accessor;
                    root.Name = System.IO.Path.GetFileNameWithoutExtension(f.FileName);
                    r.Roots.Add(root);
                    r.Save(f.FileName);
                }
            }
        }

        private bool OpenDAT(out HSDRawFile file)
        {
            file = null;
            using (OpenFileDialog f = new OpenFileDialog())
            {
                f.Filter = "HSD (*.dat)|*.dat";
                f.FileName = Text;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    file = new HSDRawFile(f.FileName);
                    return true;
                }
            }
            return false;
        }

        private void ReplaceMe(HSDAccessor newStruct)
        {
            Accessor._s.SetData(newStruct._s.GetData());
            Accessor._s.References.Clear();
            foreach (var v in newStruct._s.References)
            {
                Accessor._s.References.Add(v.Key, v.Value);
            }
        }

        public void Import()
        {
            if (MainForm.Instance.IsOpened(this))
            {
                MessageBox.Show("Error: This node is currently open in an editor");
                return;
            }
            HSDRawFile file;
            if (OpenDAT(out file))
            {
                ReplaceMe(file.Roots[0].Data);
            }
        }

        public void Delete()
        {
            if (MainForm.Instance.IsOpened(this))
            {
                MessageBox.Show("Error: This node is currently open in an editor");
                return;
            }
            if (Parent != null && Parent is DataNode parent)
            {
                if (Accessor is HSD_DOBJ dobj)
                {
                    var current = new HSD_DOBJ();
                    current._s = Accessor._s;
                    
                    if (PrevNode is DataNode prev && prev.Accessor is HSD_DOBJ next)
                    {
                        next.Next = current.Next;
                    }
                    else
                    {
                        parent.Accessor._s.ReplaceReferenceToStruct(Accessor._s, current.Next._s);
                    }
                }
                else
                if (IsArrayMember)
                {
                    // this is a mess
                    var prop = parent.Accessor.GetType().GetProperty(ArrayName);

                    var arr = prop.GetValue(parent.Accessor) as object[];
                    
                    var list = arr.ToList();
                    list.RemoveAt(ArrayIndex);

                    var outputArray = Array.CreateInstance(Accessor.GetType(), list.Count);
                    Array.Copy(list.ToArray(), outputArray, list.Count);

                    prop.SetValue(parent.Accessor, outputArray);
                }
                else
                if(parent.Accessor._s.RemoveReferenceToStruct(Accessor._s))
                    parent.Nodes.Remove(this);
                else 
                if(PrevNode is DataNode prev)
                {
                    prev.Accessor._s.RemoveReferenceToStruct(Accessor._s);
                }


                parent.Refresh();
            }
            else
            {
                MainForm.DeleteRoot(this);
            }
        }


#region Special

        /// <summary>
        /// Opens a <see cref="SBM_Model_Group"/> from a dat file and appends it to the <see cref="SBM_Map_Head"/>
        /// </summary>
        public void ImportModelGroup()
        {
            HSDRawFile file;
            if (Accessor is SBM_Map_Head head && OpenDAT(out file))
            {
                var group = head.ModelGroups.ToList();

                group.Add(new SBM_Model_Group() { _s = file.Roots[0].Data._s });

                head.ModelGroups = group.ToArray();

                Refresh();
            }
        }

#endregion

    }
}
