//
//         Modified from http://www.codeproject.com/Articles/25458/Add-Icons-in-WPF-Tree-View
//
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// todo
    /// </summary>
    public class NTTreeNode : TreeViewItem {
        #region Global variables
        ImageSource iconSource;
        TextBlock textBlock;
        Image icon;
        #endregion Global variables

        #region Constructors and Destructors
        /// <summary>
        /// todo
        /// </summary>
        public NTTreeNode() {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Header = stack;
            //Uncomment this code If you want to add an Image after the Node-HeaderText
            //textBlock = new TextBlock();
            //textBlock.VerticalAlignment = VerticalAlignment.Center;
            //stack.Children.Add(textBlock);
            icon = new Image();
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Margin = new Thickness(0, 0, 4, 0);
            icon.Source = iconSource;
            stack.Children.Add(icon);
            //Add the HeaderText After Adding the icon
            textBlock = new TextBlock();
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(textBlock);
            }
        #endregion Constructors and Destructors
        #region Properties
        /// <summary>
        /// Gets/Sets the Selected Image for a TreeViewNode
        /// </summary>
        public ImageSource MyIcon {
            set {
                iconSource = value;
                icon.Source = iconSource;
                }
            get {
                return iconSource;
                }
            }
        #endregion Properties
        #region Event Handlers
        /// <summary>
        /// Event Handler on UnSelected Event
        /// </summary>
        /// <param name="args">Eventargs</param>
        protected override void OnUnselected(RoutedEventArgs args) {
            base.OnUnselected(args);
            icon.Source = iconSource;
            }
        /// <summary>
        /// Event Handler on Selected Event 
        /// </summary>
        /// <param name="args">Eventargs</param>
        protected override void OnSelected(RoutedEventArgs args) {
            base.OnSelected(args);
            icon.Source = iconSource;
            }

        /// <summary>
        /// Gets/Sets the HeaderText of TreeViewWithIcons
        /// </summary>
        public string Text {
            set {
                textBlock.Text = value;
                
                }
            get {
                return textBlock.Text;
                }
            }
        #endregion Event Handlers


        public void AddRange(object[] Items) {
            foreach (object item in Items) {
                this.Items.Add(item);
                }
            }
        }
    }
