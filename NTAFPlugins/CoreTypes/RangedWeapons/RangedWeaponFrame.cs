using NTAF.Core;
using NTAF.Core.Properties;
using NTAF.PlugInFramework;
using NTAF.PrintEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace NTAF.ObjectClasses {
    [ObjectClassPlugIn("Ranged Weapon Frames", "0.0.0.0"), Serializable()]
    internal class RangedWeaponFrame : ObjectClassBase {
        #region Constructors

        //I recommend these 3 basic types of constructors

        /// <summary>
        /// Creates a new object from scratch
        /// </summary>
        public RangedWeaponFrame() {
            //ID = "";
            //Name = "";
            //Cost = 0;
            //Description = "";
            //SpeciesCanUseSkill = 0;
            //Group = SkillGroupFlag.Generic;
            //ModifiesStats = new List<StatsMod>();
            //RequiresPermission = null;
        }

        /// <summary>
        /// constructor to accept current class object, useful for when you have a starting point
        /// </summary>
        /// <param name="var">the object your essentially using as a template</param>
        public RangedWeaponFrame(RangedWeaponFrame var) {
            ID = var.ID;
            Name = var.Name;
            }

        /// <summary>
        /// constructor that takes all vars
        /// </summary>
        /// <param name="rwfID"></param>
        /// <param name="rwfName"></param>
        public RangedWeaponFrame(string rwfID, string rwfName) {
            //ID = skID;
            //Name = skName;
            //Cost = skCost;
            //Description = skDescription;
            //SpeciesCanUseSkill = skRacesCanUse;
            //Group = skGroup;
            //ModifiesStats = skModifysStat;
            //RequiresPermission = skRequiresPermission;
        }

        #endregion Constructors

        #region Events

        //events you may want to call when things happen
        //this one is already created for you in the ObjectBaseClass
        //public virtual event NTEventHandler MyDataChanged;

        #endregion Events

        #region fields

        //all the data you need to track for this class

        #endregion fields

        #region Properties
        /*
         * Attributes control how items are displayed and used in various functions
         * at the time of writing this when you save a file it generates a separate xml file
         * for each Object in the database the xml files are then stored in a zip container use
         * [XMLIgnore()] on calculated fields we dont need to save out that sort of data
         * [XmlAttribute()] should be used on simple filed that wont ever be complicated (short strings, numbers, ect)
         * [] By not instructing the XML parser on what to do it will create a full tag for the object <Tag>Data</Tag>
         * [Category(TagCategory.???)] Is used mostly when the property needs to be grouped with others
         * [Description("")] This is used to create a description of the property
         * [Browsable(false)] will either hide the property from basic editors
         * Often when setting a value of a field you should check to see if anythings watching for changes on the
         * object the event to check and fire is MyDataChanged()
         * You should also run a check to see if the file is being opened or locked like so
         * if (!NTAF.Core.Properties.Settings.Default.Loading)
         *          if (myOwner is ILockable)
         *              if (((ILockable)myOwner).FileLocked)
         *                  throw new FileLockedException("File is locked, and cannot be edited.");
         * 
         * if you edit a filed during loading the data may get overridden
         * if you allow people to edit a locked file... well what would be the point of locking the file?
         */
        #endregion Properties

        #region IAboutMe Members

        /// <summary>
        /// Gets a string of root basic details about the object.
        /// This method is important in that if print is unavailable,
        /// this will typically get called in its place.
        /// </summary>
        [XmlIgnore(), Browsable(false)]
        public override string aboutMe {
            get {
                //should always call the base and build on it
                String retVal = base.aboutMe;

                return retVal;
            }
        }

        #endregion IAboutMe Members

        #region IClone Members

        //This section provides a means to do a true copy of an object

        /// <summary>
        /// Clears all the basic events this object could have, you must override
        /// is basic operation if you plan on adding custom events, when overriding
        /// this method you should call base.clearMyEvents() prior to any event clearing
        /// your method will do.
        /// </summary>
        protected override void clearMyEvents() {
            base.clearMyEvents();
        }

        #endregion IClone Members

        #region ICompare Members

        //extended comparer internal to class
        private class sortNameHelper_DSC : IComparer {
            int IComparer.Compare(object a, object b) {
                RangedWeaponFrame val1 = (RangedWeaponFrame)a;
                RangedWeaponFrame val2 = (RangedWeaponFrame)b;

                //return string compare
                //dsc
                return String.Compare(val2.Name, val1.Name);
            }
        }

        private class sortIDHelper_ASC : IComparer {
            int IComparer.Compare(object a, object b) {
                RangedWeaponFrame val1 = (RangedWeaponFrame)a;
                RangedWeaponFrame val2 = (RangedWeaponFrame)b;

                //return string compair
                //asc
                return String.Compare(val1.ID, val2.ID);
            }
        }

        private class sortIDHelper_DSC : IComparer {
            int IComparer.Compare(object a, object b) {
                RangedWeaponFrame val1 = (RangedWeaponFrame)a;
                RangedWeaponFrame val2 = (RangedWeaponFrame)b;

                //return string compare
                //dsc
                return String.Compare(val2.ID, val1.ID);
            }
        }

        //extended comparer external calls link
        public static IComparer sortName_Dsc() {
            return (IComparer)new sortNameHelper_DSC();
        }

        public static IComparer sortID_Asc() {
            return (IComparer)new sortIDHelper_ASC();
        }

        public static IComparer sortID_Dsc() {
            return (IComparer)new sortIDHelper_DSC();
        }

        #endregion ICompare Members

        #region IObjectClass Members
        /// <summary>
        /// Have to override this from the base class, other methods are dependent on it
        /// </summary>
        public override Type CollectionType {
            get { return typeof(RangedWeaponFrame); }
        }

        public override bool IsOfType(object obj) {
            return obj is RangedWeaponFrame;
        }

        public override bool IsOfType(Type T) {
            return T == typeof(RangedWeaponFrame);
        }

        public override Type MyType() {
            return CollectionType;
        }

        /// <summary>
        /// This method provides a way to link other object classes to this object durring loading.
        /// if you donot link your objects after the file has been loaded updates will not appear in the parrent object
        /// </summary>
        /// <param name="DataMaster"></param>
        public override void Link(ILink DataMaster) {
            base.Link(DataMaster);
            throw new NotImplementedException();
        }

        /// <summary>
        /// replaces one object for another
        /// </summary>
        /// <param name="ObjectToReplace"></param>
        /// <param name="ReplaceWith"></param>
        public override void ReplaceReferences(ObjectClassBase ObjectToReplace, ObjectClassBase ReplaceWith) {
            base.ReplaceReferences(ObjectToReplace,ReplaceWith);
            throw new NotImplementedException();
        }

        /// <summary>
        /// this method need to be written to see if this object contains a another item with a specific
        /// ObjectClassBase
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public override bool CheckForReferences(ObjectClassBase Item) {
            base.CheckForReferences(Item);
            throw new NotImplementedException();

            //if (RequiresPermission == Item)
            //    return true;

            //return false;
        }

        #endregion IObjectClass Members

        #region IPrintable Members

        public override void Print(PrintElement element) {
            base.Print(element);
            throw new NotImplementedException();

            //String tmpString = String.Empty;
            //element.AddTitleText("Skill: " + Name);
            //element.AddCategoryText("Modifies Stats:");
            //foreach (StatsMod SM in ModifiesStats)
            //    tmpString += SM.ToString() + ", ";
            //element.AddMText(tmpString.TrimEnd(new char[] { ',', ' ' }));
            //element.AddBlankLine();
            //element.AddCategoryText("Description:");
            //element.AddMText(Description);
            //element.AddBlankLine();
            //element.AddCategoryText("Requires Permission: ", RequiresPermission.Name);
            //element.AddBlankLine();
            //element.AddCategoryText("Species avalibility: ", SpeciesCanUseSkill.ToString());
            //element.AddBlankLine();
            //element.AddCategoryText("Skill Group: ", Group.ToString());
            //element.AddBlankLine();
            //element.AddCategoryText("Cost: ", Cost.ToString());
            ////element.AddHorizontalRule();
            //element.AddBlankLine();
        }

        #endregion IPrintable Members

        #region IValidate Members

        public override void Valid() {
            List<FieldAndMessage>
                ErrorList = new List<FieldAndMessage>(),
                WarningList = new List<FieldAndMessage>();

            try {
                base.Valid();
            }
            catch (ValidationException VE) {
                ErrorList.AddRange(VE.Errors.ToArray());
            }
            finally {
                //Add all other validation criteria here
                throw new NotImplementedException();
            }
        }

        #endregion IValidate Members

        #region methods

        public override string ToString() {
            string retVal = Name;
            if (NTAF.Core.Properties.Settings.Default.VerboseToString) {
                retVal = "";
                if (NTAF.Core.Properties.Settings.Default.VerboseID)
                    retVal += ID + ": ";
                if (NTAF.Core.Properties.Settings.Default.VerboseName)
                    retVal += Name + ", ";
                //if (NTAF.Core.Properties.Settings.Default.VerboseDescription) {
                //    string DescriptionShort = (Description.Length < 120 ? (Description.Length > 0 ? Description.Substring(0, Description.Length) : "") : Description.Substring(0, 120) + "...");
                //    retVal += DescriptionShort;
                //}
            }
            return retVal;
        }

        /// <summary>
        /// creates an array of key value pairs for all the data
        /// </summary>
        /// <returns></returns>
        public override DataMember[] getDataMembers() {
            return new[] {
                new DataMember("ID", ID), 
                new DataMember("Name", Name)
            };
        }

        #endregion methods

        #region operator overrides

        public override bool Equals(object obj) {
            bool retVal = true;
            if (obj == null || !(obj is RangedWeaponFrame))
                retVal = false;
            if (retVal)
                retVal = this.Name == ((RangedWeaponFrame)obj).Name;
            if (retVal)
                retVal = this.ID == ((RangedWeaponFrame)obj).ID;
            return retVal;
        }

        static public bool operator ==(RangedWeaponFrame a, RangedWeaponFrame b) {
            bool retval = false;
            try {
                retval = a.Equals(b);
            }
            catch (NullReferenceException) {
                if ((Object)a == null && (Object)b == null)
                    retval = true;
                else
                    retval = false;
            }
            return retval;
        }

        static public bool operator !=(RangedWeaponFrame a, RangedWeaponFrame b) {
            bool retval = false;
            try {
                retval = a.Equals(b);
            }
            catch (NullReferenceException) {
                if ((Object)a == null && (Object)b == null)
                    retval = true;
                else
                    retval = false;
            }
            return !retval;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion operator overrides
    }
}