   M S B A M L   `   `   `  ����I  EDemoHttpClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DemoHttpClientDemoHttpClient  5   	       DemoHttpClient.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DemoHttpClient     5   	     � Title$  Using HttpClient��5   	    � WindowStartupLocation$ CenterScreen=�5	   	   $	��500��6.   $	��400��6:   .��5
      � 
����ح� � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   .+�5   
   �� $��0��6   $��Stretch=�6"   $��Stretch=�6>    �� Orientation$ Vertical=�6\   .+�5      �� -   5        $��labelURL��5      $	��URL��6$   �� 5      -   5        $��txtURL��5      $��http://www.contoso.com��6$   $��30��6B   �� 5      -   5        $��labelSource��5      $��HTML Source��6(   5      �� 5   
   -   5        $��	txtSource��5      $��1��6    �� TextWrapping$
 Wrap=�61   $ �� HorizontalScrollBarVisibility$
 Auto=�6E    �� AcceptsReturn$
 True��6j   " �� VerticalScrollBarVisibility$ Visible=�6   �� 5   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   $	��Top=�6X   .+�5      �� -   5         ��Name$ btnView��5      $
��25,5q�6&   $��90��64   $��	View HTML��6?   6S   �� 5      -   5        $ btnClear��5      $
��25,5q�6'   $��90��65   $��Clear��6@   6P   �� 5      -   5        $ btnClose��5      $
��25,5q�6'   $��90��65   $��Close��6@   6P   5      5      5      