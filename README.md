<p> <a href="http://bit.ly/2CePJJK">
  <img src="https://i.imgur.com/OQldEWy.png" alt="LOGO" width="144"/>
  </a>
  < Click to see it on the Play Store
</p>

# Bunk-Master
This was a passion project of mine which was worked on in 2018 while I was in junior year of my CS degree.   
Due to the nature of this project being a truly full-stack one, a lot of hard work and dedication was put into it. This is my first large scale coding project. I learned a lot during its development.  
#### The Why?
In India there exists a student culture of bunking - missing out on classes in order to take part in extra curricular activities thus educational institutions have come up with minimum attendence to make sure students are not missing out too much. Due to the number of classes taken not being a known total it does get a little tedious to keep track of it. I went further, it doesnt just track but also gives predictions in an intuitive manner.

- App was built using the Xamarin Platform.
- Codebase follows the MVVM architecture.
- All data generated is managed using SQLite.
- Automatically logs the data once class schedule is fed in.
- UX is desgined with ease in mind and minimum number of taps required to get the job done.
- UI consists of smooth transitions, custom color coded sections and simple interfaces.
- Designed and created logo and UI elements using Adobe Illustrator.
- Published on the Android Play Store.

### Dev Env
Visual Studio 2017 - Install Xamarin.

### File Sturcture - Only files that I have coded are listed

    .
    ├── Bunk_Master.Android                 # Files Realted to Android
    |   ├── CustomServiceBinder.cs
    |   ├── JobSchedulerHelper.cs
    |   ├── PeriodicService.cs
    |   └── ServiceConnection.cs
    ├── Bunk_Master.IOS                     # Files Realted to IOS
    └── Bunk_Master                         # Common files for IOS/Android
        ├── Helpers
        |   └── ColorValueConverter.cs
        ├── IConverters
        |   ├── IClassNeededConverter.cs
        |   ├── IDateTextConverter.cs
        |   ├── IGetPercentConverter.cs
        |   ├── IGetPresentConverter.cs
        |   ├── IStrToSFColorConverter.cs
        |   └── IndexToColorConverter.cs
        ├── Interfaces
        |   └── ISQLiteService.cs
        ├── Models
        |   ├── BaseItem.cs
        |   ├── ClassModel.cs
        |   ├── DatesModel.cs
        |   ├── DayAbsentModel.cs
        |   ├── OverModel.cs
        |   ├── PercentModel.cs
        |   ├── SettingModel.cs
        |   ├── SumModel.cs
        |   └── TelemetModel.cs
        ├── ViewModels                      # Bussness logic - Backend
        |   ├── BaseVM.cs
        |   ├── ClassVM.cs
        |   ├── Database.cs
        |   ├── DatesVM.cs
        |   ├── GraphGen.cs
        |   └── ViewModel.cs
        ├── Views                           # Front facing code
        |   ├── AddFormview.cs
        |   ├── BindableSKCanvasView.cs
        |   ├── ColorView.cs
        |   ├── GraphInfo.cs
        |   ├── HomeTabPage.cs
        |   ├── OverallPage.cs
        |   ├── PopupViewD.xaml
        |   ├── PopupViewD.xaml.cs
        |   ├── ScheSwitch.cs
        ├── AnalysisView.cs
        ├── App.xaml
        ├── App.xaml.cs
        ├── DiagnosticsVM.cs
        ├── GraphInfo.cs
        └── Scheduler.cs


###### For Educational Purposes ONLY - Copyright 2018 
