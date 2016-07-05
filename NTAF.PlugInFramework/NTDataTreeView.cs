using System.Collections;
using System.Windows.Forms;
using NTAF.Core;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// TreeNode object specifically for the NTTreeView conrol
    /// </summary>
    public class NTTreeNode : TreeNode, IDictionaryEnumerator {
        /// <summary>
        /// 
        /// </summary>
        private DictionaryEntry nodeEntry;
        /// <summary>
        /// 
        /// </summary>
        private IEnumerator enumerator;

        /// <summary>
        /// Creates an instance of a NewTerra TreeNode
        /// </summary>
        public NTTreeNode() {
            enumerator = base.Nodes.GetEnumerator();
        }

        /// <summary>
        /// Creates an instance of a NewTerra TreeNode and sets the NodeValue
        /// </summary>
        /// <param name="obj">Object to store in NodeValue</param>
        public NTTreeNode( object obj ) {
            enumerator = base.Nodes.GetEnumerator();
            NodeValue = obj;
        }

        /// <summary>
        /// Gets or sets the key value to enable node lookup
        /// </summary>
        public string NodeKey {
            get {
                return nodeEntry.Key.ToString();
            }

            set {
                nodeEntry.Key = value;
            }
        }
        /// <summary>
        /// Gets or sets the node value
        /// </summary>
        public object NodeValue {
            get {
                return nodeEntry.Value;
            }

            set {
                if ( nodeEntry.Value is INTName ) {
                    try { ( ( INTName )nodeEntry.Value ).EventNameChanged -= this.myNodeValueNameChanged; }
                    catch { }
                }

                nodeEntry.Value = value;

                base.Text = value.ToString();
                
                //base.NodeFont = default( Font );// Font( "Tahoma", 8.25F );

                ////this is part of the orphaning
                //if ( value is IOwner )
                //    if ( ( ( IOwner )value ).myOwner == null ) {//is an orphan
                //        base.ForeColor = System.Drawing.Color.Red;
                //        base.NodeFont = new Font( base.NodeFont, FontStyle.Bold );
                //    }

                if ( nodeEntry.Value is INTName )
                    ( ( INTName )nodeEntry.Value ).EventNameChanged += new NTEventHandler<NameChangeArgs>( myNodeValueNameChanged );
            }
        }
        /// <summary>
        /// Gets fired when the name of the containing type changes
        /// </summary>
        /// <param name="args"></param>
        private void myNodeValueNameChanged( NameChangeArgs args ) {
            //base.Text = ( ( INTName )nodeEntry.Value ).Name;
            base.Text = nodeEntry.Value.ToString();
        }
        /// <summary>
        /// gets KeyValue Pair for this node
        /// </summary>
        public DictionaryEntry Entry {
            get {
                return nodeEntry;
            }
        }
        /// <summary>
        /// Advances the enumerator
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next control;
        /// false if the enumerator has passed the end of the collection. </returns>
        public bool MoveNext() {
            bool Success;
            Success = enumerator.MoveNext();
            return Success;
        }
        /// <summary>
        /// gets the current enumerator for the node
        /// </summary>
        public object Current {
            get {
                return enumerator.Current;
            }
        }
        /// <summary>
        /// returns the key value of the node
        /// </summary>
        public object Key {
            get {
                return nodeEntry.Key;
            }
        }
        /// <summary>
        /// returns the value of the node
        /// </summary>
        public object Value {
            get {
                return nodeEntry.Value;
            }
        }
        /// <summary>
        /// resets the enumerator
        /// </summary>
        public void Reset() {
            enumerator.Reset();
        }
    }

}
