   M S B A M L   `   `   `  ����@  <HW-10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HW_10HW_10  5   	       HW_10.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:HW_10     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ longList��5      $��0��5      $��Stretch=�5      $��5q�5      $��Stretch=�5      ��5      �� 5      .R�5      �� 
���   �$��5q�61   $��1q�6<    �� CornerRadius$ 10��6P   ._�5      �� $��10q�6%   .+�5      �� $��0q�6C   $��15(�6N   ��6(   �� *	FirstName+6N   �� 5      $��0q�6=   $��20(�6H   ��6(   �� *Msg+6H   �� 5      $��10q�6>   $��10(�6J   
������� �� TextAlignment$ Center=�5   >   ��5   (   �� *Time+5   >   5      5      5      5       �� 5"   
   $��1��6   $��20(�5#      $��Send message��5$      $��Stretch=�5%      $��5q�5&      $��Stretch=�5'      .��5)      �� .+�5*      �� -   5         ��Name$ 
TargetSend��5*      $y�	Collapsed=�5,      $��20(�5-      $��5q�6'   ��5*   0   ��  �� ElementName$ longList�� �� Path$ SelectedItem.Id�5-   '   �� 5.      -   5         ��Name$ 
txtMsgText��5.      $��20(�6.   $��5q�6<   �� 5/      -   5        	 ��Name$	 
btnMsgSend��5/      $
��50,5q�6-   $��20(�6;   $
��Send��50      6(   �� 51      -   5        $	 btnHistorySave��51      $
��50,5q�61   $��20(�6?   $
��Save��52      6(   53      54      55      56      