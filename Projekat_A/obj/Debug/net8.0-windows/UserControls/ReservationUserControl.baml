   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projekat_A.UserControlsProjekat_A.UserControls  5      1clr-namespace:Projekat_A.UtilProjekat_A.Util  5      4    .Projekat_A.UserControls.ReservationUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projekat_A.UserControls     5      .utilclr-namespace:Projekat_A.Util     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � 	Resources  5         BoolToVisibilityConverter6   0Projekat_A.Util.BoolToReservationStatusConverter $  BoolToReservationStatusConverter  CenteredColumnHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  CenteredCellStyle*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels  CenteredTextBlockStyle��%   &	        &	      &	 &     &	     &	 D    �� 5   
   6'     5   
   60   �� 5   
   "  62   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� �#F0F0F06+   �� 5       � ��$ 5q�6(   �� 5       � ��$ SemiBold"�6+   5      �� 5   
   "  6*   .�5      ��  � ��$ 5q�6(   �� 5       � �� 5      �� 5      "  6&   .R�5      �� # z��6!   #��z��5   !   #��z��5   !   #��z��5    !   $
 True��5!   !   ._�5"      �� $��Center=�6/   $��Center=�6L   5#      5$      5&      5'      �� 5)   
   " ��6/   .�5*      ��  � ��$ Center=�64   �� 5+       � ��$ Center=�62   �� 5,       �@��TextAlignment$ Center=�6.   5-      5.      .��50      �  � RowDefinitions 51   
   �� 52      $
��Auto �6   �� 53      $��* �6   .+�56   
   �� $��0��6     reservation#���  57      $��32(�58      $
��Bold"�59      $��Center=�5:       $  MaterialDesignHeadline4TextBlock#��[ 5;      $��16q�5<      � 5?   
   $��1��6    5@      �� 5A      $��* �6    �� 5B      $��* �6    .+�5F      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��	16,0,16,8q�6/   .��5G      �  5H      �� 5I      $
��Auto �6(   �� 5J      $��* �6(   �� 5K      $
��Auto �6(   .+�5O      �� $��0��6      availableSession#���  5P        $  MaterialDesignHeadline6TextBlock#��[ 5Q       $��18(�5R       $��
16,16,16,8q�5S       &   System.Windows.Controls.DataGrid  5V      $��1��6   $��	16,8,16,8q�5W         AutoGenerateColumns$ False��5Z      	   
IsReadOnly$
	 True��5[      
   HeadersVisibility$
 Column=�5\         SelectionMode$ Single=�5]      $��1q�5^      ���#CCCCCC   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5`         ColumnHeaderStyle# [ 5a         	CellStyle# [ 5b      $
��True��5c      $
��Auto=�5d      $
��Auto=�5e      $	��220��5f      ��5X      �� *AvailableSessions+ �� SelectedItem 5Y      �� *SelectedSession+ �� Mode$ TwoWay=�   Columns 5g      0  *System.Windows.Controls.DataGridTextColumn  5h       	 sessionDetails,  &System.Windows.Controls.DataGridColumn   Header# � 	 61      Width$ 2* 5j   1   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 5i   1   �� *SessionDetails+ 	  ElementStyle 5k   "   �� 5l   &   " ��6,    �� BasedOn# [ 6C   5n        5p       
 trainer# � 
 61   $ * 5r   1    5q   1   �� *TrainerName+ 5s   "   �� 5t   &   " ��6,   # [ 6C   5v        5x        specilization# �  61   $ * 5z   1    5y   1   �� *TrainerSpecialization+ 5{   "   �� 5|   &   " ��6,   # [ 6C   5~        5�        hall# �  61   $
 0.8* 5�   1    5�   1   �� *
HallName+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�        hallLocation# �  61   $ * 5�   1    5�   1   �� *HallLocation+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�      4
  .System.Windows.Controls.DataGridTemplateColumn
  5�       
 status# �  65   $
 0.8* 6W    
  CellTemplate 5�   "   �� 5�   &   .R�5�   *   ��   PrimaryHueMidBrush#���  5�   4    �� Padding$	 5,0q�5�   4   $��NoWrap=�5�   4    �� TextAlignment$ Center=�5�   4   $��Center=�5�   4   ��5�   4   �� *
IsReserved+ �� 	Converter# [ 5�   4   5�   '   5�      5�      �� 5�      $��2��6!    �� Orientation$ 
Horizontal=�5�   !   $��Center=�5�   !   $��
16,8,16,16q�5�   !   .+�5�      ��   MaterialDesignRaisedButton#��[ 6!     
reserveBtn#���  5�   !   $��0,0,8,0q�5�   !   ��5�   !   �� *ReserveSessionCommand+{�5�   !   �� *
CanReserve+5�   !   �� 5�         MaterialDesignOutlinedButton#��[ 6!     	cancelBtn#���  5�   !   ��5�   !   �� *CancelReservationCommand+{�5�   !   �� *	CanCancel+5�      5�      5�        5�      $��1��6"   $��
16,8,16,16q�6/   .��5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $��0��6      myReservations#���  5�       #��[ 5�       $��18(�5�       $��
16,16,16,8q�5�         5�      $��1��6   $��
16,8,16,16q�5�      $ False��5�      $
	 True��5�      $
 Column=�5�      $��1q�5�      ���#CCCCCC$ SizeToHeader 5�      # [ 5�      # [ 5�      $
��True��5�      $
��Auto=�5�      $
��Auto=�5�      ��5�      �� *MyReservations+ 5�        5�      # � 	 61   $ 2* 5�   1    5�   1   �� *SessionDetails+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      # � 
 61   $ * 5�   1    5�   1   �� *TrainerName+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      # �  61   $ * 5�   1    5�   1   �� *
HallName+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      # �  61   $ * 5�   1    5�   1   �� *HallLocation+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�      5�      5�      5�      5�      � 5�   
   $��2��6   ���	#80000000 K� ZIndex$
 1000��5�      y�5�      �� *	IsLoading+# [  .+�5�      0   *Projekat_A.UserControls.LoadingUserControl  $��Center=�6'   $��Center=�5�   '   5�      5�      5�      