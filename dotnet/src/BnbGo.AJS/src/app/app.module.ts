import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { Home } from '../pages/home/home';
import { SearchPage } from '../pages/search/search';
import { CountryPage } from '../pages/country/country';
import { RegionPage } from '../pages/region/region';
import { CityPage } from '../pages/city/city';
import { RoomPage } from '../pages/room/room';
import { LoginPage } from '../pages/login/login';
import { LogoutPage } from '../pages/logout/logout';
import { ProfilePage } from '../pages/profile/profile';
import { ReservationPage } from '../pages/reservation/reservation';
import { RegisterPage } from '../pages/register/register';
import { ReservationConfirmedPage } from '../pages/reservation-confirmed/reservation-confirmed';

@NgModule({
  declarations: [
    MyApp,
    Home,
    SearchPage,
    CountryPage,
    RegionPage,
    CityPage,
    RoomPage,
    LoginPage,
    LogoutPage,
    ProfilePage,
    ReservationPage,
    ReservationConfirmedPage,
    RegisterPage,
  ],
  imports: [
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    Home,
    SearchPage,
    CountryPage,
    RegionPage,
    CityPage,
    RoomPage,
    LoginPage,
    LogoutPage,
    ProfilePage,
    ReservationPage,
    ReservationConfirmedPage,
    RegisterPage,
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}]
})
export class AppModule {
}
