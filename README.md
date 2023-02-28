# BerestovFirstLab
Для запуска есть 2 пути:
1. Самый простой нажимаем по "Code" и выбираем "Open with Visual Studio".
2. Качаем архив, для этого снова выбираем "Code"и нажимаем "Download ZIP".
Дополнительно можно переместить стили в отдельную папку на примере "ResourceDictionary":
1. Создаем внутри проекта папку, называем ResourceDictionary.
2. Переносим туда наши стили.
3. В файле app.xaml находим строчки:
     <ResourceDictionary Source="light.xaml"/>
     <ResourceDictionary Source="Dark.xaml"/>
Меняем  <ResourceDictionary Source="light.xaml"/> на  <ResourceDictionary Source="ResourceDictionary/light.xaml"/>
Меняем  <ResourceDictionary Source="light.xaml"/> на  <ResourceDictionary Source="ResourceDictionary/Dark.xaml"/>
Работа сделанна на C#, используя XAML.
