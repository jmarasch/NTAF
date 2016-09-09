using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ProtoGears_World_Builder.Special_Controls {
    /// <summary>
    /// Interaction logic for TextInputWithButton.xaml
    /// </summary>
    public partial class TextInputWithButton : UserControl {
        public TextInputWithButton() {
            InitializeComponent();
            }

        public TextInputWithButton(string label, string text) {
            InitializeComponent();
            this.lblLabel.Content = label;
            this.txtText.Text = text;
            }
        
        [XmlElement()]
        public Label Label { get { return lblLabel; } }
        [XmlElement()]
        public TextBox TextBox { get { return txtText; } }
        [XmlElement()]
        public Button Button { get { return button; } }
        }
    }

    
