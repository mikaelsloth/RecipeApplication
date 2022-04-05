namespace Recipe.Views.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Windows.Forms.Design.Behavior;

    /// <summary>
    /// Provides a designer that can design components
    /// that extend TextBoxBase.
    /// </summary>
    public class ModernTextBoxDesigner : ControlDesigner
    {
        /// <summary>
        /// Provides a designer that can design components
        /// that extend TextBox.
        /// </summary>
        public ModernTextBoxDesigner()
        {
            AutoResizeHandles = true;
        }

        private char passwordChar;

        private DesignerActionListCollection? _actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new();
                    _ = _actionLists.Add(new ModernTextBoxActionList(this));
                }

                return _actionLists;
            }
        }

        /// <summary>
        /// Adds a baseline SnapLine to the list of SnapLines related
        /// to this control.
        /// </summary>
        public override IList SnapLines
        {
            get
            {
                IList snapLines = base.SnapLines;

                int baseline = DesignerUtilities.GetTextBaseline(Control, ContentAlignment.TopLeft);

                ExtendedBorderStyle borderStyle = ExtendedBorderStyle.DrawnBorder;
                PropertyDescriptor? style = TypeDescriptor.GetProperties(Component)["BorderStyle"];
                if (style != null)
                {
                    borderStyle = (ExtendedBorderStyle)style.GetValue(Component);
                }

                int bordersize = 0;
                PropertyDescriptor? bsize = TypeDescriptor.GetProperties(Component)["BorderSize"];
                if (bsize != null)
                {
                    bordersize = (int)bsize.GetValue(Component);
                }

                baseline = borderStyle switch
                {
                    ExtendedBorderStyle.None => baseline += 0,
                    ExtendedBorderStyle.FixedSingle => baseline += 2,
                    ExtendedBorderStyle.Fixed3D => baseline += 3,
                    ExtendedBorderStyle.DrawnBorder => baseline += bordersize,
                    ExtendedBorderStyle.UnderLined => baseline += 1,
                    _ => baseline += 0,
                };

                _ = snapLines.Add(new SnapLine(SnapLineType.Baseline, baseline, SnapLinePriority.Medium));

                return snapLines;
            }
        }

        /// <summary>
        ///  Shadows the PasswordChar.  UseSystemPasswordChar overrides PasswordChar so independent on the value
        ///  of PasswordChar it will return the system password char.  However, the value of PasswordChar is
        ///  cached so if UseSystemPasswordChar is reset at design time the PasswordChar value can be restored.
        ///  So in the case both properties are set, we need to serialize the real PasswordChar value as well.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<API>")]
        private char PasswordChar
        {
            get
            {
                ModernTextBox? tb = Control as ModernTextBox;
                Debug.Assert(tb != null, "Designed control is not a TextBox.");

                return tb.UseSystemPasswordChar ? passwordChar : tb.PasswordChar;
            }
            set
            {
                ModernTextBox? tb = Control as ModernTextBox;
                Debug.Assert(tb != null, "Designed control is not a TextBox.");

                passwordChar = value;
                tb.PasswordChar = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<API>")]
        private string Text
        {
            get => Control.Text;
            set
            {
                Control.Text = value;

                // This fixes bug #48462. If the text box is not wide enough to display all of the text,
                // then we want to display the first portion at design-time. We can ensure this by
                // setting the selection to (0, 0).
                //
                ((ModernTextBox)Control).Select(0, 0);
            }
        }

        private bool ShouldSerializeText() => TypeDescriptor.GetProperties(typeof(ModernTextBox))["Text"].ShouldSerializeValue(Component);

        private void ResetText() => Control.Text = "";

        /// <summary>
        /// We override this so we can clear the text field set by controldesigner.
        /// </summary>
        /// <param name="defaultValues">The default values.</param>
        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            PropertyDescriptor? textProp = TypeDescriptor.GetProperties(Component)["Text"];
            if (textProp != null && textProp.PropertyType == typeof(string) && !textProp.IsReadOnly && textProp.IsBrowsable)
                textProp.SetValue(Component, "");
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            PropertyDescriptor? prop;

            // Handle shadowed properties
            //
            string[] shadowProps = new string[] { "Text", "PasswordChar" };
            Attribute[] empty = Array.Empty<Attribute>();

            for (int i = 0; i < shadowProps.Length; i++)
            {
                prop = (PropertyDescriptor)properties[shadowProps[i]]!;
                if (prop != null)
                    properties[shadowProps[i]] = TypeDescriptor.CreateProperty(typeof(ModernTextBoxDesigner), prop, empty);
            }
        }

        /// <summary>
        /// Retrieves a set of rules concerning the movement capabilities of a component.
        /// This should be one or more flags from the SelectionRules class.  If no designer
        /// provides rules for a component, the component will not get any UI services.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules rules = base.SelectionRules;

                rules |= SelectionRules.AllSizeable;

                return rules;
            }
        }
    }
}

