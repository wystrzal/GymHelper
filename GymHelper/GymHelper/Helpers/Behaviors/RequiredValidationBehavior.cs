using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GymHelper.Helpers
{
    class RequiredValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);

        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        public static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                ((Entry)sender).Placeholder = "To pole jest wymagane!";
                ((Entry)sender).PlaceholderColor = Color.Red;
            }
        }
    }
}
