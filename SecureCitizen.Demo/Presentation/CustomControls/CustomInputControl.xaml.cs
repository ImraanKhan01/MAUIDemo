using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureCitizen.Demo.Presentation.CustomControls;

public partial class CustomInputControl : ContentView
{
    private bool showPassword = false;
    public CustomInputControl()
    {
        InitializeComponent();
        SetInputControl();
    }

    void SetInputControl()
    {
        showPassword = IsPassword;
        eye.IsVisible = showPassword;
        eye.Source = "eye_open.png";
    }
    
    public string InputText { get; set; }


    public static readonly BindableProperty InputTextProperty = BindableProperty.Create(
        propertyName: "InputText",
        returnType: typeof(string),
        declaringType: typeof(CustomInputControl),
        defaultValue: "",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: InputTextPropertyChanged);

    private static void InputTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomInputControl)bindable;
        if (newValue != null)
        {
            control.input.Text = newValue.ToString();

        }

    }
    
    public string Icon { get; set; }


    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        propertyName: "Icon",
        returnType: typeof(string),
        declaringType: typeof(CustomInputControl),
        defaultValue: "",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: IconPropertyChanged);

    private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomInputControl)bindable;
        if (newValue != null)
        {
            control.icon.Source = newValue.ToString();

        }

    }
    
    public string PlaceHolder
    {
        get => base.GetValue(PlaceHolderProperty)?.ToString();
        set { base.SetValue(PlaceHolderProperty, value); }
    }

    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
        propertyName: "PlaceHolder",
        returnType: typeof(string),
        declaringType: typeof(CustomInputControl),
        defaultValue: "Enter some text...",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: PlaceHolderPropertyChanged
    );

    private static void PlaceHolderPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        var control = (CustomInputControl)bindable;
        if (newValue != null)
        {
            control.input.Placeholder = newValue.ToString();
        }
    }
    
    public bool IsPassword { get; set; }


    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        propertyName: "IsPassword",
        returnType: typeof(bool),
        declaringType: typeof(CustomInputControl),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: IsPasswordPropertyChanged);

    private static void IsPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomInputControl)bindable;
        if (newValue != null)
        {
           // control.input.IsPassword = (bool)newValue;
            control.showPassword = (bool)newValue;
            control.input.IsPassword = (bool)newValue;
            control.eye.IsVisible = (bool)newValue;
        }

    }

    // void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
		  //
    //     input.IsPassword =  !input.IsPassword;
    //     if (input.IsPassword)
    //     {
    //         eye.Source = "eye_open.png";
    //     }
    //     else
    //     {
    //         eye.Source = "eye_closed.png";
    //     }
    // }

    private void Eye_OnClicked(object? sender, EventArgs e)
    {
        input.IsPassword =  !input.IsPassword;
        if (input.IsPassword)
        {
            eye.Source = "eye_open.png";
        }
        else
        {
            eye.Source = "eye_closed.png";
        }
    }
}