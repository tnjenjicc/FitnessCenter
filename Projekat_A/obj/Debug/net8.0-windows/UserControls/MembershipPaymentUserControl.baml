   M S B A M L   `   `   `  ����E  AProjekat_A, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Projekat_A.UtilProjekat_A.Util  5      :    4Projekat_A.UserControls.MembershipPaymentUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      (utilclr-namespace:Projekat_A.Util  5        � 	Resources  5      9   3Projekat_A.Util.InverseBooleanToVisibilityConverter '  #InverseBooleanToVisibilityConverter   BooleanToVisibilityConverter.   (Projekat_A.Util.ObjectToBooleanConverter  ObjectToBooleanConverter  CenteredColumnHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  CenteredCellStyle*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels  CenteredTextBlockStyle  PriceTextBlockStyle �� BasedOn  PrimaryHueMidBrush��%�  &	        &	      &	 &     &	 9     &	 $    &	 W    &	     7    5   
   63   �� 5   
   6'     5   
   6(   �� 5   
   "  62   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� �#F0F0F06+   �� 5       � ��$ 5q�6(   �� 5       � ��$ SemiBold"�6+   5      �� 5   
   "  6*   .�5      ��  � ��$ 5q�6(   �� 5       � �� 5      �� 5      "  6&   .R�5      �� # z��6!   #��z��5   !   #��z��5   !   #��z��5    !   $
 True��5!   !   ._�5"      �� $��Center=�6/   $��Center=�6L   5#      5$      5&      5'      �� 5)   
   " ��6/   .�5*      ��  � ��$ Center=�64   �� 5+       � ��$ Center=�62   �� 5,       �@��TextAlignment$ Center=�6.   5-      �� 5/   
   " ��6,   8   .�50      ��  � ��$ SemiBold"�6+   �� 51       � ��# �  6+   52      53      .��55      �  � RowDefinitions 56   
   �� 57      $
��Auto �6   �� 58      $��* �6   �� 59      $
��Auto �6   .+�5<   
   � $��0��6   $��16q�6    5=      �� 5>      $
��Auto �6    �� 5?      $
��Auto �6    .+�5B      �� $��0��6     membershipPayment#���  5C      $��32(�5D      $
��Bold"�5E      $��Center=�5F       $	  MaterialDesignHeadline4TextBlock#��[	 5G      $��0,0,0,8q�5H      #  MaterialDesignThemes.Wpf.Card  5J      $��1��6"   $
��16,8q�5K       
 MaterialDesignPaper#��� 
 5L      $y�Visible=�5M      .��5N      �� $��Center=�6   $��18(�5O      $��SemiBold"�5P      �� 5Q        	totalDebt#��  6    �� 5R        ValidationErrorBrush#���  5S      �5R      �� *	TotalDebt+	 �� StringFormat$	 {0:C}��5S      5T      5U      5V      � 5X   
   $��1��6   
 � ColumnDefinitions
 5Y      �� 5Z      $��2* �6#   �� 5[      $��* �6#   .+�5^        $��0��6"   $��	16,0,8,16q�62   .��5_      �  5`      �� 5a      $
��Auto �6(   �� 5b      $��* �6(   �� 5c      $
��Auto �6(   .+�5f      �� $��0��6      unpaidBills#���  5g       $��18(�5h        $  MaterialDesignHeadline6TextBlock#��[ 5i       $��
16,16,16,8q�5j       � 5l      $��1��6"    % !MaterialDesignCircularProgressBar#��[ 5m   "    � Value$ 0Q�5n   "   $��40��5o   "   $��40��5p   "    � IsIndeterminate$
 True��5q   "   $��Center=�5s   "   $��Center=�5t   "   y�5r   "   �� *	IsLoading+ �� 	Converter# [ 5t   "   &   System.Windows.Controls.DataGrid  5v      $��1��6      AutoGenerateColumns$ False��5y         
IsReadOnly$
 True��5z      $��	16,8,16,8q�5{      $��Stretch=�5|      "   VerticalScrollBarVisibility$
 Auto=�5}      $   HorizontalScrollBarVisibility$ Disabled=�5~         HeadersVisibility$ Column=�5      $��1q�5�      ���#CCCCCC   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5�         ColumnHeaderStyle# [ 5�         	CellStyle# [ 5�      $
��True��5�      $
��Auto=�5�      $
��Auto=�5�      ��5w      �� *UnpaidPayments+ �� SelectedItem 5x      �� *SelectedPayment+y�5�      �� *	IsLoading+# [     Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�        membershipType,	  &System.Windows.Controls.DataGridColumn 	  Header# �  61    	  Width$ * 5�   1   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5�   1   �� *FDMembershipIdMembershipNavigation.MembershipTypeIdTypeNavigation.Name+ 
  ElementStyle 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�        paymentDate# �  61   $ * 5�   1    5�   1   �� *BillingDate+$	 {0:dd.MM.yyyy}�� 5�   "   �� 5�   &   " ��6,   # [ 6C   5�        5�       	 price# �  61   $ * 5�   1    5�   1   �� *Price+$	 {0:C}�� 5�   "   �� 5�   &   " ��6,   # [ 6C   5�      5�      5�      5�        5�      $��1��6"   $��	8,0,16,16q�5�   "   y�5�   "   �� *HasSelectedPayment+# [ .��5�      � $��16q�6    5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $��* �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6      paintmentDetails#���  5�       $��18(�5�       #��[ 5�       $��0,0,0,16q�5�       � 5�      $��1��6   $��0,0,0,16q�6(    5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      �� $��0��6$     paintmentMethod#���  5�   $      MaterialDesignBody1TextBlock#��[ 5�   $   $��0,0,0,8q�5�   $   �� 5�      $��1��6#     MaterialDesignComboBox#��[ 5�   #     selectMethod)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5�   #   ��5�   #   �� *PaymentMethods+ 5�   #   �� *SelectedPaymentMethod+5�   #   5�      � 5�      $��2��6   $��0,0,0,16q�6(    5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      �� $��0��6$     priceInf#���  5�   $   #��[ 5�   $   $��0,0,0,8q�5�   $   �� 5�      $��1��6$   �� 5�        originalPrice#��  6"    �� 5�      $��SemiBold"�5�   "   �5�   "   �� *OriginalPrice+$	 {0:C}��5�   "   5�      �� 5�      $��2��6$   $��16(�5�   $   $
��Bold"�5�   $   #���  5�   $   �� 5�        
finalPrice#��  6"    �� 5�      �6"   �� *
FinalPrice+$	 {0:C}��5�      5�      � 5�      $��3��6   $��0,0,0,16q�6(   y�5�      �� *HasAvailablePromotions+# [  5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      �� $��0��6$     availablePromotions#���  5�   $   #��[ 5�   $   $��0,0,0,8q�5�   $   �� 5�      $��1��6#    �� DisplayMemberPath$ Description��5�   #   #��[ 5�   #     selectPromotion# �  5�   #   $��0,0,0,8q�5�   #   ��5�   #   �� *AvailablePromotions+ 5�   #   �� *SelectedPromotion+5�   #   �� 5�      $��2��6!      MaterialDesignOutlinedButton#��[ 5�   !   $��Clear Promotion��5�   !   $
��Left=�5   !   ��5�   !   �� *ClearPromotionCommand+y�5  !   �� *SelectedPromotion+# [ 5       5     $��4��6*   $��12q�5  *      MaterialDesignChipBackground#���  5  *   $��0,0,0,16q�5  *   y�5  *   �� *SelectedPromotion+# [ .��5	     �� .+�5
     �� $��SemiBold"�5  (   $
��Wrap=�5  (   ��5
  (   �� *SelectedPromotion.Description+5  (   �� 5     $��0,4,0,0q�6(   �� 5  "     discount#��  6&    �� 5  "   $��SemiBold"�5  &      SecondaryHueMidBrush#���   5  &   �5  &   �� *SelectedPromotion.Discount+$
	 {0}%��5  &   5     �� 5     $��0,2,0,0q�6(   $��12(�5  (    J� Opacity$	 0.7Q�5  (   �� 5  "    ! 
validUntil#�� ! 6&    �� 5  "   �6&   �� *SelectedPromotion.ValidUntil+$	 {0:dd.MM.yyyy}��5     5     5     �� 5     $��5��6   �� 5     $��6��6    " MaterialDesignRaisedButton#��[" 5     #���  5!     $��Stretch=�5"     $��48��5#     $��16(�5$     $��SemiBold"�5%     ��5      �� *ProcessPaymentCommand+.��5&     ��  �� Orientation$ 
Horizontal=�6%   .+�5'     '  !MaterialDesignThemes.Wpf.PackIcon      Kind$  
CreditCard=�66   $��Center=�5(  6   $��0,0,8,0q�5)  6   �� 5*     $��Center=�6(   �� 5+  "    # pay#�� # 6&    �� 5,  "   �6&   �� *
FinalPrice+$	 {0:C}��5-     5.     5/     50     51     52       54  
   $��2��6   $��2��6,   $��
16,8,16,16q�55     y�56     �� *HasPaymentHistory+# [ .��57     9�  $ paymentHistory#��� $ 6   $��18(�58      % MaterialDesignExpander#��[% 59     $��8q�5:     .��5;       $ False��5<     $
 True��5=     $��8q�5>     $ Column=�5?     # [ 5@     # [ 5A     $	��200��5B     ��5;     �� *PaymentHistory+ 5C       5D     # �  6-   $ * 5F  -    5E  -   �� *FDMembershipIdMembershipNavigation.MembershipTypeIdTypeNavigation.Name+ 5G     �� 5H  "   " ��6(   # [ 6?   5J       5K     # �  6-   $ * 5L  n    6-   �� *BillingDate+$	 {0:dd.MM.yyyy}�� 5M     �� 5N  "   " ��6(   # [ 6?   5P       5Q      & 
amountPaid# � & 6-   $ * 5S  -    5R  -   �� *Price+$	 {0:C}�� 5T     �� 5U  "   " ��6(   # [ 6?   5W       5X     # �  6-   $ * 5Z  -    5Y  -   �� *PaymentMethod+ 5[     �� 5\  "   " ��6(   # [ 6?   5^       5_      
' status# � ' 6-   $ * 5`  -    5a     �� 5b  "   " ��6(   # [ 6?   .�5c  &   ��  � �� ( paid# � ( 6=   �� 5d  &    � ��# �  6C   �� 5e  &    � ��$ SemiBold"�6C   5f  #   5h     5j     5k     5l     5m     5n     