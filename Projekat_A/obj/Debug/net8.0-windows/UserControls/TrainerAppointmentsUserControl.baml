   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projekat_A.UserControlsProjekat_A.UserControls  5      =#clr-namespace:Projekat_A.ViewModelsProjekat_A.ViewModels  5      <    6Projekat_A.UserControls.TrainerAppointmentsUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projekat_A.UserControls     5      :
viewmodels#clr-namespace:Projekat_A.ViewModels     5      .��5      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6      viewTrainerAppointment#���   5      $��32(�5      $��Center=�5      $
��Bold"�5      $��0,0,0,20q�5      � 5   
   $��1��6   $��0,0,0,10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       $��* �6#   �� 5!      $
��Auto �6#   �� 5"      $��* �6#   �� 5#      $
��Auto �6#   �� 5$      $
��Auto �6#   .+�5'      �� $��0��6    
 search#���  5(      $��Center=�5)      $��0,0,10,0q�5*      �� 5,      $��1��6   $��0,0,10,0q�5.      ��5-      �� *
FilterText+ �� UpdateSourceTrigger$ PropertyChanged=�5.      �� 50      $��2��6     hall#���  51      $��Center=�52      $��	10,0,10,0q�53      �� 55      $��3��6    �� DisplayMemberPath$
 Name��58      $��0,0,10,0q�59      ��56      �� *Halls+ �� SelectedItem 57      �� *SelectedHall+59      �� 5;      $��4��6    	 apply#���  5<      $��	10,0,10,0q�5>      $
��10,5q�5?      ��5=      �� *ApplyFilterCommand+5?      �� 5A      $��5��6     resetFilter#���  5B      $��0,0,0,0q�5D      $
��10,5q�5E      ��5C      �� *ResetFilterCommand+5E      5F      &   System.Windows.Controls.DataGrid  5I   
   $��2��6      AutoGenerateColumns$ False��5L         
IsReadOnly$
 True��5M         SelectionMode$ Single=�5N      $��1q�5O      ���#DDDDDD$��0,0,0,10q�5Q      ��5J      �� *TrainingSessions+ 5K      �� *SelectedSession+   Columns 5R      0  *System.Windows.Controls.DataGridTextColumn  5S        session,  &System.Windows.Controls.DataGridColumn	   Header#	 �  6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 5U   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5T   %   �� *	Session+5U   %     5V      #	 �  6%   $
 * 5X   %    5W   %   �� *HallIdHallNavigation.Name+5X   %     5Y        hallLocation#	 �  6%   $
 * 5[   %    5Z   %   �� *HallIdHallNavigation.Location+5[   %     5\        hallCapacity#	 �  6%   $
 * 5^   %    5]   %   �� *HallIdHallNavigation.Capacity+5^   %   5`      � 5b   
   $��3��6   $��	0,10,0,10q�5d      y�5c      �� *
IsEditMode+   BooleanToVisibilityConverter �� 	Converter# [   5e      �� 5f      $
��Auto �6    �� 5g      $
��Auto �6    �� 5h      $
��Auto �6    �� 5i      $
��Auto �6    �� 5j      $
��Auto �6    .+�5n      �� $��0��6    	 editSession#��� 	 5o      $
��Bold"�5p      $��16(�5q      $��0,0,0,10q�5r      � 5u      $��1��6   $��0,0,0,10q�6     5v      �� 5w      $
��Auto �6'   �� 5x      $��* �6'   �� 5y      $
��Auto �6'   �� 5z      $��* �6'   .+�5~      �� $��0��6   #���  5      $��Center=�5�      $��0,0,10,0q�5�      �� 5�      $��1��6   $��0,0,20,0q�5�      ��5�      �� *EditSession.Session+$ PropertyChanged=�5�      �� 5�      $��2��6   #���  5�      $��Center=�5�      $��0,0,10,0q�5�      �� 5�      $��3��6   $
 Name��5�      $��0,0,0,0q�5�      ��5�      �� *Halls+ 5�      �� *
EditHall+5�      5�      �� 5�      $��2��6    
 hallInformation#��� 
 5�      $��SemiBold"�5�      $��0,5,0,5q�5�      � 5�      $��3��6   $��0,0,0,15q�6     5�      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   �� 5�      $
��Auto �6'   �� 5�      $��* �6'   .+�5�      �� $��0��6   #���  5�      $��Center=�5�      $��0,0,10,0q�5�      �� 5�      $��1��6   $��Center=�5�      $��0,0,20,0q�5�      ��5�      �� *EditHall.Location+5�      �� 5�      $��2��6   #���  5�      $��Center=�5�      $��0,0,10,0q�5�      �� 5�      $��3��6   $��Center=�5�      ��5�      �� *EditHall.Capacity+5�      5�      �� 5�      $��4��6    �� Orientation$ 
Horizontal=�5�      $��Center=�5�      $��0,10,0,0q�5�      .+�5�      ��  
 cancel#���  6   $��0,0,10,0q�5�      $
��15,5q�5�      ��5�      �� *CancelEditCommand+5�      �� 5�        save#���  6   $
��15,5q�5�      ��5�      �� *SaveSessionCommand+5�      5�      5�      �� 5�   
   $��4��6   $ 
Horizontal=�5�      $��Center=�5�      y�5�      �� *
IsEditMode+ ' #InverseBooleanToVisibilityConverter# [ .+�5�      ��   edit#���  6   $��0,0,10,0q�5�      $
��15,5q�5�      ��5�      �� *EditSessionCommand+5�      �� 5�       
 delete#���  6   $
��15,5q�5�      ��5�      �� *DeleteSessionCommand+5�      5�      5�      5�      