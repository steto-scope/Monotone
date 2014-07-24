copy "D:\vs\Monotone\Monotone\Monotone.Colors.xaml" "D:\vs\Monotone\Monotone\bin\Release\Monotone.Colors.xaml"
copy "D:\vs\Monotone\Monotone\Monotone.Brushes.xaml" "D:\vs\Monotone\Monotone\bin\Release\Monotone.Brushes.xaml"
del "D:\vs\Monotone\Monotone.zip"
del "D:\vs\Monotone\Release.zip"
D:\vs\Monotone\7za.exe a -tzip D:\vs\Monotone\Monotone.zip D:\vs\Monotone\Monotone\Monotone.Colors.xaml D:\vs\Monotone\Monotone\Monotone.Brushes.xaml D:\vs\Monotone\Monotone\Monotone.MahApps.xaml D:\vs\Monotone\Monotone\Monotone.xaml D:\vs\Monotone\Monotone\Monotone.ExtendedWPFToolkit.xaml D:\vs\Monotone\Monotone\MonotoneUtils.cs D:\vs\Monotone\MonotoneTheme\bin\Release\MonotoneTheme.dll
D:\vs\Monotone\7za.exe a -tzip D:\vs\Monotone\Release.zip D:\vs\Monotone\Monotone\bin\Release\*
exit