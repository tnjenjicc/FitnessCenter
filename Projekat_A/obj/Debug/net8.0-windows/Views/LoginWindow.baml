   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Projekat_A.UserControlsProjekat_A.UserControls  5   	   3clr-namespace:Projekat_A.ViewsProjekat_A.Views  5   	   "    Projekat_A.Views.LoginWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4uc%clr-namespace:Projekat_A.UserControls     5   	   0localclr-namespace:Projekat_A.Views     5   	     � Title$  Fitness Centar��5
   	   $	��600��6    $	��800��6-      MaterialDesignBody#���   5   	    � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ��5   	   Z� .��5      #  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 20Q�6     MaterialDesignPaper#���  5      $��25q�5      .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp5=�5      .��5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6    �� CornerRadius$ 	20,0,0,20��5      ��5      �� 5       �� 
StartPoint$	 0,0&�6*   	 �� EndPoint$		 1,1&�6;   .��5      � 
 � Color$
 #E91E63��6'    � Offset$	 0.0Q�67   � 5       $
 #F8BBD9��6'   $	 1.0Q�67   5!      ._�5$      � .+�5%      �� $��Center=�6!   $��Center=�6<   .+�5&      A� $	��180��6"   $	��180��6.   
�������6;   �� 5(      $	��100��6!   $	��100��6-   $��Center=�6:   $��
0,-140,0,0q�6W   .+�5)      F� ������9�� р�O��� ѐ�9 � �@fP�� Ѱ�� ��j � � pV4 �I� �� � w� pV4 �0�u �� Q��T ���(\�@����(\�@�Y) Q�Y) ���(\�@����(\�@��T р� 0�u �pV4  w� р� I� �pV4  � ���j ��� �P�� @f� � ��9���� ��O��� ��9�А� ��O�08 
/� 
/�D��D 
/� 
/08р�OА� ѐ�9�� 	���#E91E63 �� Stretch$ Uniform=�5*   4    �� Left$ 0��5+   %   
 �� Top$ -20.745��5,   %   $	��100��5-   %   $	��111��5.   %   $��Center=�62   $	��Top=�6O   5/      �� 51        fitness#���  6$   $��
Montserrat)�52   "   $
��Bold"�53   "   $��28(�54   "   
�������$��Center=�56   "   $��0,40,0,0q�57   "    J� Effect 58      3  -System.Windows.Media.Effects.DropShadowEffect  59   "      
BlurRadius$ 3Q�63      	Direction$	 320Q�6B      Color$ #AD1457��6R      ShadowDepth$ 2Q�6b      Opacity$	 0.6Q�6r   5;      �� 5=       	 qoute#���  6$   $��
Montserrat)�5>   "   $��Italic$�5?   "   $��16(�5@   "   
������� J� Opacity$	 0.9Q�5B   "   $��Center=�5C   "   $��0,10,0,0q�5D   "   5E      �� 5G       �� Orientation$ 
Horizontal=�6!   $��Center=�5H      $��Bottom=�5I      $��0,0,0,30q�5J      .+�5K      �� -   5         ��Name$ srButton��5K   !     MaterialDesignFlatButton#��[ 5L      $��60��5M      $��40��5N      $��0,0,10,0q�5O      $��SR��5P      
 � Tag$ SR��5Q      
�������
�������$��1q�5U      �� 5V      -   5        $ enButton��5V   !   #��[ 5W      $��60��5X      $��40��5Y      $��10,0,0,0q�5Z      $��EN��5[      $ EN��5\      
�������
�������$��1q�5`      5a      5b      5c      )  #MaterialDesignThemes.Wpf.DialogHost  5e      -   5          Name$ 
DialogHost��5g   &   $��1��5e   (      CloseOnClickAway$
 True��5f   &   .��5h      � .+�5i      '  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�6.   $	��Top=�5j   ,   $��	0,20,20,0q�5k   ,      PlacementMode$ BottomAndAlignRightEdges=�5l   ,      	StaysOpen$ False��5m   ,      PopupContent. 5n      �� $��
16,10,0,16q�6%   $ Vertical=�69   $��Center=�6P   .+�5o      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5p   "   �� $��Center=�6,     darkBlck#���  6G   d� 5q   "   -   5         d�Name$ ThemeToggle��5t   +    � Cursor$
 Hand��5q   /     	enableBtn  � ToolTip#  �  5r   +   $��12,0,8,0q�5s   +   ���#E91E63���#E91E63+  %MaterialDesignThemes.Wpf.RippleAssist!   Feedback! �#E91E63"   RippleSizeMultiplier$	" 0.5Q�5z   +   1  +MaterialDesignThemes.Wpf.ToggleButtonAssist#   SwitchTrackOnBackground$# #E91E63��5{   +   $ d� 	IsChecked$ 5u   +   �� *isDarkTheme+��5v   +   �� *DarkThemeCommand+5{   +   5|      �� 5}      -   5        $ btn_exit��5~   #   $  Close Application��5}   %     exitBtn#���  5   #   $��Center=�5�   #   ��5�   #   �� *CloseApplicationCommand+5�   #   5�      5�      �� 5�      $��Center=�6!   $
��30,0q�6<   .+�5�      �� $��0,0,0,10q�6$   $��Center=�5�   "   $��32(�5�   "   $
��Bold"�5�   "     titleStr#���  5�   "   $��
Montserrat)�5�   "   �� 5�      $��Medium"�6$   $��18(�5�   "   $��Center=�5�   "    	 logginTxtBlock#��� 	 5�   "   $��0,0,0,30q�5�   "   $	 0.7Q�5�   "   0   *Projekat_A.UserControls.LoadingUserControl  5�      % K� ZIndex$% 99��60   $��Center=�5�   !   $��Center=�5�   !   $��	0,10,0,10q�5�   !   � 5�      $	��320��6   $��0,15,0,0q�6+   .+�5�      '	  !MaterialDesignThemes.Wpf.PackIcon	  & 	  Kind$& Account=�66   $��Center=�5�   4   $��0,0,0,0q�5�   4    
 MaterialDesignBodyLight#��� 
 5�   4   �� 5�      -   5        ' ��Name$' txtUsername��5�   &   $��30,0,0,0q�5�   $   $��18(�5�   $     enterUsernameBox)
  #MaterialDesignThemes.Wpf.HintAssist( 
  Hint#( �  5�   $   $��0,0,0,2q�5�   $     MaterialDesignDivider#���  5�   $     MaterialDesignTextBox#��[ 5�   $   .  (MaterialDesignThemes.Wpf.TextFieldAssist)   UnderlineBrush) �#E91E63��5�   $   �� *
Username+5�      � 5�      $	��320��6   $��0,25,0,0q�6+   .+�5�      	  $
& Lock=�66   $��Center=�5�   4   $��0,0,0,0q�5�   4   #��� 
 5�   4   G� 5�      -   5        * G�Name$* txtPassword��5�   *   $��30,0,0,0q�5�   (   $��18(�5�   (     enterPasswordBox#( �  5�   (   $��0,0,0,2q�5�   (   #���  5�   (     MaterialDesignPasswordBox#��[ 5�   (   ) �#E91E635�   (   5�      �� 5�      -   5        $ 	logginBtn��5�   !   $��0,40,0,0q�5�   !   $��55��5�   !   $	��320��5�   !   +  %MaterialDesignThemes.Wpf.ButtonAssist+   CornerRadius$+ 25��5�   !   $��18(�5�   !   $
��Bold"�5�   !   ���#E91E63���#E91E63
�������  logginButton#���  5�   !   ��5�   !   �� *LoginCommand+ 5�        5�   "   $ 15Q�63   $	 270Q�5�   3   $ #E91E63��5�   3   $ 5Q�5�   3   $	 0.3Q�5�   3   5�      �� 5�      -	   5        $ 	signupBtn��5�      $��0,20,0,0q�5�   !   #��[ 5�      $��55��5�      $	��320��5�      $+ 25��5�      $��18(�5�      $
��Bold"�5�      ���#E91E63  createAccountBtn#���  5�      ��5�      �� *RegisterCommand+5�      5�      5�      5�      5�      5�      