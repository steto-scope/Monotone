# Monotone

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




## Supported Controls

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



## Philosophy

* **No delay for visual feedback.** Controls should not fade. That´s unnecessary.
* **Not too glossy, not to flat.** The controls should combine the best of both worlds: Stylish, but simple
* **A few colors, a lot experience.** The whole theme is based on just a few colors, but they are used and reused in a wise manner.


## Roadmap

Version |	Detail | Date
--- | --- | ---
1.0	| Initial Release |	released
1.1	| More supported Controls (Default and Extended WPF Toolkit) | released
1.2	| Support for ColorBox|	released
1.4	| Predefined Color Schemes, additional styles |	released
