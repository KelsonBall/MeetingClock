   M S B A M L   `   `   `  ����G  CMeetingClock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MeetingClock.ViewsMeetingClock.Views  5      "    MeetingClock.Views.ShellView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:MeetingClock.Views     5        � Title$  Meeting Clock��5       � Icon$ ../application_icon.ico��5      $	��500��5      $	��400��5      .��5      � 
����d�$
��Auto��6'   $��Stretch=�64    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   .@�5      m� $
��Save��6    m� Command 6)   �� *SaveCommand+m� 5      $
��Load��6    6)   �� *LoadCommand+5      5      $   MeetingClock.Views.MeetingView  5   
   $��1��6    � DataContext 6)   �� *	MeetingVm+5      5      