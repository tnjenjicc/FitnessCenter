   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projekat_A.UserControlsProjekat_A.UserControls  5      1clr-namespace:Projekat_A.UtilProjekat_A.Util  5      7    1Projekat_A.UserControls.ViewMembershipUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Projekat_A.UserControls     5      .utilclr-namespace:Projekat_A.Util     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � 	Resources  5      1   +Projekat_A.Util.DateOnlyToDateTimeConverter   DateOnlyToDateTimeConverter9   3Projekat_A.Util.InverseBooleanToVisibilityConverter ' #InverseBooleanToVisibilityConverter   BooleanToVisibilityConverter  CenteredColumnHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  CenteredCellStyle*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels  CenteredTextBlockStyle��%=  &	        &	      &	 &     &	 9     &	 $    &	 W      5   
   6+     5   
   63   �� 5   
   6'   �� 5   
   "  62   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� �#F0F0F06+   �� 5       � ��$ 5q�6(   �� 5       � ��$ SemiBold"�6+   5      �� 5   
   "  6*   .�5      ��  � ��$ 5q�6(   �� 5       � �� 5      �� 5      "  6&   .R�5      �� # z��6!   #��z��5   !   #��z��5   !   #��z��5    !   $
 True��5!   !   ._�5"      �� $��Center=�6/   $��Center=�6L   5#      5$      5&      5'      �� 5)   
   " ��6/   .�5*      ��  � ��$ Center=�64   �� 5+       � ��$ Center=�62   �� 5,       �@��TextAlignment$ Center=�6.   5-      5.      .��5/      �  � RowDefinitions 50   
   �� 51      $
��Auto �6   �� 52      $��* �6   �� 53      $
��Auto �6   .+�57   
   �� $��0��6     viewMemberships#���  58      $��32(�59      $
��Bold"�5:      $��Center=�5;       $  MaterialDesignHeadline4TextBlock#��[ 5<      $��16q�5=      � 5@   
   $��1��6    � ColumnDefinitions 5A      �� 5B      $��* �6#   �� 5C      $
��Auto �6#   .+�5G      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��	16,0,8,16q�62   .��5H      �  5I      �� 5J      $
��Auto �6(   �� 5K      $��* �6(   .+�5N      �� $��0��6      currentMemberships#���  5O        $	  MaterialDesignHeadline6TextBlock#��[	 5P       $��18(�5Q       $��
16,16,16,8q�5R       &   System.Windows.Controls.DataGrid  5T      $��1��6   	   AutoGenerateColumns$	 False��5W      
   
IsReadOnly$

 True��5X      $��
16,8,16,16q�5Y      $��Stretch=�5Z      "   VerticalScrollBarVisibility$
 Auto=�5[      $   HorizontalScrollBarVisibility$ Disabled=�5\         HeadersVisibility$ Column=�5]      $��1q�5^      ���#CCCCCC   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5`         ColumnHeaderStyle# [ 5a         	CellStyle# [ 5b      $
��True��5c      $
��Auto=�5d      $
��Auto=�5e      ��5U      �� *Memberships+ �� SelectedItem 5V      �� *SelectedMembership+   Columns 5f      0  *System.Windows.Controls.DataGridTextColumn  5g       
 membershipType,	  &System.Windows.Controls.DataGridColumn 	  Header# � 
 61    	  Width$ * 5i   1   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5h   1   �� *MembershipTypeName+ 
  ElementStyle 5j   "   �� 5k   &   " ��6,    �� BasedOn# [ 6C   5m        5n       	 price# �  61   $ * 5p   1    5o   1   �� *Price+ �� StringFormat$ {0:F2}�� 5q   "   �� 5r   &   " ��6,   # [ 6C   5t        5u        
expiryDate# �  61   $ * 5w   1    5v   1   �� *ExpirationDate+$ {0:dd.MM.yyyy.}�� 5x   "   �� 5y   &   " ��6,   # [ 6C   5{        5|       
 status# �  61   $ * 5~   1    5}   1   �� *Status+ 5   "   �� 5�   &   " ��6,   # [ 6C   5�      5�      5�      5�        5�      $��1��6"   $��	8,0,16,16q�5�   !   $	��300��5�   !   y�5�   !   �� *IsAddingMembership+ �� 	Converter# [ .��5�      � $��16q�6    5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6      addNewMembership#���  5�       #��[	 5�       $��18(�5�       $��0,0,0,16q�5�       �� 5�      $��1��6     selectMembershipType)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5�       �� DisplayMemberPath$
 Name��5�      $��0,8,0,16q�5�      ��5�      �� *MembershipTypes+ 5�      �� *SelectedMembershipType+5�      (  "System.Windows.Controls.DatePicker  5�      $��2��6!   # �  5�   !    $  MaterialDesignOutlinedDatePicker#��[ 5�   !   $��0,0,0,16q�5�   !      SelectedDate 5�   !   �� *SelectedExpirationDate+# [  5�   !   �� 5�      $��3��6!    �� Orientation$ 
Horizontal=�5�   !   $��Right=�5�   !   $��0,16,0,0q�5�   !   .+�5�      ��    MaterialDesignOutlinedButton#��[ 6!    
 cancel#���  5�   !   $��0,0,8,0q�5�   !   ��5�   !   �� *CancelAddCommand+5�   !   �� 5�        MaterialDesignRaisedButton#��[ 6!     save#���  5�   !   ��5�   !   �� *SaveMembershipCommand+5�   !   5�      5�      5�      5�      �� 5�   
   $��2��6   $��Right=�5�       & "MaterialDesignFloatingActionButton#��[ 5�      $��	0,0,16,16q�5�      ��5�      �� *ShowAddMembershipCommand+y�5�      �� *IsAddingMembership+# [ .��5�      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6&   $��24��62   $��24��6>   5�      5�      5�      