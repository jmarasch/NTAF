using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace NTAF.PlugInFramework {
    class NTDataTree {
        }
    #region treenode
    //------------------------------------------------------------------------------
    // <copyright file="NTDataTreeNode.cs" company="Microsoft">
    //     Copyright (c) Microsoft Corporation.  All rights reserved.
    // </copyright>                                                                
    //------------------------------------------------------------------------------

    /// <devdoc>
    ///    <para>
    ///       Implements a node of a <see cref='System.Windows.Forms.TreeView'/>.
    ///
    ///    </para>
    /// </devdoc>

    public class NTDataTreeNode : ICloneable {
        private const int SHIFTVAL = 12;
        private const int CHECKED = 2 << SHIFTVAL;
        private const int UNCHECKED = 1 << SHIFTVAL;
        private const int ALLOWEDIMAGES = 14;

        //the threshold value used to optimize AddRange and Clear operations for a big number of nodes
        internal const int MAX_TREENODES_OPS = 200;

        internal bool nodesCleared = false;
        
        internal int index;                  // our index into our parents child array
        internal int childCount;
        internal NTDataTreeNode[] children;
        internal NTDataTreeNode parent;
        //todo: this may be come the occtree
        //internal TreeView treeView;

        private NTDataTreeNodeCollection nodes = null;
        object userData;

        string name;
        string objectID;

        /// <devdoc>
        ///     The name for the tree node - useful for indexing.
        /// </devdoc>
        public string Name {
            get {
                return name == null ? "" : name;
                }
            set {
                this.name = value;
                }
            }

        public string ObjectID {
            get {
                return objectID == null ? "" : objectID;
                }
            set {
                this.objectID = value;
                }
            }

        public NodeTypeEnum NodeType { get; set; }

        public enum NodeTypeEnum {
            DataRoot,
            ObjectCollector,
            Object,
            Other
            }

        /// <devdoc>
        ///     Creates a NTDataTreeNode object.
        /// </devdoc>
        public NTDataTreeNode() {
            //NTDataTreeNodeState = new System.Collections.Specialized.BitVector32();
            }

        //internal NTDataTreeNode(TreeView treeView) : this() {
        //    this.treeView = treeView;
        //    }

        /// <devdoc>
        ///     Creates a NTDataTreeNode object.
        /// </devdoc>
        public NTDataTreeNode(string text) : this() {
            this.text = text;
            }

        /// <devdoc>
        ///     Creates a NTDataTreeNode object.
        /// </devdoc>
        public NTDataTreeNode(string text, NTDataTreeNode[] children) : this() {
            this.text = text;
            this.Nodes.AddRange(children);
            }
        public NTDataTreeNode(string text, string objectId) : this() {
            this.text = text;
            this.Name = text;
            this.ObjectID = objectId;
            }
        public NTDataTreeNode(string text, string objectId, NTDataTreeNode[] children) : this() {
            this.text = text;
            this.Name = text;
            this.ObjectID = objectId;
            this.Nodes.AddRange(children);
            }
        public NTDataTreeNode(ObjectClassBase obj) {
            this.text = obj.Name;
            this.Name = obj.Name;
            this.ObjectID = obj.ID;
            }


        /// <devdoc>
        ///     The first child node of this node.
        /// </devdoc>
        public NTDataTreeNode FirstNode {
            get {
                if (childCount == 0) return null;
                return children[0];
                }
            }
        
        /// <devdoc>
        ///     Returns the position of this node in relation to its siblings
        /// </devdoc>
        public int Index {
            get { return index; }
            }

        /// <devdoc>
        ///     The last child node of this node.
        /// </devdoc>
        public NTDataTreeNode LastNode {
            get {
                if (childCount == 0) return null;
                return children[childCount - 1];
                }
            }


        /// <devdoc>
        ///     This denotes the depth of nesting of the NTDataTreeNode.
        /// </devdoc>
        public int Level {
            get {
                if (this.Parent == null) {
                    return 0;
                    } else {
                    return Parent.Level + 1;
                    }
                }
            }

        /// <devdoc>
        ///     The next sibling node.
        /// </devdoc>
        public NTDataTreeNode NextNode {
            get {
                if (index + 1 < parent.Nodes.Count) {
                    return parent.Nodes[index + 1];
                    } else {
                    return null;
                    }
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public NTDataTreeNodeCollection Nodes {
            get {
                if (nodes == null) {
                    nodes = new NTDataTreeNodeCollection(this);
                    }
                return nodes;
                }
            }

        /// <devdoc>
        ///     Retrieves parent node.
        /// </devdoc>
        public NTDataTreeNode Parent {
            get {
                //TreeView tv = TreeView;

                //// Don't expose the virtual root publicly
                //if (tv != null && parent == tv.root) {
                //    return null;
                //    }

                return parent;
                }
            }

        /// <devdoc>
        ///     The previous sibling node.
        /// </devdoc>
        public NTDataTreeNode PrevNode {
            get {
                //fixedIndex is used for perf. optimization in case of adding big ranges of nodes
                int currentInd = index;
                int fixedInd = parent.Nodes.FixedIndex;

                if (fixedInd > 0) {
                    currentInd = fixedInd;
                    }

                if (currentInd > 0 && currentInd <= parent.Nodes.Count) {
                    return parent.Nodes[currentInd - 1];
                    } else {
                    return null;
                    }
                }
            }

        /// <summary>
        /// 
        /// </summary>
        public object Tag {
            get {
                return userData;
                }
            set {
                userData = value;
                }
            }

        /// <devdoc>
        ///     Called by the tree node collection to clear all nodes.  We optimize here if
        ///     this is the root node.
        /// </devdoc>
        internal void Clear() {

            // This is a node that is a child of some other node.  We have
            // to selectively remove children here.
            //
            //bool isBulkOperation = false;
            //TreeView tv = TreeView;

            try {

                //if (tv != null) {
                //    tv.nodesCollectionClear = true;

                //    if (tv != null && childCount > MAX_TREENODES_OPS) {
                //        isBulkOperation = true;
                //        tv.BeginUpdate();
                //        }
                //    }

                while (childCount > 0) {
                    children[childCount - 1].Remove(true);
                    }
                children = null;


                //if (tv != null && isBulkOperation) {
                //    tv.EndUpdate();
                //    }
                } finally {
                //if (tv != null) {
                //    tv.nodesCollectionClear = false;
                //    }
                nodesCleared = true;
                }
            }

        /// <devdoc>
        ///     Clone the entire subtree rooted at this node.
        /// </devdoc>
        public virtual object Clone() {
            Type clonedType = this.GetType();
            NTDataTreeNode node = null;

            if (clonedType == typeof(NTDataTreeNode)) {
                node = new NTDataTreeNode(text);
                } else {
                // SECREVIEW : Late-binding does not represent a security thread, see 

                node = (NTDataTreeNode)Activator.CreateInstance(clonedType);
                }

            node.Text = text;

            if (childCount > 0) {
                node.children = new NTDataTreeNode[childCount];
                for (int i = 0; i < childCount; i++)
                    node.Nodes.Add((NTDataTreeNode)children[i].Clone());
                }

            node.Tag = this.Tag;

            return node;
            }

        /// <devdoc>
        ///     Makes sure there is enough room to add n children
        /// </devdoc>
        /// <internalonly/>
        internal void EnsureCapacity(int num) {
            Debug.Assert(num > 0, "required capacity can not be less than 1");
            int size = num;
            if (size < 4) {
                size = 4;
                }
            if (children == null) {
                children = new NTDataTreeNode[size];
                } else if (childCount + num > children.Length) {
                int newSize = childCount + num;
                if (num == 1) {
                    newSize = childCount * 2;
                    }
                NTDataTreeNode[] bigger = new NTDataTreeNode[newSize];
                System.Array.Copy(children, 0, bigger, 0, childCount);
                children = bigger;
                }
            }

        /// <devdoc>
        ///     Helper function for getFullPath().
        /// </devdoc>
        private void GetFullPath(StringBuilder path, string pathSeparator) {
            if (parent != null) {
                parent.GetFullPath(path, pathSeparator);
                if (parent.parent != null)
                    path.Append(pathSeparator);
                path.Append(this.text);
                }
            }

        /// <devdoc>
        ///     Returns number of child nodes.
        /// </devdoc>
        public int GetNodeCount(bool includeSubTrees) {
            int total = childCount;
            if (includeSubTrees) {
                for (int i = 0; i < childCount; i++)
                    total += children[i].GetNodeCount(true);
                }
            return total;
            }

        /// <devdoc>
        ///     Helper function to add node at a given index after all validation has been done
        /// </devdoc>
        /// <internalonly/>
        internal void InsertNodeAt(int index, NTDataTreeNode node) {
            EnsureCapacity(1);
            node.parent = this;
            node.index = index;
            for (int i = childCount; i > index; --i) {
                (children[i] = children[i - 1]).index = i;
                }
            children[index] = node;
            childCount++;

            //if (TreeView != null && node == TreeView.selectedNode)
            //    TreeView.SelectedNode = node; // communicate this to the handle
            }

        /// <devdoc>
        ///     Remove this node from the TreeView control.  Child nodes are also removed from the
        ///     TreeView, but are still attached to this node.
        /// </devdoc>
        public void Remove() {
            Remove(true);
            }

        /// <devdoc>
        /// </devdoc>
        /// <internalonly/>
        internal void Remove(bool notify) {
            // unlink our children
            // 

            for (int i = 0; i < childCount; i++)
                children[i].Remove(false);
            // children = null;
            // unlink ourself
            if (notify && parent != null) {
                for (int i = index; i < parent.childCount - 1; ++i) {
                    (parent.children[i] = parent.children[i + 1]).index = i;
                    }

                // Fix Dev10 

                parent.children[parent.childCount - 1] = null;
                parent.childCount--;
                parent = null;
                }
            }

        string text;
        /// <devdoc>
        ///     The label text for the tree node
        /// </devdoc>
        public string Text {
            get {
                return text == null ? "" : text;
                }
            set {
                this.text = value;
                //UpdateNode(NativeMethods.TVIF_TEXT);
                }
            }

        /// <devdoc>
        ///     Returns the label text for the tree node
        /// </devdoc>
        public override string ToString() {
            return "NTDataTreeNode: " + (text == null ? "" : text);
            }
        }

    #endregion


    #region NTDataTreeNodeCollection

    //------------------------------------------------------------------------------
    // <copyright file="NTDataTreeNodeCollection.cs" company="Microsoft">
    //     Copyright (c) Microsoft Corporation.  All rights reserved.
    // </copyright>                                                                
    //------------------------------------------------------------------------------

    /*
    */

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class NTDataTreeNodeCollection : IList {
        private NTDataTreeNode owner;

        /// A caching mechanism for key accessor
        /// We use an index here rather than control so that we don't have lifetime
        /// issues by holding on to extra references.
        private int lastAccessedIndex = -1;

        //this index is used to optimize performance of AddRange
        //items are added from last to first after this index 
        //(to work around TV_INSertItem comctl32 perf issue with consecutive adds in the end of the list) 
        private int fixedIndex = -1;


        internal NTDataTreeNodeCollection(NTDataTreeNode owner) {
            this.owner = owner;
            }

        /// <internalonly/>
        internal int FixedIndex {
            get {
                return fixedIndex;
                }
            set {
                fixedIndex = value;
                }
            }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public virtual NTDataTreeNode this[int index] {
            get {
                if (index < 0 || index >= owner.childCount) {
                    throw new ArgumentOutOfRangeException("index");
                    }
                return owner.children[index];
                }
            set {
                if (index < 0 || index >= owner.childCount)
                    throw new ArgumentOutOfRangeException("index", "Out of range");
                value.parent = owner;
                value.index = index;
                owner.children[index] = value;
                }
            }

        /// <internalonly/>
        object IList.this[int index] {
            get {
                return this[index];
                }
            set {
                if (value is NTDataTreeNode) {
                    this[index] = (NTDataTreeNode)value;
                    } else {
                    throw new ArgumentException("", "value");
                    }
                }
            }

        /// <devdoc>
        ///     <para>Retrieves the child control with the specified key.</para>
        /// </devdoc>
        public virtual NTDataTreeNode this[string key] {
            get {
                // We do not support null and empty string as valid keys.
                if (string.IsNullOrEmpty(key)) {
                    return null;
                    }

                // Search for the key in our collection
                int index = IndexOfKey(key);
                if (IsValidIndex(index)) {
                    return this[index];
                    } else {
                    return null;
                    }

                }
            }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        // VSWhidbey 152051: Make this property available to Intellisense. (Removed the EditorBrowsable attribute.)
        public int Count {
            get {
                return owner.childCount;
                }
            }

        /// <internalonly/>
        object ICollection.SyncRoot {
            get {
                return this;
                }
            }

        /// <internalonly/>
        bool ICollection.IsSynchronized {
            get {
                return false;
                }
            }

        /// <internalonly/>
        bool IList.IsFixedSize {
            get {
                return false;
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public bool IsReadOnly {
            get {
                return false;
                }
            }

        /// <devdoc>
        ///     Creates a new child node under this node.  Child node is positioned after siblings.
        /// </devdoc>
        public virtual NTDataTreeNode Add(string text) {
            NTDataTreeNode tn = new NTDataTreeNode(text);
            Add(tn);
            return tn;
            }

        // <-- NEW ADD OVERLOADS IN WHIDBEY

        /// <devdoc>
        ///     Creates a new child node under this node.  Child node is positioned after siblings.
        /// </devdoc>
        public virtual NTDataTreeNode Add(string key, string text) {
            NTDataTreeNode tn = new NTDataTreeNode(text);
            tn.Name = key;
            Add(tn);
            return tn;
            }

        // END - NEW ADD OVERLOADS IN WHIDBEY -->

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public virtual void AddRange(NTDataTreeNode[] nodes) {
            if (nodes == null) {
                throw new ArgumentNullException("nodes");
                }
            if (nodes.Length == 0)
                return;
            //todo might be important
            //TreeView tv = owner.TreeView;
            //if (tv != null && nodes.Length > NTDataTreeNode.MAX_TREENODES_OPS) {
            //    tv.BeginUpdate();
            //    }
            owner.Nodes.FixedIndex = owner.childCount;
            owner.EnsureCapacity(nodes.Length);
            for (int i = nodes.Length - 1; i >= 0; i--) {
                AddInternal(nodes[i], i);
                }
            owner.Nodes.FixedIndex = -1;
            //if (tv != null && nodes.Length > TreeNode.MAX_TREENODES_OPS) {
            //    tv.EndUpdate();
            //    }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public NTDataTreeNode[] Find(string key, bool searchAllChildren) {
            ArrayList foundNodes = FindInternal(key, searchAllChildren, this, new ArrayList());

            // 
            NTDataTreeNode[] stronglyTypedFoundNodes = new NTDataTreeNode[foundNodes.Count];
            foundNodes.CopyTo(stronglyTypedFoundNodes, 0);

            return stronglyTypedFoundNodes;
            }

        private ArrayList FindInternal(string key, bool searchAllChildren, NTDataTreeNodeCollection NTDataTreeNodeCollectionToLookIn, ArrayList foundNTDataTreeNodes) {
            if ((NTDataTreeNodeCollectionToLookIn == null) || (foundNTDataTreeNodes == null)) {
                return null;
                }

            // Perform breadth first search - as it's likely people will want tree nodes belonging
            // to the same parent close to each other.

            for (int i = 0; i < NTDataTreeNodeCollectionToLookIn.Count; i++) {
                if (NTDataTreeNodeCollectionToLookIn[i] == null) {
                    continue;
                    }
                }

            // Optional recurive search for controls in child collections.

            if (searchAllChildren) {
                for (int i = 0; i < NTDataTreeNodeCollectionToLookIn.Count; i++) {
                    if (NTDataTreeNodeCollectionToLookIn[i] == null) {
                        continue;
                        }
                    if ((NTDataTreeNodeCollectionToLookIn[i].Nodes != null) && NTDataTreeNodeCollectionToLookIn[i].Nodes.Count > 0) {
                        // if it has a valid child collecion, append those results to our collection
                        foundNTDataTreeNodes = FindInternal(key, searchAllChildren, NTDataTreeNodeCollectionToLookIn[i].Nodes, foundNTDataTreeNodes);
                        }
                    }
                }
            return foundNTDataTreeNodes;
            }

        /// <devdoc>
        ///     Adds a new child node to this node.  Child node is positioned after siblings.
        /// </devdoc>
        public virtual int Add(NTDataTreeNode node) {
            return AddInternal(node, 0);
            }


        private int AddInternal(NTDataTreeNode node, int delta) {
            if (node == null) {
                throw new ArgumentNullException("node");
                }

            // If the TreeView is sorted, index is ignored
            //TreeView tv = owner.TreeView;
            //if (tv != null && tv.Sorted) {
            //    return owner.AddSorted(node);
            //    }
            node.parent = owner;
            int fixedIndex = owner.Nodes.FixedIndex;
            if (fixedIndex != -1) {
                node.index = fixedIndex + delta;
                } else {
                //if fixedIndex != -1 capacity was ensured by AddRange 
                Debug.Assert(delta == 0, "delta should be 0");
                owner.EnsureCapacity(1);
                node.index = owner.childCount;
                }
            owner.children[node.index] = node;
            owner.childCount++;

            //if (tv != null && node == tv.selectedNode)
            //    tv.SelectedNode = node; // communicate this to the handle

            //if (tv != null && tv.TreeViewNodeSorter != null) {
            //    tv.Sort();
            //    }

            return node.index;
            }

        /// <internalonly/>
        int IList.Add(object node) {
            if (node == null) {
                throw new ArgumentNullException("node");
                } else if (node is NTDataTreeNode) {
                return Add((NTDataTreeNode)node);
                } else {
                return Add(node.ToString()).index;
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public bool Contains(NTDataTreeNode node) {
            return IndexOf(node) != -1;
            }

        /// <devdoc>
        ///     <para>Returns true if the collection contains an item with the specified key, false otherwise.</para>
        /// </devdoc>
        public virtual bool ContainsKey(string key) {
            return IsValidIndex(IndexOfKey(key));
            }


        /// <internalonly/>
        bool IList.Contains(object node) {
            if (node is NTDataTreeNode) {
                return Contains((NTDataTreeNode)node);
                } else {
                return false;
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public int IndexOf(NTDataTreeNode node) {
            for (int index = 0; index < Count; ++index) {
                if (this[index] == node) {
                    return index;
                    }
                }
            return -1;
            }

        /// <internalonly/>
        int IList.IndexOf(object node) {
            if (node is NTDataTreeNode) {
                return IndexOf((NTDataTreeNode)node);
                } else {
                return -1;
                }
            }


        /// <devdoc>
        ///     <para>The zero-based index of the first occurrence of value within the entire CollectionBase, if found; otherwise, -1.</para>
        /// </devdoc>
        public virtual int IndexOfKey(String key) {
            // Step 0 - Arg validation
            if (string.IsNullOrEmpty(key)) {
                return -1; // we dont support empty or null keys.
                }

            // step 1 - check the last cached item
            if (IsValidIndex(lastAccessedIndex)) {
                if (this[lastAccessedIndex].Name == key) {
                    return lastAccessedIndex;
                    }
                }

            // step 2 - search for the item
            for (int i = 0; i < this.Count; i++) {
                if (this[i].Name == key) {
                    lastAccessedIndex = i;
                    return i;
                    }
                }

            // step 3 - we didn't find it.  Invalidate the last accessed index and return -1.
            lastAccessedIndex = -1;
            return -1;
            }


        /// <devdoc>
        ///     Inserts a new child node on this node.  Child node is positioned as specified by index.
        /// </devdoc>
        public virtual void Insert(int index, NTDataTreeNode node) {

            // If the TreeView is sorted, index is ignored
            //TreeView tv = owner.TreeView;
            //if (tv != null && tv.Sorted) {
            //    owner.AddSorted(node);
            //    return;
            //    }

            if (index < 0) index = 0;
            if (index > owner.childCount) index = owner.childCount;
            owner.InsertNodeAt(index, node);
            }

        /// <internalonly/>
        void IList.Insert(int index, object node) {
            if (node is NTDataTreeNode) {
                Insert(index, (NTDataTreeNode)node);
                } else {
                throw new ArgumentException("Error", "node");
                }
            }

        // <-- NEW INSERT OVERLOADS IN WHIDBEY

        /// <devdoc>
        ///     Inserts a new child node on this node.  Child node is positioned as specified by index.
        /// </devdoc>
        public virtual NTDataTreeNode Insert(int index, string text) {
            NTDataTreeNode tn = new NTDataTreeNode(text);
            Insert(index, tn);
            return tn;
            }

        /// <devdoc>
        ///     Inserts a new child node on this node.  Child node is positioned as specified by index.
        /// </devdoc>
        public virtual NTDataTreeNode Insert(int index, string key, string text) {
            NTDataTreeNode tn = new NTDataTreeNode(text);
            tn.Name = key;
            Insert(index, tn);
            return tn;
            }

        // END - NEW INSERT OVERLOADS IN WHIDBEY -->

        /// <devdoc>
        ///     <para>Determines if the index is valid for the collection.</para>
        /// </devdoc>
        /// <internalonly/> 
        private bool IsValidIndex(int index) {
            return ((index >= 0) && (index < this.Count));
            }

        /// <devdoc>
        ///     Remove all nodes from the tree view.
        /// </devdoc>
        public virtual void Clear() {
            owner.Clear();
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void CopyTo(Array dest, int index) {
            if (owner.childCount > 0) {
                System.Array.Copy(owner.children, 0, dest, index, owner.childCount);
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Remove(NTDataTreeNode node) {
            node.Remove();
            }

        /// <internalonly/>
        void IList.Remove(object node) {
            if (node is NTDataTreeNode) {
                Remove((NTDataTreeNode)node);
                }
            }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public virtual void RemoveAt(int index) {
            this[index].Remove();
            }

        /// <devdoc>
        ///     <para>Removes the child control with the specified key.</para>
        /// </devdoc>
        public virtual void RemoveByKey(string key) {
            int index = IndexOfKey(key);
            if (IsValidIndex(index)) {
                RemoveAt(index);
                }
            }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public IEnumerator GetEnumerator() {
            return new ObjectEnumerator(owner.children, owner.childCount);
            }
        }
    #endregion

    #region enumerator stuff
    public class ObjectEnumerator : IEnumerator {
        private object[] array;
        private int total;
        private int current;

        public ObjectEnumerator(object[] array, int count) {
            Debug.Assert(count == 0 || array != null, "if array is null, count should be 0");
            Debug.Assert(array == null || count <= array.Length, "Trying to enumerate more than the array contains");
            this.array = array;
            this.total = count;
            current = -1;
            }

        public bool MoveNext() {
            if (current < total - 1) {
                current++;
                return true;
                } else
                return false;
            }

        public void Reset() {
            current = -1;
            }

        public object Current {
            get {
                if (current == -1)
                    return null;
                else
                    return array[current];
                }
            }
        }
    #endregion
    }