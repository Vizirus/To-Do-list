   M S B A M L   `   `   `  ????E  ATo Do List, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:To_Do_List
To_Do_List  5   	       To_Do_List.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:To_Do_List     5   	   -   5          ? Title$  
To Do List??5   	   $	??520??6   $	??850??6)    ? WindowState$ 	Maximized=?65   .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??679* ?6   ?? 5      $
??121* ?6    ? RowDefinitions 5   
   ?? 5      $	??41* ?6   ?? 5      $	??85* ?6   ?? 5      $	??91* ?6   .+?5   
   ?? -   5         ??Name$ TaskList??5      $??3??6%   $??0,17,0,0q?66    ?? View 5      ?? 5      .P?5      ?? $??	Task Name??6%    ?? Width$ 80??68    ?? DisplayMemberBinding 6C   ?? *Task+?? 5      $??Task create date??6%    6?   ?? *	StartDate+?? 5      $??Task finish date??6%    6?   ?? *
FinishDate+?? 5      $??Task status??6%    6:   ?? *
TypeOfTask+5       5"      ?? 5#   
   $??1??6   $
??Auto??6#   $??Main Functions??61   .??5%      ?  5&      ?? 5'      ?? 5(      ?? 5)      .+?5+      ?? -   5        $	??Add??5+      $??0,0,20,5q?6'   $??0??6:   6G   ?? 5,      -   5        $??Delete??5,      $??0,0,20,5q?6*   $??1??6<   6I   ?? 5-      -   5        $??	Clean all??5-      $??0,0,20,5q?6-   $??2??6?   6L   5.      5/      ?? 50   
   $??Change function??6   $??1??6,   $??1??69   .??51      ?  52      ?? 53       55      ?? 56      ?? 57      ?? 58      ?? 59      ?? 5:      .+?5<      (  "System.Windows.Controls.DatePicker  -   5          Name$ 
DatePicker??5<      $??0??61   $??0,0,20,5q?6>   ?? 5=      -   5        	 ??Name$	 StatusComboBox??5=      $??1??63   $??0,0,20,5q?6@   
 ?? SelectedIndex$
 1??6R   ?? 5>      -   5         ??Name$ 
ChangeText??5>      $??2??6.   $??0,0,20,5q?6;   ?? 5?      -	   5         ??Name$ ChangeStatusButton??5?      $??Change Status??65   $??3??6M   $??0,0,20,5q?6Z   6m   5@      5B      o? 5C   
   $??0,0,0,74q?6   $	??Top=?6!   .@?5D      m? $
??File??6   .@?5E      m? -
   5        $
??Save??5E      6)   m? 5F      -   5        $
??Open??5F      6)   m? 5G      -   5        $??
Save as...??5G      6/   m? 5H      $??Change save directory??6   5J      5K      5L      5P      