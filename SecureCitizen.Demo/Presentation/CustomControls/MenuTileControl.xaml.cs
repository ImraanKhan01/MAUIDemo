using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Service.QuickSettings;

namespace SecureCitizen.Demo.Presentation.CustomControls;

public partial class MenuTileControl : ContentView
{
    public MenuTileControl()
    {
        InitializeComponent();
        tileBackground.BackgroundColor = Color.FromRgba(0, 215, 255, 0.1);
    }
    
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(Tile));

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public string Icon { get; set; }


    public static readonly BindableProperty IconProperty = BindableProperty.Create(
                                                       propertyName: "Icon",
                                                       returnType: typeof(string),
                                                       declaringType: typeof(MenuTileControl),
                                                       defaultValue: "",
                                                       defaultBindingMode: BindingMode.TwoWay,
                                                       propertyChanged: IconPropertyChanged);

    private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MenuTileControl)bindable;
        if (newValue != null)
        {
            control.icon.Source = newValue.ToString();

        }

    }

    public string Caption { get; set; }


    public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
                                                       propertyName: "Caption",
                                                       returnType: typeof(string),
                                                       declaringType: typeof(MenuTileControl),
                                                       defaultValue: "",
                                                       defaultBindingMode: BindingMode.TwoWay,
                                                       propertyChanged: CaptionPropertyChanged);

    private static void CaptionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MenuTileControl)bindable;
        if (newValue != null)
        {
            var myString = newValue.ToString();
            var caption = Regex.Replace(myString, @"\s+", " ");
            var words = caption.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if(words.Count() > 1)
            {
                control.boldFirstWord.Text = words[0];
                control.lbl.Text = caption.Substring(words[0].Length);
            }
            else
            {
                control.boldFirstWord.Text = words[0];
                control.lbl.Text = "";
            }

        }

    }
}