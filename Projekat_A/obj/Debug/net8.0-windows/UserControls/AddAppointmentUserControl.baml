   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projekat_A.UserControlsProjekat_A.UserControls  5      1clr-namespace:Projekat_A.UtilProjekat_A.Util  5      7    1Projekat_A.UserControls.AddAppointmentUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projekat_A.UserControls     5      .utilclr-namespace:Projekat_A.Util     5        � 	Resources  5      /   )Projekat_A.Util.NullToVisibilityConverter   NullToVisibilityConverter��%   &	          5   
   6)   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     addAppointment#���  5      $��32(�5      $
��Bold"�5      $��	0,20,0,30q�5      $��Center=�5      �� 5    
   $��1��6   $��20,10q�6"   .+�5!      ��   sessionDetails#���  6   $��0,0,0,5q�6@   �� 5"      $��25��5#       �� TextWrapping$
 Wrap=�5$       �� AcceptsReturn$
 True��5%      ��5"      �� *SessionDetails+ �� UpdateSourceTrigger$ PropertyChanged=�5%      5&      �� 5(   
   $��2��6   $��20,10q�6"   .+�5)      ��   
selectHall#���  6   $��0,0,0,5q�6<   �� 5*       �� DisplayMemberPath$
 Name��5,      $��0,0,0,5q�5-      ��5*      �� *AvailableHalls+ �� SelectedItem 5+      �� *SelectedHall+5-      5.      �� 50   
   $��3��6   
�������$��1q�52      $��20,10q�53       �� Padding$ 10q�54      y�55      �� *SelectedHall+ �� 	Converter# [  ._�56      �� .+�57      ��   hallDetails#���  6   $
��Bold"�6A   $��0,0,0,5q�6S   � 58      	 � ColumnDefinitions	 59      �� 5:      $
��Auto �6+   �� 5;      $��* �6+    5=      �� 5>      $
��Auto �6(   �� 5?      $
��Auto �6(   �� 5@      $
��Auto �6(   .+�5C      �� $��0��6    $��0��6-     hallName#���  6=   $��0,0,10,5q�6_   �� 5D      $��0��6    $��1��6-   $��0,0,0,5q�6`   ��6=   �� *SelectedHall.Name+6`   �� 5F      $��1��6    $��0��6-     hallLocation#���  6=   $��0,0,10,5q�6c   �� 5G      $��1��6    $��1��6-   $��0,0,0,5q�6d   ��6=   �� *SelectedHall.Location+6d   �� 5I      $��2��6    $��0��6-     hallCapacity#���  6=   $��0,0,10,5q�6c   �� 5J      $��2��6    $��1��6-   $��0,0,0,5q�6d   ��6=   �� *SelectedHall.Capacity+6d   5K      5L      5M      �� 5O   
   $��4��6   
 �� Orientation$
 
Horizontal=�5P      $��Center=�5Q      $
��0,20q�5R      .+�5S      ��   save#���  6   $	��100��5U      $��0,0,20,0q�5V      ��5T      �� *SaveCommand+5V      �� 5W       
	 cancel#��� 	 6   $	��100��5Y      ��5X      �� *CancelCommand+5Y      5Z      5[      5\      