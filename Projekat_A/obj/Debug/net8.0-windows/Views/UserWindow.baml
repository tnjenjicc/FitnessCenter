   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Projekat_A.ViewsProjekat_A.Views  5   	   !    Projekat_A.Views.UserWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   *localclr-namespace:Projekat_A.Views  5   	     � Title$  
UserWindow��5	   	   $	��700��6   $
��1140��6)      MaterialDesignBody#���   5
   	    � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	     AppFont#���  5   	   ��5   	   Z� .��5      #  MaterialDesignThemes.Wpf.Card    MaterialDesignPaper#���  6   $��10q�6M   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp4=�6Y      UniformCornerRadius$ 15Q�6�   .��5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      (  "MaterialDesignThemes.Wpf.ColorZone  $��0��6'   $��Stretch=�67   #���  6S   	   CornerRadius$	 15 0 0 0��6�   .��5      �� .+�5       � $��	0 10 5 60q�6    $
��Left=�63   .+�5!      ��   welcome#���  6$   $��20(�6E   $
��Bold"�6S   $
��Left=�6e   $��Center=�6�   $��10 0 0 0q�6�   �� 5"      $��20(�6>   $
��Bold"�6L   $��Center=�6^   $��Center=�6{   $��10 0 0 0q�6�   ��6$   �� *
Username+6�   5#      �� 5%      ��6#   �� *Menu+��5&      �� 5'      .R�5(   "   ��  ! MaterialDesignFlatMidBgButton#��[ 6)   $��40��6|   $��5 15 5 0q�6�   ��6`   �� *	Command+.��5)   &   �� 
 �� Orientation$
 
Horizontal=�61   .+�5*   *   '  !MaterialDesignThemes.Wpf.PackIcon  $��24��6X   $��24��6c   $��0 0 10 0q�6o      Kind 6B   �� *Icon+6o   �� 5+   *   $��Center=�6J   ��64   �� *Text+6J   5,   '   5-   #   5.      50      51      53      �� 56      $��1��6   $��2��6-   ��6>   �� *Navigation.CurrentView+58      59      5:      