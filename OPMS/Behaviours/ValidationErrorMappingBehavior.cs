﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace OPMS.Behaviours
{
    public class ValidationErrorMappingBehavior : Behavior<Page>
    {
        #region Properties

        public static readonly DependencyProperty ValidationErrorsProperty = DependencyProperty.Register("ValidationErrors", typeof(ObservableCollection<ValidationError>), typeof(ValidationErrorMappingBehavior), new PropertyMetadata(new ObservableCollection<ValidationError>()));

        public ObservableCollection<ValidationError> ValidationErrors
        {
            get { return (ObservableCollection<ValidationError>)this.GetValue(ValidationErrorsProperty); }
            set { this.SetValue(ValidationErrorsProperty, value); }
        }

        public static readonly DependencyProperty HasValidationErrorProperty = DependencyProperty.Register("HasValidationError", typeof(bool), typeof(ValidationErrorMappingBehavior), new PropertyMetadata(false));

        public bool HasValidationError
        {
            get { return (bool)this.GetValue(HasValidationErrorProperty); }
            set { this.SetValue(HasValidationErrorProperty, value); }
        }

        #endregion

        #region Constructors

        public ValidationErrorMappingBehavior()
            : base()
        { }

        #endregion

        #region Events & Event Methods

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
       {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                this.ValidationErrors.Add(e.Error);
            }
            else
            {
                this.ValidationErrors.Remove(e.Error);
            }

            this.HasValidationError = this.ValidationErrors.Count > 0;
        }

        #endregion

        #region Support Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            Validation.AddErrorHandler(this.AssociatedObject, Validation_Error);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Validation.RemoveErrorHandler(this.AssociatedObject, Validation_Error);
        }

        #endregion
    }
}
