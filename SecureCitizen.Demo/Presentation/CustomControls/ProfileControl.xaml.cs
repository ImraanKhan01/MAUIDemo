using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureCitizen.Demo.Presentation.CustomControls;

public partial class ProfileControl : ContentView
{
    public ProfileControl()
    {
        InitializeComponent();
    }
    
    public string ProfileImage { get; set; }


    public static readonly BindableProperty ProfileImageProperty = BindableProperty.Create(
        propertyName: "ProfileImage",
        returnType: typeof(string),
        declaringType: typeof(ProfileControl),
        defaultValue: "fake_profile",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: ProfileImagePropertyChanged);

    private static void ProfileImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProfileControl)bindable;
        if (newValue != null)
        {
            control.profileimg.Source = newValue.ToString();

        }

    }
    
    public string UserName { get; set; }


    public static readonly BindableProperty UserNameProperty = BindableProperty.Create(
        propertyName: "UserName",
        returnType: typeof(string),
        declaringType: typeof(ProfileControl),
        defaultValue: "Jane Doe",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: UserNamePropertyChanged);

    private static void UserNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProfileControl)bindable;
        if (newValue != null)
        {
            control.username.Text = newValue.ToString();

        }

    }
    
    
    public string Description { get; set; }


    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        propertyName: "Description",
        returnType: typeof(string),
        declaringType: typeof(ProfileControl),
        defaultValue: "A bio of the logged in user...",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: DescriptionPropertyChanged);

    private static void DescriptionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProfileControl)bindable;
        if (newValue != null)
        {
            control.description.Text = newValue.ToString();

        }

    }
}