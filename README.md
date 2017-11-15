# Monotone

Monotone is an easy-to-use WPF Theme.

![Monotone Overview](http://blog.recursivebytes.com/screenshots/m14misc.png "Main controls themed by Monotone")

**Figure 1:** Standard controls themed (Monotone.xaml)

![Monotone Toolkit](http://blog.recursivebytes.com/screenshots/m14toolkit.png "Extended WPF Toolkit controls themed by Monotone")

**Figure 2:** Controls of the Extended WPF Toolkit themed (Monotone.ExtendedWPFToolkit.xaml)

![Monotone Disabled](http://blog.recursivebytes.com/screenshots/m14disabled.png "Disabled controls themed by Monotone")

**Figure 3:** Disabled Controls

## How to use

Just add the needed files into your App.xaml:

    <Application x:Class="Monotone.App"
                 xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                 StartupUri="MainWindow.xaml">
        <Application.Resources>
            <ResourceDictionary>
                <ResourceDictionary.MergedDictionaries>                
                    <ResourceDictionary Source="Monotone.Colors.xaml" />
                    <ResourceDictionary Source="Monotone.Brushes.xaml" />
                    <ResourceDictionary Source="Monotone.MahApps.xaml" /> <!-- if you use MahApps -->
                    <ResourceDictionary Source="Monotone.xaml" />
                    <ResourceDictionary Source="Monotone.ExtendedWPFToolkit.xaml" /> <!-- if you use the Toolkit -->
                    <ResourceDictionary Source="Monotone.ColorBox.xaml" /> <!-- if you use ColorBox --->
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>        
        </Application.Resources>
    </Application>




## List of Supported Controls

### Standard
TextBox	
PasswordBox	
Button	
ToggleButton
CheckBox	
RadioButton	
TextBlock	
Label
ProgressBar	
GroupBox	
TabControl	
ComboBox
Menu	
MenuItem	
ContextMenu	
ToolBar
ListBox	
ListView	
ScrollViewer	
Slider
TreeView	
Calendar	
Expander	
ScrollBar
GridSplitter	
RichTextBox

### MahApps Metro 
MetroWindow

### Extended WPF Toolkit 
AvalonDock	
AutoSelectTextBox	
BusyIndicator	
ButtonSpinner
WatermarkTextBox	
DecimalUpDown	
DoubleUpDown	
ShortUpDown
IntegerUpDown	
LongUpDown	
ByteUpDown	
SingleUpDown
ColorCanvas	
ColorPicker	
Calculator	
CalculatorUpDown
CheckComboBox	
CheckListBox	
ChildWindow	
DateTimePicker
DateTimeUpDown	
SplitButton	
SelectorItem	
RichTextBox
RichTextBoxFormatBar	
PropertyGrid	
MultiLineTextEditor	
TimePicker
RageSlider	
ValueRangeTextBox	
TimeSpanUpDown

### ColorBox 
ColorBox

### Styles
* CheckBoxSwitchStyle	- Checkboxes look like Switches
* RadioButtonArrowStyle	 - Radiobuttons look like Arrows
* RadioButtonCheckStyle - Radiobuttons look like Checkboxes
* ListViewButtonArrayStyle 


## Colors and Accents

The base colors and brushes of the theme can easily adjusted in Monotone.Colors.xaml and Monotone.Brushed.xaml 

![Monotone Colors](http://blog.recursivebytes.com/screenshots/m14colors.png "Different Base Colors")

**Figure 4:** Some different base colors

The release also contains 4 compilations of Colors and Brushes, called Accents. Included is also the older style of Monotone, prior to v1.4

![Monotone Accents](http://blog.recursivebytes.com/screenshots/m14accents.png "Available Accents")

**Figure 5:** dark (upper left), classic (upper right), white (lower left) and high-contrast (lower right).

## Philosophy

* **No delay for visual feedback.** Controls should not fade. That´s unnecessary.
* **Not too glossy, not to flat.** The controls should combine the best of both worlds: Stylish, but simple
* **A few colors, a lot experience.** The whole theme is based on just a few colors, but they are used and reused in a wise manner.


## License

The License of the **XAML-Definitions that makes up Monotone** (contained in a Release monotone-VERSION.zip) is licensed under the MIT-License. The contents of this repository is the project to develop and test Monotone. It is not released under MIT. 


## Roadmap

Version |	Detail | Date
--- | --- | ---
1.0	| Initial Release |	released
1.1	| More supported Controls (Default and Extended WPF Toolkit) | released
1.2	| Support for ColorBox|	released
1.4	| Predefined Color Schemes, additional styles |	released
