   M S B A M L   `   `   `  ���� K  GWebsiteExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WebSiteExtractorWebSiteExtractor  !    WebSiteExtractor.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:WebSiteExtractor       � Title$  Websites Extractor��$	��675��$
��1200�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�.���  � ColumnDefinitions �� ��  � RowDefinitions �� �� .+��� $��0��$��0��$��1q�
���   �._��� -   $��	InputText���� $��1��$��0��$��1q�
���   �._��� -   $��
OutputText��� $��0��$��1�� �� $��* ��� $��4* �.+�� $��0�� �� $
��2.4* ��� $��2* �.+��� $��0��$��1q�
���   �._��  �� $
��2.3* ��� $
��2.8* ��� $
��1.5* �.+��� -   $��ExtractWebsitesButton��$��0��$��80��$��40��$��10q�.����  �� TextAlignment$ Center=�	Extract�� 
Websites� $��1�� �� �� .+�	� -   $��ExternalBrowserButton��$��0��$��5q�$��Use External Browser��$��Center=� d� 	IsChecked$
 TrueY�	� -   $��InternalBrowserButton��$��1��$��5q�$��Use Internal Browser��$��Center=��� -   $��ShowAllButton��$��2��{�.  $��50��$��40��$��10q�.���� $ Center=�Show�� All�� $��1��$��1q�
���   �._��  �� �� $��3* �.+��� -   $��MusicPlayerLabel��$��0��$	��Top=�$��Center=�$��0q�$��0q�$��Music Player��� $��1�� �� �� �� �� $��2* �.+��� -   $��LoadMusicButton��$��0��$
��Load��$��40��$��30��$��5q��� -	   $��PlayMusicButton��$��1��$
��Play��$��40��$��30��$��5q�{�.  �� -
   $��StopMusicButton��$��2��$
��Stop��$��40��$��30��$��5q�{�.  �� -   $��VolumeSlider��$��3��$��Center=�$��5q� � Minimum$ 0Q� � Maximum$ 1Q�	 � Value	 �� 
 �� Path$
 VolumeLevel��� $��1��$��1q�
���   �._��� -   $��WebsitesViewer��$��5q� �� SelectionMode$ Single=��� $��1��$��1��$��1q�
���   �._�(  "System.Windows.Controls.WebBrowser  -   $��Browser��