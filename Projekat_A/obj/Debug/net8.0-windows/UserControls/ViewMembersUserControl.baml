   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Projekat_A.UtilProjekat_A.Util  5      4    .Projekat_A.UserControls.ViewMembersUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      (utilclr-namespace:Projekat_A.Util  5        � 	Resources  5
      9   3Projekat_A.Util.InverseBooleanToVisibilityConverter '  #InverseBooleanToVisibilityConverter   BooleanToVisibilityConverter  CenteredColumnHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  CenteredCellStyle*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels  CenteredTextBlockStyle��%   &	        &	      &	 &     &	     &	 D      5   
   63   �� 5   
   6'   �� 5   
   "  62   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� �#F0F0F06+   �� 5       � ��$ 5q�6(   �� 5       � ��$ SemiBold"�6+   5      �� 5   
   "  6*   .�5      ��  � ��$ 5q�6(   �� 5       � �� 5      �� 5      "  6&   .R�5      �� # z��6!   #��z��5   !   #��z��5   !   #��z��5   !   $
 True��5    !   ._�5!      �� $��Center=�6/   $��Center=�6L   5"      5#      5%      5&      �� 5(   
   " ��6/   .�5)      ��  � ��$ Center=�64   �� 5*       � ��$ Center=�62   �� 5+       �@��TextAlignment$ Center=�6.   5,      5-      .��5/      �  � RowDefinitions 50   
   �� 51      $
��Auto �6   �� 52      $��* �6   �� 53      $
��Auto �6   .+�56   
   �� $��0��6     viewMembers#���  57      $��32(�58      $
��Bold"�59      $��Center=�5:       $  MaterialDesignHeadline4TextBlock#��[ 5;      $��16q�5<      � 5?   
   $��0��6   $��16q�6    5@      �� 5A      $
��Auto �6    �� 5B      $
��Auto �6    �� 5C      $
��Auto �6    .+�5G      �� $��0��6   $�� ��5H      #��[ 5I        PrimaryHueMidBrush#���  5J      �� 5M      $��1��6      MaterialDesignBody1TextBlock#��[ 5O      $
��Wrap=�5P      $��0,8,0,0q�5Q      ��5N      �� *GroupDescription+5Q      � 5T      $��2��6   $��0,8,0,0q�6     � ColumnDefinitions 5U      �� 5V      $��* �6'   �� 5W      $
��Auto �6'   .+�5Z      �� $��0��6     	 MaterialDesignBody2TextBlock#��[	 5[      �� 5\       
 trainerGroup#�� 
 6!    �� 5]      #���  6>   �6!   �� *TrainerName+6>   5^      �� 5`      $��1��6   #��[	 5a      �� 5b        numOfMembers#��  6    �� 5c      #���  6>   �6   �� *CurrentMemberCount+6>    �� 5d      $�/��6    �� 5e      �6   �� *MaxMemberCount+5f      5g      5h      � 5k   
   $��1��6    5l      �� 5m      $��* �6#   �� 5n      $
��Auto �6#   .+�5r      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��	16,0,8,16q�62   .��5s      �  5t      �� 5u      $
��Auto �6(   �� 5v      $��* �6(   �� 5w      $
��Auto �6(   .+�5z      �� $��0��6    #���  5{       $��18(�5|        $  MaterialDesignHeadline6TextBlock#��[ 5}       $��
16,16,16,8q�5~       &   System.Windows.Controls.DataGrid  5�      $��1��6   	   AutoGenerateColumns$	 False��5�      
   
IsReadOnly$

 True��5�      $��
16,8,16,16q�5�      $��Stretch=�5�      "   VerticalScrollBarVisibility$
 Auto=�5�      $   HorizontalScrollBarVisibility$ Disabled=�5�         HeadersVisibility$ Column=�5�      $��1q�5�      ���#CCCCCC   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5�         ColumnHeaderStyle# [ 5�         	CellStyle# [ 5�      $
��True��5�      $
��Auto=�5�      $
��Auto=�5�      ��5�      �� *GroupMembers+ �� SelectedItem 5�      �� *SelectedGroupMember+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�        name,  &System.Windows.Controls.DataGridColumn   Header# �  61      Width$ * 5�   1   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 5�   1   �� *Name+ 	  ElementStyle 5�   "   �� 5�   &   " ��6,    �� BasedOn# [ 6C   5�        5�        surname# �  61   $ * 5�   1    5�   1   �� *	Surname+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�        	birthDate# �  61   $ * 5�   1    5�   1   �� *DateOfBirth+ �� StringFormat$ {0:dd.MM.yyyy}�� 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      $ Upisan��61   $ * 5�   1    5�   1   �� *DateOfEnrollment+$ {0:dd.MM.yyyy}�� 5�   "   �� 5�   &   " ��6,   # [ 6C   5�      5�      �� 5�      $��2��6     MaterialDesignRaisedButton#��[ 5�       
 remove#���  5�      $��
16,0,16,16q�5�      $��Center=�5�      ��5�      �� *RemoveMemberCommand+y�5�      �� *HasSelectedMember+ �� 	Converter# [ 5�      5�      5�        5�      $��1��6"   $��	8,0,16,16q�5�   !   $	��350��5�   !   y�5�   !   �� *IsAddingMember+# [ .��5�      � $��16q�6    5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6      addNewMember#���  5�       #��[ 5�       $��0,0,0,16q�5�         5�      $��1��6   $	 False��5�      $

 True��5�      $ Column=�5�      $��1q�5�      ���#CCCCCC$ SizeToHeader 5�      # [ 5�      # [ 5�      $
��True��5�      $
��Auto=�5�      $
��Auto=�5�      ��5�      �� *AvailableMembers+ 5�      �� *SelectedAvailableMember+ 5�        5�      # �  61   $ * 5�   1    5�   1   �� *Name+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      # �  61   $ * 5�   1    5�   1   �� *	Surname+ 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�      # �  61   $ * 5�   1    5�   1   �� *DateOfBirth+$ {0:dd.MM.yyyy}�� 5�   "   �� 5�   &   " ��6,   # [ 6C   5�      5�      �� 5�      $��2��6!    �� Orientation$ 
Horizontal=�5�   !   $��Center=�5�   !   $��0,16,0,0q�5�   !   .+�5�      ��    MaterialDesignOutlinedButton#��[ 6!    
 cancel#���  5�   !   $��0,0,8,0q�5�   !   ��5�   !   �� *CancelAddCommand+5�   !   �� 5�      #��[ 6!     add#���  5  !   ��5   !   �� *AddMemberCommand+5  !   5     5     5     5     �� 5  
   $��2��6   $��Right=�5	      & "MaterialDesignFloatingActionButton#��[ 5
     $��	0,0,16,16q�5     ��5     �� *ShowAddMemberCommand+y�5     �� *IsAddingMember+# [  .��5     '
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$
 Plus=�6&   $��24��62   $��24��6>   5     5     5     