   M S B A M L   `   `   `  ����T  PStudentenAdministratieApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:StudentenAdministratieApp.Pages.Cursisten)StudentenAdministratieApp.Pages.Cursisten  5      T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      m;clr-namespace:StudentenAdministratieApp.ViewModel.Cursisten-StudentenAdministratieApp.ViewModel.Cursisten  5
      B    <StudentenAdministratieApp.Pages.Cursisten.ModuleInschrijving   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Ilocal7clr-namespace:StudentenAdministratieApp.Pages.Cursisten     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      JVW;clr-namespace:StudentenAdministratieApp.ViewModel.Cursisten     5
      
�������  � DataContext  5      R   LStudentenAdministratieApp.ViewModel.Cursisten.clsModuleInschrijvingViewModel  5   
   6.   .��5      ]� $��5q�6   $��Stretch=�6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Bottom=�6.   $��Right=�6F   $��5q�6b   .+�5      �� $��Lees Identiteitskaart��6   $��0,0,10,0q�65   �� 5      $��Gegevens Opslaan��6   $��0,0,10,0q�6u   
��������60   �� *SaveCommand+ � ToolTip 6P   �� *ValidationErrors+6�   �� 5      $��	Annuleren��6   
���  ��6)   5      � 5   
   $��Stretch=�6    � RowDefinitions 5      �� 5      $��50 �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��1* �6#   $	��150��6.   �� 5      $��1.651* �6#   $	��300��62   .+�5       �� $��2��6   $��0��6)   $��0��66   $��Stretch=�6G   $��5q�6e   $��25��6q   
�������._�5!      �� 
�������$
��auto��65   $��Center=�6B   $ 
Horizontal=�6_   $��5q�6x   .+�5"      �� $��ZOEKEN��6   $��0,0,20,0q�6-   $��25��6?   $��Center=�6N   �� 5#      -   5        $��	cmbZoeken��5#       �� 
IsEditable .  �� IsTextSearchEnabled .   �� IsTextSearchCaseSensitive .  $��25��5%   E   $	��120��6T   $��0,0,20,0q�5&      $��Center=�5'      ��5$      ��  �� Path$ 	Cursisten�	 �� SelectedItem	 5%      �� $ SelectedCursist�5'      �� 5(      $��	Selecteer��6   $��25��61   $��Center=�6@   6]   5)      5*      �� 5,      $��0��6   $��1��6%   $��5q�63   
�������._�5-      �� $��5q�6   
�������.+�5.      �� $��Modules��6   $��0,0,0,40q�6.   � 5/       50      �� 51      �� 52      �� 53      $��* �6,    55      �� 56      $��1* �6/   �� 57      $��2* �6/   .+�59      �� $
��Jaar��6    $��0��6/   $��0��6<   �� 5:      $ 
Horizontal=�6%   $��0��6>   $��1��6K   .+�5;      �� -   5        $��cbJaar1��5;   '   68   �� 5<      -   5        $��cbJaar2��5<   '   68   �� 5=      -   5        $��cbJaar3��5=   '   68   5>      �� 5?      $��Filter��6    $��1��61   $��0��6>   �� 5@      -   5        $��	txtFilter��5@   "   $��1��63   $��1��6@   ��6P   �� $ Filter�
 �� Mode$
 TwoWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�&   System.Windows.Controls.DataGrid  5A      $��0,10,0,0q�6#   $
��Auto=�65   $��2��5B      $��2��61      AutoGenerateColumns .  ��5A   e   �� $ GebruikerInschrijvingen�$
 TwoWay=� . $ PropertyChanged=� � 	Resources 5D       �� 
TargetType �� Property �� Value �� ElementName��%z   '��       �� 5E   "   " ��6(   .�5F   &   ��  � {� 6B   �� *SelectedItems.Count+$ 	cmbZoeken��5G   #   5H         Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J   "   ,  &System.Windows.Controls.DataGridColumn   Header$ Module��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Naam+4  .System.Windows.Controls.DataGridCheckBoxColumn  5K   "   $ Inschrijven��69      MetroDataGridCheckBox   ElementStyle# �   6�    6N   �� *	Checked+$
 TwoWay=�$ PropertyChanged=�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5L   "   $
 Klas��69      CellTemplate 5M   &   �� 5N   *   .R�5O   .   ��  .   �� DisplayMemberPath$
 Naam��5S   /    �� SelectedValue 5P   /   �� *SelectedKlas+$
 TwoWay=�$ PropertyChanged=���5Q   /   �� *	Klassen+$
 OneWay=�$ PropertyChanged=�5S   /   5T   +   5V   #   5X      5Z      5\      5]      �� 5^      $��1��6   $��1��6"   $��5q�62   
�������._�5_      � $��5q�6   
������� 5`      �� 5a      $��25 �6(   �� 5b      $��* �6(   .+�5d      � $��0��6    5e      �� 5f      $��1* �6/   �� 5g      $��1* �6/   .+�5i      �� .��6!   Persoonlijke Gegevens68   �� 5j      $ 
Horizontal=�6%   $��Right=�6>   $��1��6Z   .+�5k      �� .��6$   
Opgeslagen60   �� 5l       d� 	IsChecked 6'   �� $ 
Opgeslagen�$
 OneWay=�5m      5n      �� 5p      $��1��6#   .��5q      � $��5q�6    5r      (  "System.Windows.Controls.DatePicker��%�  '��       '�� �     '  .    '�� �    '�� 5    �� 5t   "   " ��6(   .�5u   &   ��  � ��$ Center=�6J   �� 5v   &    � ��$
 Left=�6L   �� 5w   &    � ��$	 200��6>   5x   #   �� 5y   "   " ��6(   .�5z   &   ��  � ��$ Center=�6J   �� 5{   &    � ��$
 Left=�6L   �� 5|   &    � ��$	 200��6>   5}   #   �� 5~   "   "  6(   .�5   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$
 Left=�6L   �� 5�   &    � ��$	 200��6>   5�   #   �� 5�   "   " ��6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ 27��6?   5�   #   �� 5�   "   " ��6(   .�5�   &   ��  � ��$ 27 �6?   5�   #   5�       5�      �� 5�   "   $	��200 �63   �� 5�   "   $��* �63    5�      �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   �� 5�   "   .+�5�      �� $
��Naam��6$   $��0��63   $��0��6C   �� 5�      $��Voornaam��6$   $��0��67   $��1��6G   �� 5�      $��Rijksregisternr��6$   $��0��6>   $��2��6N   �� 5�      $��Email��6$   $��0��64   $��3��6D   �� 5�      $	��GSM��6$   $��0��62   $��4��6B   �� 5�      $��Hoogst behaalde diploma��6$   $��0��6F   $��5��6V   �� 5�      $��Postcode��6$   $��0��67   $��6��6G   �� 5�      $��Gemeente��6$   $��0��67   $��7��6G   �� 5�      $��Notificatie Voorkeur��6$   $��0��6C   $��8��6S   �� 5�      $��
Afbeelding��6$   $��0��69   $��9��6I   $��2��6V   �� 5�      $
��Taal��6$   $��0��63   $��11��6C   �� 5�      $��Toelatingsproef��6$   $��0��6>   $��12��6N   �� 5�      $��Gebruikersnaam��6$   $��0��6=   $��13��6M   �� 5�      $��Straat��6$   $��0��65   $��14��6E   �� 5�      $��Geslacht��6$   $��0��67   $��15��6G   �� 5�      $��Nationaliteit��6$   $��0��6<   $��16��6L   �� 5�      $��Email Persoonlijk��6$   $��0��6@   $��17��6P   �� 5�      $��Telefoonnummer��6$   $��0��6=   $��18��6M   �� 5�      $��GeboorteDatum��6$   $��0��6<   $��19��6L   �� 5�      -   5        $
��Naam��5�   &     DefaultErrorTemplate =� ErrorTemplate# [ 6�   $��1��6
  $��0��6  ��62   �� $ SelectedCursist.Naam�$
 TwoWay=�$ PropertyChanged=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 6  �� 5�      -   5        $��Voornaam��5�   &   $��1��6�   $��1��6�   ��66   �� $ SelectedCursist.Voornaam�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��Rijksregisternr��5�   &   $��1��6�   $��2��6�   ��6=   �� $) #SelectedCursist.RijksregisterNummer�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -	   5        $��Email��5�   &   $��1��6�   $��3��6�   ��63   �� $ SelectedCursist.EMail�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -
   5        $	��GSM��5�   &   $��1��6�   $��4��6�   ��61   �� $ SelectedCursist.GSMNummer�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��	cmbNiveau��5�   '   $��1��69   $��5��6I   $��25��5�   N   $	��120��6]   $��Center=�5�   !   ��5�   !   �� $ Niveaus�	 5�   !   �� $ SelectedNiveau�5�   !   �� 5�      -   5        $��cmbPostCodes��5�   '   $��1��6<   $��6��6L   $��25��5�   P   $	��120��6_   $��Center=�5�   !   ��5�   !   �� $ 	PostCodes�	 5�   !   �� $ SelectedPostcode�5�   !   �� 5�      -   5        $��
Huisnummer��5�   &   $��1��6�   $��7��6�   ��68   �� $  SelectedCursist.HuisNummer�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��NotificatieVoorkeur��5�   &   $��1��6A   $��8��6Q   �� 5�      $��9��6)   $��1��66   $��2��6F   $ 
Horizontal=�6W   .+�5�   "   �� $��50��6V   $��50��6b   $
��Left=�6m   ��6(   �� $ SelectedCursist.Photo�6m   5�      �� 5�      -   5        $��cmbTaal��5�   '   $��1��67   $��11��6G   $��25��5�   L   $	��120��6[   $��Center=�5�   !   ��5�   !   �� $ Talen�	 5�   !   �� $ SelectedTaal�5�   !   �� 5�      $��1��6w   $��12��6�    6'   �� $- 'SelectedCursist.ToelatingsProefGeslaagd�$
 TwoWay=�6�   �� 5�      -   5        $��Gebruikersnaam��5�   &   $��1��6�   $��13��6�   ��6<   �� $$ SelectedCursist.Gebruikersnaam�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��Straat��5�   &   $��1��6�   $��14��6�   ��64   �� $ SelectedCursist.Straat�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��Geslacht��5�   &   $��1��6�   $��15��6�   ��66   �� $ SelectedCursist.Geslacht�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��Nationaliteit��5�   &   $��1��6�   $��16��6�   ��6;   �� $# SelectedCursist.Nationaliteit�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��EmailPersoonlijk��5�   &   $��1��6�   $��17��6�   ��6>   �� $&  SelectedCursist.EMailPersoonlijk�$
 TwoWay=�$ PropertyChanged=�6�   �� 5�      -   5        $��Telefoonnummer��5�   &   $��1��6�   $��18��6�   ��6<   �� $$ SelectedCursist.TelefoonNummer�$
 TwoWay=�$ PropertyChanged=�6�     5�      $��1��6)   $��19��69      SelectedDate 6G   �� $# SelectedCursist.GeboorteDatum�$
 TwoWay=�$ PropertyChanged=�5�      5�      5�      5�      5�      5�      5�      